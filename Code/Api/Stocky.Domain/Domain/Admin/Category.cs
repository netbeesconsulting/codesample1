using Stocky.Common;
using Stocky.Domain.DomainModels.Shared;
using Stocky.Model.Security;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stocky.Domain.DomainModels.Admin
{
    [Table("Category", Schema = "Application")]
    public class Category : BaseAuditable
    {
        [Required]
        public string Name { get; protected set; }
        public string Description { get; protected set; }

        public Category()
        {

        }

        public Category(UserModel userModel)
        {
            _user = userModel;
        }

        public Category Create(string name, string desc)
        {
            this.Validate(name);
            this.AuditableCreate();
            Name = name;
            Description = desc;
            return this;
        }

        public Category Update(UserModel doingBy,string name, string desc)
        {
            _user = doingBy;
            this.Validate(name);
            this.AuditableUpdate();
            Name = name;
            Description = desc;
            return this;
        }

        public Category Delete(UserModel user)
        {
            _user = user;
            AuditableUpdate();
            IsDeleted = true;
            return this;
        }

        public void Validate(string name)
        {
            if (name.IsNull())
            {
                throw new NullObjectException("name is required");
            }
        }
    }
}
