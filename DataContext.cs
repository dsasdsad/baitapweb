using DeThiThu2023.Models;
using Microsoft.EntityFrameworkCore;

namespace DeThiThu2023.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<LoaiHang> LoaiHang { get; set;}
        public DbSet<HangHoa> HangHoa { get; set; }
        public DbSet<tblAccount> tblAccount { get; set; }

    }
}
