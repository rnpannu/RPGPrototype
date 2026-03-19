using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Scenes;
using RPGPrototype.Objects;

namespace RPGPrototype.Scenes;

public class LevelScene : Scene
{
	private LevelInputManager _input;
	private Texture2D _background;
	private Player _player;

	public override void Initialize()
	{
		_input = new LevelInputManager();
		base.Initialize();
	}

	public override void LoadContent()
	{
		_background = Content.Load<Texture2D>("maps/Map/simplified/Level_0/_composite");
		base.LoadContent();
	}

	public override void Update(GameTime gameTime)
	{
		_input.Update(gameTime);
		base.Update(gameTime);
	}

	public override void Draw(GameTime gameTime)
	{
		Core.GraphicsDevice.Clear(new Color(32, 40, 78, 255));
		//Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: Matrix.Identity);
		Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: 
			_input.Transform
			//Matrix.Identity
			
		);
		Core.SpriteBatch.Draw(_background, Vector2.Zero, Color.White);
		Core.SpriteBatch.End();
		base.Draw(gameTime);
	}
}