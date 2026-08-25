using System;
using Microsoft.Xna.Framework;
using MonoGameLibrary;

namespace RPGPrototype.Objects.States.PlayerStates;

public class PlayerIdleState : PlayerState
{
	public event Action RequestTransitionToMove;
	
	public PlayerIdleState(Player player) : base(player)
	{
		
	}

	public override void Enter()
	{
		base.Enter();
		Player.SetAnimation(Player.AnimationKey.Idle, Player.CurrentAnimation.Item2);
	}

	public override void Exit()
	{
		base.Exit();
	}

	public override void Update(GameTime gameTime)
	{
		base.Update(gameTime);
		if (!Player.MovementDirection.IsZero())
		{
			Player.FacingDirection = Player.MovementDirection;
			RequestTransitionToMove?.Invoke();
			return;
		}
	}
}