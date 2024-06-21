using EVO.DomainLayer.Entity.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EVO.DomainLayer.Entity.Configurations.Auth
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken_>
    {
        public void Configure(EntityTypeBuilder<RefreshToken_> builder)
        {
            builder.HasKey(x => x.RecordId);

            builder.Property(x => x.RecordId)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.UserId)
                .IsRequired()
                .HasMaxLength(36);

            builder.Property(x => x.TokenId)
                .IsRequired()
                .HasMaxLength(40);

            builder.Property(x => x.RefreshToken)
                .IsRequired()
                .HasMaxLength(60);


            builder.Property(x => x.ExpirationTime)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.IsActive)
                .IsRequired()
                .HasMaxLength(1);
        }
    }
}
