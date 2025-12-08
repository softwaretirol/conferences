using Microsoft.EntityFrameworkCore;

[Keyless]
public class VConference
{
    public string Name { get; set; }
    public string City { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

public class Conference
{
    // [Key]
    public Guid Id { get; set; }

    // [MaxLength(200)]
    public string Name { get; set; }

    // [MaxLength(200)]
    public string City { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }


    public List<ConferenceSession> ConferenceSessions { get; set; } = [];
}