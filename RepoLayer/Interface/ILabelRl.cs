using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    public interface ILabelRl
    {
        public LabelEntity AddLabel(long userId, long noteId, string label);

    }
}
