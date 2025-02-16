using lab1_nour_kassem.Models;

namespace lab1_nour_kassem.Services;

public class BookService
{
    private static List<Book> books =
    [
        new Book { book_id = 1, title = "The Prophet", author_id = 4, isbn = "9780992899523", published_year = 2019, release_date = new DateOnly(2019,12,1)},
        new Book { book_id = 2, title = "The Broken Wings", author_id = 4, isbn = "9781979253185", published_year = 2017, release_date = new DateOnly(2017,12,1)},
        new Book { book_id = 3, title = "The Book of Mirdad", author_id = 3, isbn = "9781842930380", published_year = 2002, release_date = new DateOnly(2002,12,1)},
        new Book { book_id = 4, title = "Elia, la passeuse d'ames", author_id = 2, isbn = "9782266264440", published_year = 2016, release_date = new DateOnly(2016,12,1)},
        new Book {book_id = 5, title = "The Da Vinci Code", author_id = 1, isbn = "9780307474278", published_year = 2009, release_date = new DateOnly(2009,12,1)},
        new Book {book_id = 6, title = "Some other book", author_id = 2, isbn = "1234567897891", published_year = 2019, release_date = new DateOnly(2019,5,3)},
        new Book {book_id = 7, title = "Another one", author_id = 3, isbn = "4561237894561", published_year = 2019, release_date = new DateOnly(2019, 6, 6)}
    ];

    public List<Book> GetBooksByYear(int year, bool descending = false)
    {
        var yearBooks = books.Where(book => book.published_year == year);
        if (descending)
        {
            yearBooks = yearBooks.OrderByDescending(book => book.published_year);
        }
        else
        {
            yearBooks = yearBooks.OrderBy(book => book.published_year);
        }
        return yearBooks.ToList();
    }

    public int GetBooksCount()
    {
        return books.Count;
    }
    
    //because we dont have a lot of data let's just say each page has at most 1 book

    public List<Book> GetBooksPaginated(int pageSize, int pageNumber)
    {
        return books.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
    }
    
}