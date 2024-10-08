
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text;
using System.Xml.Linq;

public class Item
{
    public string Name { get; set; }
    public int Value { get; set; }

    public Item(string name, int value)
    {
        Name = name;
        Value = value;
    }
}

public class Inventory
{
    public List<Item> items = new List<Item>();

    public Inventory()
    {
        // Adding some example items
        items.Add(new Item("Sword", 150));
        items.Add(new Item("Potion", 50));
        items.Add(new Item("Shield", 100));
        items.Add(new Item("Bow", 120));
        items.Add(new Item("Helmet", 80));
    }

    // HOW IT WORKS 


    // Performs Insertion Sort on a portion of the items list.
    public void InsertionSort<TKey>(Func<Item, TKey> keySelector, bool ascending, int left, int right) where TKey : IComparable
{
    // Loop through each element in the specified range (left to right)
        for (int i = left + 1; i <= right; i++)
    {
        Item temp = items[i]; // Store the current item
        int j = i - 1; // Initialize j to point to the previous item

        // Shift elements to the right until the correct position for temp is found
            while (j >= left &&
            (ascending ? keySelector(items[j]).CompareTo(keySelector(temp)) > 0 :
            keySelector(items[j]).CompareTo(keySelector(temp)) < 0))
            {
                items[j + 1] = items[j]; // Shift item to the right
                j--; // Move to the next item on the left
            }
            items[j + 1] = temp; // Place temp in its correct position
        }
    }

    // Merges two sorted subarrays of items into a single sorted array.
    public void Merge(bool ascending, int l, int m, int r, Func<Item, IComparable> keySelector)
    {
        // Determine the lengths of the left and right subarrays
        int len1 = m - l + 1, len2 = r - m;
        Item[] left = new Item[len1]; // Left subarray
        Item[] right = new Item[len2]; // Right subarray

        // Copy data to the left subarray
        for (int x = 0; x < len1; x++)
            left[x] = items[l + x];

        // Copy data to the right subarray
        for (int x = 0; x < len2; x++)
            right[x] = items[m + 1 + x];

        int i = 0; // Initial index of the left subarray
        int j = 0; // Initial index of the right subarray
        int k = l; // Initial index of the merged array

        // Merge the left and right subarrays back into items
        while (i < len1 && j < len2)
        {
            // Compare elements and merge accordingly
            if (ascending ? keySelector(left[i]).CompareTo(keySelector(right[j])) < 0 : keySelector(left[i]).CompareTo(keySelector(right[j])) > 0)
            {
                items[k] = left[i]; // Take from left subarray
                i++;
            }
            else
            {
                items[k] = right[j]; // Take from right subarray
                j++;
            }
            k++; // Move to the next position in the merged array
        }

        // Copy remaining elements of left subarray if any
        while (i < len1)
        {
            items[k] = left[i];
            k++;
            i++;
        }

        // Copy remaining elements of right subarray if any
        while (j < len2)
        {
            items[k] = right[j];
            k++;
            j++;
        }
    }

    // Performs TimSort, a hybrid sorting algorithm derived from Merge Sort and Insertion Sort.
    public void TimSort(Func<Item, IComparable> keySelector, bool ascending = true)
    {
        int RUN = 2; // Size of the subarrays to be sorted using Insertion Sort
        int listSize = items.Count; // Total number of items to sort

        // Sort individual subarrays of size RUN using Insertion Sort
        for (int i = 0; i < listSize; i += RUN)
            InsertionSort(keySelector, ascending, i, Math.Min((i + RUN - 1), (listSize - 1)));

        // Start merging the sorted subarrays
        for (int size = RUN; size < listSize + RUN; size = 2 * size)
        {
            // Iterate through the array in chunks of size
            for (int left = 0; left < listSize; left += 2 * size)
            {
                int mid = left + size - 1; // Middle index
                int right = Math.Min((left + 2 * size - 1), (listSize - 1)); // Right boundary

                // Merge the two sorted subarrays
                if (mid < right)
                    Merge(ascending, left, mid, right, keySelector);
            }
        }
    }
    




    // Display the sorted inventory
    public void DisplayInventory()
    {
        foreach (var itema in items)
        {
            Console.WriteLine(itema.Name + ": " + itema.Value);
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Inventory inventory = new Inventory();

        Console.WriteLine("Original Inventory:");
        inventory.DisplayInventory();
        
        Console.WriteLine("\nSorted by Name (Ascending):");
        inventory.TimSort(item => item.Name, true); // Sort by Name in ascending order
        inventory.DisplayInventory();

        Console.WriteLine("\nSorted by Name (Descending):");
        inventory.TimSort(item => item.Name, false); // Sort by Name in descending order
        inventory.DisplayInventory();

       
        Console.WriteLine("\nSorted by Value (Descending):");
        inventory.TimSort(item => item.Value, false); // Sort by Value in descending order
        inventory.DisplayInventory(); 

        Console.WriteLine("\nSorted by Value (Ascending):");
        inventory.TimSort(item => item.Value, true); // Sort by Value in ascending order
        inventory.DisplayInventory();
        
    }
}
