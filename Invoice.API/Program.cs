using Invoice.Contracts;
using Invoice.Repository;
using Invoice.Service;
using Invoice.Service.Contracts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IIssuerService, IssuerService>();
builder.Services.AddScoped<ISerializeXmlService, SerializeXmlService>();
builder.Services.AddScoped<ISignerService, SignerService>();
builder.Services.AddScoped<IZipperService, ZipperService>();

builder.Services.AddScoped<IIssuerRepository, IssuerRepository>();

builder.Services.AddDbContext<InvoiceContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
