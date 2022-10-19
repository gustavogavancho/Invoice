using Invoice.Entities.Models;
using Invoice.Repository.Configurarion;
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

		modelBuilder.ApplyConfiguration(new IssuerConfiguration());
	}
}