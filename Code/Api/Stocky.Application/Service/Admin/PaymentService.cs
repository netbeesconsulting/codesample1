using Microsoft.EntityFrameworkCore;
using Stocky.Application.Service.Admin.Interface;
using Stocky.Common;
using Stocky.Model;
using Stocky.Model.Utility;
using Stocky.Model.ViewResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stocky.Application.Service.Admin
{
    public class PaymentService : BaseService, IPaymentService
    {
        public PaymentService(IBaseServiceInjector injector) : base(injector)
        {
        }

        public async Task<PageList<PaymentViewResult>> GetAll(Request<SearchRequest> searchRequest)
        {
            var paymentListQuery = _injector._context.Payment
                                        .Include(p => p.Order)
                                        .Include(p => p.Customer)
                                        .Include(p => p.PartialPayment)
                                        .Where(p => !p.IsDeleted).AsQueryable();
            if (!searchRequest.Item.SearchTerm.IsNullOrEmpty())
            {
                searchRequest.Item.SearchTerm = searchRequest.Item.SearchTerm.Trim();
                paymentListQuery = paymentListQuery.Where(p => EF.Functions.Like(p.Customer.Name, "%" + searchRequest.Item.SearchTerm + "%"));
            }
            var totalRecordCount = await paymentListQuery.CountAsync();
            var paymentList = await paymentListQuery.OrderByDescending(p => p.Id).Skip(searchRequest.Item.Skip).Take(searchRequest.Item.Take)
                                    .Select(p => new PaymentViewResult()
                                    {
                                        Id = p.Id,
                                        OrderId = p.OrderId,
                                        OrderDate = p.Order.OrderDate,
                                        CustomerName = p.Customer.Name,
                                        TotalAmount = p.TotalAmount,
                                        PaymentStatus = p.PaymentStatus.GetDescription(),
                                        Discount = p.Discount,
                                        TotalPayable = p.TotalPayable,
                                        RemainingAmount = p.PartialPayment.Count == 0 ? p.TotalPayable : p.TotalPayable - p.PartialPayment.Sum(p => p.PaidAmount)
                                    }).ToListAsync();
            return new PageList<PaymentViewResult>(paymentList, searchRequest.Item.Skip, searchRequest.Item.Take, totalRecordCount);
        }

        public async Task<PaymentViewResult> GetPaymentById(Request<long> request)
        {
            var payment = await _injector._context.Payment.Include(p => p.Order)
                                                .Include(p => p.Customer)
                                                .Include(p => p.PartialPayment)
                                                .FirstOrDefaultAsync(p => p.Id == request.Item && !p.IsDeleted);

            var paymentViewResult = new PaymentViewResult();
            paymentViewResult.SetPayment(payment.Id, payment.OrderId, payment.TotalAmount, payment.PaymentStatus.GetDescription(),
                 payment.Discount, payment.TotalPayable, payment.Customer.Name, payment.Order.OrderDate);
            paymentViewResult.PartialPayments = new List<PartialPaymentViewResult>();
            payment.PartialPayment.ForEach(item =>
            {
                paymentViewResult.PartialPayments.Add(new PartialPaymentViewResult()
                {
                    Id = item.Id,
                    PaymentId = item.PaymentId,
                    PaidDateTime = item.PaidDateTime,
                    PaidAmount = item.PaidAmount,
                    PaymentMethod = item.PaymentMethod.GetDescription()
                });
            });
            return paymentViewResult;
        }

        public async Task AddPartialPayment(Request<PartialPaymentModel> request)
        {
            try
            {
                var payment = await _injector._context.Payment
                                        .Include(p => p.PartialPayment)
                                        .FirstOrDefaultAsync(p => p.Id == request.Item.PaymentId);

                payment.AddPartialPayment(request.User, request.Item);
                await _injector._context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
