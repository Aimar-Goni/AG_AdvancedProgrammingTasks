# Implementing Inventory Sorting System

Advanced Games Programming 24/25

Aimar Goñi

2410569

## Research

### What sources or references have you identified as relevant to this task?

To develop an efficient and flexible sorting system for the game inventory, I conducted comprehensive research on sorting algorithms suitable for both strings and integers. My objective was to find an algorithm that performs well with small to medium-sized datasets typical in game inventories and can handle different data types and sort orders.

I prioritized reputable sources like academic textbooks, official documentation, and well-regarded programming websites to ensure the reliability and accuracy of the information. I aimed to deepen my understanding by implementing the algorithm from scratch rather than relying on built-in sorting functions. This approach would enhance my problem-solving skills and provide greater control over the sorting process, which is beneficial for potential future customizations.

#### Sources

- **"TimSort Algorithm" by GeeksforGeeks**  
  GeeksforGeeks is a respected educational platform offering in-depth articles on computer science topics. Their explanation of the TimSort algorithm provided valuable insights into how this hybrid sorting method combines merge sort and insertion sort to efficiently sort data.

  Key aspects analyzed:

  - Understanding how TimSort exploits natural runs in data.
  - Implementing TimSort for both ascending and descending order.
  - Adapting TimSort to sort different data types using key selectors.

  I appreciated the clear explanations and code examples, which significantly influenced my decision to implement TimSort. The ability of TimSort to handle partially sorted data efficiently made it ideal for a game inventory system.

  *Reference:* GeeksforGeeks. (n.d.). *TimSort*. Retrieved from [https://www.geeksforgeeks.org/timsort/](https://www.geeksforgeeks.org/timsort/)

- **"Sorting in C#" by Microsoft Documentation**  
  Microsoft's official documentation provided authoritative guidance on sorting collections in C#. It helped me understand how to use delegates and generics to create flexible sorting methods.

  Key aspects analyzed:

  - Utilizing `IComparable` and `IComparer` interfaces.
  - Implementing custom sorting logic with delegates.
  - Best practices for sorting in C#.

  The documentation was thorough and easy to navigate. It reinforced my understanding of how to write efficient and type-safe sorting code in C#.

  *Reference:* Microsoft Docs. (n.d.). *List<T>.Sort Method*. Retrieved from [https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.sort](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.sort)

I deliberately avoided sources with questionable credibility or overly simplistic explanations, as they could lead to inefficient implementations or misunderstandings. Ensuring the reliability of my sources was crucial to developing a robust and effective sorting system that enhances user experience and maintains game performance.

## Implementation

### What was the process of completing the task? What influenced your decision making?

The development process began with defining the core classes:

- **Item Class:** Representing each inventory item with `Name` (string) and `Value` (int) properties.
- **Inventory Class:** Managing a list of `Item` objects and providing methods for sorting and displaying the inventory.

I initially considered using built-in sorting methods but decided against it to gain a deeper understanding of sorting algorithms and to create a customizable solution. The research led me to TimSort due to its efficiency with partially sorted data and its hybrid nature combining merge sort and insertion sort.

Feedback from peers emphasized the importance of flexibility and code reusability. This influenced me to implement generic sorting methods using delegates, allowing the sorting of any property (`Name`, `Value`, etc.) in both ascending and descending order without duplicating code.

### What creative or technical approaches did you use or try, and how did this contribute to the outcome?

To achieve flexibility, I utilized generics and lambda expressions. The `TimSort` method was designed to accept a key selector function and a sort order parameter. This approach enables sorting by any comparable property and in any order.

```csharp
public void TimSort(Func<Item, IComparable> keySelector, bool ascending = true)
{
    // TimSort implementation
}
```

*Figure 1. TimSort method accepting a key selector and sort order.*

The use of `Func<Item, IComparable>` allows passing a lambda expression that specifies the property to sort by. For example:

- Sorting by name ascending: `inventory.TimSort(item => item.Name, true);`
- Sorting by value descending: `inventory.TimSort(item => item.Value, false);`

In the sorting methods (`InsertionSort` and `Merge`), I incorporated conditional logic to handle both ascending and descending orders based on the `ascending` parameter.

```csharp
while (j >= left && (ascending
    ? keySelector(items[j]).CompareTo(keySelector(temp)) > 0
    : keySelector(items[j]).CompareTo(keySelector(temp)) < 0))
{
    // Shifting logic
}
```

*Figure 2. Conditional comparison for ascending and descending order.*

This design ensures that the same codebase handles all sorting scenarios, enhancing maintainability and scalability.

### Did you have any technical difficulties? If so, what were they and did you manage to overcome them?

One significant challenge was correctly implementing the TimSort algorithm, particularly the merging of sorted runs. Initially, I encountered index out-of-range exceptions due to incorrect calculations of subarray boundaries.

To resolve this, I carefully reviewed the logic for calculating the indices of the left and right subarrays in the `Merge` method:

```csharp
int len1 = m - l + 1, len2 = r - m;
Item[] left = new Item[len1];
Item[] right = new Item[len2];

// Copying data to subarrays
for (int x = 0; x < len1; x++)
    left[x] = items[l + x];
for (int x = 0; x < len2; x++)
    right[x] = items[m + 1 + x];
```

*Figure 3. Corrected subarray initialization in the Merge method.*

I also tested the sorting methods extensively with different dataset sizes and configurations to ensure reliability. Implementing comprehensive logging and step-by-step debugging helped identify and fix the issues.

## Outcome

The final implementation successfully meets all the requirements:

- **Sorting by Name:**
  - Ascending
  - Descending
- **Sorting by Value:**
  - Ascending
  - Descending

The inventory can be sorted flexibly using the same `TimSort` method with different key selectors and sort orders.

```csharp
Console.WriteLine("\nSorted by Name (Ascending):");
inventory.TimSort(item => item.Name, true);
inventory.DisplayInventory();

Console.WriteLine("\nSorted by Value (Descending):");
inventory.TimSort(item => item.Value, false);
inventory.DisplayInventory();
```

*Figure 4. Sorting the inventory by different criteria.*

**Sample Output:**

```
Original Inventory:
Sword: 150
Potion: 50
Shield: 100
Bow: 120
Helmet: 80

Sorted by Name (Ascending):
Bow: 120
Helmet: 80
Potion: 50
Shield: 100
Sword: 150

Sorted by Value (Descending):
Sword: 150
Bow: 120
Shield: 100
Helmet: 80
Potion: 50
```

*Figure 5. Sample output demonstrating the sorting functionality.*

The efficient implementation ensures minimal performance overhead, which is crucial in a gaming environment where responsiveness is key.

## Critical Reflection

### What did or did not work well and why?

**What Worked Well:**

- **Flexibility of the Sorting System:** The use of generics and delegates made the sorting system highly adaptable. It can sort by any property and in any order without additional code modifications.
- **Efficiency of TimSort:** TimSort proved to be efficient for the dataset sizes typical in game inventories, especially when the data is partially sorted.
- **Code Reusability and Maintainability:** The modular design facilitates future enhancements and reduces the likelihood of bugs.

**What Did Not Work Well:**

- **Initial Implementation Errors:** The complexity of TimSort led to initial challenges, such as index calculation errors in the merge function. These issues required significant debugging time.
- **Lack of Unit Testing:** The absence of automated tests made it harder to catch and isolate bugs early in the development process.

### What would you do differently next time?

- **Implement Unit Tests:** Incorporating unit tests would help validate each component's functionality, making it easier to detect and fix errors promptly.
- **Performance Profiling:** I would perform detailed performance analysis to compare TimSort with other sorting algorithms like quicksort or built-in methods to ensure optimal efficiency.
- **Enhanced Features:** Adding functionality such as multi-criteria sorting (e.g., sort by value, then by name) and filtering options to improve the inventory management system's usability.

By reflecting on these aspects, I recognize the importance of thorough testing and performance evaluation in software development, especially in applications where efficiency and reliability are paramount.

## Bibliography

- GeeksforGeeks. (n.d.). *TimSort*. Retrieved from [https://www.geeksforgeeks.org/timsort/](https://www.geeksforgeeks.org/timsort/)

- Microsoft Docs. (n.d.). *List<T>.Sort Method*. Retrieved from [https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.sort](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.sort)

- Cormen, T. H., Leiserson, C. E., Rivest, R. L., & Stein, C. (2009). *Introduction to Algorithms* (3rd ed.). MIT Press.

## Declared Assets

- **Code Implementation:** All code, including classes and sorting algorithms, was written by me, Aimar Goñi, specifically for this project.

- **AI Assistance:** The development journal was created with assistance from AI language model GPT-4.

