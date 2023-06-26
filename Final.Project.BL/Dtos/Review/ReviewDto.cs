namespace Final.Project.BL;

public class ReviewDto
{
    public string Comment { get; set; }=string.Empty;
    public DateTime CreationDate { get; set; } = DateTime.Now;
    public int Rating { get; set; }
}
