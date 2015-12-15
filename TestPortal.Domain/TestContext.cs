using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestPortal.Domain.Entities;

namespace TestPortal.Domain
{
    public class TestContext : DbContext
    {
        
        public TestContext() 
        {
            Database.SetInitializer<TestContext>(new TestContextInitilizer());
        }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Passage> Passages { get; set; }
        public DbSet<Attempt> Attempts { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Test>()
                .HasMany(t => t.Questions).WithMany(q => q.Tests)
                .Map(table => table.MapLeftKey("TestId")
                    .MapRightKey("QuestionId")
                    .ToTable("TestQuestion"));
            modelBuilder.Entity<Question>()
                .HasMany(q => q.Replies)
                .WithRequired(r=>r.Question)
                .HasForeignKey(r=>r.QuestionId);
            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles).WithMany(r => r.Users)
                .Map(table => table.MapLeftKey("UserId")
                    .MapRightKey("RoleId")
                    .ToTable("UserRole"));
            modelBuilder.Entity<User>()
                .HasMany(u => u.Groups).WithMany(g => g.Users)
                .Map(table => table.MapLeftKey("UserId")
                    .MapRightKey("GroupId")
                    .ToTable("UserGroup"));
            modelBuilder.Entity<User>()
                .HasMany(u => u.Tests).WithMany(t => t.Users)
                .Map(table => table.MapLeftKey("UserId")
                    .MapRightKey("TestId")
                    .ToTable("UserTest"));
            modelBuilder.Entity<Attempt>()
                .HasMany(a => a.Passages)
                .WithRequired(p=>p.Attempt)
                .HasForeignKey(p=>p.AttemptId);
            modelBuilder.Entity<Attempt>()
                .HasRequired(a => a.Test)
                .WithMany()
                .HasForeignKey(t => t.TestId);
            modelBuilder.Entity<Attempt>()
                .HasRequired(a => a.User)
                .WithMany()
                .HasForeignKey(u => u.UserId);
            modelBuilder.Entity<Passage>()
                .HasRequired(p => p.Reply)
                .WithMany()
                .HasForeignKey(p => p.ReplyId);
        } 
    }
}
