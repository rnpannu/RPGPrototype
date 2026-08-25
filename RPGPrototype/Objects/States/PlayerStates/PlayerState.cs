using System;
using Microsoft.Xna.Framework;

namespace RPGPrototype.Objects.States.PlayerStates;

public abstract class PlayerState : State
{
	public virtual float TimeInState { get; protected set; }
	
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
		TimeInState = 0f;
	}

	public virtual void Exit()
	{
		
	}

	public virtual void Update(GameTime gameTime)
	{
		TimeInState += (float) gameTime.ElapsedGameTime.TotalMilliseconds;
	}

	public virtual void Draw(GameTime gameTime)
	{
		
	}
}