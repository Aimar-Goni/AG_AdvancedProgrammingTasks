using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using static System.Net.WebRequestMethods;

// Define the target types
public enum TargetType
{
    SingleTarget,
    AOE,
    Self
}

// Define the spell types
public enum SpellType
{
    Damage,
    Heal,
    Buff,
    Debuff
}

// The Spell class represents a single spell with attributes
public class Spell
{
    public string Name { get; set; }
    public int ManaCost { get; set; }
    public int Power { get; set; }
    public TargetType Target { get; set; }
    public SpellType Type { get; set; }

    public Spell(string name, int manaCost, int power, TargetType target, SpellType type)
    {
        Name = name;
        ManaCost = manaCost;
        Power = power;
        Target = target;
        Type = type;
    }

    // Method to print spell details
    public void PrintSpell() => Console.WriteLine($"Name: {Name}, Mana Cost: {ManaCost}, Power: {Power}, Target: {Target}, Type: {Type}");
    
}

//https://en.wikipedia.org/wiki/Levenshtein_distance
// http://www.blackbeltcoder.com/Articles/algorithms/phonetic-string-comparison-with-soundex

public class Program
{
   //This algorithm compares the changes that would be needed to convert one string to the other.
   //That way, you can check the similarities between the two and select the error threshold.
    public static int LevenshteinDistance(string source, string target)
    {
        // special cases
        if (source == target) return 0;
        if (source.Length == 0) return target.Length;
        if (target.Length == 0) return source.Length;


        int[] v0 = new int[target.Length + 1];
        int[] v1 = new int[target.Length + 1];


        for (int i = 0; i < v0.Length; i++)
            v0[i] = i;

        for (int i = 0; i < source.Length; i++)
        {

            v1[0] = i + 1;

    
            for (int j = 0; j < target.Length; j++)
            {
                var cost = (source[i] == target[j]) ? 0 : 1;
                v1[j + 1] = Math.Min(v1[j] + 1, Math.Min(v0[j + 1] + 1, v0[j] + cost));
            }

            for (int j = 0; j < v0.Length; j++)
                v0[j] = v1[j];
        }

        return v1[target.Length];
    }

    public static double CalculateSimilarity(string source, string target)
    {
        if ((source == null) || (target == null)) return 0.0;
        if ((source.Length == 0) || (target.Length == 0)) return 0.0;
        if (source == target) return 1.0;

        int stepsToSame = LevenshteinDistance(source, target);
        return (1.0 - ((double)stepsToSame / (double)Math.Max(source.Length, target.Length)));
    }

    private static bool IsSimilar(string source, string keyword) => CalculateSimilarity(source, keyword) > 0.6;

    // Task for student: Implement a search function to find spells based on keywords
    public static HashSet<Spell> SearchSpells(List<Spell> spellList, string keyword)
    {
        HashSet<Spell> results = new HashSet<Spell>();

        foreach (Spell spell in spellList)
        {

            string[] spellNameParts = spell.Name.ToLower().Split(' ');
            foreach (string part in spellNameParts)
            {
                if (CalculateSimilarity(part, keyword) > 0.6)
                {
                    results.Add(spell);
                }
            }
            if (CalculateSimilarity(spell.Target.ToString().ToLower(), keyword) > 0.6)
            {
                results.Add(spell);
            }
            if (CalculateSimilarity(spell.Type.ToString().ToLower(), keyword) > 0.6)
            {
                results.Add(spell);
            }

        }

        return results;
    }

    public static void Main(string[] args)
    {

        string jsonData = System.IO.File.ReadAllText("../../../data/spells.json");
        // Deserialize the JSON into a list of Spell objects
        List<Spell> spells = JsonConvert.DeserializeObject<List<Spell>>(jsonData);


        Console.WriteLine("Search terms: Damage, Heal, Buff, Debuff, SingleTarget, AOE, Self");

        while (true)
        {
            Console.WriteLine("\nEnter a search keyword (or type 'exit' to quit):");
            string keyword = Console.ReadLine();

            // Allow user to exit the loop
            if (keyword.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            // Search for spells based on the keyword
            HashSet<Spell> matchingSpells = SearchSpells(spells, keyword);

            // Display the results
            if (matchingSpells.Count > 0)
            {
                Console.WriteLine("Matching Spells:");
                foreach (var spell in matchingSpells)
                {
                    spell.PrintSpell();
                }
            }
            else
            {
                Console.WriteLine("No spells matched the search criteria.");
            }
        }

        Console.WriteLine("Thank you for using the spell search!");
    }
}