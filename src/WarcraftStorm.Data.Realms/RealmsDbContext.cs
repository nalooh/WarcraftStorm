using Microsoft.EntityFrameworkCore;

namespace WarcraftStorm.Data.Realms;

public class RealmsDbContext(DbContextOptions<RealmsDbContext> options) : DbContext(options)
{
   public DbSet<Realm> Realms { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountSession> AccountSessions { get; set; }
    
}
