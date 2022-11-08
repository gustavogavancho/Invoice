using Invoice.Entities.Models;
using Invoice.Repository.Configurarion;
using Microsoft.EntityFrameworkCore;

namespace Invoice.Repository;

public class InvoiceContext : DbContext
{
	public InvoiceContext(DbContextOptions<InvoiceContext> options) : base (options)
	{

	}

	public DbSet<Issuer> Issuer { get; set; }
	public DbSet<Entities.Models.Invoice> Invoice { get; set; }
	public DbSet<Ticket> Ticket { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.ApplyConfiguration(new IssuerConfiguration());
	}
}