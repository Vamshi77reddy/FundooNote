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
        private readonly UserInterfaceRl userinterfaceRl;
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
            catch (Exception ex) { throw ex; }
        }
        public string Login(LoginModel loginModel)
        {
            try
            {
                return userinterfaceRl.Login(loginModel);
            }
            catch (Exception ex) { throw ex; }

        }
        //public ForgetPasswordModel UserForgetPassword(string email)
        //{
        //    try
        //    {
        //        return userinterfaceRl.UserForgetPassword(email);
        //    }
        //    catch (Exception ex)
        //    { throw ex; }
        //}

        public ForgetPasswordModel UserForgetPassword(ForgetPasswordModel forgetPasswordModel)
        {
            try
            {
             return userinterfaceRl.UserForgetPassword(forgetPasswordModel);
            }catch (Exception ex) 
            { throw ex; } 
        }

        public ResetPasswordModel ResetPassword(string email, ResetPasswordModel resetPasswordModel)
        {
            try {
            return userinterfaceRl.ResetPassword(email, resetPasswordModel);
            }catch (Exception ex) { throw ex; }
        }
        public UserEntity SessionLogin(string email, string password)
        {
            try
            {
                return userinterfaceRl.SessionLogin(email, password);
            }catch(Exception ex) { throw ex; }
        }


    }
}
