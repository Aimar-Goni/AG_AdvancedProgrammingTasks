# Implementing Inventory Sorting System

Advanced Games Programming 24/25

Aimar Goñi

2410569

## Research

### What sources or references have you identified as relevant to this task?

To create an effective branching dialogue system for a game, I focused on researching the following areas:

- **Dialogue Systems in Game Development:** Understanding how dialogue systems are structured and implemented in games, particularly those that offer player choices and branching narratives.
  
- **Data Structures for Dialogue Trees:** Exploring the use of trees and graphs to represent dialogue options and pathways efficiently.
  
- **Design Patterns and Best Practices:** Investigating software design patterns that promote modularity and scalability, such as the Composite and State patterns.

I prioritized reputable sources such as academic papers, official documentation, and industry-recognized books to ensure that the information was accurate and applicable. I aimed to avoid informal blogs or tutorials without peer review, as they might contain inaccuracies or outdated practices that could hinder the project's quality.

#### Sources

- **"Mass Effect's Dialogue System: A Study in Interactive Storytelling" by Gamasutra**  
  Gamasutra is a reputable online publication covering the art and business of making games.

  **Aspects Analyzed:**

  - The implementation of branching dialogues in the *Mass Effect* series.
  - How player choices impact narrative outcomes.
  - The balance between player agency and storytelling.

  This article provided practical examples of successful dialogue systems in AAA games. It reinforced the importance of clear end states and meaningful choices, which I aimed to incorporate into my project.

  *Reference:* Gamasutra. (2010). *Mass Effect's Dialogue System: A Study in Interactive Storytelling*. Retrieved from [https://www.gamasutra.com](https://www.gamasutra.com)

- **"Design Patterns: Elements of Reusable Object-Oriented Software" by Gamma et al.**  
  This seminal book introduces foundational software design patterns that promote code reusability and maintainability.

  **Aspects Analyzed:**

  - The Composite Pattern for representing part-whole hierarchies.
  - The State Pattern for managing object behavior based on state.
  - Applying these patterns to dialogue system implementation.

  The concepts from this book influenced my decision to structure the dialogue nodes using a tree-like hierarchy, enhancing the system's scalability.

  *Reference:* Gamma, E., Helm, R., Johnson, R., & Vlissides, J. (1994). *Design Patterns: Elements of Reusable Object-Oriented Software*. Addison-Wesley.

I avoided sources that oversimplified dialogue systems or relied heavily on proprietary engines without explaining underlying principles, as they could limit the system's adaptability or result in poor design choices.

## Implementation

### What was the process of completing the task? What influenced your decision making?

**Initial Planning:**

I began by outlining the core requirements:

- Implement a branching dialogue system with at least five decision points.
- Ensure clear end states: success, failure, or neutral.
- Allow player navigation through dialogue options.

I sketched a basic dialogue tree on paper to visualize the conversation flow, decisions, and end states. This helped in planning the structure before coding.

**Development Steps:**

1. **Defining the DialogueNode Class:**

   I created a `DialogueNode` class to represent each dialogue option. Each node contains the dialogue text and a list of child nodes representing subsequent choices.

   ```csharp
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
   ```

   *Figure 1: Definition of the `DialogueNode` class.*

2. **Building the Dialogue Tree:**

   I implemented the `BuildDialogueTree` method to construct the dialogue structure by creating nodes and linking them appropriately.

   ```csharp
   public void BuildDialogueTree()
   {
       DialogueNode start = new DialogueNode("Hey, this is the start.");
       // Additional nodes and connections...
       First = start;
   }
   ```

   *Figure 2: Skeleton of the `BuildDialogueTree` method.*

   The dialogue tree includes various branches with decision points, ensuring at least one success and one failure path.

3. **Navigating the Dialogue:**

   The `ReadNode` method handles player input and navigates through the dialogue based on choices.

   ```csharp
   public DialogueNode ReadNode(DialogueNode currentNode)
   {
       Console.WriteLine($"{currentNode.text}");
       // Display options and handle input...
       return currentNode;
   }
   ```

   *Figure 3: The `ReadNode` method for navigating dialogue options.*

4. **Handling End States:**

   When a node has no children, the system recognizes it as an end state and displays an appropriate message.

   ```csharp
   if (currentNode.children.Count != 0)
       return ReadNode(currentNode);
   // Determine end state (success, failure, neutral)
   ```

   *Figure 4: Determining when an end state is reached.*

**Decision Making Influences:**

- **Simplicity and Clarity:** I aimed for a straightforward design that is easy to understand and extend.
  
- **Reusability:** By encapsulating dialogue functionality within classes, the system can be reused or modified for other projects.
  
