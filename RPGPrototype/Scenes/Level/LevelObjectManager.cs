using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using RPGPrototype.Objects;
using RPGPrototype.UI.Debug;

namespace RPGPrototype.Scenes;

/// <summary>
/// A class for managing game objects within levels. At initial concept
/// includes things like enemies and potentially the player.
/// </summary>
public class LevelObjectManager
{
	private DebugMenu _debug;
	
	// Todo: Create a better atlas parser that can iterate frames without specifying the coords of each one
	private TextureAtlas _objectAtlas;
	
	private readonly List<Enemy> _enemies = new();

	private bool _drawPlayer = true;
	
	public LevelObjectManager(LevelData map)
	{
		Map = map;
		Collision = new CollisionManager(map);
		
		Initialize();
	}

	public CollisionManager Collision
	{
		get => field;
		private set => field = value;
	}

	public Player Player
	{
		get => field;
		private set => field = value;
	}

	public LevelData Map
	{
		get => field;
		private set => field = value;
	}

	public void Initialize()
	{
		Player = new Player(new Vector2(50, 50), new Vector2(100, 100));
		Player.Initialize();
		Enemy slime = new Slime(new Vector2(100, 100), new Vector2(30, 30));
		
		_enemies.Add(slime);
		
		InitializeDebug();
	}

	public void InitializeDebug()
	{
		DebugMenu.Instance.Watch.RegisterWatch("player position", () => Player.Position.ToString());
		//_debug.Watch.RegisterWatch("player position", () => Player.Position.ToString());
		DebugMenu.Instance.Watch.RegisterWatch("slime postion", () => _enemies[0].Position.ToString());
		DebugMenu.Instance.Watch.RegisterWatch("player state", () => Player.StateMachine.CurrentState.Name.ToString());
		DebugMenu.Instance.Flags.RegisterFlag("Show Hitboxes" ,() => {
			Collision.ShowHitboxes = !Collision.ShowHitboxes;
		});
		DebugMenu.Instance.Flags.RegisterFlag("Draw Player" ,() => {
			_drawPlayer = !_drawPlayer;
		});
	}

	public void LoadContent(ContentManager content)
	{
		_objectAtlas = TextureAtlas.FromFile(content, "sprites/objectAtlas-definition.xml");
		TextureAtlas playerAtlas = TextureAtlas.FromFile(content, "sprites/playerAtlas-definition.xml");
		TextureAtlas enemyAtlas = TextureAtlas.FromFile(content, "sprites/enemyAtlas-definition.xml");		// for enemy in json:
		
		Player.LoadContent(playerAtlas);
		foreach (var enemy in _enemies)
		{
			enemy.LoadContent(enemyAtlas);
		}
	}
	
	/// <summary>
	/// Update entities and anything else that the object manager is responsible for.
	/// </summary>
	/// <param name="gameTime"></param>
	/// <param name="dir"> The player movement direction given by the input manager </param>
	public void Update(GameTime gameTime, Vector2 inputDirection)
	{
		Player.MovementDirection = inputDirection;
		Player.Update(gameTime);
		Vector2 prospectiveMove = Player.MovementDirection * Player.Velocity * Core.DT;
		//Vector2 prospectiveMove = new Vector2(inputDirection.X * Player.Velocity.X * Core.DT, inputDirection.Y * velocity.Y * Core.DT);
		Vector2 validatedMove = Collision.ValidateMovement(Player.Rect, Player.MovementDirection, prospectiveMove);
		
		Player.Move(validatedMove.X, validatedMove.Y);
		
		foreach (var enemy in _enemies)
		{
			enemy.Update(gameTime, Player.Position);
		}
	}

	public void Draw(GameTime gameTime)
	{
		if (Collision.ShowHitboxes)
		{
			Player.DrawHitBox();
			Collision.Draw(gameTime);
		}

		if (_drawPlayer)
		{
			Player.Draw(gameTime);
		}
		
		foreach (var enemy in _enemies)
		{
			enemy.Draw(gameTime);
		}
	}
	
}