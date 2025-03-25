namespace Ecommerce.Infrastructure.EntityConfiguration.Enumerations;

public class EventTypeEntityConfiguration : IEntityTypeConfiguration<EventType>
{
    public void Configure(EntityTypeBuilder<EventType> builder)
    {
        builder.ToTable(
            nameof(EventType).Humanize().Pascalize().Pluralize(),
            EcommerceDbContext.ECOMMERCE_SCHEMA
        );
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedNever().IsRequired();
        builder.Property(e => e.Name).HasMaxLength(200).IsRequired();
    }
}
