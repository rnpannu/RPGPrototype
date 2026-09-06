using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using RenderingLibrary.Graphics;
using RPGPrototype.Objects;
using RPGPrototype.Scenes.Input;

namespace RPGPrototype.Scenes;

/// <summary>
/// A class to control inputs and
/// </summary>
public class LevelInputManager
{
	public LevelInputManager()
	{
		Initialize();
	}

	public void Initialize()
	{

	}

	public PlayerInput GetPlayerInput(GameTime gameTime)
	{
        //LastMovementDirection = _movementDir;
        Vector2 movementDir = Vector2.Zero;
        bool playerJump = false;

		if (GameController.MoveUp()) movementDir.Y--;
		if (GameController.MoveDown()) movementDir.Y++;
		if (GameController.MoveLeft()) movementDir.X--;
        if (GameController.MoveRight()) movementDir.X++;
        if (GameController.MoveJump()) playerJump = true;

		if (movementDir != Vector2.Zero)
		{
			movementDir.Normalize();
		}

		bool esc = false;
		bool debug = false;
		if (GameController.Exit())
		{
			esc = true;
		}

		if (GameController.ToggleDebug())
		{
			debug = true;
		}
		return new PlayerInput(movementDir, playerJump, false, esc);
	}

	/*public InventoryInput GetInventoryInput(GameTime gameTime)
	{

	}*/

	public void Draw(GameTime gameTime)
	{

	}
}
