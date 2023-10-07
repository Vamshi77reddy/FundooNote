using Microsoft.Extensions.Configuration;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepoLayer.Services
{
    public class LabelRl : ILabelRl
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration configuration;

        public LabelRl(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
        }
        public LabelEntity AddLabel(long userId, long noteId, string label)
        {
            try
            {
                LabelEntity labelEntity = new LabelEntity();
                labelEntity.LabelName = label;
                labelEntity.UserId = userId;
                labelEntity.NoteID = noteId;
                fundooContext.Label.Add(labelEntity);
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
        public List<LabelEntity> GetLabelByNoteId(long userId, long noteId) {
            try
            {
                List<LabelEntity> labelEntities = new List<LabelEntity>();
                labelEntities = fundooContext.Label.Where(x => x.UserId == userId && x.NoteID == noteId).ToList();
                if (labelEntities != null)
                {
                    return labelEntities;
                }
                else
                {
                    return null;
                }
            } catch (Exception ex) { throw ex; }

        }
        public List<LabelEntity> GetLabelByLabelId(long labelId, long noteId)
        {
            try
            {
                List<LabelEntity> labelEntities = new List<LabelEntity>();
                labelEntities = fundooContext.Label.Where(x => x.LabelId == labelId && x.NoteID == noteId).ToList();
                if (labelEntities != null)
                {
                    return labelEntities;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex) { throw ex; }

        }


        public List<LabelEntity> GetLabelByUserId( long userId)
        {
            try
            {
                List<LabelEntity> labelEntities = new List<LabelEntity>();
                labelEntities = fundooContext.Label.Where(x => x.UserId == userId).ToList();
                if (labelEntities != null)
                {
                    return labelEntities;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex) { throw ex; }

        }
        public LabelEntity GetLabelByLabelName(long userId, string labelName)
        {
            try
            {
                List<LabelEntity> labelEntities = fundooContext.Label
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


        public bool DeleteLabel(long userId,int labelId)
        {
            try
            {
                LabelEntity labelEntity = fundooContext.Label.FirstOrDefault(x => x.UserId == userId && x.LabelId == labelId);
                if(labelEntity != null)
                {
                    fundooContext.Remove(labelEntity);
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }catch(Exception ex) { throw ex; }  
        }


    }
}