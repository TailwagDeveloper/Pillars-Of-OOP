using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : Shape //Cube derives from its parent class Shape
{
    private Camera cam;
    Color color1 = Color.red;
    Color color2 = Color.black;
     /* The Cube implementation of the Start method overrides the Shape 
    implementation of the Start() method. Additional functionality is 
    introduced before the functionality from the Shape class*/
    protected override void Start()
    {
        base.Start(); //base.Start(); calls the parent class functionality of the Start() method
        MoveShape(Vector3.down, 3.27f); //add a downward force ~1/3 of gravity
        InvokeRepeating("EveryThreeSeconds", 3f, 3f);
    }
    private void EveryThreeSeconds()
    {
        ChangeColor(color1, color2);
        RandomObjectToOrbit();
        RandomAxis();
    }
    
    public override void OnMouseDown()
    {
        ToggleGravity();
        base.OnMouseDown();
    }
}
