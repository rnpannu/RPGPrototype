using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;

namespace RPGPrototype.UI.Debug;

public abstract class DebugPanel
{
	protected Rectangle _bgPanel;
	protected SpriteFont _font;
	protected Texture2D _pixelTexture;
	protected Color _bgColor;

	protected int _padding = 8;

	public DebugPanel()
	{
		_bgPanel = new Rectangle(10, 10, (int)(Core.Width * 1.3), Core.Height * 3);
		_bgColor = new Color(40, 0, 0);
		
	}
	
	public virtual void LoadContent(SpriteFont font)
	{

		_font = font;
	}

	private void EnsurePixelTexture()
	{
		if (_pixelTexture == null && Core.GraphicsDevice != null)
		{
			_pixelTexture = new Texture2D(Core.GraphicsDevice, 1, 1);
			_pixelTexture.SetData(new Color[] { Color.White });
		}
	}
	
	public virtual void Update(GameTime gameTime)
	{
		
	}

	public virtual void Draw(GameTime gameTime)
	{
		EnsurePixelTexture();
		if (_pixelTexture != null)
		{
			Core.SpriteBatch.Draw(_pixelTexture, _bgPanel, _bgColor);
		}

	}
	/// <summary>Draws a key/value row with the value right-aligned.</summary>
	// ? Might want to pass new SpriteBatch if doing a separate begin/end for the UI
	protected virtual Vector2 DrawKVField(int x, int y, int w, int h, string key, string val)
	{
		//if (alt) Fill(sb, px, new Rectangle(x, y, w, h), C_RowAlt);
		Core.SpriteBatch.DrawString(_font, key, new Vector2(x + 6, y + 2), Color.DarkGoldenrod);
		var distance = _font.MeasureString(val);
		Core.SpriteBatch.DrawString(_font, val, new Vector2(x + w - distance.X - 6, y + 2), Color.Yellow);
		return distance;
	}
}