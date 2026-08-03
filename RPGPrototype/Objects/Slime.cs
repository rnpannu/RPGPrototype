using System;
using System.Collections.Generic;
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
		Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
	}
	
	public void Follow(GameTime gameTime, Vector2 target)
	{
		Vector2 delta = target - Position;
		//_arrivalSpeed = _maxVelocity * Math.Clamp(delta.LengthSquared() / 25, 0, 1); // 5 pixel slowing radius
		if (!delta.IsZero(1.5f))
		{
			//MovementDirection = Vector2.Normalize(delta);
			MovementDirection = delta;
		}
		else
		{
			Position = target;
			MovementDirection = Vector2.Zero;
		}
	}

	public void UpdateVelocity(GameTime gameTime)
	{
		float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
		

		if (MovementDirection.IsZero()) // decelerate
		{
			Velocity = Vector2.Lerp(Velocity, Vector2.Zero, 50f //* dt
			);
		}
		else
		{
			Velocity += MovementDirection * Acceleration// * dt
				;
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