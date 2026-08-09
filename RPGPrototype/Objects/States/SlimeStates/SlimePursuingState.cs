using System.Text;
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

	public override void Update(GameTime gameTime)
	{
		// if slime has LOS -> Pursue
		// if slime lose LOS -> Pursue last known location
		// if no LOS there -> return to idle / patrolling
		// + alert state, pause state
		base.Update(gameTime);
		if (!Slime.HasLOS)
		{
			Slime.StateMachine.Transition(SlimeIdleState.StateName);
			return;
		}
		
		Slime.Follow(gameTime, Slime.CurrentLOSTarget);
		
	}
}