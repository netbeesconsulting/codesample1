using Stocky.Domain.DomainModels.Shared;
using Stocky.Domain.DomainModels.Shared.Security;
using Stocky.Model.Security;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stocky.Core.Domain.Admin
{
    [Table("Customer", Schema = "Application")]
    public class Customer : BaseAuditable
    {
        [Required]
        public string Name { get; protected set; }
        [Required]
        public string Company { get; protected set; }
        public string Address { get; protected set; }
        [Required]
        public string Contact { get; protected set; }
        public long UserId { get; protected set; }
        #region FK
        [ForeignKey("UserId")]
        public User User { get; set; }
        #endregion
        public Customer()
        {

        }

        public Customer(UserModel userModel)
        {
            _user = userModel;
        }

        public Customer Create(string name, string company, string address, string contact, long userId)
        {
            this.AuditableCreate();
            Name = name;
            Company = company;
            Address = address;
            Contact = contact;
            UserId = userId;
            return this;
        }

        public Customer Update(UserModel doingBy,string name, string company, string address, string contact)
        {
            _user = doingBy;
            this.AuditableUpdate();
            Name = name;
            Company = company;
            Address = address;
            Contact = contact;
            return this;
        }

        public Customer Delete(UserModel user)
        {
            _user = user;
            AuditableUpdate();
            IsDeleted = true;
            return this;
        }

    }
}
