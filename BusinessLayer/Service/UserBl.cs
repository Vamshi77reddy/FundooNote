using BusinessLayer.Interface;
using CommonLayer.Model;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{

    public class UserBl : IuserBl
    {
      private readonly  UserInterfaceRl userinterfaceRl;
        public UserBl(UserInterfaceRl userinterfaceRl)
        {
            this.userinterfaceRl = userinterfaceRl;

        }

        public UserEntity UserRegistrations(UserRegistration userRegistration)
        {
            try
            {
                return this.userinterfaceRl.UserRegistrations(userRegistration);
            }
            catch(Exception ex) { throw ex; }
        }
    }
}
