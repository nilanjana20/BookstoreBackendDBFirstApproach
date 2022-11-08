using BussinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.Security.Claims;

namespace Bookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL iUserBl;

        public UserController(IUserBL iUserBl)
        {
            this.iUserBl = iUserBl;
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Registration([FromBody] RegisterModel userRegistration)
        {
            try
            {
                var result = this.iUserBl.Registration(userRegistration);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Registration Successfull" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Registration UnSuceessfull" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult UserLogin(LoginModel loginModel)
        {
            try
            {
                var result = iUserBl.UserLogin(loginModel);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Login Successful", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Login Failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("ForgetPassword/{EmailId}")]
        public IActionResult ForgetPassword(string EmailId)
        {
            try
            {
                var result = this.iUserBl.ForgetPassword(EmailId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Mail Sent Successful" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Mail UnSuceessfull" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpPost]
        [Route("ResetPassword")]
        public IActionResult ResetPassword(ResetModel resetModel)
        {
            try
            {
                var EmailId = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var result = this.iUserBl.ResetPassword(resetModel, EmailId);

                if (result != null)
                {
                    return Ok(new { Success = true, Message = " Password reset succcessful" });
                }
                else
                {
                    return BadRequest(new { Success = false, Message = "Password reset unsuccessful" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

    }
}
