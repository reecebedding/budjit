using budjit.core.models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Reflection;

namespace budjit.core.data.SQLite
{
    public class BudjitContext : DbContext
    {
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }

        public BudjitContext(DbContextOptions<BudjitContext> options) : base(options) { }
    }
}
