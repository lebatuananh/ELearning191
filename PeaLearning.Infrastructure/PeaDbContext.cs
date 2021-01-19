using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PeaLearning.Domain.AggregateModels.CourseAggregate;
using PeaLearning.Domain.AggregateModels.UserAggregate;
using System;
using System.Reflection;
using PeaLearning.Domain.AggregateModels.BannerAggregate;
using PeaLearning.Domain.AggregateModels.BlogAggregate;
using PeaLearning.Domain.AggregateModels.Contact;
using PeaLearning.Domain.AggregateModels.TagAggregate;

namespace PeaLearning.Infrastructure
{
    public class PeaDbContext : IdentityDbContext<User, Role, Guid>
    {
        public PeaDbContext(DbContextOptions<PeaDbContext> options) : base(options)
        {
        }

        public PeaDbContext()
        {
        }

        public virtual DbSet<Course> Courses { get; private set; }
        public virtual DbSet<Lesson> Lessons { get; private set; }
        public virtual DbSet<Question> Questions { get; private set; }
        public virtual DbSet<CourseRegistration> CourseRegistrations { get; private set; }
        public virtual DbSet<CourseAttachment> CourseAttachments { get; private set; }
        public virtual DbSet<Blog> Blogs { get; private set; }
        public virtual DbSet<BlogTag> BlogTags { get; private set; }
        public virtual DbSet<Tag> Tags { get; private set; }
        public virtual DbSet<Banner> Banners { get; private set; }
        public virtual DbSet<Contact> Contacts { get; private set; }
        public virtual DbSet<Response> Responses { get; private set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}