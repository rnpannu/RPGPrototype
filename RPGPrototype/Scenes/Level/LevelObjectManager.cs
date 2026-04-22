using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using RPGPrototype.Log;
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
	private Dictionary<int, Color> _collisionColors = new Dictionary<int, Color>();
	private Rectangle _nextTravelCell;
	private Color _nextTravelCellColor;

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
		_collisionColors.Add(0, Color.White);
		_collisionColors.Add(1, Color.Red);
	}

	public void LoadContent(ContentManager content)
	{
		_objectAtlas = TextureAtlas.FromFile(content, "sprites/objectAtlas-definition.xml");
		Player.LoadContent(_objectAtlas);
	}

	public void Update(GameTime gameTime, Vector2 dir)
	{
		int currentTravelCellX = (int)Player.Position.X / _map.TileSize;
		int currentTravelCellY = (int)Player.Position.Y / _map.TileSize;

		int nextTravelCellX = (currentTravelCellX + (int)Math.Ceiling(dir.X));
		int nextTravelCellY = (currentTravelCellY + (int)Math.Ceiling(dir.Y));

		if (dir != Vector2.Zero)
		{
			_nextTravelCell = new Rectangle(nextTravelCellX, nextTravelCellY
				, _map.TileSize, _map.TileSize);
			_nextTravelCellColor = _collisionColors[_collisionGrid[_nextTravelCell.Y, _nextTravelCell.X]];
			Logger.Log(_nextTravelCell.Y.ToString() +  ", " + _nextTravelCell.X.ToString(), LogLevel.Info);
		}
		else
		{
			_nextTravelCell = Rectangle.Empty;
		}

		_collisionGrid = _collisionGrid;
		if ((dir != Vector2.Zero) && (_collisionGrid[_nextTravelCell.Y, _nextTravelCell.X] == 1))
		{
			dir = Vector2.One;
		}
		else
		{
			Player.Position += ((dir * Core.DT * Player.Speed));
		}

		Player.Position = Vector2.Clamp(Player.Position, new Vector2(Player.Sprite.Origin.X, Player.Sprite.Origin.Y),
			new Vector2(_map.Width - Player.Sprite.Origin.X, _map.Height - Player.Sprite.Origin.Y));
		Player.Update(gameTime);
	}

	public void HighlightMovementCell()
	{
		Texture2D pixelTexture;
		pixelTexture = new Texture2D(Core.GraphicsDevice, 1, 1);
		pixelTexture.SetData(new[] { Color.White });
		Rectangle scaled = new Rectangle(_nextTravelCell.X * _map.TileSize, _nextTravelCell.Y * _map.TileSize,
			_map.TileSize, _map.TileSize);
	
		Core.SpriteBatch.Draw(pixelTexture, scaled, _nextTravelCellColor * 0.5f);
	}

	public void Draw(GameTime gameTime)
	{
		Player.Draw(gameTime);
		if (!_nextTravelCell.IsEmpty)
		{
			HighlightMovementCell();
		}
	}
}