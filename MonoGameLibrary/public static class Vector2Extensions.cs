using System;
using Microsoft.Xna.Framework;

namespace MonoGameLibrary;

public static class public_static_class_Vector2Extensions
{
	private const float DefaultEpsilon = 1e-3f; // 0.001

	public static bool IsZero(this Vector2 vector, float epsilon = DefaultEpsilon)
		=> vector.LengthSquared() < epsilon * epsilon;
	
	public static bool IsZeroX(this Vector2 vector, float epsilon = DefaultEpsilon)
		=> Math.Abs(vector.X) < epsilon;
	
	public static bool IsZeroY(this Vector2 vector, float epsilon = DefaultEpsilon)
		=> Math.Abs(vector.Y) < epsilon;
	
	public static bool IsApproximately(this Vector2 vectorA, Vector2 vectorB, float epsilon = DefaultEpsilon)
		=> (vectorA - vectorB).LengthSquared() < epsilon * epsilon;
	
	public static Vector2 SnapToZero(this Vector2 vector, float epsilon = DefaultEpsilon)
		=> vector.IsZero(epsilon) ? Vector2.Zero : vector;
}