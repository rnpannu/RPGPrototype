using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using RenderingLibrary.Graphics;
using RPGPrototype.Objects;

namespace RPGPrototype.Scenes;

/// <summary>
/// A class to control inputs and 
/// </summary>
public class LevelInputManager
{

	private Matrix _transform;
	private Vector2 _movementDir;
	private Vector2 _cameraPosition;


	public LevelInputManager()
	{
		Initialize();
	}

	public Vector2 CurrentMovementDirection
	{
		get => _movementDir;
		private set => _movementDir = value;
	}
	public Matrix Transform
	{
		get => _transform;
		private set => _transform = value;
	}
	


	public void Initialize()
	{

	}

	public void Update(GameTime gameTime)
	{
		_movementDir = Vector2.Zero;
		
		if (GameController.MoveUp()) _movementDir.Y--;
		if (GameController.MoveDown()) _movementDir.Y++;
		if (GameController.MoveLeft()) _movementDir.X--;
		if (GameController.MoveRight()) _movementDir.X++;
		
		if (_movementDir != Vector2.Zero)
		{
			_movementDir.Normalize();
		}
	}

	public void Draw(GameTime gameTime)
	{
	}
}