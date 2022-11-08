using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepoLayer.Service
{
    public class UserRL : IUserRL
    {
        public UserRL(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        SqlConnection sqlConnection;

        public bool Registration(RegisterModel model)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("DBConnection"));
            using (sqlConnection)
            {
                try
                {
                    var password = EncryptPassword(model.Password);
                    SqlCommand command = new SqlCommand("dbo.Add_User", sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;

                    sqlConnection.Open();

                    command.Parameters.AddWithValue("@FullName", model.FullName);
                    command.Parameters.AddWithValue("@EmailId", model.Email);
                    command.Parameters.AddWithValue("@MobileNumber", model.MobileNumber);
                    command.Parameters.AddWithValue("@Password", password);


                    var result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
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

        public string UserLogin(LoginModel loginModel)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("DBConnection"));
            using (sqlConnection)
            {
                try
                {
                    SqlCommand command = new SqlCommand("Login_User", sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;
                    var password = EncryptPassword(loginModel.Password);
                    sqlConnection.Open();

                    command.Parameters.AddWithValue("@EmailId", loginModel.EmailId);
                    command.Parameters.AddWithValue("@Password", password);

                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        string query = "SELECT ID FROM Users WHERE EmailId = '" + result + "'";
                        SqlCommand cmd = new SqlCommand(query, sqlConnection);
                        var Id = cmd.ExecuteScalar();
                        var token = GenerateSecurityToken(loginModel.EmailId, Id.ToString());
                        return token;
                    }
                    else
                    {
                        return null;
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

        public string GenerateSecurityToken(string email, string userID)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.Configuration[("JWT:key")]));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Role,"Users"),
                    new Claim(ClaimTypes.Email, email),
                    new Claim("ID", userID.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
        public static string EncryptPassword(string Password)
        {
            try
            {
                if (Password == null)
                {
                    return null;
                }
                else
                {
                    byte[] b = Encoding.ASCII.GetBytes(Password);
                    string encrypted = Convert.ToBase64String(b);
                    return encrypted;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string DecryptedPassword(string encryptedPassword)
        {
            byte[] b;
            string decrypted;
            try
            {
                if (encryptedPassword == null)
                {
                    return null;
                }
                else
                {
                    b = Convert.FromBase64String(encryptedPassword);
                    decrypted = Encoding.ASCII.GetString(b);
                    return decrypted;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string ForgetPassword(string Email)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("DBConnection"));
            using (sqlConnection)
            {
                try
                {
                    sqlConnection.Open();
                    string query = "SELECT EmailId FROM Users WHERE EmailId = '" + Email + "'";
                    SqlCommand cmd = new SqlCommand(query, sqlConnection);
                    var email = cmd.ExecuteScalar();
                    string query1 = "SELECT ID FROM Users WHERE EmailId = '" + Email + "'";
                    SqlCommand sqlCommand = new SqlCommand(query1, sqlConnection);
                    var id = sqlCommand.ExecuteScalar();
                    if (email != null)
                    {
                        var token = GenerateSecurityToken(email.ToString(), id.ToString());
                        MSMQ msmqModel = new MSMQ();
                        msmqModel.sendData2Queue(token);
                        return token;
                    }
                    else
                        return null;

                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }

        }

        public bool ResetPassword(ResetModel resetModel, string EmailId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("DBConnection"));
            using (sqlConnection)
            {
                try
                {
                    if (resetModel.ResetPassword.Equals(resetModel.ConfirmPassword))
                    {
                        SqlCommand command = new SqlCommand("dbo.ResetPassword", sqlConnection);
                        command.CommandType = CommandType.StoredProcedure;
                        var password = EncryptPassword(resetModel.ResetPassword);
                        sqlConnection.Open();
                        command.Parameters.AddWithValue("@EmailId", EmailId);
                        command.Parameters.AddWithValue("@Password", password);
                        int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                        return false;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }


    }
}
