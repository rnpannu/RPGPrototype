using System;
using Microsoft.Xna.Framework;

namespace MonoGameLibrary;

public static class GameUtils
{

	public static float BasicLerp(float start, float end, float t)
	{
		// Clamp t between 0 and 1 to prevent overshoot
		t = Math.Clamp(t, 0f, 1f); 
		return start + (end - start) * t;
	}
	public static Vector2 BasicLerp(Vector2 start, Vector2 end, float t)
	{
		t = Math.Clamp(t, 0f, 1f);

		return new Vector2(
			BasicLerp(start.X, end.X, t),
			BasicLerp(start.Y, end.Y, t)
		);
	}
}