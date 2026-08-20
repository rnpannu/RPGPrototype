using Microsoft.Xna.Framework;

namespace RPGPrototype.Objects.States.PlayerStates;

public class PlayerIdleState : PlayerState
{
	public const string StateName = nameof(PlayerIdleState);
	public override string Name => StateName;
	
	public PlayerIdleState(Player player) : base(player)
	{

	}

	public override void Enter()
	{
		base.Enter();
	}

	public override void Exit()
	{
		base.Exit();
	}

	public override void Update(GameTime gameTime)
	{
		base.Update(gameTime);
		if (Player.MovementDirection.LengthSquared() >= 0.01f)
		{
			Player.StateMachine.Transition(PlayerMovementState.StateName);
			return;
		}
	}
}