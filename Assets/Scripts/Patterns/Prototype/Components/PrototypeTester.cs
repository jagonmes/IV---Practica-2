using System.Collections.Generic;
using Patterns.Prototype.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Patterns.Prototype.Components
{
    public class PrototypeTester : MonoBehaviour
    {
        [SerializeField]
        private int _numberOfGobjects = 2;

        private IPrototypeSoldier[] _soldiers;
    
        public TextMeshProUGUI redSoldiersUI;
        public TextMeshProUGUI whiteSoldiersUI;
        public TextMeshProUGUI blueSoldiersUI;

        private Dictionary<string, IPrototypeSoldier> prototypes = new Dictionary<string, IPrototypeSoldier>()
        {
            { "RedArmy", new RedSoldier() },
            { "WhiteArmy", new WhiteSoldier() },
            { "BlueArmy", new BlueSoldier() }
        };

        private void Awake()
        {
            _soldiers = new IPrototypeSoldier[_numberOfGobjects];
            
            for (int i = 0; i < _numberOfGobjects; i++)
            {
                string army = "RedArmy";
                if (i > (_numberOfGobjects / 3))
                {
                    if (i < (2 * _numberOfGobjects / 3))
                        army = "WhiteArmy";
                    else
                        army = "BlueArmy";
                } 
                _soldiers[i] = prototypes[army].Clone();
            }

            redSoldiersUI.text = $"Red Soldiers: {_numberOfGobjects / 3}";
            whiteSoldiersUI.text = $"White Soldiers: {_numberOfGobjects / 3}";
            blueSoldiersUI.text = $"Blue Soldiers: {_numberOfGobjects / 3}";
        }

        private void AddSoldier(IPrototypeSoldier soldier)
        {
            for (int i = 0; i < _soldiers.Length; i++)
            {
                if (!_soldiers[i].IsAlive())
                {
                    _soldiers[i] = soldier.Clone();
                    break;
                }
            }
        }

        private void FixedUpdate()
        {
            int redSoldiers = 0;
            int whiteSoldiers = 0;
            int blueSoldiers = 0;
            
            // Renderiza
            for (int i = 0; i < _soldiers.Length; i++)
            {
                if (_soldiers[i].IsAlive())
                {
                    if (_soldiers[i].GetArmy() == "RedArmy")
                    {
                        redSoldiers++;
                    }
                    else if(_soldiers[i].GetArmy() == "WhiteArmy")
                    {
                        whiteSoldiers++;
                    }
                    else
                    {
                        blueSoldiers++;
                    }

                    _soldiers[i].Render();    
                }
            }
            
            redSoldiersUI.text = $"Red Soldiers: {redSoldiers}";
            whiteSoldiersUI.text = $"White Soldiers: {whiteSoldiers}";
            blueSoldiersUI.text = $"Blue Soldiers: {blueSoldiers}";
            
            // Mueve los círculos
            for (int i = 0; i < _soldiers.Length; i++)
            {
                if (_soldiers[i].IsAlive())
                {
                    _soldiers[i].Move(Time.fixedDeltaTime);   
                }
            }
            
            // Resuelve las colisiones
            for (int i = 0; i < _soldiers.Length - 1; i++)
            {
                if (_soldiers[i].IsAlive())
                {
                    for (int j = i + 1; j < _soldiers.Length; j++)
                    {
                        if (_soldiers[j].IsAlive())
                        {
                            if (_soldiers[i].ProcessCollissions(_soldiers[j]))
                            {
                                AddSoldier(_soldiers[i]);
                            }
                        }
                    }
                }
            }
        }
    }
}