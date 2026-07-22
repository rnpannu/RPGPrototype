using Microsoft.Xna.Framework;

namespace RPGPrototype.Objects.States.SlimeStates;

public abstract class SlimeState : State
{
	public SlimeState(Slime slime)
	{
		Slime = slime;
		SlimeStateMachine = Slime.StateMachine;
	}
	public abstract string Name { get; }
	
	protected Slime Slime
	{
		get => field;
		set => field = value;
	}
	
	protected StateMachine SlimeStateMachine
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