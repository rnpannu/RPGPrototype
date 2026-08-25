using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
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

    public Vector2 ValidateMovement(RectangleF target, Vector2 prospectiveMove)
    {
        int tileSize = Map.TileSize;
        Vector2 validatedMove = prospectiveMove;
        RectangleF prospectiveMoveX = new RectangleF(target.X + prospectiveMove.X, target.Y, target.Width, target.Height );

        _tileIntersections = GetIntersectingTilesHorizontal(prospectiveMoveX);

        foreach (var tile in _tileIntersections)
        {
            if (MapCollisionGrid[tile.Y, tile.X] == 1)
            {
                if (prospectiveMoveX.Intersects(
                    new RectangleF(tile.X * tileSize, tile.Y * tileSize, tileSize, tileSize)))
                {
                    validatedMove.X = 0;
                    break;
                }
            }
        }

        if (prospectiveMoveX.Left <= 0 || prospectiveMoveX.Right >= Map.Width - 2)
        {
            validatedMove.X = 0;
        }

        RectangleF prospectiveMoveY = new RectangleF(target.X + validatedMove.X, target.Y + prospectiveMove.Y, target.Width, target.Height);

        _tileIntersections = GetIntersectingTilesVertical(prospectiveMoveY);

        foreach (var tile in _tileIntersections)
        {
            if (MapCollisionGrid[tile.Y, tile.X] == 1)
            {
                if (prospectiveMoveY.Intersects(
                    new RectangleF(tile.X * tileSize, tile.Y * tileSize, tileSize, tileSize)))
                {
                    validatedMove.Y = 0;
                    break;
                }
            }
        }

        if (prospectiveMoveY.Top <= 0 || prospectiveMoveY.Bottom >= Map.Height - 1)
        {
            validatedMove.Y = 0;
        }
        return validatedMove;
    }

	public List<Rectangle> GetIntersectingTilesHorizontal(RectangleF target)
	{
		List<Rectangle> intersections = new();

        int tileSize = Map.TileSize;
        float targetWidth = target.Width;
        int targetWidthInt = (int)target.Width;
        float targetHeight = target.Height;
        int targetHeightInt = (int)target.Height;
		// Get hitbox in tiles
		int widthInTiles =  (targetWidthInt - (targetWidthInt % tileSize)) / tileSize;
		int heightInTiles = (targetHeightInt - (targetHeightInt % tileSize)) / tileSize;
		
		for (int x = 0; x <= widthInTiles; x++) {
			for (int y = 0; y <= heightInTiles; y++) {

				intersections.Add(new Rectangle(
					((int)target.X + x * tileSize) / tileSize,
					((int)target.Y + y*(tileSize-1)) / tileSize,
					tileSize,
					tileSize
				));
			}
		}

		return intersections;
	}

	public List<Rectangle> GetIntersectingTilesVertical(RectangleF target)
	{
		List<Rectangle> intersections = new();

		int tileSize = Map.TileSize;
		int widthInTiles = ((int)target.Width - ((int)target.Width % tileSize)) / tileSize;
		int heightInTiles = ((int)target.Height - ((int)target.Height % tileSize)) / tileSize;

		for (int x = 0; x <= widthInTiles; x++) {
			for (int y = 0; y <= heightInTiles; y++) {

				intersections.Add(new Rectangle(

					((int)target.X + x*(tileSize - 1)) / tileSize,
					((int)target.Y + y*tileSize) / tileSize,
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
		//DrawPlayerIntersections();
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