
using Patterns.Component;
using UnityEngine;

public class CircleTester : MonoBehaviour
    {
        [SerializeField]
        public int number_of_circles = 2;
        public int number_of_rec = 2;
        
        private CircleComponent[] circles;
        //private RectangleComponent[] rectangles;
        
        private void Awake()
        {
            circles = new CircleComponent[number_of_circles];
            //rectangles = new RectangleComponent[number_of_rec];
            
            for (int i = 0; i < number_of_circles; i++)
            {
                circles[i] = new CircleComponent();
                circles[i].Create();
            }
            for (int i = 0; i < number_of_rec; i++)
            {
                //rectangles[i] = new RectangleComponent();
                //rectangles[i].Create();
            }
        }

        private void FixedUpdate()
        {
            // Mueve los círculos
            for (int i = 0; i < circles.Length; i++)
            {
                circles[i].Move(Time.fixedDeltaTime);
            }

            // Mueve los rectangulos
           // for (int i = 0; i < rectangles.Length; i++)
            //{
            //    rectangles[i].Move(Time.fixedDeltaTime);
            //}

            // Resuelve las colisiones
            for (int i = 0; i < circles.Length - 1; i++)
            {
                for (int j = i + 1; j < circles.Length; j++)
                {
                    circles[i].ProcessCollissions(circles[j]);
                }
            }

            // Resuelve las colisiones rectangulos
            //for (int i = 0; i < rectangles.Length - 1; i++)
            //{
            //    for (int j = i + 1; j < rectangles.Length; j++)
            //    {
             //       rectangles[i].ProcessCollissions(rectangles[j]);
             //   }
           // }

            // Renderiza
            for (int i = 0; i < circles.Length; i++)
            {
                circles[i].Render();
            }
            // Renderiza rectangulos
           // for (int i = 0; i < rectangles.Length; i++)
           // {
            //    rectangles[i].Render();
            //}
        }
    }