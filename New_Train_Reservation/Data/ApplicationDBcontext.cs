using System.Collections.Generic;
using New_Train_Reservation.Models;
using Microsoft.EntityFrameworkCore;

namespace New_Train_Reservation.Data
{
    public class ApplicationDBcontext: DbContext
    {
        public ApplicationDBcontext(DbContextOptions<ApplicationDBcontext> options) : base(options)
        { }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Trains> Trains { get; set; }
        public DbSet<Admin_Tickets> Admin_Tickets { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<User_Tickets> User_Tickets { get; set; }
        public DbSet<Suggestions_Complaints> Suggestions_Complaints { get; set; }
    }
}
