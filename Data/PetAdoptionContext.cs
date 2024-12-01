
using Microsoft.EntityFrameworkCore;
using PetAdoptionAPI.Models;

namespace PetAdoptionAPI.Data
{
    public class PetAdoptionContext : DbContext
    {
        public PetAdoptionContext(DbContextOptions<PetAdoptionContext> options) :base (options) { }

        public DbSet<Pets> Pets { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Adoptions> Adoptions { get; set; }
    }
}




