namespace Transport.Domain.Entities;

public class TransportType(int id, string name)
{
    public int Id { get; set; } = id;

    public string Name { get; set; } = name;

    //EF не работает с array, так как у array фиксированое количество элементов, а EF нужно добавлять элементы в коллекцию 
    public ICollection<Transport> Transports { get; set; } = [];
}
