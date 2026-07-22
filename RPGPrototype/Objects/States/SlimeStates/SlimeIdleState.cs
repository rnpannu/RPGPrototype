using Microsoft.Xna.Framework;
using RPGPrototype.Objects.States.PlayerStates;

namespace RPGPrototype.Objects.States.SlimeStates;

public class SlimeIdleState : SlimeState
{
	public const string StateName = nameof(SlimeIdleState);
	public override string Name => StateName;

	public SlimeIdleState(Slime slime) : base(slime)
	{

	}

	public override void Update(GameTime gameTime)
	{
		base.Update(gameTime);
		/*if (Slime.IsAggroed())
		{
			Slime.StateMachine.Transition(SlimePursuingState.StateName);
			return;
		}

		Slime.UpdateVelocity(); // still allow deceleration*/

	}
}