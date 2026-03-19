using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;

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
		_camera = new LevelCamera();
	}
	public void Initialize()
	{

	}

	public void Update(GameTime gameTime)
	{
		_camera.UpdateCamera(Vector2.One);
		Transform = _camera.GetTransform();
	}

	public void Draw(GameTime gameTime)
	{
		
	}

}

