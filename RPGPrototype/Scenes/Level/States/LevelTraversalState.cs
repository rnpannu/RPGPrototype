using Microsoft.Xna.Framework;
using RPGPrototype.Scenes.Input;

namespace RPGPrototype.Scenes.States;

public class LevelTraversalState : LevelState
{
	//public const string StateName = nameof(LevelTraversalState);
	public override string Name => nameof(LevelTraversalState);
	
	public LevelTraversalState(LevelScene level) : base(level)
	{
		// Can rename to LevelRunningState
	}
	
	public override void Enter()
	{
		base.Enter();
		// LevelInventoryState -> InputManager.CenterMousePosition
	}

	public override void Exit()
	{
		base.Exit();
	}

	public override void Update(GameTime gameTime)
	{
		base.Update(gameTime);
		PlayerInput playerInput = Level.InputManager.GetPlayerInput(gameTime);
		Level.ObjectManager.Update(gameTime, playerInput);
		Level.Camera.Follow(Level.ObjectManager.Player.Position);
	}

	public override void Draw(GameTime gameTime)
	{
		Level.ObjectManager.Draw(gameTime);
	}
}