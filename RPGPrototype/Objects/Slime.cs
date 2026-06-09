using Microsoft.Xna.Framework;
using MonoGameLibrary.Graphics;

namespace RPGPrototype.Objects;

public class Slime : Enemy
{
	public Slime(Vector2 position, Vector2 movementSpeed) : base(position, movementSpeed)
	{
		
	}

	public AnimatedSprite AnimatedSprite => (AnimatedSprite) Sprite;
	
	public override void Initialize()
	{
		base.Initialize();
	}

	public override void LoadContent(TextureAtlas objectAtlas)
	{
		Sprite = objectAtlas.CreateAnimatedSprite("slime-idle");
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