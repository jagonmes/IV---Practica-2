
using UnityEngine;

public class RotateCoin : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        transform.Rotate(new Vector3(1.0f,0.0f,0.0f), speed * Time.deltaTime);
    }
}
