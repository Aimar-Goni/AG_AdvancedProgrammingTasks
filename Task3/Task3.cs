using System;
using System.Collections.Generic;

public class Potion
{
    public string Name { get; set; }
    public int HealingValue { get; set; }

    public Potion(string name, int healingValue)
    {
        Name = name;
        HealingValue = healingValue;
    }
}

public class Player
{
    public string Name { get; set; }
    public int CurrentHealth { get; set; }
    public int MaxHealth { get; set; }

    public Player(string name, int currentHealth, int maxHealth)
    {
        Name = name;
        CurrentHealth = currentHealth;
        MaxHealth = maxHealth;
    }
}

public class HealthPotionSystem
{
    public List<Potion> potions = new List<Potion>();

    public HealthPotionSystem()
    {
        // Adding multiple potions
        potions.Add(new Potion("Small Potion", 10));
        potions.Add(new Potion("Medium Potion", 20));
        potions.Add(new Potion("Large Potion", 50));
    }

    // Method to determine the optimal potions to consume for each player
    public void HealPlayers(List<Player> players)
    {
        foreach (var player in players)
        {
            int healthNeeded = player.MaxHealth - player.CurrentHealth;
            if (healthNeeded <= 0)
            {
                Console.WriteLine($"{player.Name} is already at max health!");
                continue;
            }

            // Sort potions by their healing value in descending order
            potions.Sort((p1, p2) => p2.HealingValue.CompareTo(p1.HealingValue));

            Console.WriteLine($"Healing {player.Name}: Current Health = {player.CurrentHealth}, Max Health = {player.MaxHealth}");

            // Implement your solution here to consume potions optimally based on healthNeeded for each player


            if (player.CurrentHealth < player.MaxHealth)
            {
                Console.WriteLine($"Not enough potions to fully heal {player.Name}!");
            }
            else
            {
                Console.WriteLine($"{player.Name} is fully healed!");
            }
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // List of multiple players
        List<Player> players = new List<Player>
        {
            new Player("Mage", 50, 100),
            new Player("Knight", 70, 100),
            new Player("Rogue", 65, 100),
            new Player("Cleric", 85, 100)
        };

        HealthPotionSystem potionSystem = new HealthPotionSystem();
        potionSystem.HealPlayers(players); // Heal all players optimally using available potions
    }
}