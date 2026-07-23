using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using RPGPrototype.Objects.States;
using RPGPrototype.Objects.States.SlimeStates;

namespace RPGPrototype.Objects;

public class Slime : Enemy
{
	public Slime(Vector2 position, Vector2 movementSpeed) : base(position, movementSpeed)
	{
		
	}

	public AnimatedSprite AnimatedSprite => (AnimatedSprite) Sprite;
	
	public override void Initialize()
	{
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
		Position += Velocity;
	}
	public void Follow(GameTime gameTime, Vector2 target)
	{
		Vector2 delta = target - Position;
		if (!GameUtils.IsZeroVector(delta))
		{
			MovementDirection = Vector2.Normalize(delta);
			UpdateVelocity(gameTime);
		}
		
	}

	public void UpdateVelocity(GameTime gameTime)
	{
		float accel = (float) Math.Pow ((double)(Acceleration.X), 2) * Core.DT;
		Velocity += MovementDirection * accel;
		Velocity = Vector2.Clamp(Velocity, new Vector2(-100, -100), new Vector2(100,100));
		//Velocity = Vector2.Clamp(Velocity, -_maxVelocity, _maxVelocity);
	}
	

	public override void Draw(GameTime gameTime)
	{
		base.Draw(gameTime);
	}
}