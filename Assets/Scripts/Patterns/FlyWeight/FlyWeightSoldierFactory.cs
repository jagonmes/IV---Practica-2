using System.Collections.Generic;

namespace Patterns.FlyWeight
{
    public class FlyWeightSoldierFactory
    {
        private static Dictionary<string, FlyWeightSoldier> _flyWeightSoldiers = new Dictionary<string, FlyWeightSoldier>();
        
        public static FlyWeightSoldier GetSoldier(string army, int MBOfData)
        {
            if (!_flyWeightSoldiers.ContainsKey(army))
            {
                _flyWeightSoldiers[army] = new FlyWeightSoldier(army, MBOfData);
            }
            
            return _flyWeightSoldiers[army];
        }
    }
}