using Game._Scripts.Interfaces;
using UnityEngine;

namespace Game._Scripts.States
{
    public class MenuState : MonoBehaviour, IGameState
    {
        public MenuState(GameStates stateType)
        {
            StateType = stateType;
        }

        public GameStates StateType { get; }
        public void Enter()
        {
            //TODO: Show Menu Ui
        }

        public void Exit()
        {
            //TODO: Hide MenuUI
        }
    }
}
