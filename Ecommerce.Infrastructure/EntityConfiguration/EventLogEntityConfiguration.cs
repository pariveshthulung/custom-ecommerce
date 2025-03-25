namespace Ecommerce.Infrastructure.EntityConfiguration;

public class EventLogEntityConfiguration : IEntityTypeConfiguration<EventLog>
{
    public void Configure(EntityTypeBuilder<EventLog> builder)
    {
        builder.ToTable(
            nameof(EventLog).Humanize().Pluralize().Pascalize(),
            EcommerceDbContext.ECOMMERCE_SCHEMA
        );
        builder.Property(x => x.Description).IsRequired().HasMaxLength(255);
        builder
            .Property<int>("EventTypeId") // defining shadow property
            .IsRequired();
        builder
            .HasOne(x => x.EventType)
            .WithMany()
            .HasForeignKey("EventTypeId")
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne<AppUser>()
            .WithMany()
            .HasForeignKey(x => x.AppUserId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();
    }
}
