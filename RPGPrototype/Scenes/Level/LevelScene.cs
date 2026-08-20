using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Scenes;
using RPGPrototype.Log;
using RPGPrototype.Objects;
using RPGPrototype.Objects.States;
using RPGPrototype.Scenes.States;
using RPGPrototype.UI.Debug;

namespace RPGPrototype.Scenes;

public class LevelScene : Scene
{
	private Texture2D _background;

	public StateMachine StateMachine
	{
		get => field;
		private set => field = value;
	}

	public LevelInputManager InputManager
	{
		get => field;
		private set => field = value;
	}

	public LevelObjectManager ObjectManager
	{
		get => field;
		private set => field = value;
	}

	public LevelData Map
	{
		get => field;
		private set => field = value;
	}

	public LevelCamera Camera
	{
		get => field;
		private set => field = value;
	}

	public override void Initialize()
	{
		Map = new LevelData(592, 448);
		Camera = new LevelCamera(Map);
		ObjectManager = new LevelObjectManager(Map);
		InputManager = new LevelInputManager();
		
		List<State> states = [
			new LevelTraversalState(this),
			new LevelPausedState(this),
			new LevelInventoryState(this)
		];
		StateMachine = new StateMachine(states);
		
		AssignEvents();
		base.Initialize();
	}

	public void AssignEvents()
	{

	}

	public void Reset()
	{
		DebugMenu.Instance.Clear();
		Initialize();
	}

	public override void LoadContent()
	{
		_background = Content.Load<Texture2D>("maps/Map/simplified/Level_0/_composite");
		ObjectManager.LoadContent(Content);
		
		base.LoadContent();
	}

	public override void UnloadContent()
	{
		base.UnloadContent();
	}
	
	public override void Update(GameTime gameTime)
	{
		StateMachine.CurrentState.Update(gameTime);
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
		Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: Camera.GetTransform());

		Core.SpriteBatch.Draw(_background, Vector2.Zero, Color.White);
		
		//ObjectManager.Draw(gameTime);
		StateMachine.CurrentState.Draw(gameTime);
		
		Core.SpriteBatch.End(); // - End Game Objects ---------------
		
		
		Core.SpriteBatch.Begin(); // - UI ----------
		DebugMenu.Instance.Draw(gameTime);
		Core.SpriteBatch.End();  // - End UI -------
		base.Draw(gameTime);
	}
}