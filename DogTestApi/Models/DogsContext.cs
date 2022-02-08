﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogTestApi.Models
{
    public class DogsContext:DbContext
    {
        public DbSet<Dog> Dogs { get; set; }
        public DogsContext(DbContextOptions<DogsContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
