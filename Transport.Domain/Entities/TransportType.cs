namespace Transport.Domain.Entities;

public class TransportType(int id, string name)
{
    public int Id { get; set; } = id; // TODO тоже не нравится, но ладно)

    public string Name { get; set; } = name;
}
