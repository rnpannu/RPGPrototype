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
	
	public void Follow(GameTime gameTime, Vector2 target)
	{
		Vector2 delta = target - Position;

		float slowingRadius = 10f;
		float arrivalTolerance = 1.5f;
		float distance = delta.Length(); // scary expensive sqrt
		
		if (distance <= arrivalTolerance)
		{
			Position = target;
			MovementDirection = Vector2.Zero;
			Velocity = Vector2.Zero;
			_speedFactor = 0f;
			return;
		}
		MovementDirection = delta / distance;
		
		_speedFactor = Math.Clamp(
			distance / slowingRadius,
			0f,
			1f
		);
		
		/*if (!delta.IsZero(1.5f))
		{
			MovementDirection = Vector2.Normalize(delta);
		}
		else
		{
			Position = target;
			MovementDirection = Vector2.Zero;
			Velocity = Vector2.Zero;
		}
		Vector2 desiredVelocity =
			MovementDirection * _maxVelocity * arrivalFactor;
		
		Vector2 steering =
			desiredVelocity - Velocity;

		Velocity += steering * someAmount * dt;*/
	}

	public void UpdateVelocity(GameTime gameTime)
	{
		float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
		

		if (MovementDirection.IsZero()) // decelerate
		{
			Velocity = Vector2.Lerp(Velocity, Vector2.Zero, Math.Clamp(10f * dt, 0f, 1f)
			);
		}
		else
		{
			float maxSpeed = _maxVelocity.X;
			
			Vector2 desiredVelocity =
				MovementDirection * maxSpeed * _speedFactor;
			
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
			_speedFactor = 1f;
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