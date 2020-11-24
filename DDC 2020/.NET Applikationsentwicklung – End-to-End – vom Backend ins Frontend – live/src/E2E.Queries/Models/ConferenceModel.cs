using System;

namespace E2E.Queries.Models
{
    // C# 9.0
    public record ConferenceModel(int Id, 
        string Name,
        DateTime Date);

    // record - class
    // Constructor - mit Parametern = Properties
    // Alle Properties sind readonly (Immutable Types)
    
    //public class ConferenceModel
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public DateTime Date { get; set; }
    //}
}