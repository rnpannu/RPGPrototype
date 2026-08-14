namespace MonoGameLibrary.Graphics;

public struct RectangleF
{
	
	public float X;
	public float Y;
	public float Width;
	public float Height;

	public float Left => this.X;
	public float Right => this.X + this.Width;
	public float Top => this.Y;
	public float Bottom => this.Y + this.Height;
	
	public RectangleF(float x, float y, float width, float height)
	{
		X = x;
		Y = y;
		Width = width;
		Height = height;
	}

	public bool Intersects(RectangleF other)
	{
		return X < other.X + other.Width && X + Width > other.X &&
		       Y < other.Y + other.Height && Y + Height > other.Y;
	}
	

}