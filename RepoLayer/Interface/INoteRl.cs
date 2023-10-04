using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    public interface INoteRl
    {
        public NoteEntity TakeANote(NotesModel notesModel, long userId);
        public List<NoteEntity> GetAllNotes(long userId);
        public NoteEntity UpdateNote(NotesModel notesModel, long userId, long noteId);
        public bool IsPinorNot(long noteId,long userId);
        public bool IsTrash(long userId, long noteId);
        public bool IsArchive(long userId,long noteId);
        public bool DeleteNote(long userId, long noteId);
        public NoteEntity Color(long userId, long noteId, string color);
        public DateTime Reminder(long userId, long noteId, DateTime reminder);
        public string UploadImage(long userId, long noteid, IFormFile image);










    }
}
