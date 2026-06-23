using Microsoft.Xna.Framework;

namespace RPGPrototype.Objects.States.PlayerStates;

public abstract class PlayerState : State
{
	public PlayerState(Player player)
	{
		Player = player;
		PlayerStateMachine = Player.StateMachine;
	}
	public abstract string Name { get; }
	
	protected Player Player
	{
		get => field;
		set => field = value;
	}
	
	protected StateMachine PlayerStateMachine
	{
		get => field;
		set => field = value;
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
}