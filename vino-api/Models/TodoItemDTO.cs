using System;
namespace vino_api.Models
{
    public class TodoItemDTO
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
