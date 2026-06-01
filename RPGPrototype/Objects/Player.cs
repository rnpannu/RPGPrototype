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
	private Sprite _sprite;
	private List<Animation> _animations;
	
	//private Action SpriteChanged 
	public Player()
	{
		Initialize();
	}
	public Vector2 Position
	{
		get => _position;
		set => _position = value;
	}

	public Vector2 Speed
	{
		get => _movementSpeed;
		set => _movementSpeed = value;
	}
	public Sprite Sprite
	{
		get => _sprite;
		private set
		{
			_sprite = value;
			_sprite.CenterOrigin();
		} 
	}

	public Rectangle Hitbox => new Rectangle((int)(Position.X - Sprite.Origin.X), (int)(Position.Y - Sprite.Origin.Y), (int)Sprite.Width, (int)Sprite.Height);

	public void Initialize()
	{
		Position = new Vector2(50, 50);
		_movementSpeed = new Vector2(130, 130);
	}

	public void LoadContent(TextureAtlas objectAtlas)
	{
		Sprite = objectAtlas.CreateSprite("player-1");
	}

	public void Update(GameTime gameTime)
	{
		
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
		
		Core.SpriteBatch.Draw(rectangleTexture, Hitbox, Color.Lavender);
		
	}

}