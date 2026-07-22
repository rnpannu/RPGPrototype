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
		base.Update(gameTime);
		if (GameUtils.IsZeroVector(Slime.Velocity))
		{
			Slime.StateMachine.Transition(SlimeIdleState.StateName);
		}
	}
}