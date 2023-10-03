using Automatonymous.Binders;
using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interface;
using RepoLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NoteEntity = RepoLayer.Entity.NoteEntity;

namespace RepoLayer.Services
{
    public class NoteRl:INoteRl
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration configuration;
        public NoteRl(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
        }
        public NoteEntity TakeANote(NotesModel notesModel,long userId)
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

                fundooContext.Add(noteEntity);
                fundooContext.SaveChanges();
                return noteEntity;


            }
            catch (Exception ex) { throw ex; }
        }
        public List<NoteEntity> GetAllNotes(long userId)
        {
            try
            {
                List<NoteEntity> notes = fundooContext.NoteTable.Where(x=>x.UserId == userId).ToList();
                if (notes != null)
                {
                    return notes;
                }else
                {
                    return null;
                }
            }catch (Exception ex) { throw ex; } 
        }
    }
}
