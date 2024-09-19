using Patterns.Prototype.Interfaces;

namespace Patterns.Prototype
{
    public class WhiteSoldier : AbstractSoldier
    {
        public WhiteSoldier() : base("WhiteArmy")
        {
        }

        public override IPrototypeSoldier Clone()
        {
            {
                IPrototypeSoldier newInstance = new WhiteSoldier();
                newInstance.Create();
                return newInstance;
            }
        }
    }
}