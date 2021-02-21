using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TeaTime.Entities;

namespace TeaTime.Data
{
    public class TeaTimeContext : DbContext
    {
        public TeaTimeContext(DbContextOptions<TeaTimeContext> options) : base(options)

        {

        }
        public DbSet<NationalPark> NationalParks { get; set; }

         



    }
}
