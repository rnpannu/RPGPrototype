using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Scenes;
using RPGPrototype.Log;
using RPGPrototype.Objects;
using RPGPrototype.UI.Debug;

namespace RPGPrototype.Scenes;

public class LevelScene : Scene
{
	private DebugMenu _debug;
	private LevelData _map;
	private LevelCamera _camera;
	private LevelInputManager _inputManager;
	private LevelObjectManager _objectManager;

	private int[,] _collisionGrid;

	private Texture2D _background;

	
	public override void Initialize()
	{
		_debug = new DebugMenu();
		_map = new LevelData(592, 448);
		_camera = new LevelCamera(_map);
		_objectManager = new LevelObjectManager(_debug, _map);
		_inputManager = new LevelInputManager();
		
		AssignEvents();
		
		base.Initialize();
	}

	public void AssignEvents()
	{
		_inputManager.MovementDirectionChange += _objectManager.Player.UpdateAnimation;
	}

	public void Reset()
	{
		Initialize();
	}

	public override void LoadContent()
	{
		_background = Content.Load<Texture2D>("maps/Map/simplified/Level_0/_composite");
		_objectManager.LoadContent(Content);
		_debug.LoadContent(Content.Load<SpriteFont>("File")); // Change to better name
		base.LoadContent();
	}

	public override void UnloadContent()
	{
		_inputManager.MovementDirectionChange -= _objectManager.Player.UpdateAnimation;
		base.UnloadContent();
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
		
		_debug.Update(gameTime);
		base.Update(gameTime);
	}

	public override void Draw(GameTime gameTime)
	{
		// Ideally would have 3 spritebatch begin/ends.
		// 1. Background
		// 2. Game objects
		// 3. UI
		// Actually, ideally it would only be one spritebatch begin/end cycle for the entire game,
		// but this is a common convention.
		Core.GraphicsDevice.Clear(new Color(32, 40, 78, 255));
		// - Game Objects ---------------
		Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _camera.GetTransform());

		Core.SpriteBatch.Draw(_background, Vector2.Zero, Color.White);
		_objectManager.Draw(gameTime);
		
		Core.SpriteBatch.End(); // - End Game Objects ---------------
		
		
		Core.SpriteBatch.Begin(); // - UI ----------
		
		_debug.Draw(gameTime);
		
		Core.SpriteBatch.End();  // - End UI -------
		base.Draw(gameTime);
	}
}