using Stocky.Common;
using Stocky.Model.Security;
using System;

namespace Stocky.Model.Utility
{
    [Serializable]
    public class Request<T>
    {
        public T Item { get; set; }
        public int UserId { get; set; }
        public Enums.UserType UserType { get; set; }

        public Request()
        {
        }

        public Request(T item)
        {
            Item = item;
        }
        public Request(T item, int userid, Enums.UserType userType)
        {
            Item = item;
            UserId = userid;
            UserType = userType;
        }
        public Request(T item, Enums.UserType userType)
        {
            Item = item;
            UserType = userType;
        }

        public UserModel User
        {
            get
            {
                return new UserModel()
                {
                    UserId = UserId,
                    UserType = UserType,
                };

            }
        }
    }
}
