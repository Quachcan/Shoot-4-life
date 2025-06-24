using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance { get; private set; }
    private BaseState currentState;
    private Dictionary<GameStates, BaseState> states;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        states = new Dictionary<GameStates, BaseState>();
        foreach (var state in GetComponentsInChildren<BaseState>())
        {
            states[state.StateType] = state;
            state.gameObject.SetActive(false);
        }
    }

    public void ChangeState(GameStates next)
    {
        if(currentState != null)
            currentState.gameObject.SetActive(false);

        if (states.TryGetValue(next, out var nextState))
        {
            currentState = nextState;
            currentState.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"State '{next}' does not exist in StateManager");
        }
    }
}
