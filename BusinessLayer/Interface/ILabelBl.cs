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

    }
}
