namespace Drivers.Service.Domain.Drivers;

public class Driver
{
    public Guid Id { get; private set; }
    public string FullName { get; private set; }
    public bool IsAvailable { get; private set; }

    private Driver() { }

    public static Driver Create(string fullName)
    {
        return new Driver
        {
            Id = Guid.NewGuid(),
            FullName = fullName,
            IsAvailable = true
        };
    }

    public void MarkUnavailable()
    {
        IsAvailable = false;
    }

    public void MarkAvailable()
    {
        IsAvailable = true;
    }
}
