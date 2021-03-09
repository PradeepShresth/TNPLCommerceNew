using System;
using System.Collections.Generic;
using System.Text;
using TNPLCommerce.Domain.UserModels;

namespace TNPLCommerce.Application.Interfaces
{
    public interface IUserServices
    {
        public void RegisterUser(User objInfo, string userRole);

        public User LoginUser(User objInfo);

        public User GetUserByEmail(User objInfo);

        public User GetUserById(int? id);
    }
}
