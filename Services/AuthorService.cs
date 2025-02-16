using lab1_nour_kassem.Models;

namespace lab1_nour_kassem.Services;

public class AuthorService
{
    private static List<Author> authors =
    [
        new Author { author_id = 1, name = "Dan Brown", birth_date = new DateOnly(1964, 06, 22), country = "USA" },
        new Author
        {
            author_id = 2, name = "Marie Vareille", birth_date = new DateOnly(1985, 02, 27), country = "France"
        },

        new Author
        {
            author_id = 3, name = "Mikhail Naimy", birth_date = new DateOnly(1889, 10, 17), country = "Lebanon"
        },

        new Author
        {
            author_id = 4, name = "Jebran Khalil Jebran", birth_date = new DateOnly(1883, 01, 06), country = "Lebanon"
        },
        new Author { author_id = 5, name = "Stephen King", birth_date = new DateOnly(1964, 09, 21), country = "USA" },
        new Author { author_id = 6, name = "Guillaume Musso", birth_date = new DateOnly(1985, 06, 30), country = "France" },
        new Author { author_id = 7, name = "Elia Abu Madi", birth_date = new DateOnly(1889, 05, 05), country = "Lebanon" },
        new Author { author_id = 8, name = "Ameen Rihani", birth_date = new DateOnly(1883, 11, 24), country = "Lebanon" },
        new Author { author_id = 9, name = "Terry Pratchett", birth_date = new DateOnly(1948, 04, 28), country = "UK" },
        new Author { author_id = 10, name = "Ian McEwan", birth_date = new DateOnly(1948, 06, 21), country = "UK" },
        new Author { author_id = 11, name = "Haruki Murakami", birth_date = new DateOnly(1964, 01, 12), country = "Japan" },
        new Author { author_id = 12, name = "Carlos Ruiz Zafón", birth_date = new DateOnly(1985, 09, 25), country = "Spain" },
        new Author { author_id = 13, name = "Knut Hamsun", birth_date = new DateOnly(1889, 08, 04), country = "Norway" }
    ];

    public List<IGrouping<int, Author>> GroupAuthorsByYear()
    {
        var groupedAuthors = authors.GroupBy(a => a.birth_date.Year).ToList();
        return groupedAuthors;
    }

    public List<IGrouping<(string country, int Year), Author>> GroupAuthorsByYearAndCountry()
    {
        var groupedAuthors = authors.GroupBy(a => (a.country, a.birth_date.Year)).ToList();
        return groupedAuthors;
    }
}