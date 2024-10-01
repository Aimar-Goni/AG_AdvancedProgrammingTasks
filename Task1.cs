
using System;
using System.Collections.Generic;
using System.Linq;

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



    public void InsertionSort<TKey>(Func<Item, TKey> keySelector, bool ascending, int left, int right) where TKey : IComparable
    {
        for (int i = left + 1; i <= right; i++)
        {
            Item temp = items[i];
            int j = i - 1;
            while (j >= left &&
            (ascending ? keySelector(items[j]).CompareTo(keySelector(temp)) > 0 :
            keySelector(items[j]).CompareTo(keySelector(temp)) < 0))
            {
                items[j + 1] = items[j];
                j--;
            }
            items[j + 1] = temp;
        }
    }

    public void Merge(bool ascending,int l, int m, int r, Func<Item, IComparable> keySelector)
    {
        int len1 = m - l + 1, len2 = r - m;
        Item[] left = new Item[len1];
        Item[] right = new Item[len2];
        for (int x = 0; x < len1; x++)
            left[x] = items[l + x];
        for (int x = 0; x < len2; x++)
            right[x] = items[m + 1 + x];

        int i = 0;
        int j = 0;
        int k = l;

        while (i < len1 && j < len2)
        {
            if (ascending ? keySelector(left[i]).CompareTo(keySelector(right[j])) < 0 : keySelector(left[i]).CompareTo(keySelector(right[j])) > 0)
            {
                items[k] = left[i];
                i++;
            }
            else
            {
                items[k] = right[j];
                j++;
            }
            k++;
        }

        while (i < len1)
        {
            items[k] = left[i];
            k++;
            i++;
        }

        while (j < len2)
        {
            items[k] = right[j];
            k++;
            j++;
        }

    }

    public void TimSort(Func<Item, IComparable> keySelector, bool ascending = true)
    {
        int RUN = 4;
        int listSize = items.Count;
        for (int i = 0; i < listSize; i += RUN)
            InsertionSort(keySelector, ascending, i, Math.Min((i + RUN - 1), (listSize - 1)));

        for (int size = RUN; size < listSize + RUN; size = 2 * size)
        {

            for (int left = 0; left < listSize; left += 2 * size)
            {
                int mid = left + size - 1;
                int right = Math.Min((left + 2 * size - 1), (listSize - 1));

                if (mid < right)
                    Merge(ascending,left, mid, right, keySelector);
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