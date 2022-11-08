using BussinessLayer.Interface;
using Model;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Service
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL iuserRL;
        public UserBL(IUserRL iuserRL)
        {
            this.iuserRL = iuserRL;
        }

        public bool Registration(RegisterModel userRegistrationModel)
        {
            try
            {
                return iuserRL.Registration(userRegistrationModel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string UserLogin(LoginModel loginModel)
        {
            try
            {
                return iuserRL.UserLogin(loginModel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string ForgetPassword(string Email)
        {
            try
            {
                return iuserRL.ForgetPassword(Email);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ResetPassword(ResetModel resetModel, string EmailId)
        {
            try
            {
                return iuserRL.ResetPassword(resetModel, EmailId);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
