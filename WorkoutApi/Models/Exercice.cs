namespace WorkoutApi.Models
{
    public class Exercice
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Muscle { get; set; }
        public string? Difficulty { get; set; }
    }
    internal class KeyAttribute : Attribute
    {
    }
}