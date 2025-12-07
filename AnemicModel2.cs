/*public class Book
{
    public string Isbn { get; set; }
    public string Title { get; set; }
    public bool IsBorrowed { get; set; }
}

public class Member
{
    public string Name { get; set; }
    public int MaxBooks { get; set; }
    public int CurrentBorrowedCount { get; set; }
}

public class LibraryService
{
    public void BorrowBook(Book book, Member member)
    {
        if (book == null || member == null)
        {
            Console.WriteLine("Книга или читатель не указаны");
            return;
        }

        if (book.IsBorrowed)
        {
            Console.WriteLine($"Книга '{book.Title}' уже выдана");
            return;
        }

        if (member.CurrentBorrowedCount >= member.MaxBooks)
        {
            Console.WriteLine($"Читатель {member.Name} достиг лимита книг");
            return;
        }

        book.IsBorrowed = true;
        member.CurrentBorrowedCount++;

        Console.WriteLine($"Книга '{book.Title}' выдана читателю {member.Name}");
    }

    public void ReturnBook(Book book, Member member)
    {
        if (book == null || member == null)
        {
            Console.WriteLine("Книга или читатель не указаны");
            return;
        }

        if (!book.IsBorrowed)
        {
            Console.WriteLine($"Книга '{book.Title}' и так в библиотеке");
            return;
        }

        book.IsBorrowed = false;
        member.CurrentBorrowedCount--;

        Console.WriteLine($"Книга '{book.Title}' возвращена от {member.Name}");
    }
}

class Program
{
    static void Main()
    {
        var book = new Book { Isbn = "123", Title = "Clean Code", IsBorrowed = false };
        var member = new Member { Name = "Alice", MaxBooks = 3, CurrentBorrowedCount = 0 };

        var service = new LibraryService();

        service.BorrowBook(book, member);
        service.ReturnBook(book, member);
    }
}
*/

public class Book
{
    public string Isbn { get; }
    public string Title { get; }
    public bool IsBorrowed { get; private set; }
    public Book(string isbn, string title)
    {
        if (string.IsNullOrWhiteSpace(isbn))
            throw new ArgumentException("ISBN обязателен", nameof(isbn));

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Название обязательно", nameof(title));

        Isbn = isbn;
        Title = title;
    }
    public void Borrow()
    {
        if (IsBorrowed)
        {
            throw new InvalidOperationException($"Книга '{Title}' уже выдана");
        }
        IsBorrowed = true;
    }
    public void Return()
    {
        if (!IsBorrowed)
        {
            throw new InvalidOperationException($"Книга '{Title}' и так в библиотеке");
        }
        IsBorrowed = false;
    }
}
public class Member
{
    public string Name { get; set; }
    public int MaxBooks { get; set; }
    public int CurrentBorrowedCount { get; set; }
    public Member(string name, int maxBooks)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Имя обязательно", nameof(name));
        if (maxBooks <= 0)
            throw new ArgumentException("Максимальное число книг должно быть > 0", nameof(maxBooks));

        Name = name;
        MaxBooks = maxBooks;
        CurrentBorrowedCount = 0;
    }
    public void BorrowBook(Book book)
    {
        if (book == null)
            throw new ArgumentNullException(nameof(book));

        if (CurrentBorrowedCount >= MaxBooks)
            throw new InvalidOperationException(
                $"Читатель {Name} достиг лимита в {MaxBooks} книг");

        // используем поведение книги
        book.Borrow();
        CurrentBorrowedCount++;
    }

    public void ReturnBook(Book book)
    {
        if (book == null)
            throw new ArgumentNullException(nameof(book));

        if (!book.IsBorrowed)
            throw new InvalidOperationException($"Книга '{book.Title}' и так в библиотеке");

        book.Return();
        CurrentBorrowedCount--;
    }
}

