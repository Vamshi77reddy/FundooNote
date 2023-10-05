using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICollabBl
    {
        public CollabEntity AddCollab(long userId, long noteId, string collabEmail);
        public List<CollabEntity> GetAllCollaborations(long userId);


    }
}
