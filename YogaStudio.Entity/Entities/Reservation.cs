namespace YogaStudio.Entity.Entities;

public class Reservation
{
    public int ReservationId { get; set; }

    public int UserId { get; set; }
    public User? User { get; set; }

    public int LessonId { get; set; }
    public Lesson? Lesson { get; set; }
}