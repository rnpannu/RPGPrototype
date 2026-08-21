using Microsoft.Xna.Framework;

namespace RPGPrototype.Objects.States.PlayerStates;

public abstract class PlayerState : State
{
	public abstract string Name { get; }
		
	protected Player Player
	{
		get => field;
		set => field = value;
	}
	
	public PlayerState(Player player)
	{
		Player = player;
	}
	
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