namespace YogaStudio.UI.Models
{
    public class ResultLessonDto
    {
        public int LessonId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime Date { get; set; }
        public int Capacity { get; set; }
        public int TrainerId { get; set; }
        public string? TrainerName { get; set; }
        public string? TrainerTitle { get; set; }
    }
}