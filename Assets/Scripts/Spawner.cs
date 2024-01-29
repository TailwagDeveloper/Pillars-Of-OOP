using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;
    private void Awake()
    {
        instance = this;
    }
    //POLYMORPHISM; The SpawnObject methods are an example of method overloading
    /// <summary>
    /// Extends the functionality of the Instantiate method by allowing the user to specify the color of the object to be spawned.
    /// </summary>
    /// <param name="objectToSpawn"></param>
    /// <param name="objectPosition"></param>
    /// <param name="objectRotation"></param>
    /// <param name="objectColor"></param>
    /// <returns>The object that was spawned.</returns>
    public GameObject SpawnObject(GameObject objectToSpawn, Vector3 objectPosition = default, Quaternion objectRotation = default, Color objectColor = default)
    {
        objectToSpawn = Instantiate(objectToSpawn, objectPosition, objectRotation);
        objectToSpawn.GetComponent<Renderer>().material.color = objectColor;
        return objectToSpawn;
    }
    /// <summary>
    /// Extends the functionality of the Instantiate method by allowing the user to specify the color of the object to be spawned and the parent object.
    /// </summary>
    /// <param name="objectToSpawn"></param>
    /// <param name="parentObject"></param>
    /// <param name="objectPosition"></param>
    /// <param name="objectRotation"></param>
    /// <param name="objectColor"></param>
    /// <returns>The object that was spawned.</returns>
    public GameObject SpawnObject(GameObject objectToSpawn, GameObject parentObject, Vector3 objectPosition = default, Quaternion objectRotation = default, Color objectColor = default)
    {
        objectToSpawn = Instantiate(objectToSpawn, objectPosition, objectRotation);
        Renderer ren = objectToSpawn.GetComponent<Renderer>();
        ren.material.color = objectColor;
        objectToSpawn.transform.parent = parentObject.transform;
        return objectToSpawn;
    }
}
