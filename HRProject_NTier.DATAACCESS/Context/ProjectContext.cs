using HRProject_NTier.CORE.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRProject_NTier.DATAACCESS.Context
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {

        }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Personnel> Personnels { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Spending> Spendings { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Calender> Calenders { get; set; }
        public DbSet<CalenderTopic> CalenderTopics { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        //public DbSet<PersonnelPermission> PersonnelPermissions { get; set; }
        public DbSet<Package> Packages { get; set; }

        public DbSet<ManagerComment> ManagerComments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //We give primary keys all of them.
            modelBuilder.Entity<Manager>().HasKey(m => m.ID);
            modelBuilder.Entity<Personnel>().HasKey(pers => pers.ID);
            modelBuilder.Entity<Company>().HasKey(com => com.ID);
            //modelBuilder.Entity<Company>().HasKey(comp => comp.ManagerID);
            modelBuilder.Entity<Spending>().HasKey(s => s.ID);
            modelBuilder.Entity<Payment>().HasKey(pay => pay.ID);
            modelBuilder.Entity<Calender>().HasKey(cal => cal.ID);
            modelBuilder.Entity<Topic>().HasKey(t => t.ID);
            modelBuilder.Entity<Permission>().HasKey(perm => perm.ID);
            modelBuilder.Entity<Package>().HasKey(pack => pack.ID);
            modelBuilder.Entity<ManagerComment>().HasKey(mc => mc.ID);

            //CalenderTopic Composite Key Define
            modelBuilder.Entity<CalenderTopic>().HasKey(ct => new
            {
                ct.CalenderID,
                ct.TopicID
            });
            //PersonnelPermission Composite Key Define
            //modelBuilder.Entity<PersonnelPermission>().HasKey(pp => new
            //{
            //    pp.PersonnelID,
            //    pp.PermissionID
            //});
            modelBuilder.Entity<Manager>().
                HasOne(x => x.Company).
                WithOne(x => x.Manager).
                HasForeignKey<Company>(x => x.ManagerID);

            //
            modelBuilder.Entity<Manager>()
               .HasOne(x => x.ManagerComment)
               .WithOne(x => x.Manager)
               .HasForeignKey<ManagerComment>(x => x.ManagerID);
            //

            //
            modelBuilder.Entity<CalenderTopic>().
                HasOne(ct => ct.Calender).
                WithMany(cal => cal.CalenderTopics).
                HasForeignKey(ct => ct.CalenderID);

            modelBuilder.Entity<CalenderTopic>().
                HasOne(ct => ct.Topic).
                WithMany(cal => cal.CalenderTopics).
                HasForeignKey(ct => ct.TopicID);


            //
            //modelBuilder.Entity<PersonnelPermission>().
            //    HasOne(pp => pp.Personnel).
            //    WithMany(pers => pers.PersonnelPermissions).
            //    HasForeignKey(pp => pp.PersonnelID);

            //modelBuilder.Entity<PersonnelPermission>().
            //   HasOne(pp => pp.Permission).
            //   WithMany(pers => pers.PersonnelPermissions).
            //   HasForeignKey(pp => pp.PermissionID);
            base.OnModelCreating(modelBuilder); 
        }

    }
}
