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
        public NoteEntity UpdateNote(NotesModel notesModel, long userId, long noteId);
        public bool IsPinorNot(long noteId,long userId);
        public bool IsTrash(long userId, long noteId);
        public bool IsArchive(long noteId);
        public bool DeleteNote(long userId, long noteId);
        public NoteEntity Color(long noteId, String color);








    }
}
