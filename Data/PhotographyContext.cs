using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Photography.Models;

namespace Photography.Data;

public partial class PhotographyContext : DbContext
{
    public PhotographyContext()
    {
    }

    public PhotographyContext(DbContextOptions<PhotographyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Like> Likes { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Photo> Photos { get; set; }

    public virtual DbSet<PhotoCategory> PhotoCategories { get; set; }

    public virtual DbSet<Plan> Plans { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Testimonial> Testimonials { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Initial Catalog=Photography;Persist Security Info=False;User ID=sa;Password=aptech;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.BlogId).HasName("PK__Blogs__54379E504FBF67FC");

            entity.Property(e => e.BlogId).HasColumnName("BlogID");
            entity.Property(e => e.AuthorName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValueSql("('Admin')");
            entity.Property(e => e.BlogContent).IsUnicode(false);
            entity.Property(e => e.BlogTitle)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Bookings__73951ACD71154896");

            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.BookingAddress)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.BookingContact)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BookingCreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.BookingDate).HasColumnType("date");
            entity.Property(e => e.BookingPlanId).HasColumnName("BookingPlanID");
            entity.Property(e => e.BookingStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('Pending')");
            entity.Property(e => e.BookingUpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.BookingUserId).HasColumnName("BookingUserID");

            entity.HasOne(d => d.BookingPlan).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.BookingPlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bookings__Bookin__693CA210");

            entity.HasOne(d => d.BookingUser).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.BookingUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bookings__Bookin__6A30C649");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comments__C3B4DFAAC0C49112");

            entity.Property(e => e.CommentId).HasColumnName("CommentID");
            entity.Property(e => e.BlogId).HasColumnName("BlogID");
            entity.Property(e => e.CommentText)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Blog).WithMany(p => p.Comments)
                .HasForeignKey(d => d.BlogId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comments__BlogID__6B24EA82");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comments__UserID__6C190EBB");
        });

        modelBuilder.Entity<Like>(entity =>
        {
            entity.HasKey(e => e.LikeId).HasName("PK__Likes__A2922CF4D47FC12E");

            entity.Property(e => e.LikeId).HasColumnName("LikeID");
            entity.Property(e => e.BlogId).HasColumnName("BlogID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Blog).WithMany(p => p.Likes)
                .HasForeignKey(d => d.BlogId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Likes__BlogID__6D0D32F4");

            entity.HasOne(d => d.User).WithMany(p => p.Likes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Likes__UserID__6E01572D");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E3216D8EF71");

            entity.Property(e => e.NotificationId).HasColumnName("NotificationID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsRead).HasDefaultValueSql("((0))");
            entity.Property(e => e.NotificationMessage).IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificat__UserI__6EF57B66");
        });

        modelBuilder.Entity<Photo>(entity =>
        {
            entity.HasKey(e => e.PhotoId).HasName("PK__Photos__21B7B58200DF4FF3");

            entity.Property(e => e.PhotoId).HasColumnName("PhotoID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.PhotoDesc)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PhotoTitle)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PhotoUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("PhotoURL");

            entity.HasOne(d => d.Category).WithMany(p => p.Photos)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Photos__Category__6FE99F9F");
        });

        modelBuilder.Entity<PhotoCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__PhotoCat__19093A2B93EE6B29");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CategoryPhoto)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Plan>(entity =>
        {
            entity.HasKey(e => e.PlanId).HasName("PK__Plans__755C22D71D4D0C58");

            entity.Property(e => e.PlanId).HasColumnName("PlanID");
            entity.Property(e => e.List1)
                .IsUnicode(false)
                .HasColumnName("list1");
            entity.Property(e => e.List2)
                .IsUnicode(false)
                .HasColumnName("list2");
            entity.Property(e => e.List3)
                .IsUnicode(false)
                .HasColumnName("list3");
            entity.Property(e => e.List4)
                .IsUnicode(false)
                .HasColumnName("list4");
            entity.Property(e => e.List5)
                .IsUnicode(false)
                .HasColumnName("list5");
            entity.Property(e => e.List6)
                .IsUnicode(false)
                .HasColumnName("list6");
            entity.Property(e => e.PlanName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE1AA1E8E08F");

            entity.ToTable("Role");

            entity.Property(e => e.RoleName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Testimonial>(entity =>
        {
            entity.HasKey(e => e.TestimonialId).HasName("PK__Testimon__91A23E53E8E405A3");

            entity.Property(e => e.TestimonialId).HasColumnName("TestimonialID");
            entity.Property(e => e.Content).IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Testimonials)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Testimoni__UserI__70DDC3D8");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACAEE1D02B");

            entity.HasIndex(e => e.UserEmail, "UQ__Users__08638DF81439BC14").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserImg)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("('defult.png')");
            entity.Property(e => e.UserName)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.UserPassword)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UserPet)
                .HasMaxLength(110)
                .IsUnicode(false);

            entity.HasOne(d => d.UserRole).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserRoleId)
                .HasConstraintName("FK__Users__UserRoleI__71D1E811");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
