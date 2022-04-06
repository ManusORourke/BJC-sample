using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SQLiteFileBlob2
{
    public class BJCContext : DbContext
    {
        //public BJCContext(DbContextOptions<BJCContext> options) : base(options)
        //{

        //}

        private string dbpath = "test.db3";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = " + dbpath);
        }

        public DbSet<CVFile> CVFiles { get; set; } = null!;
    }
}
