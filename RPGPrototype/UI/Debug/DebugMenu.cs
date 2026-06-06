

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;

namespace RPGPrototype.UI.Debug;

public class DebugMenu
{
	private bool _visible;
	
	private List<DebugPanel> _panels;
	private int _activePanel = 1;
	
	public WatchPanel Watch { get; private set; }
	public DebugMenu()
	{
		_panels = new();
		Watch = new WatchPanel();
		_panels.Add(Watch);
	}

	public void LoadContent(SpriteFont font)
	{
		foreach (var panel in _panels)
		{
			panel.LoadContent(font);
		}
	}

	public void Update(GameTime gameTime)
	{
		if (GameController.ToggleDebug())
		{
			_visible = !_visible;
		}

		if (_visible)
		{
			if (GameController.DebugWatch())
			{
				_activePanel = 1;
			}
			else if (GameController.DebugFlags())
			{
				_activePanel = 2;
			}
		}
	}

	public void Draw(GameTime gameTime)
	{
		if (_visible)
		{
			_panels[_activePanel - 1].Draw(gameTime);
		}
	}
}