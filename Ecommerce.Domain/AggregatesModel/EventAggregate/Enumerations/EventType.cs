namespace Ecommerce.Domain.AggregatesModel.EventAggregate.Enumerations;

public class EventType : Enumeration
{
    public EventType(int id, string name)
        : base(id, name) { }

    public static readonly EventType AppUserRegistered =
        new(1, nameof(AppUserRegistered).Humanize().Titleize());
    public static readonly EventType StoreCreated =
        new(2, nameof(StoreCreated).Humanize().Titleize());
    public static readonly EventType StoreUpdated =
        new(3, nameof(StoreUpdated).Humanize().Titleize());
    public static readonly EventType CartCreated =
        new(4, nameof(CartCreated).Humanize().Titleize());
    public static readonly EventType ProductAdded =
        new(5, nameof(ProductAdded).Humanize().Titleize());
    public static readonly EventType ProductUpdated =
        new(6, nameof(ProductUpdated).Humanize().Titleize());
    public static readonly EventType ProductDeleted =
        new(7, nameof(ProductDeleted).Humanize().Titleize());
}
