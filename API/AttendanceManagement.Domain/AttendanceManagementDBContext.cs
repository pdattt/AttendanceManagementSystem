using AttendanceManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Domain
{
    public class AttendanceManagementDBContext : DbContext
    {
        public AttendanceManagementDBContext(DbContextOptions<AttendanceManagementDBContext> options) : base(options)
        {

        }

        public DbSet<Attendee> Attendees { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendee>()
                .HasMany<Class>(cl => cl.Classes)
                .WithMany(at => at.Attendees);

            modelBuilder.Entity<Attendee>()
                .HasMany<Event>(cl => cl.Events)
                .WithMany(at => at.Attendees);
        }
    }
}
