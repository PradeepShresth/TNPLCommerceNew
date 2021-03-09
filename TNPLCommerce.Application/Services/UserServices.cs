using System;
using System.Collections.Generic;
using System.Text;
using TNPLCommerce.Application.Interfaces;
using TNPLCommerce.Domain.UserModels;
using TNPLCommerce.Infrastructure.Data.SqlHandlers;

namespace TNPLCommerce.Application.Services
{
    public class UserServices : IUserServices
    {
        public void RegisterUser(User objInfo, string userRole)
        {
            List<SQLParam> Param = new List<SQLParam>();
            Param.Add(new SQLParam("@Username", objInfo.Username));
            Param.Add(new SQLParam("@Email", objInfo.Email));
            Param.Add(new SQLParam("@Password", objInfo.Password));
            Param.Add(new SQLParam("@UserRole", userRole));
            string strSpName = "usp_Register";
            SQLHandler sqlHAsy = new SQLHandler();
            sqlHAsy.ExecuteNonQuery(strSpName, Param);
        }

        public User LoginUser(User objInfo)
        {
            List<SQLParam> Param = new List<SQLParam>();
            Param.Add(new SQLParam("@Email", objInfo.Email));
            Param.Add(new SQLParam("@Password", objInfo.Password));
            string strSpName = "usp_GetUser";
            SQLHandler sqlHAsy = new SQLHandler();
            return sqlHAsy.ExecuteAsObject<User>(strSpName, Param);
        }

        public User GetUserByEmail(User objInfo)
        {
            List<SQLParam> Param = new List<SQLParam>();
            Param.Add(new SQLParam("@Email", objInfo.Email));
            string strSpName = "usp_GetUserByEmail";
            SQLHandler sqlHAsy = new SQLHandler();
            return sqlHAsy.ExecuteAsObject<User>(strSpName, Param);
        }

        public User GetUserById(int? id)
        {
            List<SQLParam> Param = new List<SQLParam>();
            Param.Add(new SQLParam("@UserId", id));
            string strSpName = "usp_GetUserById";
            SQLHandler sqlHAsy = new SQLHandler();
            return sqlHAsy.ExecuteAsObject<User>(strSpName, Param);
        }
    }
}
