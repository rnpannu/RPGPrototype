using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGameLibrary;

namespace RPGPrototype.UI.Debug
{
    public class FlagsPanel : DebugPanel
    {
        // Dictionary mapping flag names to actions
        private readonly OrderedDictionary<string, Action> _actions = new();
        private int _cursor;

        public FlagsPanel()
        {
            base._bgColor = new Color(40, 40, 40);
        }

        /// <summary>
        /// Registers a new action with this panel.
        /// </summary>
        /// <param name="name">The name of the action (flag).</param>
        /// <param name="action">The action to be invoked.</param>
        public void RegisterFlag(string name, Action action)
        {
            _actions.TryAdd(name, action);
        }

        /// <summary>
        /// Invokes the action by name.
        /// </summary>
        /// <param name="name">Name of the action to invoke.</param>
        public void InvokeAction(string name)
        {
            if (_actions.ContainsKey(name))
            {
                _actions[name]?.Invoke();
            }
        }

        /// <summary>
        /// Toggles and executes the action at the current cursor position.
        /// </summary>
        public void ToggleCurrentAction()
        {
            if (_actions.Count > 0 && _cursor >= 0 && _cursor < _actions.Count)
            {
                _actions.GetAt(_cursor).Value?.Invoke();
            }
        }

        // ── Input ─────────────────────────────────────────────────────────────
        public override void Update(GameTime gameTime)
        {
            int numActions = _actions.Keys.Count;
            if (numActions == 0) return;

            // Handle cursor movement
            if (GameController.IncrementUp()) 
                _cursor = (_cursor - 1 + numActions) % numActions;
            if (GameController.IncrementDown()) 
                _cursor = (_cursor + 1) % numActions;

            // Handle action invocation
            if (GameController.Action())
            {
                ToggleCurrentAction();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            int x = _bgPanel.X + _padding;
            int y = _bgPanel.Y + _padding;
            int width = _bgPanel.Width - _padding;
            const int lineHeight = 26;

            for (int i = 0; i < _actions.Count && y <= _bgPanel.Height; i++)
            {
                bool selected = (i == _cursor);
                string key = _actions.GetAt(i).Key;

                int lineY = y + (i * lineHeight);

                // Draw selection highlight
                if (selected)
                {
                    Core.SpriteBatch.Draw(_pixelTexture, 
                        new Rectangle(x - 2, lineY - 2, width - 10, lineHeight), Color.LightGray);
                }

                // Draw the action name
                Core.SpriteBatch.DrawString(_font, key.ToString(),
                    new Vector2(x, lineY), selected ? Color.DarkGoldenrod : Color.Goldenrod);
            }
        }
    }
}