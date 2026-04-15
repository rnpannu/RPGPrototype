using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;

namespace RPGPrototype.Scenes;

public class LevelCamera
{
	private LevelData _map;
	private float _playerSpeed = 150;

	private Vector2 _position;
	private Vector2 _cameraDirection;
	private Vector2 _startingPos, _minPos, _maxPos;

	private Texture2D _cameraPlaceHolderTexture;
	private Vector2 _textureOrigin;

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

		LoadContent();
	}
	

	public void LoadContent()
	{
		_cameraPlaceHolderTexture = Core.Content.Load<Texture2D>("sprites/objects/rock_in_water_01");
		_textureOrigin = new Vector2(_cameraPlaceHolderTexture.Width / 2, _cameraPlaceHolderTexture.Height / 2);
		_minPos.X = _minPos.X + _textureOrigin.X;
		_minPos.Y = _minPos.Y + _textureOrigin.Y;
		_maxPos.X = _maxPos.X - _textureOrigin.X;
		_maxPos.Y = _maxPos.Y - _textureOrigin.Y;
	}

	/// <summary>
	/// Updates internal positions according to WASD movements
	/// </summary>
	/// <param name="newPos"></param>
	public void Follow(Vector2 position)
	{
		Position = position;
		/*_cameraDirection = Vector2.Zero;

		if (GameController.MoveUp()) _cameraDirection.Y--;
		if (GameController.MoveDown()) _cameraDirection.Y++;
		if (GameController.MoveLeft()) _cameraDirection.X--;
		if (GameController.MoveRight()) _cameraDirection.X++;*/

		/*if (direction != Vector2.Zero)
		{
			direction.Normalize();
		}

		CameraPosition += ((direction * GameManager.DT * _playerSpeed));
		CameraPosition = Vector2.Clamp(CameraPosition, _minPos, _maxPos);*/
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

	/// <summary>
	/// Draw the placeholder texture for the camera
	/// </summary>
	
	public void DrawCameraTexture()
	{
		Core.SpriteBatch.Draw(_cameraPlaceHolderTexture, _position, null, Color.White, 0.0f, _textureOrigin, 1.0f,
			SpriteEffects.None, 0.0f);
	}
}