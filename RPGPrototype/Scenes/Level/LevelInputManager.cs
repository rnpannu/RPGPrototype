using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using RenderingLibrary.Graphics;
using WizardsVsWirebacks;

namespace RPGPrototype.Scenes;

/// <summary>
/// A class to control inputs and 
/// </summary>
public class LevelInputManager
{
	private LevelCamera _camera;
	
	public Matrix Transform { get; private set; }

	public LevelInputManager()
	{
		_camera = new LevelCamera(592, 448);
	}
	public void Initialize()
	{

	}

	public void Update(GameTime gameTime)
	{
		if (GameController.Exit())
		{
			_camera.Reset();
		}
		_camera.UpdateCamera(Vector2.One);
		Transform = _camera.GetTransform();
	}

	public void Draw(GameTime gameTime)
	{
		_camera.DrawCameraTexture();	
	}

}

