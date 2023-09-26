using CommonLayer.Model;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IuserBl
    {
        public UserEntity UserRegistrations(UserRegistration userRegistration);
    }
}
