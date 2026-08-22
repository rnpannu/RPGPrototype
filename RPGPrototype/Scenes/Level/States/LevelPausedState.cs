using Microsoft.Xna.Framework;

namespace RPGPrototype.Scenes.States;

public class LevelPausedState : LevelState
{
	
	public override string Name => nameof(LevelPausedState);
	
	public LevelPausedState(LevelScene level) : base(level)
	{
		
	}
	
	public override void Enter()
	{
		base.Enter();
		// InputManager.CenterMousePosition()
	}

	public override void Exit()
	{
		base.Exit();
	}

	public override void Update(GameTime gameTime)
	{
		base.Update(gameTime);

	}

	public override void Draw(GameTime gameTime)
	{
		base.Draw(gameTime);
	}
}