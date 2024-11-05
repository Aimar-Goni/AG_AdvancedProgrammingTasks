/*
Raylib example file.
This is an example main file for a simple raylib project.
Use this as a starting point or replace it with your code.

For a C++ project simply rename the file to .cpp and re-run the build script 

-- Copyright (c) 2020-2024 Jeffery Myers
--
--This software is provided "as-is", without any express or implied warranty. In no event 
--will the authors be held liable for any damages arising from the use of this software.

--Permission is granted to anyone to use this software for any purpose, including commercial 
--applications, and to alter it and redistribute it freely, subject to the following restrictions:

--  1. The origin of this software must not be misrepresented; you must not claim that you 
--  wrote the original software. If you use this software in a product, an acknowledgment 
--  in the product documentation would be appreciated but is not required.
--
--  2. Altered source versions must be plainly marked as such, and must not be misrepresented
--  as being the original software.
--
--  3. This notice may not be removed or altered from any source distribution.

*/

#include <stdio.h>
#include <stdlib.h>
#include "resource_dir.h"	// utility header for SearchAndSetResourceDir
#include "raylib.h"


Rectangle position[5] = { {0,192,64, 64 }, { 192,128,64, 64 }, { 192,0,64, 64 }, { 0,64,64, 64 }, { 128,64,64, 64 } };
Vector2 origin = { 64 / 2.0f, 64 / 2.0f };

typedef struct EntityData
{
	float PositionX;
	float PositionY;
	int VelocityX;
	int VelocityY;
	float Rotation;
	bool Turning;
}EntityData;

void UpdatePositions(EntityData* entities, int n)
{
	for (int i = n; i > 0; i--)
	{
		entities[i].PositionX = entities[i - 1].PositionX;
		entities[i].PositionY = entities[i - 1].PositionY;
	}
	entities[0].PositionX += entities[0].VelocityX * 64;
	entities[0].PositionY += entities[0].VelocityY * 64;
}

void UpdateInput(EntityData* Entities, int n, int lastKey) {

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

void DrawSnake(EntityData* Entities, int n, Texture snakeSheet) {
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

int main ()
{
	SetConfigFlags(FLAG_VSYNC_HINT | FLAG_WINDOW_HIGHDPI);
	InitWindow(1280, 800, "Hello Raylib");
	SearchAndSetResourceDir("resources");

	// Load a texture from the resources directory
	Texture snakeSheet = LoadTexture("snake_Sprite.png");
	int EntityCount = 3;
	EntityData* Entities = (EntityData*)malloc(EntityCount * sizeof(EntityData));
	if (Entities == NULL) {
		printf("Memory allocation failed!\n");
		return 1;
	}


	Entities[0].PositionX = 256;
	Entities[0].PositionY = 256;
	Entities[0].VelocityX = 1;
	Entities[0].VelocityY = 0;
	Entities[0].Rotation = 0;
	Entities[0].Turning = false;

	Entities[1].PositionX = 198;
	Entities[1].PositionY = 256;
	Entities[1].VelocityX = 1;
	Entities[1].VelocityY = 0;
	Entities[1].Rotation = 0;
	Entities[1].Turning = false;

	Entities[1].PositionX = 134;
	Entities[1].PositionY = 256;
	Entities[1].VelocityX = 1;
	Entities[1].VelocityY = 0;
	Entities[1].Rotation = 0;
	Entities[2].Turning = false;

	int lastKeyPressed = 1;
	float timer = 0.0f;
	const float interval = 1.0f; 

	// game loop
	while (!WindowShouldClose())		// run the loop untill the user presses ESCAPE or presses the Close button on the window
	{
		// drawing
		BeginDrawing();

		// Setup the backbuffer for drawing (clear color and depth buffers)
		ClearBackground(BLACK);
		timer += GetFrameTime();


		DrawSnake(Entities, EntityCount, snakeSheet);

		if (timer >= interval)  
		{
			UpdateInput(Entities, EntityCount, lastKeyPressed);
			UpdatePositions(Entities, EntityCount);
			timer = 0.0f;
		}


	

		int key = GetKeyPressed();  
		if (key != 0)  
		{
			lastKeyPressed = key;  
		}

		// end the frame and get ready for the next one  (display frame, poll input, etc...)
		EndDrawing();
	}

	// cleanup
	// unload our texture so it can be cleaned up
	UnloadTexture(snakeSheet);

	// destory the window and cleanup the OpenGL context
	CloseWindow();
	return 0;
}