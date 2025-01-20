namespace Ecommerce.Shared.DomainDesign.Abstraction;

public abstract class Entity
{
    public Guid Guid { get; protected set; }
    public bool IsActive { get; protected set; }
    public bool IsDeleted { get; protected set; }

    protected Entity()
    {
        Guid = Guid.NewGuid();
    }
    private long _id;
    public long Id
    {
        get { return _id; }
        protected set { _id = value; }
    }
    public bool IsTransient() => this.Id == default(Int32);
    private List<INotification> _domainEvent = [];
    public IReadOnlyCollection<INotification> DomainEvent => _domainEvent.AsReadOnly();
    public void AddDomainEvent(INotification domainEvent)
    {
        _domainEvent ??= [];
        _domainEvent.Add(domainEvent);
    }
    public void RemoveDomainEvent(INotification domainEvent) => _domainEvent.Remove(domainEvent);
    public void ClearDomainEvent() => _domainEvent.Clear();
    public override bool Equals(object? obj)
    {
        if (obj is null || obj is not Entity)
            return false;
        if (object.ReferenceEquals(this, obj))
            return true;
        if (this.GetType() != obj.GetType())
            return false;
        Entity entity = (Entity)obj;
        if (this.IsTransient() || entity.IsTransient())
            return false;
        else
            return this.Id == entity.Id;
    }

    private int? _requestedHashedCode;
    public override int GetHashCode()
    {
        if (!this.IsTransient())
        {
            if (!_requestedHashedCode.HasValue)
                _requestedHashedCode = Id.GetHashCode() ^ 31;
            return _requestedHashedCode.Value;
        }
        else
            return base.GetHashCode();
    }
    public static bool operator ==(Entity left, Entity right)
    {
        if (left is null || right is null)
            return false;
        if (left is null && right is null)
            return true;
        return Equals(left, right);
    }

    public static bool operator !=(Entity left, Entity right) => !(left == right);
    public abstract class AuditableEntity : Entity
    {
        public int AddedBy { get; set; }
        public DateTime AddedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual void MarkEdited(int updatedBy, DateTime updatedOn)
        {
            UpdatedBy = updatedBy;
            UpdatedOn = updatedOn;
        }
    }
    public abstract class VersionedEntity : AuditableEntity
    {
        public byte[]? TimeStamp { get; private set; }
    }
}
