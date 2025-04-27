namespace Camping.Data.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public int UserId { get; set; }
        public int CampSiteId { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
