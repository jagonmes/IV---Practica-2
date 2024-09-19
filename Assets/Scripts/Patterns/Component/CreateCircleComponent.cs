using Patterns.Component.Interfaces;
using UnityEngine;

namespace Patterns.Component
{
    public class CreateCircleComponent: ICreateComponent
    {
        public void Create(AGObject circulo)
        {
            circulo.cam = GameObject.FindObjectOfType<Camera>();
            
            circulo.gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            circulo.gameObject.GetComponent<Renderer>().material.color = Random.ColorHSV();

            circulo.size = (Mathf.Rad2Deg * Screen.height) / (20f * GameObject.FindObjectOfType<Camera>().fieldOfView);
            circulo.halfSize = circulo.size / 2;
            circulo.direction = new Vector3((Random.value * 2f) - 1f, (Random.value * 2f) - 1f, 0f);
            circulo.position = new Vector3(Random.value * Screen.width, Random.value * Screen.height, 20f);
        }
    }
}