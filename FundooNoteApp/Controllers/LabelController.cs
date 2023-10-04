using BusinessLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using CommonLayer.Model;
using RepoLayer.Entity;

namespace FundooNoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBl ilabelBl;
        public LabelController(ILabelBl ilabelBl)
        {
            this.ilabelBl = ilabelBl;
        }
        [HttpPut("AddLabel")]
        public IActionResult AddLabel(long noteId, string label)
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
            var result=ilabelBl.AddLabel(userId,userId, label);
            if(result!=null)
            {
                return Ok(new ResponseModel<LabelEntity> { Status = true, Message = "Label Added Successfully", Data = result });

            }
            else
            {
                return BadRequest(new ResponseModel<LabelEntity> { Status = false, Message = "Label Adding UnSuccessfully", Data = result });
            }

        }

    }
}