namespace RepoLayer.Services
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using CommonLayer.Model;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using RepoLayer.Context;
    using RepoLayer.Entity;
    using RepoLayer.Interface;

    public class UserRl : UserInterfaceRl
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRl"/> class.
        /// </summary>
        /// <param name="fundooContext"></param>
        /// <param name="configuration"></param>
        public UserRl(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
        }

        /// <inheritdoc/>
        public UserEntity UserRegistrations(UserRegistration userRegistration)
        {
            try
            {
                UserEntity Entityuser = new UserEntity();
                Entityuser.FirstName = userRegistration.FirstName;
                Entityuser.LastName = userRegistration.LastName;
                Entityuser.EmailId = userRegistration.EmailId;
                Entityuser.Password = EncodePassword(userRegistration.Password);
                this.fundooContext.Add(Entityuser);
                int result = this.fundooContext.SaveChanges();
                if (result > 0)
                {
                    return Entityuser;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string EncodePassword(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        public static string Decrypt(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return null;
            }
            else
            {
                byte[] encryptedPass = Convert.FromBase64String(password);
                string decryptedPass = ASCIIEncoding.ASCII.GetString(encryptedPass);
                return decryptedPass;
            }
        }

        /// <inheritdoc/>
        public string Login(LoginModel loginModel)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity = this.fundooContext.UserTable.FirstOrDefault(x => x.EmailId == loginModel.EmailId);
                string pass = Decrypt(userEntity.Password);
                if (pass == loginModel.Password && userEntity != null)
                {
                    var token = this.GenerateJwtToken(userEntity.EmailId, userEntity.UserId);
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string GenerateJwtToken(string EmailId, long UserId)
        {
            try
            {
                var LoginTokenHandler = new JwtSecurityTokenHandler();
                var LoginTokenKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.configuration[("Jwt:Key")]));
                var LoginTokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("Email", EmailId.ToString()),
                        new Claim("UserId", UserId.ToString()),
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(LoginTokenKey, SecurityAlgorithms.HmacSha256Signature),
                };
                var token = LoginTokenHandler.CreateToken(LoginTokenDescriptor);
                return LoginTokenHandler.WriteToken(token);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ForgetPasswordModel UserForgetPassword(ForgetPasswordModel forgetPasswordModel)
            {
            try
            {
                var result = this.fundooContext.UserTable.FirstOrDefault(x => x.EmailId == forgetPasswordModel.EmailId);

                if (result != null)
                {
                    forgetPasswordModel.Token = GenerateJwtToken(result.EmailId, result.UserId);
                    forgetPasswordModel.UserId = result.UserId;
                    return forgetPasswordModel;
                }

                return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <inheritdoc/>
        public ResetPasswordModel ResetPassword(string email, ResetPasswordModel resetPasswordModel)
        {
            try
            {
                var result = this.fundooContext.UserTable.Where(x => x.EmailId == email).FirstOrDefault();
                result.Password = EncodePassword(resetPasswordModel.ConfirmPassword);
                this.fundooContext.SaveChanges();
                return resetPasswordModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <inheritdoc/>
        public UserEntity SessionLogin(string email,string password)
        {
            try
            {
                UserEntity user = this.fundooContext.UserTable.FirstOrDefault(x => x.EmailId == email);
                var pass = Decrypt(user.Password);
                if (pass == password && user != null)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
