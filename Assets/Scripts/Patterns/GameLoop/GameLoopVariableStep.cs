using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace Patterns.GameLoop
{
    public class GameLoopVariableStep : IGameLoop
    {
        private IGameLoop.TimeUpdatedData _timeUpdatedData = new IGameLoop.TimeUpdatedData();
        public event EventHandler<IGameLoop.TimeUpdatedData> TimeUpdated;

        private bool _running = true;
        private float _gameLoopTime = 0;

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
            float deltaTime = Time.deltaTime;
            _gameLoopTime += deltaTime;
            
            _timeUpdatedData.GameLoopTime = _gameLoopTime;
            _timeUpdatedData.DeltaTime = deltaTime;
        }
	
        private void render()
        {
            TimeUpdated?.Invoke(this, _timeUpdatedData);
        }
    }
}