using System.Collections.Generic;

namespace E2E.EfCore.Entities
{
    public class Speaker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public ICollection<Session> Sessions { get; set; }
    }
}