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
	public Slime(Vector2 position) : base(position)
	{
		_maxVelocity = new Vector2(30, 30);
		Acceleration = new Vector2(1, 1);
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

	public override void Update(GameTime gameTime, Vector2 target)
	{
		base.Update(gameTime);
		Follow(gameTime, target);
		Position += Velocity;
	}
	
	public void Follow(GameTime gameTime, Vector2 target)
	{
		Vector2 delta = target - Position;
		if (!delta.IsZero())
		{
			MovementDirection = Vector2.Normalize(delta);
			UpdateVelocity(gameTime);
		}
		
	}

	public void UpdateVelocity(GameTime gameTime)
	{
		Velocity += MovementDirection * Acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
		Velocity = Vector2.Clamp(Velocity, -_maxVelocity, _maxVelocity);
	}
	

	public override void Draw(GameTime gameTime)
	{
		base.Draw(gameTime);
	}
}