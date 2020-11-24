using System;

namespace E2E.EfCore.Entities
{
    public class Session
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int SpeakerId { get; set; }
        public int ConferenceId { get; set; }
        public Speaker Speaker { get; set; }
        public Conference Conference { get; set; }
    }
}