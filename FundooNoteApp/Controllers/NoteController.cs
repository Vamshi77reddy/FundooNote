using BusinessLayer.Interface;
using BusinessLayer.Service;
using CommonLayer.Model;
using MassTransit.Registration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FundooNoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : Controller
    {
        private readonly INoteBl inoteBl;
        public NoteController(INoteBl inoteBl)
        {
            this.inoteBl = inoteBl;
        }
        [HttpPost("TakeANote")]
        public IActionResult TakeANote(NotesModel notesModel)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = inoteBl.TakeANote(notesModel, userId);
                if (result != null)
                {
                    return Ok(new ResponseModel<NoteEntity> { Status = true, Message = "Takenote Successful", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<NoteEntity> { Status = false, Message = "Takenote UNSuccessful" });
                }
            }
            catch (Exception ex) { throw ex; }
        }
        [HttpGet]
        [Route("Get-All-Notes")]
        public IActionResult GetAllNote()
        {
            try
            {
                int userId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
                var result = inoteBl.GetAllNotes(userId);
                if (result != null)
                {
                    return Ok(new ResponseModel<List<NoteEntity>> { Status = true, Message = "Displaying the Notes.", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<List<NoteEntity>> { Status = false, Message = "No Notes available to Display", Data = result });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
