using UnityEngine;

public class Boundary : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Shape shape = collision.gameObject.GetComponent(typeof(Shape)) as Shape;
        if(shape != null)
        {
            shape.CompareTags(ref collision, gameObject);
        }
    }
}
