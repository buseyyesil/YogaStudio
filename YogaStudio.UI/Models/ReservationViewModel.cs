namespace YogaStudio.UI.Models
{
    public class ReservationViewModel
    {
        public int ReservationId { get; set; }
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public int LessonId { get; set; }
        public string? LessonName { get; set; }
        public string? TrainerName { get; set; }
        public DateTime? LessonDate { get; set; }
    }

    public class CreateReservationViewModel
    {
        public int UserId { get; set; }
        public int LessonId { get; set; }
    }
}