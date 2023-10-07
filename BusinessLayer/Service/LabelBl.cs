using BusinessLayer.Interface;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class LabelBl:ILabelBl
    {
        private readonly ILabelRl ilabelRl;

        public LabelBl(ILabelRl ilabelRl)
        {
            this.ilabelRl = ilabelRl;
        }
        public LabelEntity AddLabel(long userId, long noteId, string label)
        {
            return this.ilabelRl.AddLabel(userId, noteId, label);
        }
        public List<LabelEntity> GetLabelByNoteId(long userId, long noteId)
        {
            return this.ilabelRl.GetLabelByNoteId(userId, noteId);
        }
        public List<LabelEntity> GetLabelByUserId(long userId)
        {
            return this.ilabelRl.GetLabelByUserId(userId);
        }
        public bool DeleteLabel(long userId, int labelId)
        {
            return this.ilabelRl.DeleteLabel(userId, labelId);
        }
        public List<LabelEntity> GetLabelByLabelId(long labelId, long noteId)
        {
            return this.ilabelRl.GetLabelByLabelId(labelId, noteId);
        }
        public LabelEntity GetLabelByLabelName(long userId, string labelName)
        {
            return this.ilabelRl.GetLabelByLabelName(userId, labelName);
        }



    }
}
