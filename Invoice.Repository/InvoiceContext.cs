using Invoice.Entities;
using Microsoft.EntityFrameworkCore;

namespace Invoice.Repository;

public class InvoiceContext : DbContext
{
	public InvoiceContext(DbContextOptions<InvoiceContext> options) : base (options)
	{

	}

	public DbSet<Sender> Senders { get; set; }
}