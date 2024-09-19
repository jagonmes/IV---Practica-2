namespace Patterns.Component.Interfaces
{
    public interface ICollisionComponent
    {
        public void ProcessCollisions(AGObject obj, AGObject other);
    }
}