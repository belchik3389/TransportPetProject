namespace Transport.Domain.Interfaces;

public interface IAuditableDateTime
{
    DateTime CreatedDate { get; set; }

    DateTime? UpdatedDate { get; set; }
}