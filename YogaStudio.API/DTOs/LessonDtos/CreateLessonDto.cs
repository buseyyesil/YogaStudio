namespace YogaStudio.API.DTOs.LessonDtos
{
    public class CreateLessonDto
    {
        public string Name { get; set; } = null!;
        public int TrainerId { get; set; }
        public DateTime Date { get; set; }  
        public int Capacity { get; set; }   
    }
}
