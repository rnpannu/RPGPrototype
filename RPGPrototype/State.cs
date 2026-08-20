using Microsoft.Xna.Framework;

namespace RPGPrototype;

public interface State
{
	public string Name { get; }
	public void Enter();

	public void Exit();

	public void Update(GameTime gameTime);
	
	public void Draw(GameTime gameTime);
}