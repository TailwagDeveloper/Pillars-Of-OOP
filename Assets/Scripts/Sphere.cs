using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Sphere : Shape //The Sphere (child) class derives from the Shape (parent) class
{
    /* The Sphere implementation of the Start method overrides the Shape 
    implementation of the Start() method. Additional functionality is 
    introduced before the functionality from the Shape class*/
    private float timer = 5f;
    [SerializeField]
    private GameObject moon;
    private Vector3 moonOffset;
    protected override void Start() 
    {
        base.Start(); //base.Start(); calls the parent class functionality of the Start() method
        ToggleGravity(); //disable gravity
        MoveShape(Vector3.up, 3.27f); //add an upward force ~1/3 of gravity
        moonOffset = SetupMoon(ref moon);
        Spawner.instance.SpawnObject(moon, gameObject, moonOffset, default, Color.white);
        
    }
    private Vector3 SetupMoon(ref GameObject moon)
    {
        moonOffset = Vector3.one;
        moonOffset = transform.position + moonOffset;
        return moonOffset;
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if(rb.velocity.x < 0.5f)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                RandomObjectToOrbit();
            }
            if(timer <= -5)
            {
                RandomAxis();
                timer = 5f;
            }
        }
    }
    public override void OnMouseDown()
    {
        ToggleGravity();
        base.OnMouseDown();
    }
}
