namespace Patterns.FlyWeight.Interfaces
{
    public interface ISoldier
    {
        public bool IsAlive();
        public string GetArmy();
        public void Create();
        public void Move(float deltaTime);
        public void ProcessCollissions(ISoldier other);
        public void Render();
    }
}