using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Xml;
using Microsoft.Xna.Framework;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using RPGPrototype.Objects.States;

namespace RPGPrototype.Objects;

public class Enemy : Entity
{
	protected Vector2 _homePoint;
	protected Rectangle _patrolArea;
	
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

	
	public Enemy(Vector2 position) : base(position)
	{
		_homePoint = Position;
		Position = _homePoint;
	}
	public virtual StateMachine StateMachine
	{
		get => field;
		protected set => field = value;
	}

	/*public override Vector2 Position { get; set; }*/
	public int DetectionDistance { get; protected set; }
	
	public bool HasLOS { get; protected set;  }
	
	public Vector2 CurrentLOSTarget { get; protected set; }

	public List<Vector2> PatrolPoints
	{
		get => field;
		set => field = value;
	} = new();
	
	public Vector2 CurrentPatrolPoint { get; set; }

	public override void Initialize()
	{
		base.Initialize();
		GeneratePatrolPoints();
	}

	public virtual void GeneratePatrolPoints()
	{
		while (PatrolPoints.Count < 3)
		{
			int valid = 0;
			
			Vector2 patrolPoint = new Vector2(RandomNumberGenerator.GetInt32(_patrolArea.X, _patrolArea.X + _patrolArea.Width),
				RandomNumberGenerator.GetInt32(_patrolArea.Y, _patrolArea.Y + _patrolArea.Height));
			
			if (PatrolPoints.Count == 0)
			{
				PatrolPoints.Add(patrolPoint);
			}
			else
			{
				foreach (Vector2 point in PatrolPoints)
				{
					if ((patrolPoint - point).LengthSquared() > 5000f) // magic number
					{
						valid++;
					}
				}
			}

			if (valid == PatrolPoints.Count)
			{
				PatrolPoints.Add(patrolPoint);
			}
		}

}
	public virtual void LoadContent(TextureAtlas atlas)
	{
		
	}

	public virtual void Update(GameTime gameTime)
	{
		base.Update(gameTime);
		if (StateMachine != null)
		{
			StateMachine.Update(gameTime);
		}
		
	}

	public virtual void CheckLOS(Vector2 target, int[,] collisionGrid, int tileSize)
	{
		HasLOS = false;
		Vector2 delta = target - Position;
		Vector2 dir = Vector2.Normalize(delta);
		int losStepCounter = 0;

		if (!delta.IsZero() && delta.LengthSquared() < Math.Pow(DetectionDistance, 2))
		{
			Vector2 losCheckStart = Position;
			Vector2 losCheckCurrentStep = losCheckStart;
			Vector2 losStepIncrement = new Vector2(5, 5); // magic number

			while ((losCheckCurrentStep - losCheckStart).LengthSquared() <
			       delta.LengthSquared()) // while check hasn't passed total distance
			{
				losCheckCurrentStep = losCheckStart + (dir * losStepIncrement * losStepCounter);
				int xTile = (int)losCheckCurrentStep.X / tileSize;
				int yTile = (int)losCheckCurrentStep.Y / tileSize;

				if (collisionGrid[yTile, xTile] == 1)
				{
					HasLOS = false;
					break;
				}

				losStepCounter++;
			}

			if ((losCheckCurrentStep - losCheckStart).LengthSquared() > delta.LengthSquared())
			{
				HasLOS = true;
				CurrentLOSTarget = target;
			}
		}
	}

	public override void Draw(GameTime gameTime)
	{
		base.Draw(gameTime);
	}


}
