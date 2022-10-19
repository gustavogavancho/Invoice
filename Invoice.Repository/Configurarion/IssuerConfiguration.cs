using Invoice.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invoice.Repository.Configurarion;

public class IssuerConfiguration : IEntityTypeConfiguration<Issuer>
{
    public void Configure(EntityTypeBuilder<Issuer> builder)
    {
        builder.HasData(
            new Issuer
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