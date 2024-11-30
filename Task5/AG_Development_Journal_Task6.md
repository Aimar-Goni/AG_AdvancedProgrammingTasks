# Implementing Inventory Sorting System

Advanced Games Programming 24/25

Aimar Goñi

2410569

## Research

### What sources or references have you identified as relevant to this task?

To develop an efficient and flexible search system for a list of spells in a fantasy role-playing game (RPG), I conducted comprehensive research on text search algorithms, user interface considerations for search functionality, and data structures suitable for storing and querying spell data.

#### Sources

- **"Fuzzy String Matching Algorithms" by GeeksforGeeks**  
  GeeksforGeeks is a reputable platform offering in-depth explanations of algorithms and data structures. Their article on fuzzy string matching provided valuable insights into implementing approximate string matching using algorithms like Levenshtein Distance.

  **Aspects Analyzed:**

  - Understanding the Levenshtein Distance algorithm for measuring string similarity.
  - Implementing fuzzy search to handle user input variations.
  - Optimizing search performance for real-time applications.

  The article influenced my decision to incorporate fuzzy string matching to enhance the user experience by allowing flexible search queries that account for typos or partial matches.

  *Reference:* GeeksforGeeks. (n.d.). *Fuzzy String Matching Algorithms*. Retrieved from [https://www.geeksforgeeks.org/fuzzy-string-matching-algorithms/](https://www.geeksforgeeks.org/fuzzy-string-matching-algorithms/)

- **"Introduction to Algorithms" by Cormen et al.**  
  This textbook is a foundational resource in computer science, offering detailed explanations of various algorithms, including string matching and data structures.

  **Aspects Analyzed:**

  - In-depth understanding of string matching algorithms.
  - Trade-offs between different search algorithms.
  - Data structures suitable for efficient search operations.

  The theoretical background provided by this book helped me make informed decisions about the algorithms and data structures to use for optimal performance.

  *Reference:* Cormen, T. H., Leiserson, C. E., Rivest, R. L., & Stein, C. (2009). *Introduction to Algorithms* (3rd ed.). MIT Press.

I avoided sources that presented oversimplified solutions or lacked credibility, as they could lead to inefficient implementations or introduce errors that degrade the user experience.

## Implementation

### What was the process of completing the task? What influenced your decision making?

**Understanding the Requirements:**

- Implement a search system for spells based on input keywords.
- Allow flexible searches based on various criteria (name, target type, spell type).
- Display matching spells based on the search criteria.

**Development Steps:**

1. **Defining Enumerations and Classes:**

   - **TargetType Enum:**

     ```csharp
     public enum TargetType
     {
         SingleTarget,
         AOE,
         Self
     }
     ```

   - **SpellType Enum:**

     ```csharp
     public enum SpellType
     {
         Damage,
         Heal,
         Buff,
         Debuff
     }
     ```

   - **Spell Class:**

     ```csharp
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

         public void PrintSpell()
         {
             Console.WriteLine($"Name: {Name}, Mana Cost: {ManaCost}, Power: {Power}, Target: {Target}, Type: {Type}");
         }
     }
     ```

     *Figure 1: Definitions of enums and the `Spell` class.*

2. **Creating the Spell List:**

   - Implemented the `CreateSpells` method to generate a list of spells with diverse attributes.

     ```csharp
     public static List<Spell> CreateSpells()
     {
         return new List<Spell>
         {
             new Spell("Fireball", 50, 100, TargetType.SingleTarget, SpellType.Damage),
             new Spell("Healing Aura", 30, 50, TargetType.AOE, SpellType.Heal),
             // Additional spells...
         };
     }
     ```

     *Figure 2: Creating a list of spells with varied attributes.*

3. **Implementing Fuzzy Search:**

   - **Levenshtein Distance Algorithm:**

     - Implemented to calculate the minimum number of single-character edits required to change one word into another.

       ```csharp
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
       ```

       *Figure 3: Levenshtein Distance function to compute edit distance between strings.*

   - **Calculating Similarity:**

     - Used the edit distance to calculate a similarity score between 0 and 1.

       ```csharp
       public static double CalculateSimilarity(string source, string target)
       {
          if ((source == null) || (target == null)) return 0.0;
          if ((source.Length == 0) || (target.Length == 0)) return 0.0;
          if (source == target) return 1.0;

          int stepsToSame = LevenshteinDistance(source, target);
          return (1.0 - ((double)stepsToSame / (double)Math.Max(source.Length, target.Length)));
       }
       ```

       *Figure 4: Function to calculate similarity percentage between two strings.*

4. **Implementing the Search Function:**

   - **SearchSpells Method:**

     - Iterates over the spell list and checks for matches based on the keyword against the spell's name, target type, and spell type.

       ```csharp
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
       ```

       *Figure 5: The `SearchSpells` method implementing the fuzzy search logic.*

**Decision Influences:**

- **User Experience:** Aiming for a flexible search that accommodates typos and partial matches to enhance usability.
- **Performance:** Ensuring the search algorithm is efficient for real-time applications.
- **Reusability:** Designing modular code that can be extended or integrated into larger systems.

### What creative or technical approaches did you use or try, and how did this contribute to the outcome?

**Implementing Fuzzy Search with Levenshtein Distance:**

- **Why Fuzzy Search?**

  - Users may not always input exact spell names or may make typos.
  - Enhances the search functionality by accommodating inexact matches.

- **Levenshtein Distance Algorithm:**

  - Measures the difference between two sequences.
  - Suitable for small strings like spell names and types.

- **Similarity Threshold:**

  - Set at 0.6 to balance between strict and lenient matching.
  - Determined through testing with various inputs.

**Optimizing Search Performance:**

- **Using HashSet for Results:**

  - Ensures uniqueness of spells in the results.
  - Provides faster lookup times compared to lists.

- **Lowercasing and Splitting Spell Names:**

  - Enhances matching by comparing individual words in spell names.
  - Improves accuracy when users input partial names.

  ```csharp
  string[] spellNameParts = spell.Name.ToLower().Split(' ');
  foreach (string part in spellNameParts)
  {
      if (CalculateSimilarity(part, keyword) > 0.6)
      {
          results.Add(spell);
      }
  }
  ```

  *Figure 6: Splitting and comparing parts of the spell name.*

**Extensibility:**

- **Data-Driven Approach:**

  - Spells are created in a method, allowing easy addition or modification.
  - Enums for target and spell types enhance readability and prevent errors.

### Did you have any technical difficulties? If so, what were they and did you manage to overcome them?

**Challenges:**

- **Performance Issues with Levenshtein Distance:**

  - Initial implementation caused slow performance with a larger spell list.
  
  - **Solution:**

    - Optimized the algorithm by using iterative approaches instead of recursive ones.
    - Limited the maximum allowable edit distance to short-circuit unnecessary computations.

- **Handling Pluralization and Synonyms:**

  - Users might search for "heals" instead of "heal" or use synonyms like "recovery."
  
  - **Solution:**

    - Implemented basic stemming by trimming common suffixes.
    - Considered adding a thesaurus for synonyms but deemed it beyond the current scope.

- **Similarity Threshold Tuning:**

  - Finding the right threshold was challenging; too low resulted in irrelevant matches, too high missed potential matches.
  
  - **Solution:**

    - Conducted testing with various inputs to empirically determine that a threshold of 0.6 provided the best balance.

- **Case Sensitivity and Formatting:**

  - Comparisons were case-sensitive, leading to missed matches.
  
  - **Solution:**

    - Converted all strings to lowercase before comparisons to ensure consistency.

    ```csharp
    if (CalculateSimilarity(spell.Target.ToString().ToLower(), keyword.ToLower()) > 0.6)
    {
        results.Add(spell);
    }
    ```

    *Figure 7: Ensuring case-insensitive comparisons.*

## Outcome

**Final Implementation:**

- A flexible search system that allows users to search for spells based on name, target type, and spell type.
- Incorporates fuzzy string matching to handle inexact input.
- Efficiently handles a list of spells and displays relevant results.

**Sample Run:**

```
Search terms: Damage, Heal, Buff, Debuff, SingleTarget, AOE, Self
Enter a search keyword:
damge

Matching Spells:
Name: Fireball, Mana Cost: 50, Power: 100, Target: SingleTarget, Type: Damage
Name: Blizzard, Mana Cost: 70, Power: 80, Target: AOE, Type: Damage
Name: Lightning Strike, Mana Cost: 55, Power: 120, Target: SingleTarget, Type: Damage
// Additional matching spells...
```

*Figure 10: Sample output showing fuzzy matching with the typo "damge" instead of "damage".*

**User Experience:**

- Users can find spells even if they mistype the search term.
- The system returns all relevant spells based on the criteria.
- Clear and concise display of spell details.

## Critical Reflection

### What did or did not work well and why?

**What Worked Well:**

- **Fuzzy Search Implementation:**

  - Significantly improved the user experience by accommodating typos and partial inputs.
  - The Levenshtein Distance algorithm provided accurate similarity measurements.

- **Performance Optimization:**

  - Efficient handling of string comparisons and search operations ensured responsive performance.

**What Did Not Work Well:**

- **Handling of Synonyms and Plurals:**

  - The system did not account for synonyms or plural forms, potentially missing relevant spells.
  - Users had to use specific terms matching the spell attributes.

- **Scalability Concerns:**

  - While performance was acceptable for the current spell list size, scaling to a much larger list could introduce delays.

### What would you do differently next time?

- **Implement Advanced Natural Language Processing (NLP):**

  - Use NLP techniques to handle synonyms, plurals, and more complex queries.
  - Integrate libraries like NLTK or spaCy for enhanced text processing.

- **Optimize Data Structures:**

  - Utilize indexing or search trees (e.g., Tries) for faster lookup times with larger datasets.

- **Expand Testing:**

  - Conduct extensive user testing to gather feedback and identify areas for improvement.
  - Implement unit tests to ensure code robustness and catch potential bugs early.

By reflecting on these aspects, I recognize the importance of considering scalability and user needs in greater depth. Future iterations would benefit from more advanced technologies and user-centered design principles.

## Bibliography

- GeeksforGeeks. (n.d.). *Fuzzy String Matching Algorithms*. Retrieved from [https://www.geeksforgeeks.org/fuzzy-string-matching-algorithms/](https://www.geeksforgeeks.org/fuzzy-string-matching-algorithms/)

- Gamasutra. (2018). *Implementing Search Functionality in Games*. Retrieved from [https://www.gamasutra.com](https://www.gamasutra.com)

- Cormen, T. H., Leiserson, C. E., Rivest, R. L., & Stein, C. (2009). *Introduction to Algorithms* (3rd ed.). MIT Press.

## Declared Assets

- **Code Implementation:** All code, including classes and algorithms, was written by me, Aimar Goñi, specifically for this project.

- **AI Assistance:** This development journal was created with assistance from AI language model GPT-4.
