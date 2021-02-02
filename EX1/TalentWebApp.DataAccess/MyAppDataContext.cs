using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TalentWebApp.DataModels;

namespace TalentWebApp.DataAccess
{
    // https://docs.microsoft.com/en-us/ef/core/dbcontext-configuration/
    public class MyAppDataContext : DbContext
    {
        // for each table
        public DbSet<ProductModel> Products { get; set; }

        public MyAppDataContext(DbContextOptions<MyAppDataContext> options)
        : base(options)
        {
        }

    }
}
