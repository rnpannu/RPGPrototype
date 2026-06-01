using System;
using System.Collections.Generic;
using System.Diagnostics;
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

	private List<Rectangle> _intersections;
	public LevelObjectManager(LevelData map)
	{
		Map = map;
		Initialize();
	}

	public Player Player
	{
		get => _player;
		private set => _player = value;
	}

	public LevelData Map
	{
		get => _map;
		set => _map = value;
	}

	public void Initialize()
	{
		Player = new Player();
		_collisionGrid = LevelUtility.LoadIntGrid("Collision.csv", "Level_0");
		_collisionColors.Add(0, Color.White);
		_collisionColors.Add(1, Color.Red);

		_intersections = new();
	}

	public void LoadContent(ContentManager content)
	{
		_objectAtlas = TextureAtlas.FromFile(content, "sprites/objectAtlas-definition.xml");
		Player.LoadContent(_objectAtlas);
	}

	public void Update(GameTime gameTime, Vector2 dir)
	{
		ValidateMovement(dir);
		Player.Update(gameTime);
	}


	public void ValidateMovement(Vector2 movementDirection)
	{ 
		// 1. Get intersecting tiles around the player (broad pass - don't want to check every tile)
		// 2. Do finer, more precise check on player hitbox with surrounding tiles.
		_intersections.Clear();
		
		int tileSize = Map.TileSize;
		
		int leftTile = Player.Hitbox.Left / tileSize;
		int rightTile = Player.Hitbox.Right / tileSize;
		int topTile = Player.Hitbox.Top / tileSize;
		int bottomTile = Player.Hitbox.Bottom / tileSize;

		if (leftTile < 0) leftTile = 0;
		if (rightTile > Map.Width) rightTile = Map.Width; // - tileSize?
		if (topTile < 0) topTile = 0;
		if (bottomTile > Map.Height) bottomTile = Map.Height;
		
		for (int i = leftTile; i <= rightTile; i++)
		{
			for (int j = topTile; j <= bottomTile; j++)
			{
				Rectangle currentTile = new Rectangle(i, j, tileSize, tileSize);
				_intersections.Add(currentTile);
			}
		}
		
		float proposedMoveX = Player.Position.X;
		
		if (movementDirection.X != 0)
		{
			proposedMoveX = Player.Position.X + (movementDirection.X * Player.Speed.X * Core.DT);
		}

		foreach (var tile in _intersections)
		{
			if (_collisionGrid[tile.Y, tile.X] == 1)
			{
				if (movementDirection.X > 0.0f) // Moving right, lock player x to their width from the tile
				{
					proposedMoveX = (tile.X * tileSize) - Player.Hitbox.Width; 
				} else if (movementDirection.X < 0.0f) // Moving left, lock player to right side of tile (+1 tile width)
				{
					proposedMoveX = ((tile.X + 1) * tileSize); 
				}
			}
		}
		
		float proposedMoveY = Player.Position.Y;
		
		if (movementDirection.Y != 0)
		{
			proposedMoveY = Player.Position.Y + (movementDirection.Y * Player.Speed.Y * Core.DT);
		}
		
		foreach (var tile in _intersections)
		{
			if (_collisionGrid[tile.Y, tile.X] == 1)
			{
				if (movementDirection.Y > 0.0f) // Moving down, lock player Y to one sprite height above
				{
					proposedMoveY = (tile.Y * tileSize) - Player.Hitbox.Height;
				} else if (movementDirection.Y < 0.0f) // Moving left, lock player to tile bottom (+1 tile height)
				{
					proposedMoveY = ((tile.Y + 1) * tileSize); 
				}
			}
		}
		
		//Player.Position += ((movementDirection * Core.DT * Player.Speed));
		Player.Position = new Vector2(proposedMoveX, proposedMoveY);

		Player.Position = Vector2.Clamp(Player.Position, new Vector2(Player.Sprite.Origin.X, Player.Sprite.Origin.Y),
			new Vector2(Map.Width - Player.Sprite.Origin.X, Map.Height - Player.Sprite.Origin.Y));
		/*
		int currentTravelCellX = (int)Player.Position.X / Map.TileSize;
		int currentTravelCellY = (int)Player.Position.Y / Map.TileSize;
		
		int nextTravelCellX = (currentTravelCellX + (int)Math.Ceiling(movementDirection.X));
		int nextTravelCellY = (currentTravelCellY + (int)Math.Ceiling(movementDirection.Y));

		if (movementDirection != Vector2.Zero)
		{
			_nextTravelCell = new Rectangle(nextTravelCellX, nextTravelCellY
				, Map.TileSize, Map.TileSize);
			/*_nextTravelCellColor = _collisionColors[_collisionGrid[_nextTravelCell.Y, _nextTravelCell.X]];
			Logger.Log(_nextTravelCell.Y.ToString() +  ", " + _nextTravelCell.X.ToString(), LogLevel.Info);#1#
		}
		else
		{
			_nextTravelCell = Rectangle.Empty;
		}
		
		if ((movementDirection != Vector2.Zero) && (_collisionGrid[_nextTravelCell.Y, _nextTravelCell.X] == 1))
		{
			movementDirection = Vector2.One;
		}
		else
		{
			Player.Position += ((movementDirection * Core.DT * Player.Speed));
		}
		*/
		/*Player.Position += ((movementDirection * Core.DT * Player.Speed));


		Player.Position = Vector2.Clamp(Player.Position, new Vector2(Player.Sprite.Origin.X, Player.Sprite.Origin.Y),
			new Vector2(Map.Width - Player.Sprite.Origin.X, Map.Height - Player.Sprite.Origin.Y));*/
	}
	

	public void Draw(GameTime gameTime)
	{
		int tileSize = Map.TileSize;
		Texture2D rectangleTexture = new Texture2D(Core.GraphicsDevice, 1, 1);
		rectangleTexture.SetData(new Color[] {new (255, 0, 0, 255)});

		for (int i = 0; i < Map.Height / tileSize; i++)
		{
			for (int j = 0; j < Map.Width / tileSize; j++)
			{
				Core.SpriteBatch.Draw(rectangleTexture, new Rectangle((int) j * tileSize, (int) i * tileSize,
					tileSize, tileSize),
					_collisionColors[_collisionGrid[i, j]] * 0.3f);
			}
		}
		foreach (var rect in _intersections)
		{

			Core.SpriteBatch.Draw(rectangleTexture, new Rectangle((int) rect.X * tileSize, (int)rect.Y * tileSize, tileSize, tileSize), Color.White);
		}
		Player.DrawHitBox();


		Player.Draw(gameTime);
		if (!_nextTravelCell.IsEmpty)
		{
			HighlightMovementCell();
		}
	}

	public void DrawCollisionGrid()
	{
		foreach (var tile in _collisionGrid)
		{
			
		}
	}
	// TODO: Create better rectangle visualization assistance
	public void DrawRectHollow(Rectangle rect)
	{
		Texture2D rectangleTexture = new Texture2D(Core.GraphicsDevice, 1, 1);
		rectangleTexture.SetData(new Color[] {new (255, 0, 0, 255)});
		int thickness = 20;
		Core.SpriteBatch.Draw(
			rectangleTexture,
			new Rectangle(
				rect.X,
				rect.Y,
				rect.Width,
				thickness
			),
			Color.White
		);
		Core.SpriteBatch.Draw(
			rectangleTexture,
			new Rectangle(
				rect.X,
				rect.Bottom - thickness,
				rect.Width,
				thickness
			),
			Color.White
		);
		Core.SpriteBatch.Draw(
			rectangleTexture,
			new Rectangle(
				rect.X,
				rect.Y,
				thickness,
				rect.Height
			),
			Color.White
		);
		Core.SpriteBatch.Draw(
			rectangleTexture,
			new Rectangle(
				rect.Right - thickness,
				rect.Y,
				thickness,
				rect.Height
			),
			Color.White
		);
	}
	
	/// <summary>
	/// Helper function that highlights the player's movement direction callculation
	/// </summary>
	public void HighlightMovementCell()
	{
		Texture2D pixelTexture;
		pixelTexture = new Texture2D(Core.GraphicsDevice, 1, 1);
		pixelTexture.SetData(new[] { Color.White });
		Rectangle scaled = new Rectangle(_nextTravelCell.X * Map.TileSize, _nextTravelCell.Y * Map.TileSize,
			Map.TileSize, Map.TileSize);
	
		Core.SpriteBatch.Draw(pixelTexture, scaled, _nextTravelCellColor * 0.5f);
	}
}