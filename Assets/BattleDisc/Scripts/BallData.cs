namespace Arkanoid
{
    public enum BallType
    {
        Enemy,
        Player,
    }

    public enum BallState
    {
        Active,
        Aim,
        Destroy,
    }
    
    public struct BallData
    {
        public BallType BallType { get; set; }
        public BallState BallState { get; set; }

        public BallData(BallType ballType, BallState ballState)
        {
            BallType = ballType;
            BallState = ballState;
        }
    }
}