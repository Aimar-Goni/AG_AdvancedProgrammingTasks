
using System;
using System.Collections.Generic;

// The DialogueNode class represents a single dialogue option in the tree
public class DialogueNode
{
    public string text;
    public List<DialogueNode> children;

    public DialogueNode(string text)
    {
        this.text = text;
        this.children = new List<DialogueNode>();
    }

}

// The DialogueSystem class represents the overall dialogue system
public class DialogueSystem
{
    // Task for student: Define the root node of the dialogue system (the starting point of the conversation)
    public DialogueNode First;


    public void BuildDialogueTree()
    {
        // Task for student: Build the dialogue tree structure here, creating connections between nodes

        DialogueNode start = new DialogueNode("Hey, this is the start.");
        DialogueNode option1 = new DialogueNode("How are you?");
        DialogueNode option2 = new DialogueNode("What are you doing?");
        DialogueNode option3 = new DialogueNode("Tell me about the town.");
        DialogueNode option4 = new DialogueNode("Goodbye.");

        // Connecting nodes
        start.children.Add(option1);
        start.children.Add(option2);
        start.children.Add(option3);
        start.children.Add(option4);

        // Adding children to option1
        option1.children.Add(new DialogueNode("I'm fine, thanks!"));
        option1.children.Add(new DialogueNode("Not so great..."));

        // Adding children to option2
        option2.children.Add(new DialogueNode("Just working on a project."));
        option2.children.Add(new DialogueNode("Just hanging out."));

        // Adding children to option3
        DialogueNode townInfo = new DialogueNode("The town is beautiful, with a great park and lots of shops.");
        townInfo.children.Add(new DialogueNode("What about the park?"));
        townInfo.children.Add(new DialogueNode("Are there any good restaurants?"));

        option3.children.Add(townInfo);
        option3.children.Add(new DialogueNode("I don't know much about it."));

        // Adding children to townInfo's first option
        townInfo.children[0].children.Add(new DialogueNode("It's a great place to relax and enjoy nature."));
        townInfo.children[0].children.Add(new DialogueNode("I heard there's a lake nearby."));

        // Adding children to townInfo's second option
        townInfo.children[1].children.Add(new DialogueNode("There's a fantastic Italian place downtown."));
        townInfo.children[1].children.Add(new DialogueNode("The cafe on Main Street is very popular."));

        // Setting the first node
        First = start;
    }

    public DialogueNode ReadNode(DialogueNode currentNode)
    {
        Console.WriteLine($"{currentNode.text}");
        Console.Write($"Choose an option: \n");

        for (int i = 0; i < currentNode.children.Count; i++)
        {
            Console.WriteLine($"{i + 1}: {currentNode.children[i].text}");
        }


        string input = Console.ReadLine();
        int choice;

        bool correct = false;
        while (!correct)
        {

            if (int.TryParse(input, out choice) && choice > 0 && choice <= currentNode.children.Count)
            {
                currentNode = currentNode.children[choice - 1];
                correct = true;
            }
            else
            {
                Console.WriteLine("Invalid choice. Try again.");
                input = Console.ReadLine();
            }
        }
        if (currentNode.children.Count != 0)
            return ReadNode(currentNode);
        return currentNode;
    }

    public void StartDialogue()
    {
        // Task for student: Implement a function to navigate through the dialogue based on player choices


        DialogueNode a = ReadNode(First);
        Console.WriteLine("You got to the end.");
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        DialogueSystem dialogueSystem = new DialogueSystem();

        dialogueSystem.BuildDialogueTree(); // Build the dialogue structure
        dialogueSystem.StartDialogue();     // Start navigating through the dialogue
    }
}
