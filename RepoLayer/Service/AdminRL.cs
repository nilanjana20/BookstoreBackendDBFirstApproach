using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepoLayer.Service
{
    public class AdminRL : IAdminRL
    {
        public AdminRL(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        SqlConnection sqlConnection;

        public bool AdminLogin(LoginModel loginModel)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("DBConnection"));
            using (sqlConnection)
            {
                try
                {
                    SqlCommand command = new SqlCommand("AdminLogin", sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;

                    sqlConnection.Open();

                    command.Parameters.AddWithValue("@EmailId", loginModel.EmailId);
                    command.Parameters.AddWithValue("@Password", loginModel.Password);

                    var result = command.ExecuteScalar();
                    if ( result != null)
                    {
                        string query = "SELECT AdminId FROM AdminTable WHERE AdminEmailID = '" + result + "'";
                        SqlCommand cmd = new SqlCommand(query, sqlConnection);
                        var Id = cmd.ExecuteScalar();
                        return true;
                    }
                    else
                    {
                            return false;
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }

        public string GenerateSecurityToken(string email, long userID)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.Configuration[("JWT:key")]));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
               {
                    new Claim(ClaimTypes.Role, "AdminTable"),
                    new Claim(ClaimTypes.Email, email),
                    new Claim("ID", userID.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

    }
}
