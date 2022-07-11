using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Enumerators;

namespace SocialMedia.Infrastructure.Data.Configurations
{
    public class SecurityConfiguration : IEntityTypeConfiguration<Security>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Security> builder)
        {
            builder.ToTable("Seguridad");
                 builder.HasKey(e => e.Id);
                builder.Property(e => e.Id)
                .HasColumnName("IdPublicacion");


                builder.Property(e => e.User)
                .IsRequired()
                .HasColumnName("Usuario")
                .HasMaxLength(50)
                .IsUnicode(false);

                builder.Property(e => e.UserName)
                .IsRequired()
                .HasColumnName("NombreUsuario")
                .HasMaxLength(100)
                .IsUnicode(false);

                builder.Property(e => e.Password)
                .IsRequired()
                .HasColumnName("Contrasena")
                .HasMaxLength(200)
                .IsUnicode(false);

                builder.Property(e => e.Role)
                .IsRequired()
                .HasColumnName("Rol")
                .HasMaxLength(15)
                .HasConversion(x => x.ToString(), x => (RoleType)Enum.Parse(typeof(RoleType), x));

        }
    }
}