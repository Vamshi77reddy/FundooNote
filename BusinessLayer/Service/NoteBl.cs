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


    }
}
