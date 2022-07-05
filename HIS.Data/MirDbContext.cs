using HIS.Data.DictionariesModel;
using Microsoft.EntityFrameworkCore;

namespace HIS.Data
{
    public class MirDbContext : DbContext
    {
        // 通过这个构造函数去构造参数
        public MirDbContext(DbContextOptions<MirDbContext> options) : base(options) { }

        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
