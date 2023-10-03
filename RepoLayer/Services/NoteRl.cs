using Automatonymous.Binders;
using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
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
        public NoteEntity UpdateNote(NotesModel notesModel,long userId,long noteId)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();
                noteEntity = fundooContext.NoteTable.Where(x => x.UserId == userId && x.NoteID == noteId).FirstOrDefault();
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
                    fundooContext.SaveChanges();
                    return noteEntity;
                }
                else
                {
                    return null;
                }
            }catch(Exception ex) { throw ex; }

        }
        public bool IsPinorNot(long noteId,long userId)
        {
            NoteEntity noteEntity=fundooContext.NoteTable.Where(x=>x.NoteID== noteId&& x.UserId==userId).FirstOrDefault();
            if (noteEntity.IsPin == true)
            {
                noteEntity.IsPin = false;
                fundooContext.SaveChanges() ;
                return false;
            }
            else
            {
                noteEntity.IsPin = true; 
                fundooContext.SaveChanges() ; 
                return true;
            }
        }
        public bool IsTrash(long userId, long noteId)
        {
            NoteEntity noteEntity = fundooContext.NoteTable.Where(x => x.NoteID == noteId&&x.UserId==userId).FirstOrDefault();
            if (noteEntity.IsTrash == true)
            {
                noteEntity.IsTrash = false;
                fundooContext.SaveChanges();
                return false;
            }
            else
            {
                noteEntity.IsTrash = true;
                fundooContext.SaveChanges();
                return true;
            }
        }

        public bool IsArchive(long noteId)
        {
            NoteEntity noteEntity = fundooContext.NoteTable.Where(x => x.NoteID == noteId).FirstOrDefault();
            if (noteEntity.IsArchive == true)
            {
                noteEntity.IsArchive = false;
                fundooContext.SaveChanges();
                return false;
            }
            else
            {
                noteEntity.IsArchive = true;
                fundooContext.SaveChanges();
                return true;
            }

        }
        public bool DeleteNote(long userId,long noteId)
        {
            try
            {
                NoteEntity noteEntity = fundooContext.NoteTable.Where(x => x.NoteID == noteId&&x.UserId==userId).FirstOrDefault();
                if (noteEntity.IsTrash == true)
                {
                    fundooContext.Remove(noteEntity);
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    noteEntity.IsTrash = true;
                
                    fundooContext.SaveChanges();
                    return false;
                }
            }catch (Exception ex) { throw ex; }
        }

        public NoteEntity Color(long noteId,String color)
        {
            try
            {
                NoteEntity noteEntity = fundooContext.NoteTable.Where(x => x.NoteID == noteId).FirstOrDefault();
                if (noteEntity.Color != null)
                {
                    noteEntity.Color = color;
                    fundooContext.SaveChanges();
                    return noteEntity;
                }
                else
                {
                    return null;
                }
            }catch(Exception ex) { throw ex; }  
        }

    }
}
