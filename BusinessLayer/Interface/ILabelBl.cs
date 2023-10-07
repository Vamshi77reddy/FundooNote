using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ILabelBl
    {
        public LabelEntity AddLabel(long userId, long noteId, string label);
        public List<LabelEntity> GetLabelByNoteId(long userId, long noteId);
        public List<LabelEntity> GetLabelByUserId(long userId);
        public bool DeleteLabel(long userId, int labelId);
        public List<LabelEntity> GetLabelByLabelId(long labelId, long noteId);
        public LabelEntity GetLabelByLabelName(long userId, string labelName);






    }
}
