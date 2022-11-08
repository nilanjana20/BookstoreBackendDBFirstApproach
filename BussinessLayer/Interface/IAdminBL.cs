using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interface
{
    public interface IAdminBL
    {
        public bool AdminLogin(LoginModel loginModel);

    }
}
