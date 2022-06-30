using UnityEngine;
public class Pillar : Shape //INHERITANCE
{
    protected override void Start() //POLYMORPHISM
    {
        ren = GetComponent<Renderer>();
    }
    protected override void FixedUpdate() //POLYMORPHISM
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        CompareTags(ref collision); //ABSTRACTION
    }
}
