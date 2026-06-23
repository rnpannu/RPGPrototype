using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using RPGPrototype.UI.Debug;

namespace RPGPrototype.Scenes;

public class CollisionManager
{
	private readonly int[,] _mapCollisionGrid;
	private readonly Dictionary<int, Color> _collisionColors = new();
	private readonly Texture2D _pixelTexture;
	private Rectangle _nextTravelCell;
	private Color _nextTravelCellColor;
	
	private List<Rectangle> _tileIntersections = [];
	
	public CollisionManager(LevelData map)
	{
		Map = map;

		_pixelTexture = new Texture2D(Core.GraphicsDevice, 1, 1);
		_pixelTexture.SetData(new[] { Color.White });
		
		_mapCollisionGrid = LevelUtility.LoadIntGrid("Collision.csv", "Level_0");
		
		_collisionColors.Add(0, Color.GreenYellow);
		_collisionColors.Add(1, Color.Red);
		
		Initialize();
	}

	public void Initialize()
	{
		
	}
	public LevelData Map
	{
		get => field;
		set => field = value;
	}
	public bool ShowHitboxes { get; set; }

	public void LoadContent()
	{
		
	}

	/// <summary>
	/// Check for collisions before admitting a move. Ideally works in 2 steps
	/// 1. Get intersecting tiles around the player (broad pass - don't want to check every tile)
	/// 2. Do finer, more precise check on player hitbox with surrounding tiles.
	/// Currently the system does not do #2, will be implemented later.
	/// </summary>
	/// <param name="movementDirection"> The direction of player movement </param>
	public Vector2 ValidateMovement(Rectangle target, Vector2 movementDirection, Vector2 prospectiveMove)
	{
		// TODO: Remove movementdirection?
		int tileSize = Map.TileSize;
		bool xCollision = false;
		bool yCollision = false;
		
		//float prospectiveMoveX = (movementDirection.X * velocity.X * Core.DT);
		_tileIntersections = GetIntersectingTilesHorizontal(
			new Rectangle(target.X + (int) prospectiveMove.X, target.Y, target.Width, target.Height));

		foreach (var tile in _tileIntersections)
		{
			if (_mapCollisionGrid[tile.Y, tile.X] == 1)
			{
				// TODO: Potential fix to bug: Implement momentum and velocity slowdown, and then check 
				// Currently the system only works based on the intersecting tiles and not the hitbox
				if (movementDirection.X != 0) // New: just don't permit a move if it results in a collision
				{
					xCollision = true;
				} // Old: Reset player position if it results in a collion: can cause jittering
				/*if (movementDirection.X > 0.0f) // Moving right, lock player x to their width from the tile
				{
					Player.Move(-(movementDirection.X * Player.MovementSpeed.X * Core.DT), 0);
					//Player.Move((tile.X * tileSize) - Player.Rect.Width, Player.Position.Y, true);
					//Player._position.X = (tile.X * tileSize) - Player.Rect.Width; 
				}
				else if (movementDirection.X < 0.0f) // Moving left, lock player to right side of tile (+1 tile width)
				{
					Player.Move(-(movementDirection.X * Player.MovementSpeed.X * Core.DT), 0);
					//Player.Move(((tile.X + 1) * tileSize), Player.Position.Y, true);
					//Player._position.X = ((tile.X + 1) * tileSize); 
				}*/
			}
		}
		
		/*float prospectiveMoveY = (movementDirection.Y * velocity.Y * Core.DT);*/
		_tileIntersections = GetIntersectingTilesVertical(new Rectangle(
			target.X, target.Y + (int) prospectiveMove.Y, target.Width, target.Height));
		
		foreach (var tile in _tileIntersections)
		{
			if (_mapCollisionGrid[tile.Y, tile.X] == 1)
			{
				if (movementDirection.Y != 0)
				{
					yCollision = true;
				}
				/*if (movementDirection.Y > 0.0f) // Moving down, lock player Y to one sprite height above
				{
					Player.Move(0, -(movementDirection.Y * Player.MovementSpeed.Y * Core.DT));
					//Player._position.Y = (tile.Y * tileSize) - Player.Rect.Height;
				} else if (movementDirection.Y < 0.0f) // Moving left, lock player to tile bottom (+1 tile height)
				{
					Player.Move(0, -(movementDirection.Y * Player.MovementSpeed.Y * Core.DT));
					//Player._position.Y = ((tile.Y + 1) * tileSize); 
				}*/
			}
		}

		//return new Vector2(prospectiveMoveX * xCollision, prospectiveMoveY * (int) yCollision);
		// subtract target because Move() is already relative to the player's existing position
		return new Vector2(xCollision ? 0 : prospectiveMove.X, yCollision ? 0 : prospectiveMove.Y);
	}
	
