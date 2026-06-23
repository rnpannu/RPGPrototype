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
		
		if (input == Vector2.Zero && speed == Vector2.Zero)
		{
			Player.StateMachine.Transition(PlayerIdleState.StateName);
			return;
		} 
		Player.UpdateVelocity();
		/*else if (input == Vector2.Zero)
		{
			Player.Velocity -= _acceleration * Core.DT;
		}
		else
		{
			Player.Velocity += input * _acceleration * Core.DT;
			Player.Velocity = Vector2.Clamp(Player.Velocity, -_maxVelocity, _maxVelocity);
		}#1#*/
		
	}
}