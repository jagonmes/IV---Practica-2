using System;
using Patterns.Command.Components;
using UnityEngine;

namespace Components
{
    public class Board : MonoBehaviour
    {
        private Camera _camera;
        private BoardCube _currentCube;
        private BoardCube[,] _cubes;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Start()
        {
            _cubes = new BoardCube[10, 10];
            foreach (BoardCube cube in transform.GetComponentsInChildren<BoardCube>())
            {
                cube.Row = (int)Math.Truncate(cube.transform.position.z);
                cube.Col = (int)Math.Truncate(cube.transform.position.x);
                _cubes[cube.Row, cube.Col] = cube;
            }
        }

        private void Update()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                BoardCube cube = hit.collider.GetComponent<BoardCube>();
                if (cube != null && cube != _currentCube && Input.GetMouseButtonDown(0))
                {
                    _currentCube?.Deactivate();
                    cube.Activate();
                    _currentCube = cube;
                    Debug.Log($"Current cube: [{cube.Row}, {cube.Col}]");
                }
            }
        }
    }
}