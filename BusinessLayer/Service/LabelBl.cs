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

        private LabelBl(ILabelRl ilabelRl)
        {
            this.ilabelRl = ilabelRl;
        }
        public LabelEntity AddLabel(long userId, long noteId, string label)
        {
            return this.ilabelRl.AddLabel(userId, noteId, label);
        }

    }
}
