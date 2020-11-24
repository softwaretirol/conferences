using System;
using System.Collections.Generic;

namespace E2E.EfCore.Entities
{
    public class Conference
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public ICollection<Session> Sessions { get; set; }
    }
}