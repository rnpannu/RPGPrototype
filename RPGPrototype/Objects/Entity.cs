using Microsoft.Xna.Framework;
using MonoGameLibrary.Graphics;

namespace RPGPrototype.Objects;

public abstract class Entity
{
	public Vector2 Position { get; protected set; }
	public Sprite Sprite { get; protected set; }

	public void Initialize()
	{
		throw new System.NotImplementedException();
	}

	public void LoadContent()
	{
		throw new System.NotImplementedException();
	}

	public void Update(GameTime gameTime)
	{
		throw new System.NotImplementedException();
	}

	public void Draw(GameTime gameTime)
	{
		
	}
}