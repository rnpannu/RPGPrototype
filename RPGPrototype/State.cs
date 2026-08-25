using System;
using Microsoft.Xna.Framework;

namespace RPGPrototype;

public interface State
{
	//public float TimeInState { get; }
	public void Enter();

	//public bool CanExit(); // Say when stunned, cannot exit until timer is completely over 
	public void Exit();

	public void Update(GameTime gameTime);
	
	public void Draw(GameTime gameTime);
}