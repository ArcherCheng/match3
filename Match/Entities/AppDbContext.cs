using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace Match.Entities
{
    public partial class AppDbContext : DbContext
    {
        private readonly IConfiguration configuration;
        //private IDbConnection DbConnection { get; set; }
        //private string ConnectionString { get; set; }

        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration)
            : base(options)
        {
            this.configuration = configuration;
            //DbConnection = new SqlConnection(this.configuration.GetConnectionString("DefaultConnection"));
            //ConnectionString = this.configuration.GetConnectionString("DefaultConnection");
            //Console.WriteLine(DbConnection);
            //Console.WriteLine(ConnectionString);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("server=(local)\\SqlExpress;database=Match;Trusted_Connection=True;");
                //optionsBuilder.UseSqlServer(DbConnection.ToString());
                //optionsBuilder.UseSqlServer(ConnectionString);
            }
        }

        public virtual DbSet<GroupKeyValue> GroupKeyValue { get; set; }
        public virtual DbSet<Liker> Liker { get; set; }
        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<MemberCondition> MemberCondition { get; set; }
        public virtual DbSet<MemberDetail> MemberDetail { get; set; }
        public virtual DbSet<MemberPhoto> MemberPhoto { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<SysTransLog> SysTransLog { get; set; }
        public virtual DbSet<SysUserLog> SysUserLog { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<GroupKeyValue>(entity =>
            {
                entity.HasIndex(e => new { e.KeyGroup, e.KeyValue })
                    .HasName("GroupKeyValue_in1")
                    .IsUnique();

                entity.Property(e => e.KeyGroup)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.KeyLabel)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.KeyValue)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Liker>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LikerId })
                    .HasName("Pk_liker");

                entity.Property(e => e.AddedDate).HasColumnType("datetime");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.DeleteDate).HasColumnType("datetime");

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.Property(e => e.WriteIp).HasMaxLength(30);

                entity.HasOne(d => d.LikerNavigation)
                    .WithMany(p => p.LikerLikerNavigation)
                    .HasForeignKey(d => d.LikerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Liker_MyLiker");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.LikerUser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Liker_LikerMe");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("pk_Member");

                entity.HasIndex(e => e.Email)
                    .HasName("Member_in1")
                    .IsUnique();

                entity.HasIndex(e => e.Phone)
                    .HasName("Member_in2")
                    .IsUnique();

                entity.Property(e => e.ActiveDate).HasColumnType("datetime");

                entity.Property(e => e.Blood).HasMaxLength(2);

                entity.Property(e => e.City).HasMaxLength(30);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.JobType).HasMaxLength(30);

                entity.Property(e => e.LoginDate).HasColumnType("datetime");

                entity.Property(e => e.MainPhotoUrl).HasMaxLength(250);

                entity.Property(e => e.NickName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.PasswordHash).HasMaxLength(2000);

                entity.Property(e => e.PasswordSalt).HasMaxLength(2000);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Religion).HasMaxLength(30);

                entity.Property(e => e.School).HasMaxLength(30);

                entity.Property(e => e.Star).HasMaxLength(30);

                entity.Property(e => e.Subjects).HasMaxLength(30);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.Property(e => e.UserRole).HasMaxLength(15);

                entity.Property(e => e.WriteIp).HasMaxLength(30);
            });

            modelBuilder.Entity<MemberCondition>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("pk_MemberCondition");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.BloodInclude).HasMaxLength(15);

                entity.Property(e => e.CityInclude).HasMaxLength(120);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.JobTypeInclude).HasMaxLength(120);

                entity.Property(e => e.ReligionInclude).HasMaxLength(120);

                entity.Property(e => e.StarInclude).HasMaxLength(120);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.Property(e => e.WriteIp).HasMaxLength(30);

                entity.HasOne(d => d.User)
                    .WithOne(p => p.MemberCondition)
                    .HasForeignKey<MemberCondition>(d => d.UserId)
                    .HasConstraintName("Condition_Member");
            });

            modelBuilder.Entity<MemberDetail>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("pk_MemberDetail");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.Property(e => e.WriteIp).HasMaxLength(30);

                entity.HasOne(d => d.User)
                    .WithOne(p => p.MemberDetail)
                    .HasForeignKey<MemberDetail>(d => d.UserId)
                    .HasConstraintName("MemberDetail_Member");
            });

            modelBuilder.Entity<MemberPhoto>(entity =>
            {
                entity.HasIndex(e => new { e.UserId, e.Id })
                    .HasName("MemberPhoto_n1");

                entity.Property(e => e.AddedDate).HasColumnType("datetime");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Descriptions).HasMaxLength(250);

                entity.Property(e => e.PhotoUrl).HasMaxLength(250);

                entity.Property(e => e.PublicId).HasMaxLength(250);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.Property(e => e.WriteIp).HasMaxLength(30);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MemberPhoto)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("Photo_Member");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasIndex(e => new { e.RecipientId, e.SenderId })
                    .HasName("message_in2");

                entity.HasIndex(e => new { e.SenderId, e.RecipientId })
                    .HasName("message_in1");

                entity.Property(e => e.Contents)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.ReadDate).HasColumnType("datetime");

                entity.Property(e => e.SendDate).HasColumnType("datetime");

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.Property(e => e.WriteIp).HasMaxLength(30);

                entity.HasOne(d => d.Recipient)
                    .WithMany(p => p.MessageRecipient)
                    .HasForeignKey(d => d.RecipientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Message_Recipient");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.MessageSender)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Message_Sender");
            });

            modelBuilder.Entity<SysTransLog>(entity =>
            {
                entity.Property(e => e.TableName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.WriteTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<SysUserLog>(entity =>
            {
                entity.Property(e => e.Destination).HasMaxLength(255);

                entity.Property(e => e.IpAddress).HasMaxLength(30);

                entity.Property(e => e.Method).HasMaxLength(30);

                entity.Property(e => e.QueryString).HasMaxLength(255);

                entity.Property(e => e.Refer).HasMaxLength(255);

                entity.Property(e => e.RequestTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(30);
            });


        }
    }
}
