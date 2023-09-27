using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RepoLayer.Entity;
using System;

namespace FundooNoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IuserBl userBl;

        public UserController(IuserBl userBl)
        {
            this.userBl = userBl;
        }

        [HttpPost("Register")]
        public IActionResult Register(UserRegistration registration)
        {

            try
            {
                var result = userBl.UserRegistrations(registration);
                if (result != null)
                {
                    return Ok(new ResponseModel<UserEntity> { Status = true, Message = "UserRegistration Successfull", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<UserEntity> { Status = false, Message = "UserRegistration Failed" });
                }

            }
            catch(Exception ex) { throw ex; }
        }

    }
}
