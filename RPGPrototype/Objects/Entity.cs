using System.Collections.Specialized;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;

namespace RPGPrototype.Objects;

public abstract class Entity
{
	/*private int _baseMaxHealth;
	private Vector2 _baseMovementSpeed;*/

	protected Vector2 _maxVelocity;

	public Entity(Vector2 position)
	{
		Position = position;
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
		set
		{
			field = value;
		}
	}

	public virtual float ZElevation
    {
        get => field;
        set
        {
            field = value;
        }
    }

    public virtual float ZVelocity
    {
        get => field;
        set
        {
            field = value;
        }
    }
    public virtual float JumpDuration
    {
        get => field;
        set
        {
            field = 120;
        }
    }

    public virtual float CurrentJumpDuration
    {
        get => field;
        set
        {
            field = value;
        }
    }

    public virtual float ZAcceleration
    {
        get => field;
        set {
            field = value;
        }
    }

    public virtual Vector2 MovementDirection
    {
        get => field;
        set
        {
            field = value.SnapToZero(); // Input direction
            if (field != Vector2.Zero)
            {
                FacingDirection = field;
            }
        }
    }

    public virtual Vector2 FacingDirection
    {
        get => field;
        set => field = value.SnapToZero();
    } = new Vector2(0, 1); // facing down


    public virtual Vector2 Velocity
    {
        get => field;
        set => field = value.SnapToZero();
    }

	public virtual Vector2 Acceleration {
		get => field;
		set => field = value.SnapToZero(); }

	public virtual Rectangle Rect =>
		new Rectangle((int)(Position.X - Sprite.Origin.X),
			(int)(Position.Y - Sprite.Origin.Y),
			(int)Sprite.Width,
			(int)Sprite.Height);

	public virtual RectangleF RectF =>
		new RectangleF((Position.X - Sprite.Origin.X),
			(Position.Y - Sprite.Origin.Y),
			Sprite.Width,
			Sprite.Height);

	public virtual RectangleF Hitbox { get; protected set; }

	public virtual void Initialize()
	{

	}

	public virtual void LoadContent()
	{

	}

	public virtual void Think(GameTime gameTime) { }

    public virtual void UpdateVelocity(GameTime gameTime) { }

    public virtual void Jump(GameTime gameTime) { }

	public virtual void ApplyMovement(GameTime gameTime)
	{
		Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
		if (Sprite is AnimatedSprite sprite)
		{
			sprite.Update(gameTime);
		}
	}

	// convenience wrapper for anything that doesn't require collision resolution
	public virtual void Update(GameTime gameTime) // Should every entity have a state machine?
	{
		Think(gameTime);
		UpdateVelocity(gameTime);
		ApplyMovement(gameTime);
	}

	public virtual void Draw(GameTime gameTime)
    {
        float scaleZ = ZElevation / 100; //100 Should be replaced with max jump height for player.
        Vector2 jumpPos = new Vector2(Position.X, Position.Y - (scaleZ * 16));
		Sprite.Draw(Core.SpriteBatch, jumpPos);
	}

	/// <summary>
	/// Utility: Highlight the entity's rectangle or hitbox
	/// </summary>
	public virtual void DrawHitBox()
	{
		int tileSize = 16;
		Texture2D rectangleTexture = new Texture2D(Core.GraphicsDevice, 1, 1);
		rectangleTexture.SetData(new Color[] {new (255, 0, 0, 255)});
		Core.SpriteBatch.Draw(rectangleTexture, new Rectangle((int) Hitbox.X, (int) Hitbox.Y, (int) Hitbox.Width, (int) Hitbox.Height), Color.Lavender);
	}
}
