using System;
namespace vino_api.Models
{
    public class BatchDTO
    {
        public long Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string Name { get; set; }
    }
}
