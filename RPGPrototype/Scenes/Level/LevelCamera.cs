using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using WizardsVsWirebacks;

namespace RPGPrototype.Scenes;

public class LevelCamera
{
	private const float PLAYER_SPEED = 300;
	private Texture2D _cameraPlaceHolderTexture;
	private Vector2 _textureOrigin;
	private Vector2 _cameraPosition;
	private Vector2 _cameraDirection;
	private Vector2 _startingPos, _minPos, _maxPos;

	public LevelCamera()
	{
		Initialize();
	}
	public void Initialize()
	{
		Matrix invert = Matrix.Invert(//Core.Scale * 
		                              Matrix.CreateScale(4.0f));
		//Matrix invert = Matrix.Invert(Matrix.CreateScale(1.0f));
		//_startingPos = Vector2.Transform(new Vector2(Core.VirtualWidth / 2, Core.VirtualHeight / 2), invert);
		_startingPos = Vector2.Transform(new Vector2(Core.Width / 2, Core.Height / 2), invert);
		_cameraPosition = _startingPos;

		//_cameraPlaceHolderTexture = Core.Content.Load<Texture2D>("rock_in_water_01");
		//_textureOrigin = new Vector2(_cameraPlaceHolderTexture.Width / 2, _cameraPlaceHolderTexture.Height / 2);

		_minPos = new Vector2(0, 0);
		_maxPos = new Vector2(592, 448); // Current width of map, replace magic numbers
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
        
		_cameraPosition += ((_cameraDirection * GameManager.DT * PLAYER_SPEED));
		//_cameraPosition = Vector2.Clamp(_cameraPosition, _minPos, _maxPos);
	}

	public Matrix CalculateTranslation()
	{
		float deltaX = _startingPos.X - _cameraPosition.X;
		// TODO: Performance - concern: Is camera choppier when clamping is applied? 
		// Discard redundant inputs once clamp is hit
		// Want to have a smooth camera feel - apply smoothstep?
		deltaX = MathHelper.Clamp(deltaX, -(_startingPos.X + 592 / 4) , _startingPos.X);
		float deltaY = _startingPos.Y - _cameraPosition.Y;
		//deltaY = MathHelper.Clamp(deltaY, 0, 180 + _startingPos.Y);
		return Matrix.CreateTranslation(deltaX, deltaY, 0f);
	}

	public Matrix GetTransform()
	{
		return CalculateTranslation() 
		       * 
			       Matrix.CreateScale(4.0f)
			       //*
			       //Core.Scale
		       ;
	}
	public void DrawCameraTexture()
	{
		//Core.SpriteBatch.Draw(_cameraPlaceHolderTexture, _cameraPosition, Color.White);
	}
}