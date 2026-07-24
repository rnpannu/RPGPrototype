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

}