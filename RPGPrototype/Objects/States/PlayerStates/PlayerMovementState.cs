using Microsoft.Xna.Framework;
using MonoGameLibrary;

namespace RPGPrototype.Objects.States.PlayerStates;

public class PlayerMovementState : PlayerState
{
	// lastMoveDir, basespeed, health, maxhealth, attackdir, scale,
	public const string StateName = nameof(PlayerMovementState);
	public override string Name => StateName;

	private Vector2 LastMovementDirection { get; set; }
	
	public PlayerMovementState(Player player) : base(player)
	{

	}

	public override void Update(GameTime gameTime)
	{
		base.Update(gameTime);
		
		Vector2 input = Player.MovementDirection;
		Vector2 speed = Player.Velocity;
		
		if (input != LastMovementDirection)
		{
			Player.UpdateAnimation(input);
		}
		LastMovementDirection = input;
		
		if (input.IsZero() && speed.IsZero())
		{
			Player.StateMachine.Transition(PlayerIdleState.StateName);
			return;
		}
	}
}
