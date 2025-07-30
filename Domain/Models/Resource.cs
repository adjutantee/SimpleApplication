using Domain.Enums;

namespace Domain.Models
{
    public class Resource
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public State State { get; set; } = State.Active;
    }
}
