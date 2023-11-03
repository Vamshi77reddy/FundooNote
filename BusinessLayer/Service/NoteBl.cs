using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepoLayer.Entity;
using RepoLayer.Interface;
using RepoLayer.Services;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace BusinessLayer.Service
{
    public class NoteBl : INoteBl
    {
        private readonly INoteRl noteRl;
        public NoteBl(INoteRl noteRl)
        {
            this.noteRl = noteRl;
        }

        public NoteEntity TakeANote(NotesModel notesModel, long userId)
        {
            return this.noteRl.TakeANote(notesModel, userId);

        }
        public List<NoteEntity> GetAllNotes(long userId)
        {
            return this.noteRl.GetAllNotes(userId);
        }
        public NoteEntity UpdateNote(NotesModel notesModel, long userId, long noteId)
        {
            return this.noteRl.UpdateNote(notesModel, userId, noteId);
        }

        public bool IsPinorNot(long noteId, long userId)
        {
           return this.noteRl.IsPinorNot(noteId, userId);
        }

        public bool IsTrash(long userId, long noteId)
        {
            return this.noteRl.IsTrash(userId, noteId);
        }

        public bool IsArchive(long userId, long noteId)
        {
            return this.noteRl.IsArchive(userId, noteId);
        }
        public bool DeleteNote(long userId, long noteId)
        {
            return this.noteRl.DeleteNote( userId,noteId);
        }
        public NoteEntity Color(long userId, long noteId, string color)
        {
            return this.noteRl.Color(userId, noteId, color);
        }
        public DateTime Reminder(long userId, long noteId, DateTime reminder)
        {
            return this.noteRl.Reminder(userId, noteId, reminder);
        }

        public string UploadImage(long userId, long noteid, IFormFile image)
        {
            return this.noteRl.UploadImage(userId, noteid, image);
        }
        public NoteEntity GetNoteByName(long userId, string noteName)
        {
            return this.GetNoteByName(userId, noteName);
        }

        public List<NoteEntity> NotebyDate(long userId, DateTime date)
        {
            return this.NotebyDate(userId, date);
        }

    }
}