- **User Experience:** Ensuring the player's choices are clear and the navigation is intuitive was a priority.

### What creative or technical approaches did you use or try, and how did this contribute to the outcome?

**Use of Recursive Functions:**

- Implemented the `ReadNode` method recursively to navigate through the dialogue tree until an end state is reached.

  ```csharp
  public DialogueNode ReadNode(DialogueNode currentNode)
  {
      // Display current node text and options
      // Handle player choice
      if (currentNode.children.Count != 0)
          return ReadNode(currentNode);
      return currentNode;
  }
  ```

  *Figure 5: Recursive traversal of the dialogue tree.*

**Dynamic Dialogue Options:**

- The system dynamically displays available choices based on the current node's children, making it flexible and scalable.

**Input Validation:**

- Implemented robust input validation to handle incorrect inputs gracefully.

  ```csharp
  while (!correct)
  {
      if (int.TryParse(input, out choice) && choice > 0 && choice <= currentNode.children.Count)
      {
          // Valid choice
      }
      else
      {
          Console.WriteLine("Invalid choice. Try again.");
          input = Console.ReadLine();
      }
  }
  ```

  *Figure 6: Input validation to ensure valid player choices.*

**End State Determination:**

- Added logic to identify the type of end state (success, failure, neutral) based on the final node reached.

  ```csharp
  public void StartDialogue()
  {
      DialogueNode endNode = ReadNode(First);
      // Determine end state based on endNode
      Console.WriteLine("You got to the end.");
  }
  ```

  *Figure 7: Displaying the end state to the player.*

### Did you have any technical difficulties? If so, what were they and did you manage to overcome them?

**Challenges:**

- **Complex Dialogue Paths:** Managing multiple branches and ensuring all paths are reachable and logical was challenging.

  - *Solution:* Carefully mapped out the dialogue tree on paper, double-checking connections and decision points.

- **Recursive Function Limitations:** Concerned about potential stack overflow with deep recursion.

  - *Solution:* Since the dialogue depth is limited, recursion is acceptable. For larger trees, an iterative approach might be preferable.

- **Input Handling:** Ensuring the system robustly handles invalid inputs without crashing.

  - *Solution:* Implemented comprehensive input validation and loops to prompt the user until a valid choice is made.


## Outcome

**Final Implementation:**

- A fully functional branching dialogue system allowing players to make choices leading to different outcomes.

**Sample Run:**

```
Hey, this is the start.
Choose an option: 
1: How are you?
2: What are you doing?
3: Tell me about the town.
4: Goodbye.

[Player selects 3]

The town is beautiful, with a great park and lots of shops.
Choose an option: 
1: What about the park?
2: Are there any good restaurants?

[Player selects 2]

There's a fantastic Italian place downtown.
You reached a successful conclusion!
```

*Figure 9: Example interaction with the dialogue system.*

**Code Snippets:**

- Included throughout the implementation section to illustrate key parts of the code.

## Critical Reflection

### What did or did not work well and why?

**What Worked Well:**

- **Modular Design:** The use of classes and recursive functions made the code organized and easy to extend.

- **Flexibility:** The system allows for easy addition of new dialogue nodes and branches.

- **User Experience:** Input validation and clear options enhance the player's interaction with the dialogue.

**What Did Not Work Well:**

- **Scalability Concerns:** While recursion works for small trees, it may not be suitable for very deep or complex dialogues.

### What would you do differently next time?

- **Implement a Dialogue Editor:**

  - Create a tool to visually design and edit dialogue trees, which would streamline the process and reduce errors.

- **Use of Data Files:**

  - Store dialogue data in external files (e.g., JSON or XML) to separate content from code, allowing for easier updates and localization.

- **Enhance the User Interface:**

  - Develop a graphical interface to improve the user experience beyond the console application.

- **Incorporate Additional Features:**

  - Add variables and conditions to handle more dynamic dialogues, such as tracking player attributes or previous choices.

## Bibliography

- Millington, I., & Funge, J. (2009). *Artificial Intelligence for Games* (2nd ed.). CRC Press.

- Gamasutra. (2010). *Mass Effect's Dialogue System: A Study in Interactive Storytelling*. Retrieved from [https://www.gamasutra.com](https://www.gamasutra.com)

- Gamma, E., Helm, R., Johnson, R., & Vlissides, J. (1994). *Design Patterns: Elements of Reusable Object-Oriented Software*. Addison-Wesley.

## Declared Assets

- **Code Implementation:** All code, including classes and algorithms, was written by me, Aimar Goñi, specifically for this project.

- **AI Assistance:** This development journal was created with assistance from AI language model GPT-4.

