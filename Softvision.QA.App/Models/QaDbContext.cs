using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Softvision.QA.App.Models
{
    public class QaDbContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
        public QaDbContext()
            : base("name=QaDbContext")
        {
        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<QuestionVote> QuestionVotes { get; set; }
        public DbSet<AnswerVote> AnswerVotes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Building Many-To-Many Relationship between "Question" and "Tag" Models.
            modelBuilder.Entity<Question>()
                        .HasMany<Tag>(t => t.Tags)
                        .WithMany(q => q.Questions)
                        .Map(tq =>
                        {
                            tq.MapLeftKey("QuestionId");
                            tq.MapRightKey("TagId");
                            tq.ToTable("QuestionTag");
                        });

            modelBuilder.Entity<Answer>()
                        .HasRequired(q => q.Question)
                        .WithMany(a => a.Answers)
                        .HasForeignKey(fk => fk.QuestionId);
        }
    }
}
