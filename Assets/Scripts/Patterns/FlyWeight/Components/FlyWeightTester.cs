using System;
using Patterns.FlyWeight.Interfaces;
using TMPro;
using UnityEngine;

namespace Patterns.FlyWeight.Components
{
    public class FlyWeightTester : MonoBehaviour
    {
        [SerializeField]
        public int number_of_gobjects = 2;

        [SerializeField] 
        public int megabytes_of_data = 1;
        
        private ISoldier[] soldiers;

        public EventHandler<int> WhiteSoldiersUpdated;
        public EventHandler<int> RedSoldiersUpdated;

        public TextMeshProUGUI redSoldiersUI;
        public TextMeshProUGUI whiteSoldiersUI;
        
        private void Awake()
        {
            soldiers = new ISoldier[number_of_gobjects];
            
            for (int i = 0; i < number_of_gobjects; i++)
            {
                string army = (i < number_of_gobjects / 2) ? "RedArmy" : "WhiteArmy";
                soldiers[i] = new Soldier(army, megabytes_of_data);
                soldiers[i].Create();
            }

            redSoldiersUI.text = $"Red Soldiers: {number_of_gobjects / 2}";
            whiteSoldiersUI.text = $"White Soldiers: {number_of_gobjects / 2}";
        }

        private void FixedUpdate()
        {
            int redSoldiers = 0;
            int whiteSoldiers = 0;
            
            // Renderiza
            for (int i = 0; i < soldiers.Length; i++)
            {
                if (soldiers[i].IsAlive())
                {
                    if (soldiers[i].GetArmy() == "RedArmy")
                    {
                        redSoldiers++;
                    }
                    else
                    {
                        whiteSoldiers++;
                    }
                    soldiers[i].Render();    
                }
            }
            
            redSoldiersUI.text = $"Red Soldiers: {redSoldiers}";
            whiteSoldiersUI.text = $"White Soldiers: {whiteSoldiers}";
            
            // Mueve los círculos
            for (int i = 0; i < soldiers.Length; i++)
            {
                if (soldiers[i].IsAlive())
                {
                    soldiers[i].Move(Time.fixedDeltaTime);   
                }
            }
            
            // Resuelve las colisiones
            for (int i = 0; i < soldiers.Length - 1; i++)
            {
                if (soldiers[i].IsAlive())
                {
                    for (int j = i + 1; j < soldiers.Length; j++)
                    {
                        if (soldiers[j].IsAlive())
                        {
                            soldiers[i].ProcessCollissions(soldiers[j]);    
                        }
                    }
                }
            }
        }
    }
}