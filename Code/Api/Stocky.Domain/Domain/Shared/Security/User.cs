using Stocky.Common;
using Stocky.Common.Utility;
using Stocky.Model.Security;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stocky.Domain.DomainModels.Shared.Security
{
    [Table("User", Schema = "Identity")]
    public class User : BaseAuditable
    {
        [Required, EmailAddress]
        public string Email { get; protected set; }
        [Required, StringLength(200)]
        public string Password { get; protected set; }
        public Enums.UserType UserType { get; protected set; }
        public Enums.RecordState RecordStatus { get; protected set; }

        public User()
        {

        }

        public User(UserModel userModel)
        {
            _user = userModel;
        }

        public User Create(string username, string password, Enums.UserType userType)
        {
            AuditableCreate();
            Email = username;
            Password = Encyption.Encrypt(password);
            UserType = userType;
            RecordStatus = Enums.RecordState.Active;
            return this;
        }

        public bool Validate(string password)
        {
            return Encyption.Encrypt(password) != Password ? false : true;

        }
    }
}
