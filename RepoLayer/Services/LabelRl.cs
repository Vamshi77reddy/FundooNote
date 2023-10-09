namespace RepoLayer.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Extensions.Configuration;
    using RepoLayer.Context;
    using RepoLayer.Entity;
    using RepoLayer.Interface;

    public class LabelRl : ILabelRl
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelRl"/> class.
        /// </summary>
        /// <param name="fundooContext"></param>
        /// <param name="configuration"></param>
        public LabelRl(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
        }

        /// <inheritdoc/>
        public LabelEntity AddLabel(long userId, long noteId, string label)
        {
            try
            {
                LabelEntity labelEntity = new LabelEntity();
                labelEntity.LabelName = label;
                labelEntity.UserId = userId;
                labelEntity.NoteID = noteId;
                this.fundooContext.Label.Add(labelEntity);
                int result = this.fundooContext.SaveChanges();
                if (result > 0)
                {
                    return labelEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex) { throw ex; }
        }

        /// <inheritdoc/>
        public List<LabelEntity> GetLabelByNoteId(long userId, long noteId) {
            try
            {
                List<LabelEntity> labelEntities = new List<LabelEntity>();
                labelEntities = this.fundooContext.Label.Where(x => x.UserId == userId && x.NoteID == noteId).ToList();
                if (labelEntities != null)
                {
                    return labelEntities;
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

        /// <inheritdoc/>
        public List<LabelEntity> GetLabelByLabelId(long labelId, long noteId)
        {
            try
            {
                List<LabelEntity> labelEntities = new List<LabelEntity>();
                labelEntities = this.fundooContext.Label.Where(x => x.LabelId == labelId && x.NoteID == noteId).ToList();
                if (labelEntities != null)
                {
                    return labelEntities;
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

        /// <inheritdoc/>
        public List<LabelEntity> GetLabelByUserId( long userId)
        {
            try
            {
                List<LabelEntity> labelEntities = new List<LabelEntity>();
                labelEntities = this.fundooContext.Label.Where(x => x.UserId == userId).ToList();
                if (labelEntities != null)
                {
                    return labelEntities;
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

        /// <inheritdoc/>
        public LabelEntity GetLabelByLabelName(long userId, string labelName)
        {
            try
            {
                List<LabelEntity> labelEntities = this.fundooContext.Label
                    .Where(x => x.UserId == userId)
                    .ToList();

                LabelEntity label = labelEntities.FirstOrDefault(x => x.LabelName == labelName);

                return label;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <inheritdoc/>
        public bool DeleteLabel(long userId,int labelId)
        {
            try
            {
                LabelEntity labelEntity = this.fundooContext.Label.FirstOrDefault(x => x.UserId == userId && x.LabelId == labelId);
                if (labelEntity != null)
                {
                    this.fundooContext.Remove(labelEntity);
                    this.fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}