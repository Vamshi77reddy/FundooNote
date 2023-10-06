﻿using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepoLayer.Services
{
    public class CollabRl:ICollabRl
    {
        private readonly FundooContext fundooContext;
        public CollabRl(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        public CollabEntity AddCollab(long userId,long noteId,string collabEmail)
        {
            try
            {
                CollabEntity collabEntity = new CollabEntity();
                collabEntity.UserId = userId;
                collabEntity.NoteID = noteId;
                collabEntity.CollabEmail = collabEmail;
                fundooContext.Add(collabEntity);
                fundooContext.SaveChanges();
                return collabEntity;
            }catch (Exception ex) { throw ex; }
        }

        public List<CollabEntity> GetAllCollaborations(long userId)
        {
            try
            {
                var result = fundooContext.Collab.Where(x => x.UserId == userId).ToList();
                if (result != null)
                {
                    return result;
                }
                return null;
            }catch(Exception ex) { throw ex; }  
        
        }

        public CollabEntity DeleteCollab(long userId,long noteId,long collabId)
        {
            try
            {

                CollabEntity collabEntity = fundooContext.Collab.Where(x => x.UserId == userId && x.NoteID == noteId).FirstOrDefault();
                if (collabEntity != null)
                {
                    fundooContext.Remove(collabEntity);
                    fundooContext.SaveChanges();
                    return collabEntity;
                }
                else
                {
                    return null;
                }
            }catch (Exception ex) { throw ex; }
        }

    }
}
