/*public class UserService
{
    public void Register(string email)
    {
        // какая-то логика регистрации...

        var smtp = new SmtpClient("smtp.server.com");
        smtp.Send("no-reply@site.com", email, "Welcome", "Thanks for registering!");
    }
}*/
public interface IEmailSender
{
    void SendWelcome(string email);
}
public class SmtpEmailSender : IEmailSender
{
    private readonly string _host;
    public SmtpEmailSender(string host)
    {
        _host = host;
    }
    public void SendWelcome(string email)
    {
        using var smtp = new SmtpClient(_host);
        smtp.Send("no-reply@site.com", email, "Welcome", "Thanks!");
    }
}
public class UserService
{
    private readonly IEmailSender _emailSender;

    public UserService(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    public void Register(string email)
    {
        // логика регистрации...

        _emailSender.SendWelcome(email);
    }
}
/*public class OrdersController : ControllerBase
{
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        using var conn = new SqlConnection("Server=.;Database=App;Trusted_Connection=True;");
        conn.Open();

        var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT Total FROM Orders WHERE Id = @id";
        cmd.Parameters.AddWithValue("@id", id);

        var total = (decimal)cmd.ExecuteScalar();

        return Ok(total);
    }
}
*/

public interface IOrderRepository
{
    decimal GetTotal(int id);
}
public class SqlOrderRepository : IOrderRepository
{
    private readonly string _connectionString;
    public SqlOrderRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    public decimal GetTotal(int id) 
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();

        using var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT Total FROM Orders WHERE Id = @id";
        cmd.Parameters.AddWithValue("@id", id);
        return (decimal)cmd.ExecuteScalar();
    }
}
public class OrdersController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;
    public OrdersController(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var total = _orders.GetTotal(id);
        return Ok(total);
    }
}
/*public class PaymentService
{
    private readonly FileLogger _logger = new FileLogger("payments.log");

    public void Pay(decimal amount)
    {
        // логика оплаты...

        _logger.Log($"Paid {amount}");
    }
}

public class FileLogger
{
    private readonly string _path;

    public FileLogger(string path)
    {
        _path = path;
    }

    public void Log(string message)
    {
        File.AppendAllText(_path, message + Environment.NewLine);
    }
}*/
public interface ILogger
{
    void Log(string message);
}
public class FileLogger : ILogger
{
    private readonly string _path;
    public FileLogger(string path)
    {
        _path = path;
    }
    public void Log(string message) 
    {
        File.AppendAllText(_path, message + Environment.NewLine);
    }
}
public class PaymentService
{
    private readonly ILogger _logger;
    public PaymentService(ILogger logger)
    {
        _logger = logger;
    }
    public void Pay(decimal amount)
    {
        _logger.Log($"Paid {amount}");
    }
}
/*public class LoginForm
{
    public void LoginButtonClick(string email, string password)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            Console.WriteLine("Please enter email and password");
            return;
        }

        using var conn = new SqlConnection("Server=.;Database=App;Trusted_Connection=True;");
        conn.Open();

        var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT COUNT(*) FROM Users WHERE Email = @email AND Password = @pwd";
        cmd.Parameters.AddWithValue("@email", email);
        cmd.Parameters.AddWithValue("@pwd", password);

        var count = (int)cmd.ExecuteScalar();
        if (count > 0)
            Console.WriteLine("Login success");
        else
            Console.WriteLine("Login failed");
    }
}
*/
public interface IAuthService
{
    bool Login(string email, string password);
}
public class SqlAuthService : IAuthService
{
    private readonly string _connectionString;
    public SqlAuthService(string connectionString)
    {
        _connectionString = connectionString;
    }
    public bool Login(string email, string password)
    {
        using var conn = new SqlAuthService(_connectionString);
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT COUNT(*) FROM Users WHERE Email = @email AND Password = @pwd";
        cmd.Parameters.AddWithValue("@email", email);
        cmd.Parameters.AddWithValue("@pwd", password);

        var count = (int)cmd.ExecuteScalar();
        return count > 0;
    }
}
public class LoginForm : IAuthService
{
    private readonly IAuthService _authService;
    public LoginForm(IAuthService authService)
    {
        _authService = authService;
    }
    public bool LoginButtonClick(string email, string password) 
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            Console.WriteLine("Please enter email and password");
            return;
        }
        bool ok = _auth.Login(email, password);
        Console.WriteLine(ok ? "Login success" : "Login failed");
    }
}

/*Файловая система в логике
public class ConfigLoader
{
    public string Load() => File.ReadAllText("/etc/app/config.json");
}
*/

public interface ILoad
{
    string ReadAllText(string path);
}
public class ConfigLoader
{
    private readonly ILoad _load;
    private readonly string _path;
    public ConfigLoader(ILoad load)
    {
        _load = load;
        _path = path;
    }
    public string Load() => _load.ReadAllText(_path);
}

/*public class WeatherApi
{
    public async Task<string> GetAsync(string city)
    {
        using var http = new HttpClient();
        return await http.GetStringAsync($"https://api?q={city}");
    }
}*/

public interface IHttpClient
{
    Task<string> GetStringAsync(string url);
}
public class WeatherApi
{
    private readonly IHttpClient _http;
    public WeatherApi(IHttpClient http) => _http = http;
    public Task<string> GetAsync(string city) => _http.GetStringAsync
       ($"https://api?q={city}");
}

/*public class Notifier
{
    public Task Send(string email, string msg)
        => new SmtpClient("smtp.local").SendMailAsync
        ("noreply@x", email, "Hi", msg);
}*/

public interface ISmtpClient
{
    Task SendAsync(string to, string subject, string body);
}
public class Notifier
{
    private readonly ISmtpClient _smtp;
    public Notifier(ISmtpClient smtp)
    {
        _smtp = smtp;
    }
    public Task Send(string email, string msg) =>
       _smtp.SendAsync(email, "Hi", msg);
}

/*public class UserService
{
    public void Save(User u)
    {
        using var c = new SqlConnection("...");
        c.Open();
        // SQL commands...
    }
}
*/
public interface IUserRepository { void Save(User u); }

public class UserService
{
    private readonly IUserRepository _repo;
    public UserService(IUserRepository repo) => _repo = repo;
    public void Save(User u) => _repo.Save(u);
}

/*public class ExportService
{
    public void Export(string type, string data)
    {
        if (type == "CSV") new CsvExporter().Export(data);
        else if (type == "JSON") new JsonExporter().Export(data);
    }
}*/
public interface IExporter
{
    void Export(string data);
}
public class ExportService : IExporter
{
    private readonly IReadOnlyDictionary<string, IExporter> _map;
    public ExportService(IReadOnlyDictionary<string, IExporter> map) 
        => _map = map;
    public void Export(string type, string data) 
        => _map[type].Export(data);
}
