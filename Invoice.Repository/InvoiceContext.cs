using Invoice.Entities;
using Microsoft.EntityFrameworkCore;

namespace Invoice.Repository;

public class InvoiceContext : DbContext
{
	public InvoiceContext(DbContextOptions<InvoiceContext> options) : base (options)
	{

	}

	public DbSet<Issuer> Issuers { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Issuer>().HasData(new Issuer
		{
			Id = Guid.NewGuid(),
			IssuerId = 20606022779,
			IssuerName = "SWIFTLINE SAC",
			IssuerType = "6",
			GeoCode = "220901",
			EstablishmentCode = "0000",
			Department = "SAN MARTIN",
			Province = "SAN MARTIN",
			District = "TARAPOTO",
			Address = "PSJE. LIMATAMBO 121"
        });
	}
}