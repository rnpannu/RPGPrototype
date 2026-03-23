using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Scenes;
using RPGPrototype.Objects;

namespace RPGPrototype.Scenes;

public class LevelScene : Scene
{
	private LevelInputManager _inputManager;
	private LevelObjectManager _objectManager;

	private Texture2D _background;


	public override void Initialize()
	{
		_objectManager = new LevelObjectManager();
		_inputManager = new LevelInputManager();
		base.Initialize();
	}

	public override void LoadContent()
	{
		_background = Content.Load<Texture2D>("maps/Map/simplified/Level_0/_composite");
		_objectManager.LoadContent(Content);
		base.LoadContent();
	}

	public override void Update(GameTime gameTime)
	{
		_inputManager.Update(gameTime);
		_objectManager.Update(gameTime, _inputManager.CameraPosition);
		base.Update(gameTime);
	}

	public override void Draw(GameTime gameTime)
	{
		Core.GraphicsDevice.Clear(new Color(32, 40, 78, 255));
		//Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: Matrix.Identity); // Remove all transformations
		Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _inputManager.Transform);

		Core.SpriteBatch.Draw(_background, Vector2.Zero, Color.White);
		_inputManager.Draw(gameTime);
		_objectManager.Draw(gameTime);
		Core.SpriteBatch.End();
		base.Draw(gameTime);
	}
}