using Microsoft.Xna.Framework;

namespace RPGPrototype.Scenes.States;

public abstract class LevelState : State
{
	public abstract string Name { get; }

	public LevelState(LevelScene level) // just pass in the scene for now. Later create new Level object?
	{
		Level = level;
	}

	public LevelScene Level { get; protected set; }
	public virtual void Enter()
	{

	}

	public virtual void Exit()
	{

	}

	public virtual void Update(GameTime gameTime)
	{

	}

	public virtual void Draw(GameTime gameTime)
	{

	}
}