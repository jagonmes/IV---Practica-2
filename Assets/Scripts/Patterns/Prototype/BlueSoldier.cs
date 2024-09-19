using Patterns.Prototype.Interfaces;

namespace Patterns.Prototype
{
    public class BlueSoldier : AbstractSoldier
    {
        public BlueSoldier() : base("BlueArmy")
        {
        }

        public override IPrototypeSoldier Clone()
        {
            {
                IPrototypeSoldier newInstance = new BlueSoldier();
                newInstance.Create();
                return newInstance;
            }
        }
    }
}