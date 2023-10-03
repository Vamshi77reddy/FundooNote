using CommonLayer.Model;
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


    }
}
