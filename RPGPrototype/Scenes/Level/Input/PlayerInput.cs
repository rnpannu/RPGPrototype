using Microsoft.Xna.Framework;

namespace RPGPrototype.Scenes.Input;

public readonly struct PlayerInput(Vector2 movementDirection, bool AttackPressed)
{
	public readonly Vector2 MovementDirection = movementDirection;
	public readonly bool AttackPressed = AttackPressed;
}