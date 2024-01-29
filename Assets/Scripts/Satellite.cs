using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Satellite : Shape //INHERITANCE
{
    private const float xTimer = 5;
    private const float zTimer = 5;
    protected override void Start() //POLYMORPHISM
    {
        ren = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
        currentObject = gameObject.transform.parent.gameObject;
        shapeText = currentObject.GetComponent<Sphere>().shapeText;
        CreateMaterial(); //ABSTRACTION
    }
    protected override void FixedUpdate() //POLYMORPHISM
    {
        float distanceFromParent = Vector3.Distance(transform.position, currentObject.transform.position);
        if (distanceFromParent > 5f && distanceFromParent > 1f && !IsInvoking("MoveToParent"))
        {
            StartCoroutine(nameof(MoveToParent));
        }
        base.FixedUpdate(); //ABSTRACTION
    }
    private IEnumerator MoveToParent() //ABSTRACTION
    {
        while (Vector3.Distance(transform.position, currentObject.transform.position) > 1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentObject.transform.position, Time.deltaTime);
            yield return null;
        }
    }
    static void CreateMaterial() //ABSTRACTION
    {
        if (!Mat)
        {
            // Unity has a built-in shader that is useful for drawing
            // simple colored things.
            Shader shader = Shader.Find("Hidden/Internal-Colored");
            Mat = new Material(shader)
            {
                hideFlags = HideFlags.HideAndDontSave
            };
            // Turn on alpha blending
            Mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            Mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            // Turn backface culling off
            Mat.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
            // Turn off depth writes
            Mat.SetInt("_ZWrite", 0);
            
        }
    }
    public override void OnMouseDown() //POLYMORPHISM
    {
        ToggleGravity(); //ABSTRACTION
        base.OnMouseDown(); //ABSTRACTION
    }
}
