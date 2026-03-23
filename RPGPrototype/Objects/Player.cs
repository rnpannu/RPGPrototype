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

	public Sprite Sprite
	{
		get => _sprite;
		set => _sprite = value;
	}

	public void Initialize()
	{
		Position = Vector2.Zero;
	}

	public void LoadContent(TextureAtlas objectAtlas)
	{
		Sprite = objectAtlas.CreateSprite("player-1");
		Sprite.CenterOrigin();
	}

	public void Update(GameTime gameTime, Vector2 position)
	{
		Position = position;
	}

	public void Draw(GameTime gameTime)
	{
		Sprite.Draw(Core.SpriteBatch, Position);
	}


	


}