// <copyright file="NoteRl.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace RepoLayer.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;
    using Automatonymous.Binders;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using CommonLayer.Model;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using RabbitMQ.Client;
    using RepoLayer.Context;
    using RepoLayer.Entity;
    using RepoLayer.Interface;
    using RepoLayer.Migrations;
    using NoteEntity = RepoLayer.Entity.NoteEntity;

    public class NoteRl : INoteRl
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="NoteRl"/> class.
        /// </summary>
        /// <param name="fundooContext"></param>
        /// <param name="configuration"></param>
        public NoteRl(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
        }

        /// <inheritdoc/>
        public NoteEntity TakeANote(NotesModel notesModel, long userId)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();

                noteEntity.UserId = userId;

                noteEntity.Title = notesModel.Title;
                noteEntity.Note = notesModel.Note;
                noteEntity.Reminder = notesModel.Reminder;
                noteEntity.Color = notesModel.Color;
                noteEntity.Image = notesModel.Image;
                noteEntity.IsArchive = notesModel.IsArchive;
                noteEntity.IsPin = notesModel.IsPin;
                noteEntity.IsTrash = notesModel.IsTrash;
                noteEntity.Createat = DateTime.Now;
                noteEntity.Modifiedat = DateTime.Now;

                this.fundooContext.Add(noteEntity);
                this.fundooContext.SaveChanges();
                return noteEntity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <inheritdoc/>
        public List<NoteEntity> GetAllNotes(long userId)
        {
            try
            {
                // List<NoteEntity> notes = fundooContext.NoteTable.Where(x=>x.UserId == userId).ToList();
                var notes = this.fundooContext.NoteTable.Where(x => x.UserId == userId).ToList();
                if (notes != null)
                {
                    return notes;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <inheritdoc/>
        public NoteEntity UpdateNote(NotesModel notesModel, long userId, long noteId)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();
                noteEntity = this.fundooContext.NoteTable.Where(x => x.UserId == userId && x.NoteID == noteId).FirstOrDefault();
                if (noteEntity != null)
                {
                    noteEntity.Title = notesModel.Title;
                    noteEntity.Note = notesModel.Note;
                    noteEntity.Reminder = notesModel.Reminder;
                    noteEntity.Color = notesModel.Color;
                    noteEntity.Image = notesModel.Image;
                    noteEntity.IsArchive = notesModel.IsArchive;
                    noteEntity.IsPin = notesModel.IsPin;
                    noteEntity.IsTrash = notesModel.IsTrash;
                    noteEntity.Createat = DateTime.Now;
                    noteEntity.Modifiedat = DateTime.Now;
                    this.fundooContext.SaveChanges();
                    return noteEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <inheritdoc/>
        public bool IsPinorNot(long noteId, long userId)
        {
            NoteEntity noteEntity = this.fundooContext.NoteTable.Where(x => x.NoteID == noteId && x.UserId == userId).FirstOrDefault();
            if (noteEntity.IsPin == true)
            {
                noteEntity.IsPin = false;
                this.fundooContext.SaveChanges();
                return false;
            }
            else
            {
                noteEntity.IsPin = true;
                this.fundooContext.SaveChanges();
                return true;
            }
        }

        /// <inheritdoc/>
        public bool IsTrash(long userId, long noteId)
        {
            NoteEntity noteEntity = this.fundooContext.NoteTable.Where(x => x.NoteID == noteId && x.UserId == userId).FirstOrDefault();
            if (noteEntity.IsTrash == true)
            {
                noteEntity.IsTrash = false;
                this.fundooContext.SaveChanges();
                return false;
            }
            else
            {
                noteEntity.IsTrash = true;
                this.fundooContext.SaveChanges();
                return true;
            }
        }

        /// <inheritdoc/>
        public bool IsArchive(long userId, long noteId)
        {
            NoteEntity noteEntity = this.fundooContext.NoteTable.Where(x => x.NoteID == noteId && x.UserId == userId).FirstOrDefault();
            if (noteEntity.IsArchive == true)
            {
                noteEntity.IsArchive = false;
                this.fundooContext.SaveChanges();
                return false;
            }
            else
            {
                noteEntity.IsArchive = true;
                this.fundooContext.SaveChanges();
                return true;
            }
        }

        /// <inheritdoc/>
        public bool DeleteNote(long userId, long noteId)
        {
            try
            {
                NoteEntity noteEntity = this.fundooContext.NoteTable.Where(x => x.NoteID == noteId && x.UserId == userId).FirstOrDefault();
                if (noteEntity.IsTrash == true)
                {
                    this.fundooContext.Remove(noteEntity);
                    this.fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    noteEntity.IsTrash = true;
                    this.fundooContext.SaveChanges();
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <inheritdoc/>
        public NoteEntity Color(long userId, long noteId, string color)
        {
            try
            {
                NoteEntity noteEntity = this.fundooContext.NoteTable.Where(x => x.NoteID == noteId && x.UserId == userId).FirstOrDefault();
                if (noteEntity.Color != null)
                {
                    noteEntity.Color = color;
                    this.fundooContext.SaveChanges();
                    return noteEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <inheritdoc/>
        public DateTime Reminder(long userId, long noteId, DateTime reminder)
        {
            try
            {
                NoteEntity noteEntity = this.fundooContext.NoteTable.Where(x => x.NoteID == noteId && x.UserId == userId).FirstOrDefault();
                if (noteEntity.Reminder != null)
                {
                    noteEntity.Reminder = reminder;
                    this.fundooContext.SaveChanges();
                    return noteEntity.Reminder;
                }

                return noteEntity.Reminder;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <inheritdoc/>
        public string UploadImage(long userId, long noteid, IFormFile image)
        {
            try
            {
                var result = this.fundooContext.NoteTable.Where(x => x.NoteID == noteid && x.UserId == userId).FirstOrDefault();
                if (result != null)
                {
                    Account account = new Account(
                        this.configuration["CloudinarySettings:CloudName"],
                        this.configuration["CloudinarySettings:ApiKey"],
                        this.configuration["CloudinarySettings:ApiSecret"]);
                    Cloudinary cloudinary = new Cloudinary(account);
                    var UploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(image.FileName, image.OpenReadStream()),
                    };
                    var uploadResult = cloudinary.Upload(UploadParams);
                    string imagePath = uploadResult.Url.ToString();
                    result.Image = imagePath;
                    this.fundooContext.SaveChanges();
                    return "Image Upload Successful";
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
