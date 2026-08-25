using System;
using Microsoft.Xna.Framework;
using MonoGameLibrary.Graphics;

namespace RPGPrototype.Objects.States.PlayerStates;

public class PlayerAttackState : PlayerState
{
	private float _windupTime;
	private float _activeAttackTime;

	public RectangleF? ActiveHitbox = null;
	
	public event Action RequestTransitionOutOfAttack;
	
	public PlayerAttackState(Player player) : base(player)
	{
		_windupTime = 100f;
		_activeAttackTime = 300f; //adds up to 0.4ms
	}
	
	public override void Enter()
	{
		base.Enter();
		
		Player.SetAnimation(Player.AnimationKey.Attack, Player.CurrentAnimation.Item2);
		Player.CanMove = false; // can be in this or caller
	}

	public override void Exit()
	{
		base.Exit();
	}

	public override void Update(GameTime gameTime)
	{
		base.Update(gameTime);
		if (TimeInState <= _windupTime) ;
		else if (TimeInState >= _windupTime + _activeAttackTime) RequestTransitionOutOfAttack?.Invoke();
		else ;
		
	}
}