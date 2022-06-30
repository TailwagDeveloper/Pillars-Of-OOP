using UnityEngine;
using TMPro;

public class Shape : MonoBehaviour
{
    protected GameObject currentObject;
    public TextMeshProUGUI shapeText;
    public GameObject[] objects = new GameObject[4];
    //the mat variable will be assignable in the inspector for the Shape class and any child classes that derive from Shape.
    [SerializeField] private static Material mat;
    public static Material Mat //ENCAPSULATION
    {
        get => mat;
        set => mat = value;
    }
    private float maxVelocity = 2f;
    protected float MaxVelocity //ENCAPSULATION
    {
        get => maxVelocity;
    }
    Vector3 axis = Vector3.up;
    //the rigidbody and renderer components will be accessed in the Start method
    protected Rigidbody rb;
    protected Renderer ren;
    protected Color currentColor;
    protected string message;
    // A virtual property with a set accessor can be overriden in child classes
    protected string Message //ENCAPSULATION
    {
        set => message = value;
    }
    /* The protected keyword is an access modifier which limits access to the method to only the parent class 
    and classes which derive from or implement the parent class. Methods of the parent class may include the 
    virtual keyword after the accessor, allowing child classes to override the method for implementing 
    additional functionality or removing the functionality of the parent class. */
    protected virtual void Start()
    {
        rb = GetRigidBody();
        ren = GetRenderer();
        currentColor = ren.material.color;
        currentObject = RandomObjectToOrbit();
    }
    protected Rigidbody GetRigidBody() //ABSTRACTION
    {
        return GetComponent<Rigidbody>();
    }
    protected Renderer GetRenderer() //ABSTRACTION
    {
        return GetComponent<Renderer>();
    }
    protected virtual void ChangeColor(Color color1, Color color2) //ABSTRACTION
    {
        ren.material.color = currentColor == color1 ? 
        Color.Lerp(currentColor, color2, Mathf.PingPong(Time.time, 1)) : 
        Color.Lerp(currentColor, color1, Mathf.PingPong(Time.time, 1));
        currentColor = ren.material.color;
    }
    /* The following PillarToOrbit method encapsulates int index, 
    preventing the index variable from being modified outside of 
    the scope of the ObjectToOrbit method */
    protected GameObject RandomObjectToOrbit() //ABSTRACTION
    {
        int index = Random.Range(0, objects.Length);
        return objects[index];
    }
    protected Vector3 RandomAxis() //ABSTRACTION
    {
        int index = Random.Range(0, 6);
        axis = index switch
        {
            0 => Vector3.right,
            1 => Vector3.left,
            2 => Vector3.up,
            3 => Vector3.down,
            4 => Vector3.forward,
            5 => Vector3.back,
            _ => default
        };
        return axis;
    }
    protected virtual void FixedUpdate()
    {
        if(currentObject != null)
        {
            transform.RotateAround(currentObject.transform.position, axis, 30f * Time.deltaTime);
        }
        if(transform.position.z <= -7)
        {
            rb.velocity = -rb.velocity.normalized;
        }
        if(rb?.velocity.x >= MaxVelocity)
        {
            rb.velocity = new Vector3(MaxVelocity, rb.velocity.y, rb.velocity.z);
        }
        if(rb?.velocity.x <= -MaxVelocity)
        {
            rb.velocity = new Vector3(-MaxVelocity, rb.velocity.y, rb.velocity.z);
        }
        if(rb?.velocity.y >= MaxVelocity)
        {
            rb.velocity = new Vector3(rb.velocity.x, MaxVelocity, rb.velocity.z);
        }
        if(rb?.velocity.y <= -MaxVelocity)
        {
            rb.velocity = new Vector3(rb.velocity.x, -MaxVelocity, rb.velocity.z);
        }
        if(rb?.velocity.z >= MaxVelocity)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, MaxVelocity);
        }
        if(rb?.velocity.z <= -MaxVelocity)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -MaxVelocity);
        }
    }
    /* The two following SpawnObject methods exemplify method overloading, a feature of polymorphism */
    
    public void ToggleGravity() //ABSTRACTION
    {
        rb.useGravity = !rb.useGravity;
    }
    protected void MoveShape(Vector3 direction, float speed) //ABSTRACTION
    {
        rb.AddForce(direction * speed);
    }
    public void CompareTags(ref Collision collision, GameObject obj = null)
    {
        ren = obj != null ? obj.GetComponent<Renderer>() : ren;
        if(collision.gameObject.CompareTag("Cube"))
        {
            ren.material.color = new Color(Random.Range(0f, 1.0f), Random.Range(0f, 1.0f), 0, Random.Range(0.3f, 1.0f));
        }
        else if(collision.gameObject.CompareTag("Sphere"))
        {
            ren.material.color = new Color(0, Random.Range(0f, 1.0f), Random.Range(0f, 1.0f), Random.Range(0.3f, 1.0f));
        }
        else
        {
            ren.material.color = new Color(Random.Range(0f, 1.0f), Random.Range(0f, 1.0f), Random.Range(0f, 1.0f), Random.Range(0.3f, 1.0f));
        }
    }
    public virtual void OnMouseDown()
    {
        message = "I'm the " + name + ".";
        shapeText.text = message + "";
        if(!IsInvoking("ClearText"))
        {
            Invoke("ClearText", 3f);
        }
    }
    private void ClearText() //ABSTRACTION
    {
        shapeText.text = "Clicking a shape also toggles gravity!";
    }
}
