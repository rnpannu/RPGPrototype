using System.Collections.Generic;
using Microsoft.Xna.Framework;
using RPGPrototype.Log;

namespace RPGPrototype.Objects.States;

public class StateMachine
{
	private State _currentState;
	private Dictionary<string, State> _states = new();

	public StateMachine(List<State> states)
	{
		foreach (State state in states)
		{
			_states[state.Name] = state;
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

	public void Transition(string newStateName)
	{
		if(_states.TryGetValue(newStateName, out State state))
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
			throw new KeyNotFoundException("No state with name: " + newStateName);
		}
	}
}