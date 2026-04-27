namespace Transport.Domain.Entities;

public class TransportType(int id, string name)
{
    public int Id { get; set; } = id;

    public string Name { get; set; } = name;
}
