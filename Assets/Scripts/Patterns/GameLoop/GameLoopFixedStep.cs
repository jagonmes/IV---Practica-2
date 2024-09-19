using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace Patterns.GameLoop
{
    public class GameLoopFixedStep : IGameLoop
    {
        private IGameLoop.TimeUpdatedData _timeUpdatedData = new IGameLoop.TimeUpdatedData();
        public event EventHandler<IGameLoop.TimeUpdatedData> TimeUpdated;

        private bool _running = true;
        private float _gameLoopTime = 0;

        private float _lag = 0;
        private float step = 0.010f;

        public IEnumerator DoGameLoop() {
            Debug.Log("I'm going to start the game loop!!!");

            // ESTE ES EL GAME LOOP!!!!!
            while(_running)
            {
                processInput();
                updateGame();
                render();

                yield return null;
            }
            
            Debug.Log("End of game loop!!!!");
            EditorApplication.isPlaying = false;
        }
	
        private void processInput() {
            if(Input.GetKey(KeyCode.Escape)) {
                _running = false;
            }
        }
	
        private void updateGame()
        {
            while (_lag >= step)
            {
                float deltaTime = step;
                _gameLoopTime += deltaTime;
            
                _timeUpdatedData.GameLoopTime = _gameLoopTime;
                _timeUpdatedData.DeltaTime = deltaTime;
                _lag = _lag - step;
            }

            _lag = _lag + Time.deltaTime;
        }
	
        private void render()
        {
            TimeUpdated?.Invoke(this, _timeUpdatedData);
        }
    }
}