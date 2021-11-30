namespace DevSession.EfCore;

public class DdcSession
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Time { get; set; }
    public string Room { get; set; }

    public int SpeakerId { get; set; }
    public DdcSpeaker Speaker { get; set; }
}