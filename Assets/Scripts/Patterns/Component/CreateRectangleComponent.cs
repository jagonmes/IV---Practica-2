using Patterns.Component.Interfaces;
using UnityEngine;

namespace Patterns.Component
{
    public class CreateRectangleComponent:ICreateComponent
    {
        public void Create(AGObject rectangulo)
        {
            rectangulo.cam = GameObject.FindObjectOfType<Camera>();
            
            rectangulo.gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            rectangulo.gameObject.GetComponent<Renderer>().material.color = Random.ColorHSV();

            rectangulo.size = (Mathf.Rad2Deg * Screen.height) / (20f * GameObject.FindObjectOfType<Camera>().fieldOfView);
            rectangulo.halfSize = rectangulo.size / 2;
            rectangulo.direction = new Vector3((Random.value * 2f) - 1f, (Random.value * 2f) - 1f, 0f);
            rectangulo.position = new Vector3(Random.value * Screen.width, Random.value * Screen.height, 20f);
        }
    }
}