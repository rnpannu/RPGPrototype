using System;
using Microsoft.Xna.Framework;
using MonoGameLibrary.Graphics;
using RPGPrototype.Objects;

namespace RPGPrototype.Scenes;

/// <summary>
/// A class for managing game objects within levels. At initial concept
/// includes things like enemies and potentially the player.
/// </summary>
public class LevelObjectManager
{
	private Player _player;
	private TextureAtlas _objectAtlas;

	public LevelObjectManager()
	{
		Initialize();
	}

	public void Initialize()
	{
		
	}

	public void LoadContent()
	{
	}

	public void Update(GameTime gameTime, Vector2 playerPosition)
	{
	}

	public void Draw(GameTime gameTime)
	{
	}
}