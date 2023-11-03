﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace RepoLayer.Entity
{
    public class LabelEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LabelId { get; set; }

        public string LabelName { get; set; }

        [ForeignKey("User")]
        public long UserId { get; set; }

        [JsonIgnore]
        public virtual UserEntity User { get; set; }

        [ForeignKey("Note")]
        public long NoteID { get; set; }

        [JsonIgnore]
        public virtual NoteEntity Note { get; set;}

    }
}
