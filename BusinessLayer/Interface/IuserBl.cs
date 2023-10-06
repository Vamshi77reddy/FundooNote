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
        public string Login(LoginModel loginModel);
        public ForgetPasswordModel UserForgetPassword(ForgetPasswordModel forgetPasswordModel);
        public ResetPasswordModel ResetPassword(string email, ResetPasswordModel resetPasswordModel);
        public UserEntity SessionLogin(string email, string password);


    }
}
