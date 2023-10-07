using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    public interface ILabelRl
    {
        public LabelEntity AddLabel(long userId, long noteId, string label);
        public List<LabelEntity> GetLabelByNoteId(long userId, long noteId);
        public List<LabelEntity> GetLabelByUserId(long userId);
        public bool DeleteLabel(long userId, int labelId);
        public List<LabelEntity> GetLabelByLabelId(long labelId, long noteId);
        public LabelEntity GetLabelByLabelName(long userId, string labelName);





    }
}
