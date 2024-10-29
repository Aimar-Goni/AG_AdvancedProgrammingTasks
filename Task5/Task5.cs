using System;
using System.Collections.Generic;

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
    public void PrintSpell()
    {
        Console.WriteLine($"Name: {Name}, Mana Cost: {ManaCost}, Power: {Power}, Target: {Target}, Type: {Type}");
    }
}

public class Program
{
    // Create a list of spells with the attributes
    public static List<Spell> CreateSpells()
    {
        return new List<Spell>
        {
            new Spell("Fireball", 50, 100, TargetType.SingleTarget, SpellType.Damage),
            new Spell("Healing Aura", 30, 50, TargetType.AOE, SpellType.Heal),
            new Spell("Shield", 20, 0, TargetType.Self, SpellType.Buff),
            new Spell("Curse", 40, 60, TargetType.SingleTarget, SpellType.Debuff),
            new Spell("Blizzard", 70, 80, TargetType.AOE, SpellType.Damage),
            new Spell("Regenerate", 25, 30, TargetType.Self, SpellType.Heal),
            new Spell("Lightning Strike", 55, 120, TargetType.SingleTarget, SpellType.Damage),
            new Spell("Mass Shield", 60, 0, TargetType.AOE, SpellType.Buff),
            new Spell("Flame Burst", 45, 110, TargetType.AOE, SpellType.Damage),
            new Spell("Light Aura", 30, 0, TargetType.AOE, SpellType.Buff),
            new Spell("Dark Curse", 40, 70, TargetType.SingleTarget, SpellType.Debuff),
            new Spell("Water Wave", 35, 90, TargetType.AOE, SpellType.Damage),
            new Spell("Thunderbolt", 60, 130, TargetType.SingleTarget, SpellType.Damage),
            new Spell("Earthquake", 65, 150, TargetType.AOE, SpellType.Damage),
            new Spell("Magic Barrier", 50, 0, TargetType.Self, SpellType.Buff),
            new Spell("Invisibility", 30, 0, TargetType.Self, SpellType.Buff),
            new Spell("Meteor Shower", 90, 200, TargetType.AOE, SpellType.Damage),
            new Spell("Ice Spike", 35, 80, TargetType.SingleTarget, SpellType.Damage),
            new Spell("Hurricane", 75, 140, TargetType.AOE, SpellType.Damage),
            new Spell("Holy Light", 20, 40, TargetType.Self, SpellType.Heal),
            new Spell("Lightning Storm", 80, 180, TargetType.AOE, SpellType.Damage),
            new Spell("Sacred Flame", 60, 100, TargetType.SingleTarget, SpellType.Damage),
            new Spell("Venom Strike", 30, 70, TargetType.SingleTarget, SpellType.Debuff),
            new Spell("Frost Nova", 70, 90, TargetType.AOE, SpellType.Damage),
            new Spell("Mana Shield", 40, 0, TargetType.Self, SpellType.Buff),
            new Spell("Arcane Missiles", 45, 85, TargetType.SingleTarget, SpellType.Damage),
            new Spell("Healing Rain", 35, 60, TargetType.AOE, SpellType.Heal),
            new Spell("Divine Protection", 50, 0, TargetType.Self, SpellType.Buff),
            new Spell("Poison Cloud", 50, 100, TargetType.AOE, SpellType.Debuff),
            new Spell("Stone Skin", 25, 0, TargetType.Self, SpellType.Buff),
            new Spell("Life Drain", 50, 70, TargetType.SingleTarget, SpellType.Debuff),
            new Spell("Phoenix Fire", 100, 250, TargetType.AOE, SpellType.Damage),
            new Spell("Raging Inferno", 90, 210, TargetType.AOE, SpellType.Damage),
            new Spell("Whirlwind", 55, 85, TargetType.AOE, SpellType.Damage),
            new Spell("Blessing of Light", 30, 0, TargetType.Self, SpellType.Buff),
            new Spell("Shadow Bolt", 40, 90, TargetType.SingleTarget, SpellType.Damage),
            new Spell("Serpent's Bite", 45, 65, TargetType.SingleTarget, SpellType.Debuff),
            new Spell("Cleansing Wave", 25, 0, TargetType.AOE, SpellType.Heal),
            new Spell("Chill Touch", 35, 50, TargetType.SingleTarget, SpellType.Damage),
            new Spell("Blood Pact", 60, 0, TargetType.Self, SpellType.Buff),
            new Spell("Lunar Strike", 75, 160, TargetType.SingleTarget, SpellType.Damage),
            new Spell("Solar Flare", 65, 140, TargetType.AOE, SpellType.Damage),
            new Spell("Nature's Grasp", 50, 80, TargetType.SingleTarget, SpellType.Buff),
            new Spell("Wrath of the Ancients", 100, 250, TargetType.AOE, SpellType.Damage),
            new Spell("Ethereal Form", 40, 0, TargetType.Self, SpellType.Buff),
            new Spell("Radiant Heal", 30, 70, TargetType.SingleTarget, SpellType.Heal),
            new Spell("Stormcall", 80, 150, TargetType.AOE, SpellType.Damage),
            new Spell("Chain Lightning", 70, 130, TargetType.AOE, SpellType.Damage)
        };


    }
    public static int LevenshteinDistance(string source, string target)
    {
        // degenerate cases
        if (source == target) return 0;
        if (source.Length == 0) return target.Length;
        if (target.Length == 0) return source.Length;

        // create two work vectors of integer distances
        int[] v0 = new int[target.Length + 1];
        int[] v1 = new int[target.Length + 1];

        // initialize v0 (the previous row of distances)
        // this row is A[0][i]: edit distance for an empty s
        // the distance is just the number of characters to delete from t
        for (int i = 0; i < v0.Length; i++)
            v0[i] = i;

        for (int i = 0; i < source.Length; i++)
        {
            // calculate v1 (current row distances) from the previous row v0

            // first element of v1 is A[i+1][0]
            //   edit distance is delete (i+1) chars from s to match empty t
            v1[0] = i + 1;

            // use formula to fill in the rest of the row
            for (int j = 0; j < target.Length; j++)
            {
                var cost = (source[i] == target[j]) ? 0 : 1;
                v1[j + 1] = Math.Min(v1[j] + 1, Math.Min(v0[j + 1] + 1, v0[j] + cost));
            }

            // copy v1 (current row) to v0 (previous row) for next iteration
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
    private static bool IsSimilar(string source, string keyword)
    {
        return CalculateSimilarity(source, keyword) > 0.6;
    }
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
        List<Spell> spells = CreateSpells();

        Console.WriteLine("Search terms: Damage, Heal, Buff, Debuff, SingleTarget, AOE, Self");
        Console.WriteLine("Enter a search keyword:");
        string keyword = Console.ReadLine();

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
}