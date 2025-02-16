using System.Runtime.InteropServices.JavaScript;

namespace lab1_nour_kassem.Models;

public class Book
{
    public long book_id  { get; set; }
    public string title  { get; set; }
    public long author_id  { get; set; }
    public string isbn  { get; set; }
    public int published_year { get; set; }
    public DateOnly release_date { get; set; }
}