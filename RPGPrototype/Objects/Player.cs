using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using RPGPrototype.Objects.States;
using RPGPrototype.Objects.States.PlayerStates;

namespace RPGPrototype.Objects;

public class Player : Entity
{
	private AnimatedSprite AnimatedSprite => (AnimatedSprite) Sprite;
	private List<Animation> _animations = new();
	
	public Player(Vector2 position) : base(position)
	{
		_maxVelocity = new Vector2(100, 100);
		Acceleration = new Vector2(80, 80);
	}

	public StateMachine StateMachine
	{
		get => field;
		private set => field = value;
	}

	public override Vector2 MovementDirection
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
	
	
	public override void Initialize()
	{
		List<State> states =
		[
			new PlayerIdleState(this),
			new PlayerMovementState(this)
		];
		StateMachine = new StateMachine(states);
	}
	
	public void LoadContent(TextureAtlas objectAtlas)
	{
		//Sprite = objectAtlas.CreateSprite("player-1");
		_animations.Add(objectAtlas.GetAnimation("player-walking-up"));
		_animations.Add(objectAtlas.GetAnimation("player-walking-down"));
		_animations.Add(objectAtlas.GetAnimation("player-walking-right"));
		Sprite =  objectAtlas.CreateAnimatedSprite("player-walking-right");

	}
	
	public override void Update(GameTime gameTime)
	{
		base.Update(gameTime);
		StateMachine.Update(gameTime);
	}
	
	/// <summary>
	/// Increase or decay velocity according to current movement input.
	/// </summary>
	public void UpdateVelocity(GameTime gameTime)
	{
		// Potentially want to refactor into movement state
		float accel = (float)Math.Pow((double)(Acceleration.X), 2) * Core.DT; // Make everything linear
		float velocityDecay = 50f * (float) gameTime.ElapsedGameTime.TotalSeconds; // Decays at 50 / second at 60fps? Math could be wrong

		if (!MovementDirection.IsZeroX() && !MovementDirection.IsZeroY())
		{
			Velocity += MovementDirection * Acceleration; //* (float) gameTime.ElapsedGameTime.TotalSeconds;
			Velocity = Vector2.Clamp(Velocity, -_maxVelocity, _maxVelocity);
		}
		else
		{
			float newX = !MovementDirection.IsZeroX()
				? Velocity.X + MovementDirection.X * Acceleration.X //* (float)gameTime.ElapsedGameTime.TotalSeconds)
				: GameUtils.BasicLerp(Velocity.X, 0, velocityDecay);

			float newY = !MovementDirection.IsZeroY()
				? Velocity.Y + MovementDirection.Y * Acceleration.Y //* (float) gameTime.ElapsedGameTime.TotalSeconds
				: GameUtils.BasicLerp(Velocity.Y, 0, velocityDecay);
			
			Velocity = Vector2.Clamp(new Vector2(newX, newY), -_maxVelocity, _maxVelocity);
		}
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

	public void Move()
	{
		Position += Velocity * Core.DT;
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
	
	public override void Draw(GameTime gameTime)
	{
		base.Draw(gameTime);
		//Sprite.Draw(Core.SpriteBatch, Position);
	}
}