using Patterns.Prototype.Interfaces;

namespace Patterns.Prototype
{
    public class RedSoldier : AbstractSoldier
    {
        public RedSoldier() : base("RedArmy")
        {
        }

        public override IPrototypeSoldier Clone()
        {
            IPrototypeSoldier newInstance = new RedSoldier();
            newInstance.Create();
            return newInstance;
        }
    }
}