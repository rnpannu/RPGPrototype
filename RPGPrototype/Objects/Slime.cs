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
	
	public Slime(Vector2 position) : base(position)
	{
		_maxVelocity = new Vector2(40, 40);
		Acceleration = new Vector2(800, 800);
		DetectionDistance = 100;
		Hitbox = new RectangleF(Position.X - 4, Position.Y - 4, 8, 8);
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
		}

	}
}