﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CECS475LabAssignment8.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CECS475LabAssignment8.DAL
{
    public class IdentityDataContext : IdentityDbContext<IdentityUser>
    {
        

        public IdentityDataContext(DbContextOptions<IdentityDataContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}