using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoGameLibrary;
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
	// Todo: Create a better atlas parser that can iterate frames without specifying the coords of each one
	private TextureAtlas _objectAtlas;

	public Player Player
	{
		get => _player;
		private set => _player = value;
	}

	public LevelObjectManager()
	{
		Initialize();
	}

	public void Initialize()
	{
		Player = new Player();
	}

	public void LoadContent(ContentManager content)
	{
		_objectAtlas = TextureAtlas.FromFile(content, "sprites/objectAtlas-definition.xml");
		Player.LoadContent(_objectAtlas);
	}

	public void Update(GameTime gameTime, Vector2 cameraPosition)
	{
		Player.Position = cameraPosition;
	}

	public void Draw(GameTime gameTime)
	{
		Player.Draw(gameTime);
	}
}