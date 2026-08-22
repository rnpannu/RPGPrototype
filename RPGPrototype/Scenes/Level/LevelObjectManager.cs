
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using RPGPrototype.Objects;
using RPGPrototype.Scenes.Input;
using RPGPrototype.UI.Debug;

namespace RPGPrototype.Scenes;

/// <summary>
/// A class for managing game objects within levels. At initial concept
/// includes things like enemies and potentially the player.
/// </summary>
public class LevelObjectManager
{
	// Todo: Create a better atlas parser that can iterate frames without specifying the coords of each one
	private TextureAtlas _objectAtlas;
	private readonly List<Enemy> _enemies = new();
	private bool _drawPlayer = true;
	
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

	public bool CanSlimeSee { get; private set; }

	public LevelObjectManager(LevelData map)
	{
		Map = map;
		Collision = new CollisionManager(map);

		Initialize();
	}

	public void Initialize()
	{
		Player = new Player(new Vector2(50, 50));
		Player.Initialize();
		Enemy slime = new Slime(new Vector2(250, 150));
		_enemies.Add(slime);
		foreach (Enemy enemy in _enemies)
		{
			enemy.Initialize();
		}
		InitializeDebug();
	}
	
	
	public void InitializeDebug()
	{
		DebugMenu.Instance.Watch.RegisterWatch("player state", () => Player.StateMachine.CurrentState.Name.ToString());
		DebugMenu.Instance.Watch.RegisterWatch("player position", () => Vector2.Round(Player.Position).ToString());
		DebugMenu.Instance.Watch.RegisterWatch("player movement direction", () => Player.MovementDirection.ToString());
		DebugMenu.Instance.Watch.RegisterWatch("player velocity", () => Vector2.Round(Player.Velocity).ToString());
		DebugMenu.Instance.Watch.RegisterWatch("player prospective move relative", () => Vector2.Round(Player.Velocity * Core.DT).ToString());
		DebugMenu.Instance.Watch.RegisterWatch("player prospective move absolute", () => Vector2.Round(Player.Position + (Player.Velocity * Core.DT)).ToString());
		DebugMenu.Instance.Watch.RegisterWatch("player current animation", () => Player.CurrentAnimation.ToString());
		DebugMenu.Instance.Watch.RegisterWatch("player facing direction", () => Vector2.Round(Player.FacingDirection).ToString());
		DebugMenu.Instance.Watch.RegisterWatch("", () => "");
		DebugMenu.Instance.Watch.RegisterWatch("slime postion", () => _enemies[0].Position.ToString());
		DebugMenu.Instance.Watch.RegisterWatch("Slime LOS", () => _enemies[0].HasLOS.ToString());
		DebugMenu.Instance.Watch.RegisterWatch("Current Slime State", () => _enemies[0].StateMachine.CurrentState.Name.ToString());
		DebugMenu.Instance.Watch.RegisterWatch("LOS target", () => _enemies[0].CurrentLOSTarget.ToString());
		
		
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
	/// Advance an entity's velocity and validate the resulting move against level collision
	/// </summary>
	private void ResolveMovement(Entity entity, GameTime gameTime)
	{
		entity.Think(gameTime);
		entity.UpdateVelocity(gameTime);

		float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
		Vector2 prospectiveMove = entity.Velocity * dt;
		Vector2 validatedMove = Collision.ValidateMovement(entity.Hitbox, prospectiveMove);

		Vector2 velocity = entity.Velocity;
		if (validatedMove.X == 0) velocity.X = 0;
		if (validatedMove.Y == 0) velocity.Y = 0;
		entity.Velocity = velocity;

		entity.ApplyMovement(gameTime);
	}
	
	/// <summary>
	/// Update entities and anything else that the object manager is responsible for.
	/// </summary>
	/// <param name="gameTime"></param>
	/// <param name="dir"> The player movement direction given by the input manager </param>
	public void Update(GameTime gameTime, PlayerInput input)
	{
		Player.CurrentInput = input;
		ResolveMovement(Player, gameTime);

		foreach (var enemy in _enemies)
		{
			enemy.CheckLOS(Player.Position, Collision.MapCollisionGrid, Map.TileSize);
			ResolveMovement(enemy, gameTime);
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
