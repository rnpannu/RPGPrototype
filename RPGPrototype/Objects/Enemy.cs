using System.Xml;
using Microsoft.Xna.Framework;
using MonoGameLibrary.Graphics;

namespace RPGPrototype.Objects;

public class Enemy : Entity
{
	private Vector2 _homePoint;

	private int _baseHealth;
	//private UniqueId _uniqueId;

	private Vector2 lastMovementDirection;
	private int _searchDistance;
	private int _wanderDistance;
	private int attackDistance;
	private Vector2 attackDirection;
	private int _attackBuffer;
	private Vector2 _walkSpeed;
	private Vector2 _pursueSpeed;
	// last pursue tile index
	private Vector2 _acceleration;
	
	public Enemy(Vector2 position, Vector2 movementSpeed) : base(position, movementSpeed)
	{
		
	}

	public override void Initialize()
	{
		base.Initialize();
	}

	public virtual void LoadContent(TextureAtlas atlas)
	{
		
	}

	public void Update(GameTime gameTime, Vector2 target)
	{
		base.Update(gameTime);
		Pathfind(gameTime, target);
	}

	public void Pathfind(GameTime gameTime, Vector2 target)
	{
		Vector2 delta = target - Position;
		delta.Normalize();
	}

	public override void Draw(GameTime gameTime)
	{
		base.Draw(gameTime);
	}
}