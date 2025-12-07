/*class AppManager {
    private String userName;
    private String theme;
    private List<String> logs = new ArrayList<>();

    public void login(String name) {
        this.userName = name;
        logs.add("User logged in: " + name);
        System.out.println("Welcome, " + name);
    }

    public void changeTheme(String newTheme) {
        this.theme = newTheme;
        logs.add("Theme changed: " + newTheme);
        System.out.println("Theme is now: " + newTheme);
    }

    public void printLogs() {
        System.out.println("=== LOGS ===");
        for (String log : logs) {
            System.out.println(log);
        }
    }
}*/

using System;
using System.Collections.Generic;

class Logger
{
    private readonly List<string> _logs = new List<string>();

    public void Log(string message)
    {
        _logs.Add(message);
    }

    public void PrintLogs()
    {
        Console.WriteLine("=== LOGS ===");
        foreach (var log in _logs)
        {
            Console.WriteLine(log);
        }
    }
}

class UserSession
{
    public string UserName { get; private set; }
    private readonly Logger _logger;

    public UserSession(Logger logger)
    {
        _logger = logger;
    }

    public void Login(string name)
    {
        UserName = name;
        _logger.Log("User logged in: " + name);
        Console.WriteLine("Welcome, " + name);
    }
}

class ThemeManager
{
    public string Theme { get; private set; }
    private readonly Logger _logger;

    public ThemeManager(Logger logger)
    {
        _logger = logger;
    }

    public void ChangeTheme(string newTheme)
    {
        Theme = newTheme;
        _logger.Log("Theme changed: " + newTheme);
        Console.WriteLine("Theme is now: " + newTheme);
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