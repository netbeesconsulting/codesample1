using Stocky.Common;
using Stocky.Model.Security;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace Stocky.Domain.DomainModels.Shared
{
    public class BaseAuditable : BaseEntity
    {
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long CreatorUserId { get; set; }
        public long? ModifiedUserId { get; set; }
        public bool IsDeleted { get; set; } = false;
        [NotMapped]
        protected UserModel _user;

        public BaseAuditable AuditableCreate()
        {
            IsValidUser();
            CreatedOn = DateTime.Now;
            CreatorUserId = _user.UserId;
            return this;
        }

        public BaseAuditable AuditableUpdate()
        {
            IsValidUser();
            ModifiedOn = DateTime.Now;
            ModifiedUserId = _user.UserId;
            return this;
        }

        private void IsValidUser()
        {
            if (_user.IsNull())
            {
                throw new InvalidDataException("User cannot be null");
            }
        }

    }
}
