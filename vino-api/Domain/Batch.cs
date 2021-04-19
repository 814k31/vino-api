using System;

namespace vino_api.Domain
{
    public class Batch
    {
        public long Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string Name { get; set; }
        public string Secret { get; set; }
    }
}
