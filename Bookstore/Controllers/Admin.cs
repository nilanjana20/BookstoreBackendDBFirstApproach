using BussinessLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Bookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Admin : ControllerBase
    {
        private readonly IAdminBL iAdminBl;

        public Admin(IAdminBL iAdminBl)
        {
            this.iAdminBl = iAdminBl;
        }

        [HttpPost]
        [Route("AdminLogin")]
        public IActionResult Login(LoginModel loginModel)
        {
            try
            {
                var result = this.iAdminBl.AdminLogin(loginModel);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Admin Login Successfull" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Admin Login UnSuceessfull" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

    }
}
