﻿using BridgingTheGap.Data;
using BridgingTheGap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgingTheGap.Services
{
    public class TutorService
    {
        private readonly Guid _userId;
        public TutorService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateTutor(TutorCreate model)
        {
            var tutorModel =
                new Tutor
                {
                    OwnerId = _userId,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Tutors.Add(tutorModel);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<TutorListItem> GetAllTutors()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Tutors
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                        e =>
                            new TutorListItem
                            {

                                TutorId = e.TutorId,
                                FirstName = e.FirstName,
                                LastName = e.LastName
                            });
                return query.ToArray();
            }            
        }
        public TutorDetails GetTutorById(int id)
        {
            using (var ctx =  new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Tutors
                        .Single(e => e.TutorId == id && e.OwnerId == _userId);
                return
                    new TutorDetails
                    {
                        TutorId = entity.TutorId,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                        OwnerId = entity.OwnerId,
                        NumberOfSubjects = entity.Subjects.Count()
                    };
            }
        }
        public bool DeleteTutor(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Tutors
                        .Single(e => e.TutorId == id && e.OwnerId == _userId);
                ctx.Tutors.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public bool UpdateTutor(TutorEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                        ctx
                            .Tutors
                            .Single(e => e.TutorId == model.TutorId && model.OwnerId == _userId);               
                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                return ctx.SaveChanges() == 1;                
            }
        }
    }
}
