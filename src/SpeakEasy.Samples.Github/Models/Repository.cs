using System;

namespace SpeakEasy.Samples.Github.Models
{
    public class Repository
    {
        public string Url { get; set; }

        public bool Fork { get; set; }

        public bool Private { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime PushedAt { get; set; }
    }
}