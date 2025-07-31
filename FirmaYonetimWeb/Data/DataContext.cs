using Microsoft.AspNetCore.Identity;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using FirmaYonetimWeb.Enum;
using FirmaYonetimWeb.Helper;
using FirmaYonetimWeb.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FirmaYonetimWeb.Data
{
    public class DataContext:IdentityDbContext<AppUser, AppRole, int>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new ApplicationUserEntityConfiguraiton());
        }

        public override int SaveChanges()
        {
            BeforeSaveChanges();
            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            BeforeSaveChanges();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void BeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is FirmaYonetimWeb.Models.Audit || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;
                var auditEntry = new AuditEntry(entry);
                auditEntry.TableName = entry.Entity.GetType().Name;
                auditEntries.Add(auditEntry);
                foreach (var property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.AuditType = AuditType.Create;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                        
                            break;
                        case EntityState.Deleted:
                            auditEntry.AuditType = AuditType.Delete;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                   
                            break;
                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.ChangedColumns.Add(propertyName);
                                auditEntry.AuditType = AuditType.Update;
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }
                            break;
                    }
                }
            }
            foreach (var auditEntry in auditEntries)
            {
                AuditLogs.Add(auditEntry.ToAudit());
            }
        }


        public DbSet<AppUser> Kullanicilar { get; set; }
        public DbSet<Belediye> Belediyeler { get; set; }
        public  DbSet<KaynakTuru> KaynakTurleri { get; set; }
        public DbSet<VPNAltTuru> VPNAltTurleri { get; set; }
        public DbSet<BelediyeKaynak> BelediyeKaynakları {  get; set; }
        public DbSet<KaynakGiris> KaynakGirisleri { get; set; }
        public DbSet<VPN> VPNs { get; set; }
        public DbSet<RDP> RDPs { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Any> Anys { get; set; }
        public DbSet<PostrgreSQL> PostrgreSQLs { get; set; }

        public DbSet<Not> Notlar { get; set; }

        public DbSet<Gorev> Gorevler { get; set; }

        public DbSet<FirmaYonetimWeb.Models.Audit> AuditLogs { get; set; }

        public DbSet<GeoServer> GeoServers { get; set; }

        public DbSet<BelediyePersonel> BelediyePersonelleri { get; set; }

    }
    public class ApplicationUserEntityConfiguraiton : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(x => x.FirstName).HasMaxLength(100);
            builder.Property(x => x.LastName).HasMaxLength(100);
        }
    }
}
