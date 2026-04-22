using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Scenes;
using RPGPrototype.Log;
using RPGPrototype.Objects;

namespace RPGPrototype.Scenes;

public class LevelScene : Scene
{
	private LevelData _map;
	private LevelCamera _camera;
	private LevelInputManager _inputManager;
	private LevelObjectManager _objectManager;

	private int[,] _collisionGrid;

	private Texture2D _background;

	
	public override void Initialize()
	{
		_map = new LevelData(592, 448);
		_camera = new LevelCamera(_map);
		_objectManager = new LevelObjectManager(_map);
		_inputManager = new LevelInputManager();
		
		base.Initialize();
	}

	public void Reset()
	{
		Initialize();
	}

	public override void LoadContent()
	{
		_background = Content.Load<Texture2D>("maps/Map/simplified/Level_0/_composite");
		_objectManager.LoadContent(Content);
		base.LoadContent();
	}

	public override void Update(GameTime gameTime)
	{
		// Temp
		if (GameController.Exit())
		{
			Reset();
		}

		_inputManager.Update(gameTime);
		_objectManager.Update(gameTime, _inputManager.CurrentMovementDirection);
		_camera.Follow(_objectManager.Player.Position);
		base.Update(gameTime);
	}

	public override void Draw(GameTime gameTime)
	{
		Core.GraphicsDevice.Clear(new Color(32, 40, 78, 255));
		Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _camera.GetTransform());

		Core.SpriteBatch.Draw(_background, Vector2.Zero, Color.White);
		//_inputManager.Draw(gameTime);
		_objectManager.Draw(gameTime);
		
		Core.SpriteBatch.End();
		base.Draw(gameTime);
	}
}