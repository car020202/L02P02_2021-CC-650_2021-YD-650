using Microsoft.EntityFrameworkCore;
namespace L02P02_2021_CC_650_2021_YD_650.Models
{
    public class libreriaDbContext : DbContext
    {
        public libreriaDbContext(DbContextOptions options): base (options) 
        {

        }
    }
}
