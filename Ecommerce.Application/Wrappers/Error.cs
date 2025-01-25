namespace Ecommerce.Application.Wrappers;

public class Error
{
    public int ErrorCode { get; set; }
    public string FieldName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public Error(int errorCode, string fieldName, string description)
    {
        ErrorCode = errorCode;
        FieldName = fieldName;
        Description = description;
    }
    public override bool Equals(object? obj)
    {
        if (obj is null || obj is not Error other)
            return false;
        return ErrorCode == other.ErrorCode
            && FieldName == other.FieldName
            && Description == other.Description;
    }
    public override int GetHashCode() => HashCode.Combine(ErrorCode, FieldName, Description);
}
