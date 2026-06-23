using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using RPGPrototype.Scenes;
using RPGPrototype.UI.Debug;

namespace RPGPrototype;

public class GameManager() : Core("RPGPrototype", 1600, 900, false)
{

	protected override void Initialize()
	{
		base.Initialize();
		ChangeScene(new LevelScene());
	}

	protected override void LoadContent()
	{
		base.LoadContent();
		DebugMenu.Instance.LoadContent(Content.Load<SpriteFont>("File")); // Change to better name
	}

	protected override void Update(GameTime gameTime)
	{
		base.Update(gameTime);
		DebugMenu.Instance.Update(gameTime);
	}

	protected override void Draw(GameTime gameTime)
	{
		base.Draw(gameTime);
		//DebugMenu.Instance.Draw(gameTime);
	}
	
	
}