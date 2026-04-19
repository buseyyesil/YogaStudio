namespace YogaStudio.Entity.Entities;

public class Lesson
{
    public int LessonId { get; set; }
    public string Name { get; set; }

    public int TrainerId { get; set; } //Bu ders hangi eğitmene ait (foreign key) Veri tabanında tabloları birbirine bağlayan foreign key veri tutarlılığını sağlar, alt ve üst tablolar arasındaki ilişkiyi kurar.
    public Trainer? Trainer { get; set; } //bire bir/bire çok ilişki, 1 ders 1 eğitmen , 1 dersin sadece 1 hocası olur

    public DateTime Date { get; set; }
    public int Capacity { get; set; }

    public List<Reservation> Reservations { get; set; } = new List<Reservation>(); //bire çok (liste) ,1 ders → ÇOK rezervasyon, 1 derse birden fazla kişi kayıt olur
    public string? Category { get; set; }
    public string? ZoomLink { get; set; }
}
