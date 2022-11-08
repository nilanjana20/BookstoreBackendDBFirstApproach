using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    public interface IUserRL
    {
        public bool Registration(RegisterModel model);
        public string UserLogin(LoginModel loginModel);
        public string ForgetPassword(string Email);
        public bool ResetPassword(ResetModel resetModel, string EmailId);

    }
}
