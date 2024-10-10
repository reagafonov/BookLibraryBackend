using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFramework.Configuration;

/// <summary>
/// Автоматически подключаемая конфигурация для таблицы авторов
/// </summary>
public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.ToTable("Author", NpgSqlConstants.DefaultSchema);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn().HasColumnName("Id");
        builder.Property(x => x.FirstName)
            .HasMaxLength(DomainConstraints.AuthorFirstNameMaxLength)
            .IsRequired(DomainConstraints.AuthorFirstNameIsRequired)
            .HasColumnName("FirstName");
        builder.Property(x => x.LastName)
            .HasMaxLength(DomainConstraints.AuthorLastNameMaxLength)
            .IsRequired(DomainConstraints.AuthorFirstNameIsRequired)
            .HasColumnName("LastName");
        builder.Property(x => x.Deleted)
            .IsRequired()
            .HasDefaultValue(false)
            .HasColumnName("Deleted");

        builder.HasQueryFilter(author => !author.Deleted);

        builder.HasIndex(x => new { x.FirstName, x.LastName })
            .IsUnique();
    }
}