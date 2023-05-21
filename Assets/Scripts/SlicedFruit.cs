using UnityEngine;

public class SlicedFruit : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(Vector2.up * (Time.deltaTime * Fruit.RotationForce/2));
    }
}
