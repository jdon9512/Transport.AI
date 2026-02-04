namespace Fleet.Service.Domain.Vehicles;

public class Vehicle
{
    public Guid Id { get; private set; }
    public string Plate { get; private set; }
    public decimal CapacityKg { get; private set; }
    public VehicleStatus Status { get; private set; }

    private Vehicle() { }

    public static Vehicle Create(string plate, decimal capacity)
    {
        return new Vehicle
        {
            Id = Guid.NewGuid(),
            Plate = plate,
            CapacityKg = capacity,
            Status = VehicleStatus.Available
        };
    }

    public void Assign() => Status = VehicleStatus.Assigned;
}

