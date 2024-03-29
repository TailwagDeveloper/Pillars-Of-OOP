using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : Shape //INHERITANCE; Cube derives from its parent class Shape
{
    private Camera cam;
    Color color1 = Color.red;
    Color color2 = Color.black;
     /* The Cube implementation of the Start method overrides the Shape 
    implementation of the Start() method. Additional functionality is 
    introduced along with the functionality from the Shape class*/
    protected override void Start() //POLYMORPHISM
    {
        base.Start(); //ABSTRACTION; base.Start(); calls the parent class functionality of the Start() method
        MoveShape(Vector3.down, 3.27f); //ABSTRACTION; add a downward force ~1/3 of gravity
        InvokeRepeating(nameof(EveryThreeSeconds), 3f, 3f); //ABSTRACTION
    }
    private void EveryThreeSeconds()
    {
        ChangeColor(color1, color2); //ABSTRACTION
        RandomObjectToOrbit(); //ABSTRACTION
        RandomAxis(); //ABSTRACTION
    }
    
    public override void OnMouseDown() //POLYMORPHISM
    {
        ToggleGravity(); //ABSTRACTION
        base.OnMouseDown(); //ABSTRACTION
    }
}
