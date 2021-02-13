using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TaskTracker.Core.Models;

namespace TaskTracker.Infrastructure.Data
{
    public partial class TaskTrackerContext : DbContext
    {
        public TaskTrackerContext()
        {
        }

        public TaskTrackerContext(DbContextOptions<TaskTrackerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<Friendship> Friendships { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Priority> Priorities { get; set; }
        public virtual DbSet<Process> Processes { get; set; }
        public virtual DbSet<Specialty> Specialties { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<TaskTag> TasksTags { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserSpecialty> UsersSpecialties { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=TaskTracker;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<File>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.FileType)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.File)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Files_Tasks");
            });

            modelBuilder.Entity<Friendship>(entity =>
            {
                entity.Property(e => e.FriendId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.JoinDate).HasColumnType("datetime");

                entity.Property(e => e.LeaveDate).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Friend)
                    .WithMany(p => p.FriendshipsFriend)
                    .HasForeignKey(d => d.FriendId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Friendship_Person1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FriendshipsUser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Friendship_Person");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.Property(e => e.NotificationValue).IsRequired();

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notification_User");
            });

            modelBuilder.Entity<Priority>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Process>(entity =>
            {
                entity.Property(e => e.JoinDate).HasColumnType("datetime");

                entity.Property(e => e.LeaveDate).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.Processes)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Process_Task");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Processes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Process_Person");
            });

            modelBuilder.Entity<Specialty>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.Property(e => e.CloseDate).HasColumnType("datetime");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreaterId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.DueDate).HasColumnType("datetime");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.ReOpenDate).HasColumnType("datetime");

                entity.HasOne(d => d.Creater)
                    .WithMany(p => p.Task)
                    .HasForeignKey(d => d.CreaterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tasks_Users");

                entity.HasOne(d => d.ParentTask)
                    .WithMany(p => p.InverseParentTask)
                    .HasForeignKey(d => d.ParentTaskId)
                    .HasConstraintName("FK_Task_Task");

                entity.HasOne(d => d.Priority)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.PriorityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Task_Priority");
            });

            modelBuilder.Entity<TaskTag>(entity =>
            {
                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.TasksTags)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TaskTag_Tags");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.TasksTags)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TaskTag_Task");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<UserSpecialty>(entity =>
            {
                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Specialty)
                    .WithMany(p => p.UsersSpecialties)
                    .HasForeignKey(d => d.SpecialtyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserSpecialty_Specialty");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UsersSpecialties)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserSpecialty_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
