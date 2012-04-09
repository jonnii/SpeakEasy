using System;

namespace Resticle.Samples.Basecamp.Models
{
    public class Project
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
