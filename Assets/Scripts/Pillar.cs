using UnityEngine;

public class Pillar : Shape
{
    protected override void Start()
    {
        ren = GetComponent<Renderer>();
    }
    protected override void FixedUpdate()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        CompareTags(ref collision);
    }
}
