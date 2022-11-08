using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interface
{
    public interface IUserBL
    {
        public bool Registration(RegisterModel userRegistrationModel);
        public string UserLogin(LoginModel loginModel);
        public string ForgetPassword(string Email);
        public bool ResetPassword(ResetModel resetModel, string EmailId);
    }
}
