/*public class AppManager
{
    public string CurrentUserEmail { get; private set; }
    public string ConnectionString { get; private set; } = "...";

    public void Login(string email, string password)
    {
        // check hardcoded credentials
        if (email == "admin@mail.com" && password == "123456")
        {
            CurrentUserEmail = email;
            File.AppendAllText("log.txt", $"Login: {email}\n");
        }
    }

    public void LoadSettings()
    {
        if (!File.Exists("settings.json"))
            File.WriteAllText("settings.json", "{}");

        var json = File.ReadAllText("settings.json");
        Console.WriteLine("Settings loaded: " + json);
    }

    public void OpenDatabase()
    {
        var conn = new SqlConnection(ConnectionString);
        conn.Open();
        Console.WriteLine("DB opened");
    }
}
*/

public class SignIn
{
    public string CurrentUserEmail { get; private set; }
    public void Login(string email, string password)
    {
        if (email == "admin@mail.com" && password = "123456")
        {
            CurrentUserEmail = email;
            File.AppendAllText("log.txt", $"Login: {email}\n");
        }
    }
}
public class Settings
{
    public void Load()
    {
        if (!File.Exists("settings.json"))
            File.WriteAllText("settings.json", "{}");

        var json = File.ReadAllText("settings.json");
        Console.WriteLine("Settings loaded: " + json);
    }
}
public class DB
{
    public string ConnectionString { get; private set; }
    public void OpenDatabase()
    {
        var conn = new SqlConnection(ConnectionString);
        conn.Open();
        Console.WriteLine("DB opened");
    }
}


/*class Game
{
    private int _playerHealth = 100;
    private int _enemyHealth = 50;
    private int _score = 0;

    public void ShootEnemy()
    {
        _enemyHealth -= 10;
        _score += 5;
        Console.WriteLine($"Enemy HP: {_enemyHealth}, score: {_score}");
    }

    public void EnemyAttack()
    {
        _playerHealth -= 15;
        Console.WriteLine($"Player HP: {_playerHealth}");
    }

    public void ResetGame()
    {
        _playerHealth = 100;
        _enemyHealth = 50;
        _score = 0;
        Console.WriteLine("Game reset");
    }
}*/



class Player
{
    public int Health { get; private set; } = 100;

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }
}

class Enemy
{
    public int Health { get; private set; } = 50;

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }
}

class Score
{
    public int Value { get; private set; }

    public void Add(int points)
    {
        Value += points;
    }
}

class Game
{
    private Player _player = new Player();
    private Enemy _enemy = new Enemy();
    private Score _score = new Score();

    public void ShootEnemy()
    {
        _enemy.TakeDamage(10);
        _score.Add(5);

        Console.WriteLine($"Enemy HP: {_enemy.Health}, score: {_score.Value}");
    }

    public void EnemyAttack()
    {
        _player.TakeDamage(15);
        Console.WriteLine($"Player HP: {_player.Health}");
    }

    public void ResetGame()
    {
        _player = new Player();
        _enemy = new Enemy();
        _score = new Score();
        Console.WriteLine("Game reset");
    }
}


/*class ShopSystem
{
    private List<string> _products = new List<string>();
    private List<int> _prices = new List<int>();
    private int _balance = 0;

    public void AddProduct(string name, int price)
    {
        _products.Add(name);
        _prices.Add(price);
        Console.WriteLine($"Product added: {name} - {price}");
    }

    public void AddMoney(int amount)
    {
        _balance += amount;
        Console.WriteLine($"Balance: {_balance}");
    }

    public void BuyProduct(int index)
    {
        if (index < 0 || index >= _products.Count)
        {
            Console.WriteLine("No such product");
            return;
        }

        int price = _prices[index];
        if (_balance >= price)
        {
            _balance -= price;
            Console.WriteLine($"You bought: {_products[index]}");
        }
        else
        {
            Console.WriteLine("Not enough money");
        }
    }
}*/
class Product{
    private List<string> _products = new List<string>();
    private List<int> _prices = new List<int>();
    public void AddProduct(string name, int price){
        _products.Add(name);
        _prices.Add(price);
        Console.WriteLine($"Product added: {name} - {price}");
    }
}
class Money{
    public int _balance {get; private set;}

    public void AddMoney(int amount)
    {
        _balance += amount;
        Console.WriteLine($"Balance: {_balance}");
    }
}
class Buy{
    public void BuyProduct(int index)
    {
        if (index < 0 || index >= _products.Count)
        {
            Console.WriteLine("No such product");
            return;
        }

        int price = _prices[index];
        if (_balance >= price)
        {
            _balance -= price;
            Console.WriteLine($"You bought: {_products[index]}");
        }
        else
        {
            Console.WriteLine("Not enough money");
        }
    }
}

/*public class GameManager
{
    private int _score;
    private bool _isRunning;

    public void Start()
    {
        _score = 0;
        _isRunning = true;
        Console.WriteLine("Game started");
    }

    public void Update()
    {
        if (!_isRunning) return;

        // handle input
        if (Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.Spacebar)
        {
            _score++;
        }

        // update physics
        Console.WriteLine("Updating physics...");

        // render
        Console.WriteLine($"Rendering score: {_score}");
    }

    public void Stop()
    {
        _isRunning = false;
        Console.WriteLine("Game stopped");
    }
}*/

public class Starting
{
    private int _score;
    private bool _isRunning;
    public void Start()
    {
        _score = 0;
        _isRunning = true;
        Console.WriteLine("Game started");
    }
}
public class Updating
{
    private int _score;
    private int _isRunning;
    public void Update()
    {
        if (!_isRunning) return;

        // handle input
        if (Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.Spacebar)
        {
            _score++;
        }

        // update physics
        Console.WriteLine("Updating physics...");

        // render
        Console.WriteLine($"Rendering score: {_score}");
    }
}
public class Stopping
{
    private bool _isRunning;
    public void Stop() 
    {
        _isRunning = false;
        Console.WriteLine("Game stopped");
    }
}



/*
public class ReportService
{
    public void GenerateMonthlyReport(int month, int year)
    {
        // load data
        using (var conn = new SqlConnection("..."))
        {
            conn.Open();
            Console.WriteLine($"Loading data for {month}/{year}...");
        }

        // build HTML
        var html = $"<h1>Report for {month}/{year}</h1>";

        // save to disk
        File.WriteAllText($"report_{year}_{month}.html", html);

        // "send" to boss
        Console.WriteLine("Emailing report to boss@company.com");

        // log
        File.AppendAllText("logs.txt", $"Report generated: {month}/{year}\n");
    }
}
*/

public class ReportDataLoader
{
    private readonly string _connectionString;

    public ReportDataLoader(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void LoadData(int month, int year)
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        Console.WriteLine($"Loading data for {month}/{year}...");
        // тут могла бы быть реальная выборка данных
    }
}
public class ReportStorage
{
    public void Save(string html, int month, int year)
    {
        var fileName = $"report_{year}_{month}.html";
        File.WriteAllText(fileName, html);
    }
}
public class ReportNotifier
{
    private readonly string _bossEmail;

    public ReportNotifier(string bossEmail)
    {
        _bossEmail = bossEmail;
    }

    public void Notify(int month, int year)
    {
        Console.WriteLine($"Emailing report {month}/{year} to {_bossEmail}");
    }
}
public class Logger
{
    private readonly string _logFile;

    public Logger(string logFile = "logs.txt")
    {
        _logFile = logFile;
    }

    public void Log(string message)
    {
        File.AppendAllText(_logFile, message + Environment.NewLine);
    }
}
