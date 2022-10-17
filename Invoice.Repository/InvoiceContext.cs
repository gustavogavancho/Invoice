using Invoice.Entities;
using Microsoft.EntityFrameworkCore;

namespace Invoice.Repository;

public class InvoiceContext : DbContext
{
	public InvoiceContext(DbContextOptions<InvoiceContext> options) : base (options)
	{

	}

	public DbSet<Sender> Senders { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Sender>().HasData(new Sender
		{
			Id = Guid.NewGuid(),
			SenderId = 20606022779,
			SenderName = "SWIFTLINE SAC",
			SenderType = "6",
			GeoCode = "220901",
			EstablishmentCode = "0000",
			Department = "SAN MARTIN",
			Province = "SAN MARTIN",
			District = "TARAPOTO",
			Address = "PSJE. LIMATAMBO 121"
        });
	}
}