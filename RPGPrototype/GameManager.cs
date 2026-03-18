using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using RPGPrototype.Scenes;

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

	}
	
}