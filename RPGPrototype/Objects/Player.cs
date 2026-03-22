using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGameLibrary.Graphics;

namespace RPGPrototype.Objects;

public class Player : Entity
{
	private Vector2 _position;
	private Sprite _sprite;
	private List<Animation> _animations;

	public Player(AnimatedSprite sprite, Vector2 position)
	{
		Sprite = sprite;
		Position = position;
	}
	public Vector2 Position
	{
		get => _position;
		private set => _position = value;
	}

	public Sprite Sprite
	{
		get => _sprite;
		private set => _sprite = value;
	}

	public void Initialize()
	{
		
	}

	public void LoadContent()
	{

	}

	public void Update(GameTime gameTime, Vector2 position)
	{
		Position = position;
	}

	public void Draw(GameTime gameTime)
	{
		
	}


	


}