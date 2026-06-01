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
		
		 Player._position.X += (movementDirection.X * Player.Speed.X * Core.DT);
		
		_intersections = getIntersectingTilesHorizontal(Player.Rect);

		foreach (var tile in _intersections)
		{
			if (_collisionGrid[tile.Y, tile.X] == 1)
			{
				if (movementDirection.X > 0.0f) // Moving right, lock player x to their width from the tile
				{
					Player._position.X = (tile.X * tileSize) - Player.Rect.Width; 
				} else if (movementDirection.X < 0.0f) // Moving left, lock player to right side of tile (+1 tile width)
				{
					Player._position.X = ((tile.X + 1) * tileSize); 
				}
			}
		}
		
		_intersections.Clear();
		
		Player._position.Y += (movementDirection.Y * Player.Speed.Y * Core.DT);
		_intersections = getIntersectingTilesVertical(Player.Rect);
		
		foreach (var tile in _intersections)
		{
			if (_collisionGrid[tile.Y, tile.X] == 1)
			{
				if (movementDirection.Y > 0.0f) // Moving down, lock player Y to one sprite height above
				{
					Player._position.Y = (tile.Y * tileSize) - Player.Rect.Height;
				} else if (movementDirection.Y < 0.0f) // Moving left, lock player to tile bottom (+1 tile height)
				{
					Player._position.Y = ((tile.Y + 1) * tileSize); 
				}
			}
		}

	}

	public List<Rectangle> getIntersectingTilesHorizontal(Rectangle target)
	{
		List<Rectangle> intersections = new();

		int tileSize = _map.TileSize;
		
		// Get hitbox in tiles
		int widthInTiles = (target.Width - (target.Width % tileSize)) / tileSize;
		int heightInTiles = (target.Height - (target.Height % tileSize)) / tileSize;

		for (int x = 0; x <= widthInTiles; x++) {
			for (int y = 0; y <= heightInTiles; y++) {

				intersections.Add(new Rectangle(
					(target.X + x * tileSize) / tileSize,
					(target.Y + y*(tileSize-1)) / tileSize,
					tileSize,
					tileSize
				));
			}
		}

		return intersections;
	}
	public List<Rectangle> getIntersectingTilesVertical(Rectangle target)
	{
		List<Rectangle> intersections = new();

		int tileSize = _map.TileSize;
		int widthInTiles = (target.Width - (target.Width % tileSize)) / tileSize;
		int heightInTiles = (target.Height - (target.Height % tileSize)) / tileSize;

		for (int x = 0; x <= widthInTiles; x++) {
			for (int y = 0; y <= heightInTiles; y++) {

				intersections.Add(new Rectangle(

					(target.X + x*(tileSize - 1)) / tileSize,
					(target.Y + y*tileSize) / tileSize,
					tileSize,
					tileSize
				));
			}
		}

		return intersections;
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