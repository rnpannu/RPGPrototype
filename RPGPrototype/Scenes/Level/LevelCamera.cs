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

	private Matrix _zoomFactor;
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

	public Matrix ZoomFactor
	{
		get => _zoomFactor;
		private set => _zoomFactor = value;
	}

	public Matrix TotalScale => Core.Scale * ZoomFactor;
	public void Initialize()
	{
		ZoomFactor = Matrix.CreateScale((3f / 4f));
		// Regardless of what the end resolution is, start the camera in the middle of the top left corner
		Matrix invert = Matrix.Invert(TotalScale);
		//_startingPos = Vector2.Transform(new Vector2(Core.VirtualWidth / 2, Core.VirtualHeight / 2), invert);
		// Should startingPos be in world space or screenSpace?
		_startingPos = Vector2.Transform(new Vector2(Core.Width / 2, Core.Height / 2), invert); 
		_cameraPosition = _startingPos;
		_minPos = new Vector2(0, 0);
		_maxPos = new Vector2(MapWidth, MapHeight); // Current width of map, replace magic numbers
		
		// TODO: Troubleshooting player sprite collision with the edge of the map
		//_cameraPlaceHolderTexture = Core.Content.Load<Texture2D>("rock_in_water_01");
		//_textureOrigin = new Vector2(_cameraPlaceHolderTexture.Width / 2, _cameraPlaceHolderTexture.Height / 2);


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
        
		_cameraPosition += ((_cameraDirection * GameManager.DT * _playerSpeed));
		_cameraPosition = Vector2.Clamp(_cameraPosition, _minPos, _maxPos);
	}

	public Matrix CalculateTranslation()
	{
		
		float deltaX = _startingPos.X - _cameraPosition.X;
		// TODO: Performance - concern: Is camera choppier when clamping is applied? 
		// Discard redundant inputs once clamp is hit
		// Want to have a smooth camera feel - apply smoothstep?
		deltaX = MathHelper.Clamp(deltaX, -(_startingPos.X + MapWidth / 4) , _startingPos.X);
		float deltaY = _startingPos.Y - _cameraPosition.Y;
		//deltaY = MathHelper.Clamp(deltaY, 0, 180 + _startingPos.Y);
		return Matrix.CreateTranslation(deltaX, deltaY, 0f);
	}

	public Matrix GetTransform()
	{
		return CalculateTranslation()
		       *
		       Core.Scale
			       * ZoomFactor
		       ;
	}
	public void DrawCameraTexture()
	{
		//Core.SpriteBatch.Draw(_cameraPlaceHolderTexture, _cameraPosition, Color.White);
	}
}