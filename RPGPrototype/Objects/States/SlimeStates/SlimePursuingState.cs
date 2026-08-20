using System.Text;
using System;
using Microsoft.Xna.Framework;
using MonoGameLibrary;

namespace RPGPrototype.Objects.States.SlimeStates;

public class SlimePursuingState : SlimeState
{
	public const string StateName = nameof(SlimePursuingState);
	public override string Name => StateName;

	public SlimePursuingState(Slime slime) : base(slime)
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
        // if slime has LOS -> Pursue
        // if slime lose LOS -> Pursue last known location
        // if no LOS there -> return to idle / patrolling
        // + alert state, pause state
        base.Update(gameTime);
        Vector2 lastLOS = Slime.CurrentLOSTarget;
		if (!Slime.HasLOS)
        {
            if (Slime.Position.IsApproximately(lastLOS, 1.5f))
            {
                Slime.StateMachine.Transition(SlimeIdleState.StateName);
            }
            else
            {
                Slime.Follow(gameTime, lastLOS);
            }
		}

		Slime.Follow(gameTime, Slime.CurrentLOSTarget);

	}
}
