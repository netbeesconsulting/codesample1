using Stocky.Common;
using System.ComponentModel;

namespace Stocky.Model.Security
{
    public class AuthResponse
    {
        public long UserId { get; protected set; }
        [Description("username or email address")]
        public string Email { get; protected set; }
        public Enums.RecordState RecordState { get; protected set; }
        public Enums.UserType UserType { get; protected set; }

        public AuthResponse Create(long userId, string email, Enums.RecordState recordState, Enums.UserType userType)
        {
            UserId = userId;
            Email = email;
            RecordState = recordState;
            UserType = userType;
            return this;
        }
    }
}
