using BusinessLayer.Interface;
using CommonLayer.Model;
using RepoLayer.Entity;
using RepoLayer.Interface;
using RepoLayer.Services;
using System;
using System.Collections.Generic;
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

        public bool IsArchive(long noteId)
        {
            return this.noteRl.IsArchive(noteId);
        }
        public bool DeleteNote(long userId, long noteId)
        {
            return this.noteRl.DeleteNote( userId,noteId);
        }
        public NoteEntity Color(long noteId, String color)
        {
            return this.noteRl.Color(noteId, color);
        }

    }
}
