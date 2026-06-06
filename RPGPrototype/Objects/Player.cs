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
	public Vector2 _position;
	private Vector2 _movementSpeed;
	private AnimatedSprite _sprite;
	//private AnimatedSprite _sprite;
	private List<Animation> _animations = new();
	
	//private Action SpriteChanged 
	public Player()
	{
		Initialize();
	}
	public new Vector2 Position
	{
		get => _position;
		set => _position = value;
	}

	public Vector2 Speed
	{
		get => _movementSpeed;
		set => _movementSpeed = value;
	}
	
	public AnimatedSprite Sprite
	{
		get => _sprite;
		private set
		{
			_sprite = value;
			//_sprite.CenterOrigin();
		} 
	}

	public void updateAnimiation(Vector2 movementDir)
	{
		Sprite.Effects = SpriteEffects.None;
		if (Math.Abs(movementDir.Y) > Math.Abs(movementDir.X) * 1.5)
		{
			//UP
			if (movementDir.Y < 0)
			{
				Sprite.Animation = _animations[0];
			}
			//Down
			else
			{
				Sprite.Animation = _animations[1];
			}
		}

		else
		{
			//Right
			if (movementDir.X > 0)
			{
				Sprite.Animation = _animations[2];
			}
			//Left
			else
			{
				
				Sprite.Effects = SpriteEffects.FlipHorizontally;
				Sprite.Animation = _animations[2];
				
			}
		}
	}

	
	
	
	
	//public Rectangle Hitbox => new Rectangle((int)(Position.X - Sprite.Origin.X), (int)(Position.Y - Sprite.Origin.Y), (int)Sprite.Width, (int)Sprite.Height);
	public Rectangle Rect => new Rectangle((int)(Position.X), (int)(Position.Y), (int)_sprite.Width, (int)_sprite.Height);
	public void Initialize()
	{
		Position = new Vector2(50, 50);
		_movementSpeed = new Vector2(130, 130);
	}

	public void LoadContent(TextureAtlas objectAtlas)
	{
		//Sprite = objectAtlas.CreateSprite("player-1");
		_animations.Add(objectAtlas.GetAnimation("player-walking-up"));
		_animations.Add(objectAtlas.GetAnimation("player-walking-down"));
		_animations.Add(objectAtlas.GetAnimation("player-walking-right"));
		_sprite = objectAtlas.CreateAnimatedSprite("player-walking-right");
		
	}

	public void Update(GameTime gameTime)
	{
		_sprite.Update(gameTime);
	}

	public void Move(float xAmount, float yAmount, bool absolute = false)
	{
		if (!absolute)
		{
			_position.X += xAmount;
			_position.Y += yAmount;
		}
		else
		{
			_position.X = xAmount;
			_position.Y = yAmount;
		}
	}

	public void Draw(GameTime gameTime)
	{
		Sprite.Draw(Core.SpriteBatch, Position);
	}

	public void DrawHitBox()
	{
		int tileSize = 16;
		Texture2D rectangleTexture = new Texture2D(Core.GraphicsDevice, 1, 1);
		rectangleTexture.SetData(new Color[] {new (255, 0, 0, 255)});
		
		Core.SpriteBatch.Draw(rectangleTexture, Rect, Color.Lavender);
		
	}

}