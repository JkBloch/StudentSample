using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentSample.Database
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options)
        : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<StudentClass> StudentClass { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SubjectLanguageMapping> SubjectLanguageMapping { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherSubjectMapping> TeacherSubjectMapping { get; set; }
        private void StudentConfigureConstraint(ModelBuilder builder)
        {
            builder.Entity<Student>(entity =>
            {
                entity.HasOne(i => i.StudentClass).WithMany(g => g.Student).HasForeignKey(x => x.StudentClassId).OnDelete(DeleteBehavior.Restrict).IsRequired(false);                
            });
            
            builder.Entity<SubjectLanguageMapping>(entity =>
            {
                entity.HasOne(i => i.Subject).WithMany(g => g.SubjectLanguageMapping).HasForeignKey(x => x.SubjectId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(i => i.Language).WithMany(g => g.SubjectLanguageMapping).HasForeignKey(x => x.LanguageId).OnDelete(DeleteBehavior.Restrict);
            });
            builder.Entity<Teacher>(entity =>
            {
                entity.HasCheckConstraint("Sex",
                    @"(Sex = 'Male')  or (Sex = 'Female') or (Sex = 'Other')");
            });
            builder.Entity<TeacherSubjectMapping>(entity =>
            {
                entity.HasOne(i => i.StudentClass).WithMany(g => g.TeacherSubjectMapping).HasForeignKey(x => x.StudentClassId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(i => i.Teacher).WithMany(g => g.TeacherSubjectMapping).HasForeignKey(x => x.TeacherId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(i => i.Subject).WithMany(g => g.TeacherSubjectMapping).HasForeignKey(x => x.SubjectId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(i => i.Language).WithMany(g => g.TeacherSubjectMapping).HasForeignKey(x => x.LanguageId).OnDelete(DeleteBehavior.Restrict);
            });

           

           

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            StudentConfigureConstraint(builder);
        }
    }
}
