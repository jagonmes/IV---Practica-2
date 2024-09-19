using System;
using Patterns.GameLoop;
using UnityEngine;

namespace Tools
{
    public class GameLoopTester : MonoBehaviour
    {
        // Start is called before the first frame update
        private float _time = 0;
        private float _gameLoopTime = 0;
        private float _gameLoopDeltaTime = 0;
    
        IGameLoop gameLoop = new GameLoopFixedStep();

        private Vector3 direction = Vector3.right;
        
        void Start()
        {
            Debug.Log("Game object started!!!");
            gameLoop.TimeUpdated += GameLoopOnTimeUpdated;
        }
    
        private void GameLoopOnTimeUpdated(object sender, IGameLoop.TimeUpdatedData timeUpdatedData)
        {
            _gameLoopTime = timeUpdatedData.GameLoopTime;
            _gameLoopDeltaTime = timeUpdatedData.DeltaTime;
            
            if (transform.position.x >= 6f)
            {
                direction = Vector3.left;
            } else if (transform.position.x <= -6f)
            {
                direction = Vector3.right;
            }
            
            transform.Translate(direction * _gameLoopDeltaTime * 10);
        }

        private bool coroutineStarted = false;
    
        void Update()
        {
            if (!coroutineStarted)
            {
                StartCoroutine(gameLoop.DoGameLoop());
                coroutineStarted = true;
            }
            _time += Time.deltaTime;
        }

        private void OnGUI()
        {
            GUIStyle labelStyle = GUI.skin.label;
            labelStyle.fontSize = 28;
            TimeSpan timespan = TimeSpan.FromSeconds(_time);
            
            GUI.Label(
                new Rect(10f, 10f, 600f, 50f), 
                $"Real time: {timespan:g}",
                labelStyle
                );
            
            timespan = TimeSpan.FromSeconds(_gameLoopTime);
            GUI.Label(
                new Rect(10f, 60f, 400f, 50f), 
                $"GameLoop time: {timespan:g}",
                labelStyle);

            GUI.Label(
                new Rect(420f, 60f, 500f, 50f), 
                $"GameLoop delta time: {_gameLoopDeltaTime:g}",
                labelStyle);

            GUI.Label(
                new Rect(10f, 120f, 500f, 50f), 
                $"Difference: {_gameLoopTime - _time:f2}",
                labelStyle);
        }
    }
}
