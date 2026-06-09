using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;

namespace RPGPrototype.Objects;

public class Player : Entity
{
	public Player(Vector2 position, Vector2 movementSpeed) : base(position, movementSpeed)
	{
		
	}

	private AnimatedSprite AnimatedSprite => (AnimatedSprite) Sprite;
	private List<Animation> _animations = new();
	
	//public Rectangle Hitbox => new Rectangle((int)(Position.X - Sprite.Origin.X), (int)(Position.Y - Sprite.Origin.Y), (int)Sprite.Width, (int)Sprite.Height);
	//public Rectangle Rect => new Rectangle((int) (Position.X), (int)(Position.Y), (int)Sprite.Width, (int)Sprite.Height);
	
	public override void Initialize()
	{
		
	}

	public void LoadContent(TextureAtlas objectAtlas)
	{
		//Sprite = objectAtlas.CreateSprite("player-1");
		_animations.Add(objectAtlas.GetAnimation("player-walking-up"));
		_animations.Add(objectAtlas.GetAnimation("player-walking-down"));
		_animations.Add(objectAtlas.GetAnimation("player-walking-right"));
		Sprite =  objectAtlas.CreateAnimatedSprite("player-walking-right");

	}
	
	/// <summary>
	/// Change the player's animation upon a movement direction change
	/// </summary>
	/// <param name="movementDir">The current direction of movement</param>
	public void UpdateAnimation(Vector2 movementDir)
	{
		AnimatedSprite.Effects = SpriteEffects.None;
		if (Math.Abs(movementDir.Y) > Math.Abs(movementDir.X) * 1.5) // Prefer horizontal animations
		{
			if (movementDir.Y < 0) // Up
			{
				AnimatedSprite.Animation = _animations[0];
			}
			else // Down
			{
				AnimatedSprite.Animation = _animations[1];
			}
		}
		else 
		{
			if (movementDir.X > 0) // Right
			{
				AnimatedSprite.Animation = _animations[2];
			}
			else // Left
			{
				Sprite.Effects = SpriteEffects.FlipHorizontally;
				AnimatedSprite.Animation = _animations[2];
			}
		}
	}
	public override void Update(GameTime gameTime)
	{
		base.Update(gameTime);
		//AnimatedSprite.Update(gameTime);
	}
	
	/// <summary>
	/// Alter the Player's position by an amount, or force a move to an absolute position.
	/// </summary>
	/// <param name="xAmount">Offset from the current X position | New absolute X position</param>
	/// <param name="yAmount">Offset from the current Y position | New absolute Y position</param>
	/// <param name="absolute">Boolean flag to alter behaviour to an absolute positioning.</param>
	public void Move(float xAmount, float yAmount, bool absolute = false)
	{
		Position = !absolute ? new Vector2(Position.X + xAmount, Position.Y + yAmount) : new Vector2(xAmount, yAmount);
	}

	public override void Draw(GameTime gameTime)
	{
		base.Draw(gameTime);
		//Sprite.Draw(Core.SpriteBatch, Position);
	}
	
	/// <summary>
	/// Utility: Highlight the player's rectangle or hitbox
	/// </summary>
	public void DrawHitBox()
	{
		int tileSize = 16;
		Texture2D rectangleTexture = new Texture2D(Core.GraphicsDevice, 1, 1);
		rectangleTexture.SetData(new Color[] {new (255, 0, 0, 255)});
		Core.SpriteBatch.Draw(rectangleTexture, Rect, Color.Lavender);
	}

}