using System;
using Microsoft.Xna.Framework;
using MonoGameLibrary;
using RPGPrototype.Objects.States.PlayerStates;

namespace RPGPrototype.Objects.States.SlimeStates;

public class SlimeIdleState : SlimeState
{
	private Vector2 TargetPoint { get; set; }
	
	public SlimeIdleState(Slime slime) : base(slime)
	{
		int randomIndex = Random.Shared.Next(Slime.PatrolPoints.Count);
		TargetPoint = Slime.PatrolPoints[randomIndex];
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
		
		if (Slime.HasLOS)
		{
			Slime.StateMachine.Transition(typeof(SlimePursuingState));
			return;
		}
		else
		{
			Patrol(gameTime);
		}
	}

	public void Patrol(GameTime gameTime)
	{
		if (Slime.Position.IsApproximately(TargetPoint, 1.5f))
		{
			/*int randomIndex = Random.Shared.Next(Slime.PatrolPoints.Count);
			TargetPoint = Slime.PatrolPoints[randomIndex];*/
			int currentTargetIndex = Slime.PatrolPoints.IndexOf(TargetPoint);
			int newTargetIndex = (currentTargetIndex + 1) % Slime.PatrolPoints.Count;
			Slime.Position = TargetPoint;
			TargetPoint = Slime.PatrolPoints[newTargetIndex];
 			Console.WriteLine("new target: " + TargetPoint.ToString());
		}
		else
		{
			Slime.Follow(gameTime, TargetPoint);
		}
	}
}