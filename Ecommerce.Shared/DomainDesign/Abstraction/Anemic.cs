namespace Ecommerce.Shared.DomainDesign.Abstraction;

public abstract class Anemic
{
	public Guid Guid { get; protected set; }
	protected Anemic()
	{
		Guid = Guid.NewGuid();
	}
	private long _id;
	private int? _requestedHashedCode;
	public virtual long Id
	{
		get { return _id; }
		protected set { _id = value; }
	}

	public bool IsTransient() => this.Id == default(Int32);

	public override bool Equals(object? obj)
	{
		if (obj is not Anemic || obj is null)
			return false;
		if (object.ReferenceEquals(this, obj))
			return true;
		if (obj.GetType() != this.GetType())
			return false;
		Entity entity = (Entity)obj;
		if (this.IsTransient() || entity.IsTransient())
			return false;
		else
			return this.Id == entity.Id;
	}

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

	public static bool operator ==(Anemic left, Anemic right)
	{
		if (left is null || right is null)
			return false;
		if (left is null && right is null)
			return true;
		return left!.Equals(right);
	}

	public static bool operator !=(Anemic left, Anemic right) => !(left == right);

	public class AuditableAnemic : Anemic
	{
		public int AddedBy { get; set; }
		public DateOnly AddedOn { get; set; }
		public int? UpdatedBy { get; set; }
		public DateTime? UpdatedOn { get; set; }

		public virtual void MarkEdited(int updatedBy, DateTime updatedOn)
		{
			UpdatedBy = updatedBy;
			UpdatedOn = updatedOn;
		}
	}
}