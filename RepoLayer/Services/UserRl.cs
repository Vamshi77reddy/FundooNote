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
                Entityuser.Password = userRegistration.Password;
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
    }
}
