using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;

namespace RPGPrototype.Scenes;

public class LevelCamera
{
	private LevelData _map;

	private Vector2 _position;
	private Vector2 _cameraDirection;
	private Vector2 _startingPos, _minPos, _maxPos;

	private float _zoomAmount;
	private Matrix _zoom;

	public LevelCamera(LevelData map)
	{
		_map = map;
		Initialize();
	}
	// Create OnZoomChanged event?
	public float ZoomAmount
	{
		get => _zoomAmount;
		set
		{
			_zoomAmount = value;
			_zoom = Matrix.CreateScale(_zoomAmount);
		}
	}

	public Vector2 Position
	{
		get => _position;
		private set => _position = value;
	}

	public Matrix TotalScale => Core.Scale * _zoom;
	
	public void Initialize()
	{
		ZoomAmount = 3f / 5f;
		// Regardless of what the end resolution is, start the camera in the middle of the top left corner
		Matrix totalScale = Core.Scale * _zoom;
		Matrix invert = Matrix.Invert(TotalScale);
		_startingPos = Vector2.Transform(new Vector2(Core.VirtualWidth / 2f, Core.VirtualHeight / 2f), invert);

		_position = _startingPos;
		_minPos = new Vector2(0, 0);
		_maxPos = _map.Bounds;
	}
	
	/// <summary>
	/// Updates internal positions according to WASD movements
	/// </summary>
	/// <param name="newPos"></param>
	public void Follow(Vector2 position)
	{
		Position = position;
	}

	/// <summary>
	/// Get the total translation from WASD movements done to the world
	/// </summary>
	/// <returns></returns>
	public Matrix CalculateTranslation()
	{
		float deltaX = _startingPos.X - Position.X;
		// TODO: Want to have a smooth camera feel - apply smoothstep?
		deltaX = MathHelper.Clamp(deltaX, -(_map.Width - _startingPos.X * 2), 0);
		float deltaY = _startingPos.Y - Position.Y;
		deltaY = MathHelper.Clamp(deltaY, -(_map.Height - _startingPos.Y * 2), 0);
		return Matrix.CreateTranslation(deltaX, deltaY, 0f);
	}

	/// <summary>
	/// Get the total transform matrix applied to the world
	/// </summary>
	/// <returns> A matrix to be applied to the spritebatch begin </returns>
	public Matrix GetTransform()
	{
		return CalculateTranslation() * TotalScale;
	}
	
}