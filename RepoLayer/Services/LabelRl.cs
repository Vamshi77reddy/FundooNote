using Microsoft.Extensions.Configuration;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
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

    }
}