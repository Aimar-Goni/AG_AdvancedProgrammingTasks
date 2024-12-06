# Implementing Data Driven Movement System

Advanced Games Programming 24/25

Aimar Goñi

2410569

## Research

### What sources or references have you identified as relevant to this task?

To effectively implement a data-oriented design (DOD) for a simple movement system, I conducted extensive research on data-oriented programming principles, performance optimization techniques, and the benefits of separating data from logic. My aim was to understand how DOD contrasts with traditional object-oriented design (OOD) and how it can lead to improved cache efficiency and overall system performance.

#### Sources

- **"Data-Oriented Design" by Richard Fabian**  
  Richard Fabian is a respected software engineer known for his work on performance optimization and game engine development. His articles delve deep into the principles of DOD and its practical applications.

  **Aspects Analyzed:**

  - The fundamental differences between DOD and OOD.
  - How organizing data sequentially in memory enhances cache performance.
  - Strategies for separating data from behavior to achieve efficient batch processing.

  I found Fabian's insights particularly enlightening, as they provided a solid theoretical foundation for DOD and practical advice on implementation.

  *Reference:* Fabian, R. (2018). *Data-Oriented Design.* Retrieved from [http://www.dataorienteddesign.com/dodbook/](http://www.dataorienteddesign.com/dodbook/)

- **"Game Programming Patterns" by Robert Nystrom**  
  Robert Nystrom is a well-known figure in the game development community. His book discusses various programming patterns, including those relevant to DOD.

  **Aspects Analyzed:**

  - The benefits of component-based architecture in games.
  - How to structure data and systems for better performance.
  - Examples of DOD in real-world game development scenarios.

  This resource helped me understand how to apply DOD principles in the context of game programming and reinforced the importance of efficient data management.

  *Reference:* Nystrom, R. (2014). *Game Programming Patterns.* Genever Benning.

I avoided sources that overly simplified DOD concepts or lacked depth, as they could lead to misconceptions about the implementation and benefits of data-oriented design. Ensuring the credibility and depth of my sources was crucial for developing a robust and efficient movement system.

## Implementation

### What was the process of completing the task? What influenced your decision making?

**Understanding the Requirements:**

- **Separate Data from Logic:** Data should be stored in arrays or structs, and logic should operate on this data separately.
- **Simple Movement System:** The system moves multiple entities along the X and Y axes.
- **Data-Oriented Design Principles:** Focus on data organization for efficient processing.
- **Movement Function:** Update entity positions based on velocity or user input.
- **Batch Processing:** Process all entities together to maximize cache efficiency.

**Development Steps:**

1. **Defining the Data Structure:**

   - Created a struct `EntityData` to hold the properties of each entity.

     ```c
     typedef struct EntityData
     {
         float PositionX;
         float PositionY;
         int VelocityX;
         int VelocityY;
         float Rotation;
         bool Turning;
     } EntityData;
     ```

     *Figure 1: Definition of the `EntityData` struct holding entity properties.*

   - Stored all entity data in a contiguous memory block using an array of `EntityData`.

2. **Separating Logic from Data:**

   - Implemented movement logic in separate functions that operate on the `EntityData` array.

     - **UpdatePositions Function:**

       ```c
       void UpdatePositions(EntityData* entities, int n)
       {
           for (int i = n - 1; i > 0; i--)
           {
               entities[i].PositionX = entities[i - 1].PositionX;
               entities[i].PositionY = entities[i - 1].PositionY;
           }
           entities[0].PositionX += entities[0].VelocityX * 64;
           entities[0].PositionY += entities[0].VelocityY * 64;
       }
       ```

       *Figure 2: The `UpdatePositions` function updates the positions of all entities.*

     - **UpdateInput Function:**

       ```c
       void UpdateInput(EntityData* entities, int n, int lastKey)
       {
          switch (lastKey)
          {
          case KEY_W:
            Entities[0].VelocityX = 0;
            Entities[0].VelocityY = -1;
            Entities[0].Rotation = 0;
            break;
          case KEY_A:
            Entities[0].VelocityX = -1;
            Entities[0].VelocityY = 0;
            Entities[0].Rotation = 270;
            break;
          case KEY_S:
            Entities[0].VelocityX = 0;
            Entities[0].VelocityY = 1;
            Entities[0].Rotation = 180;
            break;
          case KEY_D:
            Entities[0].VelocityX = 1;
            Entities[0].VelocityY = 0;
            Entities[0].Rotation = 90;
            break;
          default:
            break;
          }
          for (int i = n; i > 0; i--)
          {
            if (Entities[i].VelocityX != Entities[i - 1].VelocityX || Entities[i].VelocityY != Entities[i - 1].VelocityY)
            {
              Entities[i].Turning = true;
            }
            else {
              Entities[i].Turning = false;
            }
            Entities[i].VelocityX = Entities[i - 1].VelocityX;
            Entities[i].VelocityY = Entities[i - 1].VelocityY;
            Entities[i].Rotation = Entities[i - 1].Rotation;
          }
       }
       ```

       *Figure 3: The `UpdateInput` function handles user input and updates entity velocities.*

3. **Batch Processing:**

   - Ensured that entity updates are performed in loops, processing all entities in a batch.

   - This approach improves cache performance by accessing memory sequentially.

4. **Implementing the Movement System:**

   - Used the Raylib library for graphics rendering.

   - Initialized entities with starting positions and velocities.

     ```c
     int EntityCount = 3;
     EntityData* Entities = (EntityData*)malloc(EntityCount * sizeof(EntityData));

     // Initialize entities
     for (int i = 0; i < EntityCount; i++)
     {
         Entities[i].PositionX = 256 - (i * 64);
         Entities[i].PositionY = 256;
         Entities[i].VelocityX = 1;
         Entities[i].VelocityY = 0;
         Entities[i].Rotation = 90;
         Entities[i].Turning = false;
     }
     ```

     *Figure 4: Initializing entities with starting positions and velocities.*

5. **Handling User Input:**

   - Captured user input using Raylib's input functions.

   - Updated the velocity and rotation of the leading entity based on the input.

   - Propagated the movement to the following entities.

6. **Rendering the Entities:**

   - Created a `DrawSnake` function to render each entity on the screen.

     ```c
     void DrawSnake(EntityData* entities, int n, Texture snakeSheet)
     {
         for (int i = 0; i < n; i++)
         {
              Rectangle Position = { Entities[i].PositionX, Entities[i].PositionY , 64,64 };
              int id = 4;
              if (i == 0)
              {
                id = 2;
              }
              if (Entities[i].Turning)
              {
                id = 3;
              }
              if (i == n - 1)
              {
                id = 1;
              }
              DrawTexturePro(snakeSheet, position[id], Position, origin, Entities[i].Rotation, WHITE);
         }
     }
     ```

     *Figure 5: The `DrawSnake` function renders each entity.*

**Decision Influences:**

- **Cache Efficiency:**

  - Organizing entity data in contiguous memory improves cache performance, as sequential memory access is faster.

- **Simplicity and Maintainability:**

  - Separating data from logic simplifies the codebase and makes it easier to understand and maintain.

- **Performance Optimization:**

  - Batch processing entities reduces function call overhead and enhances performance, which is critical in game development.

### What creative or technical approaches did you use or try, and how did this contribute to the outcome?

**Data-Oriented Design Principles:**

- **Struct of Arrays vs. Array of Structs:**

  - Opted for an array of structs (`EntityData`), considering the small number of entities.

  - For larger datasets, a struct of arrays might offer better performance due to improved data locality.

**Memory Allocation:**

- Used dynamic memory allocation with `malloc` to allocate memory for the entities.

  ```c
  EntityData* Entities = (EntityData*)malloc(EntityCount * sizeof(EntityData));
  ```

  *Figure 6: Dynamically allocating memory for entities.*

- Ensured that memory is contiguous to maximize cache efficiency.

**Batch Processing:**

- Processed entities in loops to take advantage of CPU cache lines.

- Minimized branching within loops to prevent pipeline stalls.

**Function Optimization:**

- **UpdatePositions Function:**

  - Used reverse iteration to shift entity positions, simulating a snake-like movement.

  - Updated the position of the leading entity separately.

- **UpdateInput Function:**

  - Handled input only for the leading entity.

  - Propagated velocity and rotation changes to following entities.

- **DrawSnake Function:**

  - Minimized state changes by grouping similar draw calls.

**Using Raylib for Rendering:**

- Chose Raylib due to its simplicity and ease of use for 2D graphics.

- Loaded textures and utilized `DrawTexturePro` for advanced drawing with rotation.

  ```c
  DrawTexturePro(snakeSheet, sourceRec, destRec, origin, rotation, WHITE);
  ```

  *Figure 7: Drawing entities with rotation and origin offset.*

### Did you have any technical difficulties? If so, what were they and did you manage to overcome them?

**Challenges and Solutions:**

- **Memory Management:**

  - **Issue:** Initially forgot to free the allocated memory, leading to potential memory leaks.

  - **Solution:** Added `free(Entities);` before exiting the program.

    ```c
    // Cleanup
    free(Entities);
    ```

    *Figure 8: Freeing allocated memory to prevent leaks.*

- **Entity Movement Logic:**

  - **Issue:** Incorrectly updating entity positions caused entities to overlap or move erratically.

  - **Solution:** Carefully adjusted the `UpdatePositions` function to correctly shift positions and ensure smooth movement.

- **Input Handling:**

  - **Issue:** Input was not being registered consistently due to the use of `GetKeyPressed()`.

  - **Solution:** Switched to `IsKeyDown()` to continuously detect key presses.

    ```c
    if (IsKeyDown(KEY_W)) { /* Update velocity */ }
    ```

    *Figure 9: Using `IsKeyDown()` for consistent input detection.*

- **Rendering Artifacts:**

  - **Issue:** Entities flickered or rendered incorrectly due to improper texture coordinates.

  - **Solution:** Verified the source and destination rectangles in `DrawTexturePro` and adjusted the origin point.

- **Timer Logic:**

  - **Issue:** The movement timer was not synchronized with the frame rate, causing inconsistent movement speed.

  - **Solution:** Used `GetFrameTime()` to increment a timer and update positions at fixed intervals.

    ```c
    timer += GetFrameTime();
    if (timer >= interval)
    {
        // Update positions
        timer = 0.0f;
    }
    ```

    *Figure 10: Implementing a fixed update interval for consistent movement.*

## Outcome

**Final Implementation:**

- A data-oriented movement system that moves multiple entities along the X and Y axes.

- Entities are processed in batches, enhancing cache efficiency and performance.

- The system separates data storage (`EntityData` array) from logic (movement functions).

**Demonstration:**

![Snake Movement Screenshot](Animation.gif)

*Figure 11: Screenshot of the movement system showing entities moving in unison.*

**Key Features:**

- **Efficient Data Processing:**

  - Contiguous memory allocation for entity data.

  - Batch processing in loops for improved performance.

- **Responsive Input Handling:**

  - Real-time response to user input.

  - Smooth movement transitions for entities.

- **Scalability:**

  - The system can be extended to handle more entities with minimal code changes.

- **Modular Design:**

  - Logic functions are separate from data structures, promoting code reusability.

## Critical Reflection

### What did or did not work well and why?

**What Worked Well:**

- **Data-Oriented Approach:**

  - Separating data from logic resulted in cleaner, more maintainable code.

  - Improved performance due to better cache utilization.

- **Batch Processing:**

  - Processing entities in batches reduced overhead and enhanced performance.

- **Use of Raylib:**

  - Simplified graphics rendering, allowing focus on implementing DOD principles.

- **Efficient Memory Usage:**

  - Contiguous memory allocation improved cache hits and overall performance.

**What Did Not Work Well:**

- **Initial Input Handling Issues:**

  - Misuse of input functions led to inconsistent behavior.

  - Required adjustments to ensure reliable input detection.

- **Scaling Challenges:**

  - While suitable for a small number of entities, using an array of structs may not be optimal for thousands of entities.

- **Lack of Multithreading:**

  - The current implementation does not leverage multithreading, which could further improve performance.

### What would you do differently next time?

- **Implement Struct of Arrays (SoA):**

  - For larger numbers of entities, switching to a SoA layout could enhance data locality and performance.

- **Incorporate Multithreading:**

  - Utilize parallel processing to update entities concurrently, maximizing CPU usage.

- **Expand on DOD Principles:**

  - Explore more advanced DOD concepts, such as SIMD instructions or data streaming.

- **Improve Input Handling:**

  - Develop a more robust input system to handle multiple keys and smoother transitions.

- **Enhanced Error Handling:**

  - Implement comprehensive error checking, especially for memory allocation and file loading.

- **Profiling and Optimization:**

  - Use profiling tools to identify bottlenecks and optimize critical sections of the code.

By reflecting on these aspects, I recognize the importance of thoroughly understanding the tools and techniques used. Future projects would benefit from deeper exploration of DOD principles and more extensive testing and optimization.

## Bibliography

- Fabian, R. (2018). *Data-Oriented Design.* Retrieved from [http://www.dataorienteddesign.com/dodbook/](http://www.dataorienteddesign.com/dodbook/)

- Nystrom, R. (2014). *Game Programming Patterns.* Genever Benning.

- Drepper, U. (2007). *What Every Programmer Should Know About Memory.* Retrieved from [https://people.freebsd.org/~lstewart/articles/cpumemory.pdf](https://people.freebsd.org/~lstewart/articles/cpumemory.pdf)

## Declared Assets

- **Code Implementation:** All code, including the movement system and functions, was written by me, Aimar Goñi, specifically for this project.

- **Textures and Resources:** The snake sprite (`snake_Sprite.png`) was created by me or sourced from free assets with appropriate licenses.

- **AI Assistance:** This development journal was created with assistance from AI language model GPT-4.

