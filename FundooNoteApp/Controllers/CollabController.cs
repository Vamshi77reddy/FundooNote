using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoLayer.Entity;
using RepoLayer.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FundooNoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        private readonly ICollabBl collabBl;
        public CollabController(ICollabBl collabBl)
        {
            this.collabBl = collabBl;
        }
        [HttpPut("AddCollaboration")]
        public IActionResult AddCollaboration(long noteId,string collabEmail)
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
            var result=collabBl.AddCollab(userId,noteId,collabEmail);
            if(result!=null)
            {
                return Ok(new ResponseModel<CollabEntity> { Status = true, Message = "Email Adding Successfully", Data = result });

            }
            else
            {
                return BadRequest(new ResponseModel<CollabEntity> { Status = false, Message = "Email Adding UnSuccessfully", Data = result });

            }

        }
        [HttpGet("GetAllCollaborations")]
        public IActionResult GetAllCollaborations()
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e=>e.Type =="UserId").Value);
            var result=collabBl.GetAllCollaborations(userId);
            if(result!=null)
            {
                return Ok(new ResponseModel<List<CollabEntity>>{ Status = true, Message = "Showing all Emails", Data = result });

            }
            else
            {
                return BadRequest(new ResponseModel<List<CollabEntity>> { Status = false, Message = "Email Adding Successfully", Data = result });

            }
        }
    }
}
