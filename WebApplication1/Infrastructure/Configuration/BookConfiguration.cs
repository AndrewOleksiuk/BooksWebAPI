using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Book");
            builder.Property(e => e.Id)
                .HasColumnName("id");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("name")
                .HasMaxLength(150);

            builder.Property(e => e.Publisher)
                .HasColumnName("publisher")
                .HasMaxLength(150);

            builder.Property(e => e.Notice)
                .HasColumnName("notice")
                .HasMaxLength(250);

            builder.Property(a => a.IsRead)
                .HasColumnName("isread")
                .HasColumnType("bit");
        }
    }
}
