namespace DevSession.EfCore;

public class DdcSpeaker
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public List<DdcSession> Sessions { get; set; }
}