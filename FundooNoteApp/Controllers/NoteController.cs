using BusinessLayer.Interface;
using BusinessLayer.Service;
using CommonLayer.Model;
using GreenPipes.Caching;
using MassTransit.Registration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using RepoLayer.Context;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FundooNoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : Controller
    {
        private readonly INoteBl inoteBl;
        private readonly FundooContext fundooContext;
        private readonly IDistributedCache distributedCache;
        public NoteController(INoteBl inoteBl, FundooContext fundooContext, IDistributedCache distributedCache)
        {
            this.inoteBl = inoteBl;
            this.fundooContext = fundooContext;
            this.distributedCache = distributedCache;
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
                    return BadRequest(new ResponseModel<NoteEntity> { Status = false, Message = "Takenote UnSuccessful" });
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
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
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

        [HttpPost("UpdateNote")]
        public IActionResult UpdateNote(NotesModel notesModel, int noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = inoteBl.UpdateNote(notesModel, userId, noteId);
                if (result != null)
                {

                    return Ok(new ResponseModel<NoteEntity> { Status = true, Message = "Updates the Notes.", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<NoteEntity> { Status = false, Message = "Cannot Update", Data = result });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPut("IsPinorNot")]
        public IActionResult IsPinorNot(long noteId)
        {
            
            
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = inoteBl.IsPinorNot(userId, userId);
                if (result)
                {
                    return Ok(new ResponseModel<bool> { Status = true, Message = " Note pinned", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<bool> { Status = false, Message = " Note pinned", Data = result });
                }
            
        
        }
        [HttpPut("IsTrash")]
        public IActionResult IsTrash( long noteId)
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

            var result = inoteBl.IsTrash(userId,noteId);
            if (result)
            {
                return Ok(new ResponseModel<bool> { Status = true, Message = " Note Trashed", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<bool> { Status = false, Message = " Note Restored", Data = result });
            }
        }

        [HttpPut("IsArchive")]

        public IActionResult IsArchive( long noteId)
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

            var result = inoteBl.IsArchive(userId,noteId);
            if (result)
            {
                return Ok(new ResponseModel<bool> { Status = true, Message = " Note Archive", Data = result });
            }
            else
            {
                        
            }
            {
                return BadRequest(new ResponseModel<bool> { Status = false, Message = " Note UnArchive", Data = result });
            }
        }
        [HttpDelete("Delete")]
        public IActionResult Delete(long noteId)
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

            var result = inoteBl.DeleteNote(userId, noteId);
            if (result)
            {
                return Ok(new ResponseModel<bool> { Status = true, Message = " Note Deleted", Data = result });

            }
            else
            {
                return BadRequest(new ResponseModel<bool> { Status = false, Message = " Note NotDeleted but Trashed", Data = result });

            }
        }
        [HttpPut("Color")]
        public IActionResult Color(long noteId,string color) {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = inoteBl.Color(userId,noteId, color);
                if (result != null)
                {
                    return Ok(new ResponseModel<NoteEntity> { Status = true, Message = "Color changed Successful", Data = result });

                }
                else
                {
                    return BadRequest(new ResponseModel<NoteEntity> { Status = false, Message = "Color change UnSuccessful", Data = result });

                }
            }catch (Exception ex) { throw ex; }
        }
        [HttpPut("Reminder")]
        public IActionResult Reminder(long noteId,DateTime reminder)
        {
             long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
            var result= inoteBl.Reminder(userId, noteId, reminder);
            if(result == reminder) 
            {
                return Ok(new ResponseModel<DateTime> { Status = true, Message = "Reminder set Successfully", Data = result });

            }
            else
            {
                return BadRequest(new ResponseModel<DateTime> { Status = false, Message = "Reminder set UnSuccessfully", Data = result });

            }


        }
        [HttpPut("UploadImage")]
        public IActionResult UploadImage(long noteId, IFormFile filePath)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = inoteBl.UploadImage(userId, noteId, filePath);
                if (result != null)
                {
                    return Ok(new ResponseModel<string> { Status = true, Message = "ImageUpload Successfully", Data = result });

                }
                else
                {
                    return BadRequest(new ResponseModel<string> { Status = false, Message = "ImageUpload UnSuccessfully", Data = result });
                }
            }catch (Exception ex) { throw ex; }
        }

        [HttpGet("GetAllNotesUsingRedisCache")]
        public async Task<IActionResult> GetAllNotesUsingRedisCache()
        {
            try
            {
                var cacheKey = "NotesList";
                List<NoteEntity> noteList;
                // Trying to get data from the Redis cache
                byte[] RedisNotesList = await distributedCache.GetAsync(cacheKey);
                if (RedisNotesList != null)
                {
                    // If the data is found in the cache, encode and deserialize cached data.
                    var cachedDataString = Encoding.UTF8.GetString(RedisNotesList);
                    noteList = JsonSerializer.Deserialize<List<NoteEntity>>(cachedDataString);
                }
                else
                {
                    // If the data is not found in the cache, then fetch data from database
                    long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                    noteList = (List<NoteEntity>)inoteBl.GetAllNotes(userId);

                    // Serializing the data
                    string cachedDataString = JsonSerializer.Serialize(noteList);
                    var dataToCache = Encoding.UTF8.GetBytes(cachedDataString);

                    // Setting up the cache options
                    DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddMinutes(5))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(3));

                    // Add the data into the cache
                    await distributedCache.SetAsync(cacheKey, dataToCache, options);
                }
                return Ok(noteList);
            }
            catch (Exception ex) { throw ex; }
        }
    }

}

