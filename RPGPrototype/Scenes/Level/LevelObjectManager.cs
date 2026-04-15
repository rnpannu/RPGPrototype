using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using RPGPrototype.Objects;

namespace RPGPrototype.Scenes;

/// <summary>
/// A class for managing game objects within levels. At initial concept
/// includes things like enemies and potentially the player.
/// </summary>
public class LevelObjectManager
{
	private Player _player;

	private LevelData _map;
	// Todo: Create a better atlas parser that can iterate frames without specifying the coords of each one
	private TextureAtlas _objectAtlas;
	private int[,] _collisionGrid;
	
	public LevelObjectManager(LevelData map)
	{
		_map = map;
		Initialize();
	}
	public Player Player
	{
		get => _player;
		private set => _player = value; 
	}
	public void Initialize()
	{
		Player = new Player();
		_collisionGrid = LevelUtility.LoadIntGrid("Collision.csv", "Level_0");
	}

	public void LoadContent(ContentManager content)
	{
		_objectAtlas = TextureAtlas.FromFile(content, "sprites/objectAtlas-definition.xml");
		Player.LoadContent(_objectAtlas);
	}

	public void Update(GameTime gameTime, Vector2 dir)
	{
		Player.Position += ((dir * Core.DT * Player.Speed));
		Player.Position = Vector2.Clamp(Player.Position, new Vector2(Player.Sprite.Origin.X, Player.Sprite.Origin.Y), new Vector2(_map.Width - Player.Sprite.Origin.X, _map.Height - Player.Sprite.Origin.Y));
		Player.Update(gameTime);

		// nextTravelCell = Player.Position + Player.Direction * cell_width 
	}

	public void Draw(GameTime gameTime)
	{
		Player.Draw(gameTime);
	}
}