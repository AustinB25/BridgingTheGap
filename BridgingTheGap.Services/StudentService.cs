using BridgingTheGap.Data;
using BridgingTheGap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgingTheGap.Services
{
    public class StudentService
    {
        private readonly Guid _userId;
        public StudentService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateStudent(StudentCreate model)
        {
            var entity =
                new Student
                {
                    OwnerId = _userId,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Students.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<StudentListItem> GetStudents()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Students
                    .Where(e => e.OwnerId == _userId)
                    .Select(
                        e =>
                    new StudentListItem
                    {
                        StudentId = e.StudentId,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        NumberOfSubjects = e.Subjects.Count
                    });
                return query.ToArray();
            }
        }
        public StudentDetail GetStudentById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Students
                        .Single(e => e.StudentId == id && e.OwnerId == _userId);
                var model =
                    new StudentDetail
                    {
                        StudentId = entity.StudentId,
                        OwnerId = entity.OwnerId,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                        Subjects = entity.Subjects
                    };
                return model;
            }
        }
       public bool DeleteStudent(int id )
       {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Students
                    .Single(e => e.StudentId == id && e.OwnerId == _userId);
                ctx.Students.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
       }
        public bool UpdateStudent(StudentEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Students
                    .Single(e => e.StudentId == model.StudentId && e.OwnerId == _userId);
                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                return ctx.SaveChanges() == 1;
            }
        }
        public bool AddSubjectToStudent(int subjectId, int studentId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var studentEntity =
                    ctx
                    .Students
                    .Single(e => e.StudentId == studentId);
                var subjectEntity =
                    ctx
                    .Subjects
                    .Single(e => e.SubjectId == subjectId);
                studentEntity.Subjects.Add(subjectEntity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
