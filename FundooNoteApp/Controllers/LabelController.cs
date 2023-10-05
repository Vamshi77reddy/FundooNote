using BusinessLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using CommonLayer.Model;
using RepoLayer.Entity;
using System.Collections.Generic;

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
            var result=ilabelBl.AddLabel(userId,noteId, label);
            if(result!=null)
            {
                return Ok(new ResponseModel<LabelEntity> { Status = true, Message = "Label Added Successfully", Data = result });

            }
            else
            {
                return BadRequest(new ResponseModel<LabelEntity> { Status = false, Message = "Label Adding UnSuccessfully", Data = result });
            }

        }
        [HttpGet("GetLabelByNoteId")]
        public IActionResult GetLabelByNoteId(long noteId)
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
            var result = ilabelBl.GetLabelByNoteId(userId, noteId);
            if (result!=null)
            {
                return Ok(new ResponseModel<List<LabelEntity>> { Status = true, Message = "Displaying Label", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<List<LabelEntity>> { Status = false, Message = "No Label Found", Data = result });
            }

        }
        [HttpGet("GetLabelByLabelId")]
        public IActionResult GetLabelByLabelId(long LabelId,int noteId)
        {
            var result = ilabelBl.GetLabelByLabelId(LabelId, noteId);
            if (result != null)
            {
                return Ok(new ResponseModel<List<LabelEntity>> { Status = true, Message = "Displaying Label", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<List<LabelEntity>> { Status = false, Message = "No Label Found", Data = result });
            }

        }
        [HttpGet("GetLabelByUserId")]
        public IActionResult GetLabelByUserId()
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
            var result = ilabelBl.GetLabelByUserId(userId);
            if (result != null)
            {
                return Ok(new ResponseModel<List<LabelEntity>> { Status = true, Message = "Displaying Label", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<List<LabelEntity>> { Status = false, Message = "No Label Found", Data = result });
            }

        }
        [HttpDelete("DeleteLabel")]
        public IActionResult DeleteLabel(int labelId)
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
            var result=ilabelBl.DeleteLabel(userId, labelId);
            if (result)
            {
                return Ok(new ResponseModel<bool> { Status = true, Message = "Label Deleted UnSuccessfully", Data = result });

            }
            else
            {
                return BadRequest(new ResponseModel<bool> { Status = false, Message = "Label Not Deleting ", Data = result });

            }

        }
    }
}