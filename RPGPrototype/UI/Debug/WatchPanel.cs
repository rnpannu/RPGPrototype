using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGameLibrary;

namespace RPGPrototype.UI.Debug;

public class WatchPanel : DebugPanel
{
	private Dictionary<string, Func<string>> _watches = new ();

	
	/// <summary>Register a live variable. The getter is called each Draw.</summary>
	public void Register(string name, Func<string> getter)
		=> _watches.Add(name, getter);

	public WatchPanel()
	{
	}

	public void Update(GameTime gameTime)
	{
		
	}
	
	public override void Draw(GameTime gameTime)
	{
		base.Draw(gameTime);
		Vector2 startPos = new Vector2((float) (_bgPanel.X + _padding), (float)(_bgPanel.Y + _padding));
		//int index = 0;
		Vector2 offset = Vector2.Zero;
		
		foreach (var pair in _watches)
		{
			string val;
			try   { val = pair.Value() ?? "<null>"; }
			catch (Exception ex) { val = $"<err: {ex.GetType().Name}>"; }
			
			offset = DrawKVField((int)startPos.X, (int) (startPos.Y + (offset.Y)),
				_bgPanel.Width - _padding * 2
				, _padding,
				pair.Key, val);
			offset.Y += _padding;
		}
		


	}

	public void RegisterWatch(string key, Func<string> getter)
	{
		_watches.Add(key, getter);
	}
	
	// public void RemoveWatch(string key)
}