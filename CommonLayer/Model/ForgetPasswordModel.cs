using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class ForgetPasswordModel
    {
        public long UserId { get; set; }
        public string EmailId { get; set; }
        public string Token { get; set; }

    }
}
