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
	
	private LevelData _map;
	
	private Player _player;
	

	// Todo: Create a better atlas parser that can iterate frames without specifying the coords of each one
	private TextureAtlas _objectAtlas;
	
	private int[,] _collisionGrid;
	private Dictionary<int, Color> _collisionColors = new();
	private Rectangle _nextTravelCell;
	private Color _nextTravelCellColor;

	private List<Rectangle> _intersections = new();
	public LevelObjectManager(DebugMenu debug, LevelData map)
	{
		_debug = debug;
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
		//To make the watch dynamic, you need to pass a lambda expression () => ....
		//This creates a closure that captures the Player reference itself, forcing the program to evaluate Player.
		//Position cleanly from scratch every single time the Draw loop invokes the delegate.
		//_debug.Watch.RegisterWatch("player position", Player.Position.ToString);
		_debug.Watch.RegisterWatch("player position", () => Player.Position.ToString());
	}

	public void LoadContent(ContentManager content)
	{
		_objectAtlas = TextureAtlas.FromFile(content, "sprites/objectAtlas-definition.xml");
		Player.LoadContent(_objectAtlas);
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
	}
	/// <summary>
	/// Check for collisions before admitting a move
	/// </summary>
	/// <param name="movementDirection"> The direction of player movement </param>
	public void ValidateMovement(Vector2 movementDirection)
	{ 
		// 1. Get intersecting tiles around the player (broad pass - don't want to check every tile)
		// 2. Do finer, more precise check on player hitbox with surrounding tiles.
		int tileSize = Map.TileSize;
		//_intersections.Clear(); don't know if this is needed
		
		// Check horizontal collisions ----------------------------------------
		//float prospectiveMoveX = Player.Position.X + (movementDirection.X * Player.Speed.X * Core.DT);
		Player.Move( movementDirection.X * Player.Speed.X * Core.DT, 0);
		_intersections = GetIntersectingTilesHorizontal(Player.Rect);
		foreach (var tile in _intersections)
		{
			if (_collisionGrid[tile.Y, tile.X] == 1)
			{
				// TODO: Potential fix to bug: Implement momentum and velocity slowdown, and then check 
				// AND/OR: rewire with a prospective move framework.
				// Currently the system only works based on the intersecting tiles and not the hitbox
				if (movementDirection.X > 0.0f) // Moving right, lock player x to their width from the tile
				{
					Player.Move( -(movementDirection.X * Player.Speed.X * Core.DT), 0);
					//Player.Move((tile.X * tileSize) - Player.Rect.Width, Player.Position.Y, true);
					//Player._position.X = (tile.X * tileSize) - Player.Rect.Width; 
				} else if (movementDirection.X < 0.0f) // Moving left, lock player to right side of tile (+1 tile width)
				{
					Player.Move( -(movementDirection.X * Player.Speed.X * Core.DT), 0);
					//Player.Move(((tile.X + 1) * tileSize), Player.Position.Y, true);
					//Player._position.X = ((tile.X + 1) * tileSize); 
				}
			}
		}
		
		//_intersections.Clear();
		Player.Move( 0, (movementDirection.Y * Player.Speed.Y * Core.DT));
		_intersections = GetIntersectingTilesVertical(Player.Rect);
		
		foreach (var tile in _intersections)
		{
			if (_collisionGrid[tile.Y, tile.X] == 1)
			{
				if (movementDirection.Y > 0.0f) // Moving down, lock player Y to one sprite height above
				{
					Player.Move(0, -(movementDirection.Y * Player.Speed.Y * Core.DT));
					//Player._position.Y = (tile.Y * tileSize) - Player.Rect.Height;
				} else if (movementDirection.Y < 0.0f) // Moving left, lock player to tile bottom (+1 tile height)
				{
					Player.Move(0, -(movementDirection.Y * Player.Speed.Y * Core.DT));
					//Player._position.Y = ((tile.Y + 1) * tileSize); 
				}
			}
		}

	}

	public List<Rectangle> GetIntersectingTilesHorizontal(Rectangle target)
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
	public List<Rectangle> GetIntersectingTilesVertical(Rectangle target)
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
		
		foreach (var rect in _intersections)
		{
			//Core.SpriteBatch.Draw(rectangleTexture, new Rectangle((int) rect.X * tileSize, (int)rect.Y * tileSize, tileSize, tileSize), Color.White);
			DrawRectHollow(new Rectangle((int) rect.X * tileSize, (int)rect.Y * tileSize, tileSize, tileSize));
		}

		DrawRectHollow(Player.Rect);
		Player.Draw(gameTime);
		/*if (!_nextTravelCell.IsEmpty)
		{
			HighlightMovementCell();
		}*/
	}
	/// <summary>
	/// TODO: Implement and integrate with debug
	/// </summary>
	public void DrawCollisionGrid()
	{
		/*for (int i = 0; i < Map.Height / tileSize; i++)
		{
			for (int j = 0; j < Map.Width / tileSize; j++)
			{
				Core.SpriteBatch.Draw(rectangleTexture, new Rectangle((int) j * tileSize, (int) i * tileSize,
					tileSize, tileSize),
					_collisionColors[_collisionGrid[i, j]] * 0.3f);
				//DrawRectHollow();
			}
		}*/
		foreach (var tile in _collisionGrid)
		{
			
		}
	}
	/// <summary>
	/// Helper method to draw the outline of a rectangle
	/// </summary>
	/// <param name="rect"> The rectangle to be outlined</param>
	public void DrawRectHollow(Rectangle rect)
	{
		Texture2D rectangleTexture = new Texture2D(Core.GraphicsDevice, 1, 1);
		rectangleTexture.SetData(new Color[] {new (255, 0, 0, 255)});
		int thickness = 1;
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