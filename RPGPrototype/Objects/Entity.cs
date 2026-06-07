using Microsoft.Xna.Framework;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;

namespace RPGPrototype.Objects;

public abstract class Entity
{
	public virtual Sprite Sprite { get; protected set; }
	public virtual Vector2 Position { get; protected set; }
	public virtual Vector2 MovementSpeed { get; protected set; }
	public virtual Vector2 CurrentVelocity { get; protected set; }
	
	public virtual void Initialize()
	{

	}

	public virtual void LoadContent()
	{
		
	}

	public virtual void Update(GameTime gameTime)
	{

	}

	public virtual void Draw(GameTime gameTime)
	{
		Sprite.Draw(Core.SpriteBatch, Position);
	}
}