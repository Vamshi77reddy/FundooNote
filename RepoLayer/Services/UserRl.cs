using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Services
{
    public class UserRl: UserInterfaceRl
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration configuration;
        public UserRl(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
        }
        public UserEntity UserRegistrations(UserRegistration userRegistration) {
            try
            {
                UserEntity Entityuser = new UserEntity();
                Entityuser.FirstName = userRegistration.FirstName;
                Entityuser.LastName = userRegistration.LastName;
                Entityuser.EmailId = userRegistration.EmailId;
                Entityuser.Password = EncodePassword(userRegistration.Password);
                fundooContext.Add(Entityuser);
                int result = fundooContext.SaveChanges();
                if (result>0)
                {
                    return Entityuser;
                }
                else
                {
                    return null;
                }

            }
            catch(Exception) { throw; }
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
    }
}
