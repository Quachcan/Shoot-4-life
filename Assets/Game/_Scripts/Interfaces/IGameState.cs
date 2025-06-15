namespace Game._Scripts.Interfaces
{
    public interface IGameState
    {
        GameStates StateType  { get; }
        public void Enter();
        public void Exit();
    }
}
