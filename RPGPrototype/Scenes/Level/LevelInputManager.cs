using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using RenderingLibrary.Graphics;
using RPGPrototype.Objects;

namespace RPGPrototype.Scenes;

/// <summary>
/// A class to control inputs and 
/// </summary>
public class LevelInputManager
{
	private LevelCamera _camera;
	private Matrix _transform;
	private Vector2 _cameraPosition;

	public LevelInputManager()
	{
		Initialize();
	}

	public Matrix Transform
	{
		get => _transform;
		private set => _transform = value;
	}

	public Vector2 CameraPosition
	{
		get => _cameraPosition;
		private set => _cameraPosition = value;
	}

	public void Initialize()
	{
		_camera = new LevelCamera(592, 448);
	}

	public void Update(GameTime gameTime)
	{
		if (GameController.Exit())
		{
			_camera.Reset();
		}

		_camera.UpdateCamera();
		CameraPosition = _camera.CameraPosition;
		Transform = _camera.GetTransform();
	}

	public void Draw(GameTime gameTime)
	{
	}
}