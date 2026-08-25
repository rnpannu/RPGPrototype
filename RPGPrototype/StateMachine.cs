using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using RPGPrototype.Log;

namespace RPGPrototype;

public class StateMachine
{
	private State _currentState;
	private Dictionary<Type, State> _states = new();

	public StateMachine(List<State> states)
	{
		foreach (State state in states)
		{
			_states[state.GetType()] = state;
		}

		CurrentState = states[0];
		CurrentState.Enter();
	}

	public State CurrentState
	{
		get => _currentState;
		protected set => _currentState = value;
	}

	public void Update(GameTime gameTime)
	{
		CurrentState.Update(gameTime);
	}

	public void Transition(Type newState)
	{
		if(_states.TryGetValue(newState, out State state))
		{
			if (state == CurrentState)
			{
				Logger.Log("Redundant attempt to transition to state "
				           //+ _currentState.Name
					, LogLevel.Warning);
			}
			else
			{
				CurrentState.Exit();
				CurrentState = state;
				CurrentState.Enter();
			}
		}
		else
		{
			throw new KeyNotFoundException("No state with name: " + nameof(newState));
		}
	}
}