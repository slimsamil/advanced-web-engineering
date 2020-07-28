using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication7.Models
{
   
        public class MyContext : DbContext
        {
            public MyContext(DbContextOptions<MyContext> options) : base(options)
            { }

            public DbSet<Thesis> Thesen { get; set; }

            public DbSet<Supervisor> Betreuer { get; set; }

            public DbSet<Programme> Programme { get; set; }

        }


    }

