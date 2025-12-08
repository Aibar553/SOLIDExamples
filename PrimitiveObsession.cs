/*public class User
{
    public string Email { get; set; }
}

public class RegistrationService
{
    public void Register(User user)
    {
        if (string.IsNullOrWhiteSpace(user.Email) || !user.Email.Contains("@"))
            throw new ArgumentException("Invalid email");

        // ... логика регистрации
    }
}

public class NewsletterService
{
    public void Subscribe(string email)
    {
        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            throw new ArgumentException("Invalid email");

        // ... логика подписки
    }
}
*/

public class Email
{
    public string Value { get; }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !value.Contains("@"))
            throw new ArgumentException("Invalid email", nameof(value));

        Value = value;
    }
    public override string ToString() => Value;
}
public class User
{
    public Email Email { get; private set; }

    public User(Email email)
    {
        Email = email ?? throw new ArgumentNullException(nameof(email));
    }
}

public class RegistrationService
{
    public void Register(User user)
    {
        
    }
}

public class NewsletterService
{
    public void Subscribe(Email email)
    {
        // Email уже гарантированно валиден.
    }
}
