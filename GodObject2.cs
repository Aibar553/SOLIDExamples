/*class Message
{
    private List<string> _message = new List<string>();
    public void SendMessage(string user, string text)
    {
        string full = $"{user}:{text}";
        _messages.Add(full);
        Console.WriteLine(full);
        Console.WriteLine($"[LOG] {DateTime.Now}: message sent");
    }
}
class Print
{
    public void PrintHistory()
    {
        Console.WriteLine("=== CHAT HISTORY ===");
        foreach (var msg in _messages)
        {
            Console.WriteLine(msg);
        }
    }
}
class File
{
    public void SaveToFile(string path)
    {
        System.IO.File.WriteAllLines(path, _messages);
        Console.WriteLine($"Chat saved to {path}");
    }
}*/
using System;
using System.Collections.Generic;
using System.IO;

class ChatHistory
{
    private readonly List<string> _messages = new List<string>();

    public void Add(string message)
    {
        _messages.Add(message);
    }

    public IReadOnlyList<string> Messages => _messages;
}

class ChatSender
{
    private readonly ChatHistory _history;

    public ChatSender(ChatHistory history)
    {
        _history = history;
    }

    public void SendMessage(string user, string text)
    {
        string full = $"{user}: {text}";
        _history.Add(full);

        Console.WriteLine(full);
        Console.WriteLine($"[LOG] {DateTime.Now}: message sent");
    }
}

class ChatPrinter
{
    private readonly ChatHistory _history;

    public ChatPrinter(ChatHistory history)
    {
        _history = history;
    }

    public void PrintHistory()
    {
        Console.WriteLine("=== CHAT HISTORY ===");
        foreach (var msg in _history.Messages)
        {
            Console.WriteLine(msg);
        }
    }
}

class ChatStorage
{
    private readonly ChatHistory _history;

    public ChatStorage(ChatHistory history)
    {
        _history = history;
    }

    public void SaveToFile(string path)
    {
        File.WriteAllLines(path, _history.Messages);
        Console.WriteLine($"Chat saved to {path}");
    }
}



/*class Add
{
    private List<string> _books = new List<string>();
    private List<bool> _isBorrowed = new List<bool>();
    public void AddBook(string title)
    {
        _books.Add(title);
        _isBorrowed.Add(false);
        Console.WriteLine($"Book added: {title}");
    }
}
class Borrow
{
    private List<string> _books = new List<string>();
    private List<bool> _isBorrowed = new List<bool>();
    public void BorrowBook(int index)
    {
        if (index < 0 || index >= _books.Count)
        {
            Console.WriteLine("No such book");
            return;
        }

        if (_isBorrowed[index])
        {
            Console.WriteLine("Already borrowed");
            return;
        }

        _isBorrowed[index] = true;
        Console.WriteLine($"You borrowed: {_books[index]}");
    }
}
class Print
{
    private List<string> _books = new List<string>();
    private List<bool> _isBorrowed = new List<bool>();
    public void PrintBooks()
    {
        Console.WriteLine("=== BOOKS ===");
        for (int i = 0; i < _books.Count; i++)
        {
            string status = _isBorrowed[i] ? "Borrowed" : "Available";
            Console.WriteLine($"{i}. {_books[i]} - {status}");
        }
    }
}*/

class Library
{
    private readonly List<string> _books = new List<string>();
    private readonly List<bool> _isBorrowed = new List<bool>();

    public int Count => _books.Count;

    public void AddBook(string title)
    {
        _books.Add(title);
        _isBorrowed.Add(false);
    }

    public string GetTitle(int index) => _books[index];

    public bool IsBorrowed(int index) => _isBorrowed[index];

    public void SetBorrowed(int index, bool borrowed)
    {
        _isBorrowed[index] = borrowed;
    }

    public IReadOnlyList<string> Books => _books;
    public IReadOnlyList<bool> BorrowedFlags => _isBorrowed;
}

class LibraryService
{
    private readonly Library _library;

    public LibraryService(Library library)
    {
        _library = library;
    }

    public void AddBook(string title)
    {
        _library.AddBook(title);
        Console.WriteLine($"Book added: {title}");
    }

    public void BorrowBook(int index)
    {
        if (index < 0 || index >= _library.Count)
        {
            Console.WriteLine("No such book");
            return;
        }

        if (_library.IsBorrowed(index))
        {
            Console.WriteLine("Already borrowed");
            return;
        }

        _library.SetBorrowed(index, true);
        Console.WriteLine($"You borrowed: {_library.GetTitle(index)}");
    }
}

class LibraryPrinter
{
    private readonly Library _library;

    public LibraryPrinter(Library library)
    {
        _library = library;
    }

    public void PrintBooks()
    {
        Console.WriteLine("=== BOOKS ===");
        for (int i = 0; i < _library.Count; i++)
        {
            string title = _library.GetTitle(i);
            bool borrowed = _library.IsBorrowed(i);
            string status = borrowed ? "Borrowed" : "Available";
            Console.WriteLine($"{i}. {title} - {status}");
        }
    }
}
