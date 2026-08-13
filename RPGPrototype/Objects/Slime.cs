using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using RPGPrototype.Objects.States;
using RPGPrototype.Objects.States.SlimeStates;

namespace RPGPrototype.Objects;

public class Slime : Enemy
{
	private static Texture2D _pixel;

	private float _speedFactor;
	//private Vector2 _arrivalSpeed;
	public Slime(Vector2 position) : base(position)
	{
		_maxVelocity = new Vector2(30, 30);
		Acceleration = new Vector2(80, 80);
		DetectionDistance = 100;
	}

	public AnimatedSprite AnimatedSprite => (AnimatedSprite) Sprite;
	
	public override void Initialize()
	{
		_pixel = new Texture2D(Core.GraphicsDevice, 1, 1);
		_pixel.SetData(new[] { Color.White });
		
		_patrolArea = new Rectangle(150, 50, 200, 200);
		base.Initialize();
		List<State> states =
		[
			new SlimeIdleState(this),
			new SlimePursuingState(this)
		];
		StateMachine = new StateMachine(states);
	}

	public override void LoadContent(TextureAtlas objectAtlas)
	{
		Sprite = objectAtlas.CreateAnimatedSprite("slime-idle");
	}

	public override void Update(GameTime gameTime)
	{
		base.Update(gameTime);
		UpdateVelocity(gameTime);
		Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds
			;
	}
	
	/// <summary>
	/// Path towards a target, 
	/// </summary>
	/// <param name="gameTime"></param>
	/// <param name="target"></param>
	public void Follow(GameTime gameTime, Vector2 target)
	{
		Vector2 delta = target - Position;
		float distance = delta.Length();
		float arrivalTolerance = 1.5f; // 1.5 pixel zone for "reaching" target
		float slowingRadius = 10f; // begin braking
		
		if (distance <= arrivalTolerance) // On top of target
		{
			Position = target;
			MovementDirection = Vector2.Zero;
			Velocity = Vector2.Zero;
			_speedFactor = 0f;
			return;
		}
		
		MovementDirection = delta / distance; // Normalize direction value
		
		// Decay velocity if within slowing zone
		_speedFactor = Math.Clamp(
			distance / slowingRadius,
			0f,
			1f
		);
		
	}
	
	public void UpdateVelocity(GameTime gameTime)
	{
		float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
		
		if (MovementDirection.IsZero()) // decelerate
		{
			float speed = Velocity.Length();
			float decelerationAmount = Acceleration.X * dt;

			if (speed <= decelerationAmount)
			{
				Velocity = Vector2.Zero;
			}
			else
			{
				Velocity -= Vector2.Normalize(Velocity) * decelerationAmount;
			}
			/*Velocity = Vector2.Lerp(Velocity, Vector2.Zero, Math.Clamp(10f * dt, 0f, 1f) // 0.167 magic value i guess, asymptotic lerp 16% toward 0 this frame i think
			);
			Velocity.SnapToZero();*/
		}
		else
		{
			float maxSpeed = _maxVelocity.X;
			
			Vector2 desiredVelocity = // without respect to acceleration
				MovementDirection * maxSpeed * _speedFactor;
			
			// Steer towards intended direction and magnitude without breaking acceleration limit
			Vector2 steering = 
				desiredVelocity - Velocity;
			
			float maxVelocityChange = Acceleration.X * dt;
			
			if (steering.LengthSquared() >
			    maxVelocityChange * maxVelocityChange)
			{
				steering = Vector2.Normalize(steering)
				           * maxVelocityChange;
			}
			Velocity += steering;
			_speedFactor = 1f; // Reset every frame
			//Velocity += MovementDirection * Acceleration * dt;
		}
		
		float maxSpeed2 = _maxVelocity.X;
		if (Velocity.LengthSquared() >
		    maxSpeed2 * maxSpeed2)
		{
			Velocity =
				Vector2.Normalize(Velocity) * maxSpeed2;
		}
		Velocity = Vector2.Clamp(Velocity, -_maxVelocity, _maxVelocity);
	}
	

	public override void Draw(GameTime gameTime)
	{
		base.Draw(gameTime);
		int size = 10;
		foreach (Vector2 point in PatrolPoints)
		{
			var rect = new Rectangle(
				(int)(point.X - size / 2f),
				(int)(point.Y - size / 2f),
				(int)size,
				(int)size);

			Core.SpriteBatch.Draw(_pixel, rect, Color.Green);
			/*spriteBatch.Draw(_pixel,
				new Rectangle((int)(point.X - radius), (int)(point.Y - thickness / 2f), (int)(radius * 2f), (int)thickness),
				color);
			spriteBatch.Draw(_pixel,
				new Rectangle((int)(point.X - thickness / 2f), (int)(point.Y - radius), (int)thickness, (int)(radius * 2f)),
				color);*/
		}
		
	}
}