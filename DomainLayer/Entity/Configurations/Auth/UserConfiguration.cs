using ApplicationLayer.DTOs.Auth;
using EVO.DomainLayer.Entity.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EVO.DomainLayer.Entity.Configurations.Auth
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.RecordId);
            builder.Property(x => x.RecordId)
    .           ValueGeneratedOnAdd();

            builder.Property(x => x.UserId)
                .IsRequired()
                .HasMaxLength(36);

            builder.Property(x => x.EmailAddress)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Password)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.RoleUser)
                .IsRequired()
                .HasMaxLength(2);


            builder.HasData(
                new User { RecordId = 1, UserId = Guid.NewGuid().ToString(), EmailAddress = "a@a.a", Password = "a", RoleUser = EnumRoleUser.None }
                );
        }
    }
}
