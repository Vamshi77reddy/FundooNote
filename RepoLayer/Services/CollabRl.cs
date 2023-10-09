// <copyright file="CollabRl.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace RepoLayer.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RepoLayer.Context;
    using RepoLayer.Entity;
    using RepoLayer.Interface;

    public class CollabRl : ICollabRl
    {
        private readonly FundooContext fundooContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollabRl"/> class.
        /// </summary>
        /// <param name="fundooContext"></param>
        public CollabRl(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        /// <inheritdoc/>
        public CollabEntity AddCollab(long userId, long noteId, string collabEmail)
        {
            try
            {
                CollabEntity collabEntity = new CollabEntity();
                collabEntity.UserId = userId;
                collabEntity.NoteID = noteId;
                collabEntity.CollabEmail = collabEmail;
                this.fundooContext.Add(collabEntity);
                this.fundooContext.SaveChanges();
                return collabEntity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <inheritdoc/>
        public List<CollabEntity> GetAllCollaborations(long userId)
        {
            try
            {
                var result = this.fundooContext.Collab.Where(x => x.UserId == userId).ToList();
                if (result != null)
                {
                    return result;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <inheritdoc/>
        public CollabEntity DeleteCollab(long userId, long noteId, long collabId)
        {
            try
            {
                CollabEntity collabEntity = this.fundooContext.Collab.Where(x => x.UserId == userId && x.NoteID == noteId).FirstOrDefault();
                if (collabEntity != null)
                {
                    this.fundooContext.Remove(collabEntity);
                    this.fundooContext.SaveChanges();
                    return collabEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
