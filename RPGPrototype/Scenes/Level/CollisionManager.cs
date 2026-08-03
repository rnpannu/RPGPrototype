using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using RPGPrototype.Objects;
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

	public int[,] MapCollisionGrid => _mapCollisionGrid;

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
	public Vector2 ValidateMovement(Rectangle target, Vector2 prospectiveMove)
	{
		// TODO: Remove movementdirection?
		int tileSize = Map.TileSize;
		bool xCollision = false;
		bool yCollision = false;

		Rectangle prospectiveMoveX = new Rectangle(target.X + (int) prospectiveMove.X * 3, target.Y, target.Width, target.Height); // Magic number 3?
        _tileIntersections = GetIntersectingTilesHorizontal(prospectiveMoveX);
            Console.WriteLine(prospectiveMoveX.Right);

        foreach (var tile in _tileIntersections)
        {
            if (MapCollisionGrid[tile.Y, tile.X] == 1)
            {
                if (prospectiveMoveX.Intersects(new Rectangle(tile.X * tileSize, tile.Y * tileSize, tileSize, tileSize)))
                {
                    xCollision = true;
                }
            }
        }

        if (prospectiveMoveX.Left <= 0 ||prospectiveMoveX.Right >= Map.Width - 2)
        {
            Console.WriteLine(prospectiveMoveX.Right);
            Console.WriteLine(Map.Width);
            Console.WriteLine("X Collision");
            xCollision = true;
        }


		Rectangle prospectiveMoveY = new Rectangle(target.X, target.Y + (int) prospectiveMove.Y * 3, target.Width, target.Height); // Magic number 3?

        _tileIntersections = GetIntersectingTilesVertical(prospectiveMoveY);

        foreach (var tile in _tileIntersections)
        {
            if (MapCollisionGrid[tile.Y, tile.X] == 1)
            {
                if (prospectiveMoveY.Intersects(new Rectangle(tile.X * tileSize, tile.Y * tileSize, tileSize, tileSize)))
                {
                    yCollision = true;
                }
            }
        }

        if (prospectiveMoveY.Top <= 0 || prospectiveMoveY.Bottom >= Map.Height - 1)
        {
            Console.WriteLine(prospectiveMoveY.Bottom);
            Console.WriteLine(Map.Height);
            Console.WriteLine("Y Collision");
            yCollision = true;
        }

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
					_collisionColors[MapCollisionGrid[i, j]] * 0.3f);
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
	/*public void HighlightMovementCell()
	{
		if (!(_nextTravelCell.IsEmpty))
		{
			Rectangle scaled = new Rectangle(_nextTravelCell.X * Map.TileSize, _nextTravelCell.Y * Map.TileSize,
				Map.TileSize, Map.TileSize);
			Core.SpriteBatch.Draw(_pixelTexture, scaled, _nextTravelCellColor * 0.5f);
		}
	}*/
}
