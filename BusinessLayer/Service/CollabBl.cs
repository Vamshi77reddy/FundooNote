using BusinessLayer.Interface;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class CollabBl:ICollabBl
    {
        private readonly ICollabRl collabRl;
        public CollabBl(ICollabRl collabRl)
        {
            this.collabRl = collabRl;
        }
        public CollabEntity AddCollab(long userId, long noteId, string collabEmail)
        {
           return  this.collabRl.AddCollab(userId, noteId, collabEmail);
        }
        public List<CollabEntity> GetAllCollaborations(long userId)
        {
            return this.collabRl.GetAllCollaborations(userId);
        }
        public CollabEntity DeleteCollab(long userId, long noteId, long collabId)
        {
            return this.collabRl.DeleteCollab(userId, noteId, collabId);
        }

    }
}
