using System;
using System.Collections.Generic;
using EmergereJobCareerWebApi.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EmergereJobCareerWebApi.Entities
{
    public partial class emergerecareerdbContext : IdentityDbContext<ApplicationUser>
    {
        public emergerecareerdbContext()
        {
        }

        public emergerecareerdbContext(DbContextOptions<emergerecareerdbContext> options)
            : base(options)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
// optionsBuilder.UseMySql("server=localhost;uid=root;pwd=password-1;database=emergerecareerdb", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.31-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                base.OnModelCreating(modelBuilder);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

            
    }
}
