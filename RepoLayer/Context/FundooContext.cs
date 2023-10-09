// <copyright file="FundooContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace RepoLayer.Context
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.EntityFrameworkCore;
    using RepoLayer.Entity;

    public class FundooContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FundooContext"/> class.
        /// </summary>
        /// <param name="options"></param>
        public FundooContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<UserEntity> UserTable { get; set; }

        public DbSet<NoteEntity> NoteTable { get; set; }

        public DbSet<LabelEntity> Label { get; set; }

        public DbSet<CollabEntity> Collab { get; set; }
    }
}
