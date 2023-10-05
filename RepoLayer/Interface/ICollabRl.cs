using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    public interface ICollabRl
    {
        public CollabEntity AddCollab(long userId, long noteId, string collabEmail);
        public List<CollabEntity> GetAllCollaborations(long userId);


    }
}
