public class ConferenceSession
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    // Navigation Properties

    public Guid ConferenceId { get; set; }
    public Conference Conference { get; set; }
}