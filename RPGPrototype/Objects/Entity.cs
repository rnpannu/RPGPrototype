using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;

namespace RPGPrototype.Objects;

public abstract class Entity
{
	/*private int _baseMaxHealth;
	private Vector2 _baseMovementSpeed;*/
	
	public Entity(Vector2 position, Vector2 velocity)
	{
		Position = position;
		Velocity = velocity;
		Velocity = Vector2.Zero;
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

	public virtual Vector2 Position
	{
		get => field;
		protected set
		{
			field = value;
		}
	}
	public virtual Vector2 MovementDirection { get; set; }
	
	public virtual Vector2 FacingDirection { get; set; }
	
	public virtual Vector2 Velocity { get; set; }
	
	public virtual Vector2 Acceleration { get; set; }
	
	public virtual Rectangle Rect =>
		new Rectangle((int)(Position.X - Sprite.Origin.X),
			(int)(Position.Y - Sprite.Origin.Y),
			(int)Sprite.Width,
			(int)Sprite.Height);

	public virtual Rectangle Hitbox { get; protected set; }
	
	public virtual void Initialize()
	{
		Position = new Vector2(250, 250);
		Velocity = new Vector2(10, 10);
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
	public virtual void DrawHitBox()
	{
		int tileSize = 16;
		Texture2D rectangleTexture = new Texture2D(Core.GraphicsDevice, 1, 1);
		rectangleTexture.SetData(new Color[] {new (255, 0, 0, 255)});
		Core.SpriteBatch.Draw(rectangleTexture, Rect, Color.Lavender);
	}
}