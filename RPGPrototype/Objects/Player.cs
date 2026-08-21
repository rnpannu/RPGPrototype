using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using RPGPrototype.Objects.States;
using RPGPrototype.Objects.States.PlayerStates;
using RPGPrototype.Scenes.Input;

namespace RPGPrototype.Objects;

public class Player : Entity
{
	public StateMachine StateMachine
	{
		get => field;
		private set => field = value;
	}
	
	public PlayerInput CurrentInput { get; set; }
	
	public override RectangleF Hitbox => new RectangleF(Position.X - 6, Position.Y - 8, 12, 16);	 // magic numbers from spritesheet

	public enum AnimationKey { Idle, Walk, Attack, Dead }
	public enum AnimationDirection { Up, Down, Side, None }
	private AnimatedSprite AnimatedSprite => (AnimatedSprite) Sprite;
	public (AnimationKey, AnimationDirection) CurrentAnimation { get; private set; }
	
	public Dictionary<(AnimationKey, AnimationDirection), Animation?> Animations
	{
		get => field;
		set => field = value;
	} = new();
	
	public Player(Vector2 position) : base(position)
	{
		_maxVelocity = new Vector2(100, 100);
		Acceleration = new Vector2(800, 800);
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
		var up = AnimationDirection.Up;
		var down = AnimationDirection.Down;
		var side = AnimationDirection.Side;

		var key = AnimationKey.Walk;
		Animations[(key, up)] = objectAtlas.GetAnimation("player-walking-up");
		Animations[(key, down)] = objectAtlas.GetAnimation("player-walking-down");
		Animations[(key, side)] = objectAtlas.GetAnimation("player-walking-right");
		key = AnimationKey.Idle;
		Animations[(key, up)] = objectAtlas.GetAnimation("player-idle-up");
		Animations[(key, down)] = objectAtlas.GetAnimation("player-idle-down");
		Animations[(key, side)] = objectAtlas.GetAnimation("player-idle-right");
		//Animations.TryAdd((key, up), objectAtlas.GetAnimation("player-walking-up"));
		
		Sprite =  objectAtlas.CreateAnimatedSprite("player-idle-right");
		SetAnimation(key, side);
	}
	
	public override void Think(GameTime gameTime)
	{
		ProcessInput();
		StateMachine.Update(gameTime);
	}

	public void ProcessInput(//PlayerInput input
	)
	{
		MovementDirection = CurrentInput.MovementDirection;
		if (!MovementDirection.IsZero())
		{
			FacingDirection = MovementDirection;
		}
	}
	/// <summary>
	/// Increase or decay velocity according to current movement input. Permit wall sliding by retaining velocity in other directions.
	/// </summary>
	public override void UpdateVelocity(GameTime gameTime)
	{
		// Potentially want to refactor into movement state
		float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
		float velocityDecay = 50f * (float) gameTime.ElapsedGameTime.TotalSeconds; // Decays at 50 / second at 60fps? Math could be wrong

		float newX;
		if (!MovementDirection.IsZeroX())
		{
			newX = Velocity.X + MovementDirection.X * Acceleration.X * dt;
		}
		else
		{
			newX = Decelerate(Velocity.X, Acceleration.X, dt);
		}

		float newY;
		if (!MovementDirection.IsZeroY())
		{
			newY = Velocity.Y + MovementDirection.Y * Acceleration.Y * dt;
		}
		else
		{
			newY = Decelerate(Velocity.Y, Acceleration.Y, dt);
		}
		
		Velocity = Vector2.Clamp(new Vector2(newX, newY), -_maxVelocity, _maxVelocity);
	}

	/// <summary>
	/// Helper function to decrease a velocity by an acceleration value
	/// </summary>
	/// <param name="velocity"></param>
	/// <param name="deceleration"></param>
	/// <param name="dt"></param>
	/// <returns>The new velocity after one deceleration increment</returns>
	private float Decelerate(float velocity, float deceleration, float dt)
	{
		float amount = deceleration * dt;

		if (Math.Abs(velocity) <= amount)
		{
			return 0f;
		}

		return velocity -
		       Math.Sign(velocity) * amount;
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

	public void SetAnimation(AnimationKey key, AnimationDirection direction)
	{
		Animations.TryGetValue((key, direction), out Animation? animation);
		if (animation != null)
		{
			AnimatedSprite.Animation = animation;
			CurrentAnimation = (key, direction);
			if (direction == AnimationDirection.Side) // Left / right
			{
				AnimatedSprite.Effects = FacingDirection.X < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
			}
			else
			{
				AnimatedSprite.Effects = SpriteEffects.None;
			}
		}
	}
	/// <summary>
	/// Change the player's animation upon a movement direction change
	/// </summary>
	/// <param name="movementDir">The current direction of movement</param>
	public void UpdateAnimation()
	{
		AnimationDirection direction;
		if (Math.Abs(FacingDirection.Y) > Math.Abs(FacingDirection.X) * 1.5) // Prefer horizontal animations
		{
			if (FacingDirection.Y < 0) 
			{
				direction = AnimationDirection.Up;
			}
			else 
			{
				direction = AnimationDirection.Down;
			}
		}
		else
		{
			direction = AnimationDirection.Side;
		}

		SetAnimation(CurrentAnimation.Item1, direction);
	}
	
	public override void Draw(GameTime gameTime)
	{
		base.Draw(gameTime);
	}
}
