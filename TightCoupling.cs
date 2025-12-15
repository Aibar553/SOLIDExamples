/*public class WeatherService
{
    public string Get()
    {
        var client = new HttpClient();
        return client.GetStringAsync("https://api").Result;
    }
}*/
public interface IHttpClient
{
    Task<string> Get(string url);
}


/*public class PaymentProcessor
{
    public void Pay()
    {
        var bank = new KazBankApi();  // конкретная реализация
        bank.SendPayment();
    }
}*/

public interface IBank
{
    void SendPayment();
}
public class PaymentProcessor
{
    private readonly IBankApi _bank;
    public PaymentProcessor(IBankApi bank) => _bank = bank;
}

/*public class ReportService
{
    public string BuildReport(List<string> data)
    {
        return string.Join(",", data); // всегда CSV
    }
}*/

public interface IBuilder
{
    string Format(List<string> data);
}
/*
 public class WeatherService
{
    public async Task<decimal> GetTempCAsync(string city)
    {
        using var client = new HttpClient();
        var json = await client.GetStringAsync($"https://api.weather/?q={city}");
        var parts = json.Split(':', ',', '}'); // "быстрый парсинг"
        var i = Array.IndexOf(parts, "\"tempC\"");
        return decimal.Parse(parts[i + 1], CultureInfo.InvariantCulture);
    }
}
*/

public interface IHttp
{
    Task<string> GetAsync(string url);
}

public interface IWeatherParser
{
    decimal ParseTempC(string json);
}

public class WeatherService
{
    private readonly IHttp _http;
    private readonly IWeatherParser _parser;

    public WeatherService(IHttp http, IWeatherParser parser)
        => (_http, _parser) = (http, parser);

    public async Task<decimal> GetTempCAsync(string city)
    {
        var json = await _http.GetAsync($"https://api.weather/?q={city}");
        return _parser.ParseTempC(json);
    }
}
/*public class CheckoutService
{
    public void Checkout(Order order)
    {
        using var conn = new SqlConnection("...");
        conn.Open();
        using var tx = conn.BeginTransaction();

        try
        {
            new SqlCommand("INSERT ...", conn, tx).ExecuteNonQuery();
            new SqlCommand("UPDATE ...", conn, tx).ExecuteNonQuery();

            File.AppendAllText("log.txt", $"Order {order.Id} committed\n");
            tx.Commit();
        }
        catch (Exception ex)
        {
            File.AppendAllText("log.txt", $"Order {order.Id} failed: {ex.Message}\n");
            tx.Rollback();
            throw;
        }
    }
}
*/

public interface IUnitOfWork : IDisposable
{
    void Commit();
    void Rollback();
}

public interface IOrderRepository
{
    void Add(Order order);
    void UpdateTotals(Order order);
}

public interface ILogger
{
    void Info(string msg);
    void Error(string msg);
}

public class CheckoutService
{
    private readonly IUnitOfWork _uow;
    private readonly IOrderRepository _orders;
    private readonly ILogger _log;

    public CheckoutService(IUnitOfWork uow, IOrderRepository orders, ILogger log)
        => (_uow, _orders, _log) = (uow, orders, log);

    public void Checkout(Order order)
    {
        try
        {
            _orders.Add(order);
            _orders.UpdateTotals(order);
            _log.Info($"Order {order.Id} committed");
            _uow.Commit();
        }
        catch (Exception ex)
        {
            _log.Error($"Order {order.Id} failed: {ex.Message}");
            _uow.Rollback();
            throw;
        }
    }
}
/*public class SalesReportService
{
    public async Task SendDailyAsync(DateTime day, string email)
    {
        var data = LoadFromDb(day); // приватный метод внутри
        var csv = string.Join('\n', data.Select(x => $"{x.Id};{x.Total}"));

        using var smtp = new SmtpClient("smtp.local");
        await smtp.SendMailAsync("noreply@corp", email, "Daily Report", csv);
    }

    private IEnumerable<(int Id, decimal Total)> LoadFromDb(DateTime day) { ... }
}*/

public interface ISalesDataSource
{
    IEnumerable<(int Id, decimal Total)> Load(DateTime day);
}
public interface IReportFormatter
{
    string Format(IEnumerable<(int Id, decimal Total)> rows);
}
public interface IReportSender
{
    Task SendAsync(string subject, string body, string recipient);
}
public class SalesReportService
{
    private readonly ISalesDataSource _source;
    private readonly IReportFormatter _formatter;
    private readonly IReportSender _sender;

    public SalesReportService(ISalesDataSource s, IReportFormatter f, IReportSender sd)
        => (_source, _formatter, _sender) = (s, f, sd);

    public async Task SendDailyAsync(DateTime day, string email)
    {
        var rows = _source.Load(day);
        var body = _formatter.Format(rows);
        await _sender.SendAsync("Daily Report", body, email);
    }
}

/*
Ещё один частый мини-провал (время)
public class Clock
{
    public bool IsNight() => DateTime.Now.Hour < 6;
}*/

public interface ITimeProvider {DateTime Now {get;}}
public class Clock
{
    private readonly ITimeProvider _time;
    public Clock(ITimeProvider time) => _time = time;
    public bool IsNight() => _time.Now.Hour < 6;
}


/*
Guid.NewGuid() внутри доменной логики
public class IdFactory
{
    public string NewOrderId() => Guid.NewGuid().ToString("N");
}
*/

public interface IFactory
{
    Guid NewGuid();
}
public class IdFactory
{
    private readonly IFactory _factory;
    public IdFactory(IFactory factory) => _factory = factory;
    public string NewOrderId() => _factory.NewGuid().ToString("N");
}


/*
Thread.Sleep в бизнес-методе
public class Retryer
{
    public void Do(Action act)
    {
        for (int i = 0; i < 3; i++) 
        try { act(); return; } catch { Thread.Sleep(200); }
    }
}
*/
public interface IWaiter
{
    void Delay(TimeSpan t);
}
public class Retryer
{
    private readonly IWaiter _wait;
    public Retryer(IWaiter w) => _wait = w;
    public void Do(Action act)
    {
        for(int i = 0; i < 3; i++) 
        try{
            act(); return;
        }
        catch{
            _wait.Delay(TimeSpan.FromMilliseconds(200));
        }
    }
}

/*
new HttpClient() прямо в методе
public class WeatherApi
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
    public Task<string> GetAsync(string city) 
    => _http.GetStringAsync($"https://api?q={city}");
}