	public List<Rectangle> GetIntersectingTilesHorizontal(Rectangle target)
	{
		List<Rectangle> intersections = new();

		int tileSize = Map.TileSize;
		
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

		int tileSize = Map.TileSize;
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
		DrawCollisionGrid();
		DrawPlayerIntersections();
	}
		// -- -----Utility drawing functions -------
	/// <summary>
	/// Helper method to highlight the tiles the player is intersecting
	/// </summary>
	public void DrawPlayerIntersections()
	{
		int tileSize = Map.TileSize;
		foreach (var rect in _tileIntersections)
		{
			DrawRectHollow(new Rectangle((int) rect.X * tileSize, (int)rect.Y * tileSize, tileSize, tileSize));
		}
	}
	
	/// <summary>
	/// Helper method to highlight collision map
	/// </summary>
	public void DrawCollisionGrid()
	{
		int tileSize = Map.TileSize;
		for (int i = 0; i < Map.Height / tileSize; i++)
		{
			for (int j = 0; j < Map.Width / tileSize; j++)
			{
				Core.SpriteBatch.Draw(_pixelTexture, new Rectangle((int) j * tileSize, (int) i * tileSize,
					tileSize, tileSize),
					_collisionColors[_mapCollisionGrid[i, j]] * 0.3f);
			}
		}
	}
	/// <summary>
	/// Helper method to draw the outline of a rectangle
	/// </summary>
	/// <param name="rect"> The rectangle to be outlined</param>
	public void DrawRectHollow(Rectangle rect)
	{
		//_pixelTexture.SetData(new Color[] {new (255, 0, 0, 255)});
		int thickness = 1;
		Core.SpriteBatch.Draw(
			_pixelTexture,
			new Rectangle(
				rect.X,
				rect.Y,
				rect.Width,
				thickness
			),
			Color.Red
		);
		Core.SpriteBatch.Draw(
			_pixelTexture,
			new Rectangle(
				rect.X,
				rect.Bottom - thickness,
				rect.Width,
				thickness
			),
			Color.Red
		);
		Core.SpriteBatch.Draw(
			_pixelTexture,
			new Rectangle(
				rect.X,
				rect.Y,
				thickness,
				rect.Height
			),
			Color.Red
		);
		Core.SpriteBatch.Draw(
			_pixelTexture,
			new Rectangle(
				rect.Right - thickness,
				rect.Y,
				thickness,
				rect.Height
			),
			Color.Red
		);
	}
	
	/// <summary>
	/// Helper function that highlights the player's movement direction callculation
	/// </summary>
	public void HighlightMovementCell()
	{
		if (!(_nextTravelCell.IsEmpty))
		{
			Rectangle scaled = new Rectangle(_nextTravelCell.X * Map.TileSize, _nextTravelCell.Y * Map.TileSize,
				Map.TileSize, Map.TileSize);
			Core.SpriteBatch.Draw(_pixelTexture, scaled, _nextTravelCellColor * 0.5f);
		}
	}
}