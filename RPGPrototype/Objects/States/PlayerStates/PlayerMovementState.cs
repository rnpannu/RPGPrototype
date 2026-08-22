using Microsoft.Xna.Framework;
using MonoGameLibrary;

namespace RPGPrototype.Objects.States.PlayerStates;

public class PlayerMovementState : PlayerState
{
	public const string StateName = nameof(PlayerMovementState);
	public override string Name => StateName;

	private Vector2 LastMovementDirection { get; set; }
	
	public PlayerMovementState(Player player) : base(player)
	{

	}
	
	public override void Enter()
	{
		base.Enter();
		Player.SetAnimation(Player.AnimationKey.Walk, Player.CurrentAnimation.Item2);
	}

	public override void Exit()
	{
		base.Exit();
	}
	
	public override void Update(GameTime gameTime)
	{
		base.Update(gameTime);
		
		Vector2 moveDir = Player.MovementDirection;
		Vector2 speed = Player.Velocity;
		
		if (moveDir != LastMovementDirection && !moveDir.IsZero())
		{
			// Can potentially move UpdateAnimation trigger into FacingDirection
			// property setter, will see after implementing attacks
			//Player.UpdateAnimation();
		}
		LastMovementDirection = moveDir;
		
		if (moveDir.IsZero() && speed.IsZero())
		{
			Player.StateMachine.Transition(PlayerIdleState.StateName);
			return;
		}
	}
}
