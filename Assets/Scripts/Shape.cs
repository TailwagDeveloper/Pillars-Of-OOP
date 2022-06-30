using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class Shape : MonoBehaviour
{
    protected GameObject currentObject;
    public GameObject[] objects = new GameObject[4];
    //the mat variable will be assignable in the inspector for the Shape class and any child classes that derive from Shape.
    [SerializeField] private static Material mat;
    public static Material Mat
    {
        get => mat;
        set => mat = value;
    }
    private float maxVelocity = 2f;
    protected float MaxVelocity
    {
        get => maxVelocity;
    }
    Vector3 axis = Vector3.up;
    //the rigidbody and renderer components will be accessed in the Start method
    protected Rigidbody rb;
    protected Renderer ren;
    protected Color currentColor;
    protected TextMeshProUGUI message;
    // A virtual property with a set accessor can be overriden in child classes
    protected virtual TextMeshProUGUI Message
    {
        set => message.text = "This is the parent class message. I come in many forms.";
    }
    /* The protected keyword is an access modifier which limits access to the method to only the parent class 
    and classes which derive from or implement the parent class. Methods of the parent class may include the 
    virtual keyword after the accessor, allowing child classes to override the method for implementing 
    additional functionality or removing the functionality of the parent class. */
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        ren = GetComponent<Renderer>();
        currentColor = ren.material.color;
        currentObject = RandomObjectToOrbit();
    }
    protected virtual void ChangeColor(Color color1, Color color2)
    {
        ren.material.color = currentColor == color1 ? 
        Color.Lerp(currentColor, color2, Mathf.PingPong(Time.time, 1)) : 
        Color.Lerp(currentColor, color1, Mathf.PingPong(Time.time, 1));
        currentColor = ren.material.color;
    }
    /* The following PilarToOrbit method encapsulates int index, 
    preventing the index variable from being modified outside of 
    the scope of the ObjectToOrbit method */
    protected GameObject RandomObjectToOrbit()
    {
        int index = Random.Range(0, objects.Length);
        return objects[index];
    }
    protected Vector3 RandomAxis()
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
        if(transform.position.z <= -8)
        {
            rb.velocity = -rb.velocity;
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
    
    public void ToggleGravity()
    {
        rb.useGravity = !rb.useGravity;
    }
    protected void MoveShape(Vector3 direction, float speed)
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
            ren.material.color = new Color(Random.Range(0f, 1.0f), 0, Random.Range(0f, 1.0f), Random.Range(0.3f, 1.0f));
        }
    }
}
