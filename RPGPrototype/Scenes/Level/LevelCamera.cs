using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using WizardsVsWirebacks;

namespace RPGPrototype.Scenes;

public class LevelCamera
{
	private readonly int _mapWidth;
	private readonly int _mapHeight;
	private float _playerSpeed = 300;
	
	private Vector2 _cameraPosition;
	private Vector2 _cameraDirection;
	private Vector2 _startingPos, _minPos, _maxPos;
	
	private Texture2D _cameraPlaceHolderTexture;
	private Vector2 _textureOrigin;

	private float _zoomAmount;
	private Matrix _zoom;
	public LevelCamera(int mapWidth, int mapHeight)
	{
		MapWidth = mapWidth;
		MapHeight = mapHeight;
		Initialize();
	}

	public int MapWidth
	{
		get => _mapWidth;
		init => _mapWidth = value;
	}

	public int MapHeight
	{
		get => _mapHeight;
		init => _mapHeight = value;
	}

	public float ZoomAmount
	{
		get => _zoomAmount;
		set
		{
			_zoomAmount = value;
			_zoom = Matrix.CreateScale(_zoomAmount);
		}
	}
	

	public Vector2 CameraPosition
	{
		get => _cameraPosition;
		private set => _cameraPosition = value;
	}
	public Matrix TotalScale => Core.Scale * _zoom;

	public void Reset()
	{
		Initialize();
	}
	public void Initialize()
	{
		ZoomAmount = 3f / 5f;
		// Regardless of what the end resolution is, start the camera in the middle of the top left corner
		Matrix totalScale = Core.Scale * _zoom;
		Matrix invert = Matrix.Invert(TotalScale);
		_startingPos = Vector2.Transform(new Vector2(Core.VirtualWidth / 2f, Core.VirtualHeight / 2f), invert);
		
		_cameraPosition = _startingPos;
		_minPos = new Vector2(0, 0);
		_maxPos = new Vector2(MapWidth, MapHeight);
		
		// TODO: Troubleshooting player sprite collision with the edge of the map
		//_cameraPlaceHolderTexture = Core.Content.Load<Texture2D>("rock_in_water_01");
		//_textureOrigin = new Vector2(_cameraPlaceHolderTexture.Width / 2, _cameraPlaceHolderTexture.Height / 2);
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

	public void UpdateCamera(Vector2 newPos)
	{
		_cameraDirection = Vector2.Zero;
        
		if (GameController.MoveUp()) _cameraDirection.Y--;
		if (GameController.MoveDown()) _cameraDirection.Y++;
		if (GameController.MoveLeft()) _cameraDirection.X--;
		if (GameController.MoveRight()) _cameraDirection.X++;

		if(_cameraDirection != Vector2.Zero)
		{
			_cameraDirection.Normalize();
		}
        
		CameraPosition += ((_cameraDirection * GameManager.DT * _playerSpeed));
		CameraPosition = Vector2.Clamp(CameraPosition, _minPos, _maxPos);
	}

	public Matrix CalculateTranslation()
	{
		/*Vector2 minCameraBounds = new Vector2(-(MapWidth - _startingPos.X * 2), -70);
		Vector2 maxCameraBounds = new Vector2(0, 70);
		float deltaX = _startingPos.X - _cameraPosition.X;
		float deltaY = _startingPos.Y - _cameraPosition.Y;

		Vector2 translation = new Vector2(deltaX, deltaY);
		translation = Vector2.Clamp(translation, minCameraBounds, maxCameraBounds);*/
		
		float deltaX = _startingPos.X - CameraPosition.X;
		// TODO: Want to have a smooth camera feel - apply smoothstep?
		deltaX = MathHelper.Clamp(deltaX, -(MapWidth - _startingPos.X * 2), 0);
		float deltaY = _startingPos.Y - CameraPosition.Y;
		deltaY = MathHelper.Clamp(deltaY, -(MapHeight- _startingPos.Y * 2), 0);
		return Matrix.CreateTranslation(deltaX, deltaY, 0f);
	}

	public Matrix GetTransform()
	{
		return CalculateTranslation() * TotalScale;
	}
	public void DrawCameraTexture()
	{
		Core.SpriteBatch.Draw(_cameraPlaceHolderTexture, _cameraPosition, null, Color.White, 0.0f, _textureOrigin,1.0f, SpriteEffects.None, 0.0f);
	}
}