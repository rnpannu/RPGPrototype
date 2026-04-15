using Microsoft.Xna.Framework;

namespace RPGPrototype.Scenes;

public class LevelData
{
	public int Width { get; private set; }
	public int Height { get; private set; }
	
	public Vector2 Bounds => new Vector2(Width, Height);

	public LevelData(int width, int height)
	{
		Width = width;
		Height = height;
	}
}