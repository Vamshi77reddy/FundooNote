using BusinessLayer.Interface;
using CommonLayer;
using CommonLayer.Model;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RepoLayer.Entity;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FundooNoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IuserBl userBl;
        private readonly IBus bus;
        private readonly ILogger<UserController> logger;

        public UserController(IuserBl userBl,IBus bus, ILogger<UserController> logger)
        {
            this.userBl = userBl;
            this.bus = bus;
            this.logger = logger;
        }

        [HttpPost("Register")]
        public IActionResult Register(UserRegistration registration)
        {

            try
            {
                var result = userBl.UserRegistrations(registration);
                if (result != null)
                {
                    logger.LogInformation("UserRegistration Successful");
                    return Ok(new ResponseModel<UserEntity> { Status = true, Message = "UserRegistration Successful", Data = result });
                }
                else
                {
                    logger.LogError("UserRegistration Failed");

                    return BadRequest(new ResponseModel<UserEntity> { Status = false, Message = "UserRegistration Failed" });
                }

            }
            catch(Exception ex) {
                logger.LogCritical("Exception"); 
                throw ex; }
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginModel loginModel)
        {

            try
            {
                var result = userBl.Login(loginModel);
                if (result != null)
                {
                    logger.LogInformation("Login Successful");

                    return Ok(new ResponseModel<string> { Status = true, Message = "Login Successful", Data = result });
                }
                else
                {
                    logger.LogError("Login Unsuccessful");
       
                    return BadRequest(new ResponseModel<string> { Status = false, Message = "Login Failed" });                                                                            
                }

            }
            catch (Exception ex) { throw ex; }
        }
        //public async Task<IActionResult> UserForgetPassword(string email)
        //{
        //    try
        //    {
        //        if (email != null)
        //        {
        //            Send send = new Send();
        //            ForgetPasswordModel forgetPasswordModel = userBl.UserForgetPassword(email);
        //            send.SendingMail(forgetPasswordModel.EmailId, forgetPasswordModel.Token);
        //            Uri uri = new Uri("rabbitmq://localhost//FundoNotesEmail_Queue");
        //            var endPoint = await bus.GetSendEndpoint(uri);
        //            await endPoint.Send(forgetPasswordModel);
        //            return Ok(new ResponseModel<string> { Status = true, Message = "send Email successful", Data = forgetPasswordModel.Token });

        //        }
        //        else
        //        {
        //            return BadRequest(new ResponseModel<string> { Status = false, Message = "send Email unsuccessful" });
        //        }
        //    }
        //    catch (Exception ex)
        //    { throw ex; }
        //}

        [HttpPost("UserForgetPassword")]
        public async Task<IActionResult> UserForgetPassword(ForgetPasswordModel forgetPasswordModel)
        {
            try { 
            
                var result = userBl.UserForgetPassword(forgetPasswordModel);

                if (result != null)
                {
                     Send send = new Send();
                               send.SendingMail(forgetPasswordModel.EmailId, forgetPasswordModel.Token);//mail sent to user
                               Uri uri = new Uri("rabbitmq://localhost//FundoNotesEmail_Queue");
                               var endPoint = await bus.GetSendEndpoint(uri);
                               await endPoint.Send(forgetPasswordModel);
                    return Ok(new ResponseModel<ForgetPasswordModel> { Status = true, Message = "Forget Successful", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<ForgetPasswordModel> { Status = false, Message = "Forget  failed", Data = result });

                }
            }
            catch (Exception ex) 
            { throw ex; }
        }
         [Authorize]
        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            try
             {
               // string email = User.FindFirst("EmailId").Value;


                var email = User.FindFirst("Email").Value;
               //var email= HttpContext.Session.GetString("Email");

                var result = userBl.ResetPassword(email, resetPasswordModel);
                if (result != null)
                {
                    return Ok(new ResponseModel<ResetPasswordModel> { Status = true, Message = "Password Reset Successful", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<ResetPasswordModel> { Status = false, Message = "Password Reset Failed", Data = result });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("SessionLogin")]

        public IActionResult SessionLogin(string email, string password)
        {
            try
            {
                var result = userBl.SessionLogin(email, password);
                if(result != null)
                {
                    HttpContext.Session.SetString("email", result.EmailId);
                    HttpContext.Session.SetString("password", result.Password);
                
                    return Ok(new ResponseModel<UserEntity> { Status = true, Message = "Login Successful", Data = result });

                } 
                else
                {
                    return BadRequest(new ResponseModel<UserEntity> { Status = true, Message = "Login Successful", Data = result });

                }
            }catch (Exception ex) { throw ex; }
        }

    }
}






