using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;

namespace RPGPrototype.Objects;

public class Player : Entity
{
	private Vector2 _position;
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

	public void Initialize()
	{
		Position = Vector2.Zero;
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

}