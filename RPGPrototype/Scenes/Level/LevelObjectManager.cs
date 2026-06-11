using System.Collections.Generic;
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
	
	public LevelObjectManager(DebugMenu debug, LevelData map)
	{
		_debug = debug;
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
		Enemy slime = new Slime(new Vector2(100, 100), new Vector2(30, 30));
		
		_enemies.Add(slime);
		
		_debug.Watch.RegisterWatch("player position", () => Player.Position.ToString());
		_debug.Watch.RegisterWatch("slime postion", () => slime.Position.ToString());
		_debug.Flags.RegisterFlag("Show Hitboxes" ,() => {
			Collision.ShowHitboxes = !Collision.ShowHitboxes;
		});
		_debug.Flags.RegisterFlag("Draw Player" ,() => {
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
	public void Update(GameTime gameTime, Vector2 dir)
	{
		ValidateMovement(dir);
		Player.Update(gameTime);
		foreach (var enemy in _enemies)
		{
			enemy.Update(gameTime);
		}
	}
	/// <summary>
	/// Check for collisions before admitting a move. Ideally works in 2 steps
	/// 1. Get intersecting tiles around the player (broad pass - don't want to check every tile)
	/// 2. Do finer, more precise check on player hitbox with surrounding tiles.
	/// Currently the system does not do #2, will be implemented later.
	/// </summary>
	/// <param name="movementDirection"> The direction of player movement </param>
	public void ValidateMovement(Vector2 movementDirection)
	{
		Vector2 validatedMove = Collision.ValidateMovement(Player.Rect, movementDirection, Player.MovementSpeed);
		Player.Move(validatedMove.X, validatedMove.Y);
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