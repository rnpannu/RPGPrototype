using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;

namespace RPGPrototype.Objects;

public abstract class Entity
{
	public Entity(Vector2 position, Vector2 movementSpeed)
	{
		Position = position;
		MovementSpeed = movementSpeed;
		CurrentVelocity = Vector2.Zero;
	}
	public virtual Sprite Sprite
	{
		get => field;
		protected set
		{
			field = value;
			field.CenterOrigin();
		}
	}

	public Vector2 Position
	{
		get => field;
		protected set
		{
			field = value;
		}
	}

	public virtual Rectangle Rect =>
		new Rectangle((int)(Position.X - Sprite.Origin.X),
			(int)(Position.Y - Sprite.Origin.Y),
			(int)Sprite.Width,
			(int)Sprite.Height);

	public Vector2 MovementSpeed { get; protected set; }
	
	public Vector2 CurrentVelocity { get; protected set; }
	
	public virtual void Initialize()
	{
		Position = new Vector2(250, 250);
		MovementSpeed = new Vector2(10, 10);
	}

	public virtual void LoadContent()
	{
		
	}

	public virtual void Update(GameTime gameTime)
	{
		if (Sprite is AnimatedSprite sprite)
		{
			sprite.Update(gameTime);
		}
	}

	public virtual void Draw(GameTime gameTime)
	{
		Sprite.Draw(Core.SpriteBatch, Position);
	}

	/// <summary>
	/// Utility: Highlight the entity's rectangle or hitbox
	/// </summary>
	public void DrawHitBox()
	{
		int tileSize = 16;
		Texture2D rectangleTexture = new Texture2D(Core.GraphicsDevice, 1, 1);
		rectangleTexture.SetData(new Color[] {new (255, 0, 0, 255)});
		Core.SpriteBatch.Draw(rectangleTexture, Rect, Color.Lavender);
	}
}