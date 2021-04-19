using System;
namespace vino_api.Domain
{
    public class BatchDTO
    {
        public long Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string Name { get; set; }
    }
}
