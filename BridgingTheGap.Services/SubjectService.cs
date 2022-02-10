using BridgingTheGap.Data;
using BridgingTheGap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgingTheGap.Services
{
    public class SubjectService
    {
        private readonly Guid _userId;
        public SubjectService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateSubject(SubjectCreate model)
        {
            var entity =
                new Subject
                {
                    OwnerId = _userId,
                    Name = model.Name
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Subjects.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<SubjectListItem> GetSubjects()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Subjects                    
                    .Select(
                        e =>
                        new SubjectListItem
                        {
                            SubjectId = e.SubjectId,
                            Name = e.Name
                        });
                return query.ToArray();                    
            }
        }
        public SubjectDetail GetSubjectById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Subjects
                    .Single(e => e.SubjectId == id);
                var model =
                    new SubjectDetail
                    {
                        SubjectId = entity.SubjectId,
                        Name = entity.Name,
                        StudentsInClass = entity.Students.Count()
                    };
                return model;
            }
        }
        public bool DeleteSubject(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Subjects
                    .Single(e => e.OwnerId == _userId && e.SubjectId == id);
                ctx.Subjects.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public bool UpdateSubject(SubjectEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Subjects
                    .Single(e => e.OwnerId == _userId && e.SubjectId == model.SubjectId);
                entity.Name = model.Name;
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
