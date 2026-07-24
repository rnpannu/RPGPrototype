using Microsoft.Xna.Framework;
using MonoGameLibrary;

namespace RPGPrototype.Objects.States.PlayerStates;

public class PlayerMovementState : PlayerState
{
	// lastMoveDir, basespeed, health, maxhealth, attackdir, scale, 
	public const string StateName = nameof(PlayerMovementState);
	public override string Name => StateName;
	
	public PlayerMovementState(Player player) : base(player)
	{

	}

	public override void Update(GameTime gameTime)
	{
		base.Update(gameTime);
		
		Vector2 input = Player.MovementDirection;
		Vector2 speed = Player.Velocity;
		
		if (input.LengthSquared() < 0.01f && speed.LengthSquared() < 0.01f)
		{
			Player.StateMachine.Transition(PlayerIdleState.StateName);
			return;
		} 
		Player.UpdateVelocity(gameTime);

	}
}