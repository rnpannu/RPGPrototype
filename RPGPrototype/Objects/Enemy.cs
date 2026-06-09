using Microsoft.Xna.Framework;
using MonoGameLibrary.Graphics;

namespace RPGPrototype.Objects;

public class Enemy : Entity
{
	public Enemy(Vector2 position, Vector2 movementSpeed) : base(position, movementSpeed)
	{
		
	}

	public override void Initialize()
	{
		base.Initialize();
	}

	public virtual void LoadContent(TextureAtlas atlas)
	{
		
	}

	public override void Update(GameTime gameTime)
	{
		base.Update(gameTime);
	}

	public override void Draw(GameTime gameTime)
	{
		base.Draw(gameTime);
	}
}