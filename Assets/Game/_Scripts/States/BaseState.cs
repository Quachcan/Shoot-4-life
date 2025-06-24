using UnityEngine;
using UnityEngine.Events;

public enum GameStates
{
    Menu,
    PlayInit,
    Playing,
    Pause,
    GameOver,
    End
}

public abstract class BaseState : MonoBehaviour
{
    public GameStates StateType;

    [Header("STATE UI PANELS")]
    public GameObject[] showOnEnter;
    public GameObject[] hideOnEnter; 
    public GameObject[] showOnExit;
    public GameObject[] hideOnExit;

    [Header("TASKS")]
    public UnityEvent onEnter;
    public UnityEvent onExit;


    private void OnEnable()
    {
        OnEnter();
    }

    private void OnDisable()
    {
        OnExit();
    }

    protected void OnEnter()
    {
        foreach (var go in showOnEnter) go.SetActive(true);
        foreach (var go in  hideOnEnter) go.SetActive(false);
        onEnter?.Invoke();
    }

    protected void OnExit()
    {
        foreach (var go in showOnExit) go.SetActive(true);
        foreach (var go in hideOnExit) go.SetActive(false);
        onExit?.Invoke();
    }

}


