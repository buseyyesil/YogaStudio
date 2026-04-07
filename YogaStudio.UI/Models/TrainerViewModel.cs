namespace YogaStudio.UI.Models
{
    public class TrainerViewModel
    {
        public int TrainerId { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
    }
}