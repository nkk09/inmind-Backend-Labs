namespace lab1_nour_kassem.Models;

public class Author
{
    public long author_id  { get; set; }
    public string name  { get; set; }
    public DateOnly birth_date { get; set; }
    public string country { get; set; }
}