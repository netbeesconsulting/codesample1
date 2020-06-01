using static Stocky.Common.Enums;

namespace Stocky.Model.Security
{
    public class UserModel
    {
        public long UserId { get; set; }
        public UserType UserType { get; set; }
    }
}
