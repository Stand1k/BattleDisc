namespace BattleDisc
{
    public interface IGameWonHandle : IGlobalSubscriber
    {
        public void GameWon();
    }
}