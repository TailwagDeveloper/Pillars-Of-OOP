using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;
    private void Awake()
    {
        instance = this;
    }
    public void SpawnObject(GameObject objectToSpawn) //POLYMORPHISM; The SpawnObject methods are an example of method overloading
    {
        objectToSpawn = Instantiate(objectToSpawn, default, Quaternion.identity);
    }
    public GameObject SpawnObject(GameObject objectToSpawn, Vector3 objectPosition = default, Quaternion objectRotation = default, Color objectColor = default)
    {
        objectToSpawn = Instantiate(objectToSpawn, objectPosition, objectRotation);
        objectToSpawn.GetComponent<Renderer>().material.color = objectColor;
        return objectToSpawn;
    }
    public GameObject SpawnObject(GameObject objectToSpawn, GameObject parentObject, Vector3 objectPosition = default, Quaternion objectRotation = default, Color objectColor = default)
    {
        objectToSpawn = Instantiate(objectToSpawn, objectPosition, objectRotation);
        Renderer ren = objectToSpawn.GetComponent<Renderer>();
        ren.material.color = objectColor;
        objectToSpawn.transform.parent = parentObject.transform;
        return objectToSpawn;
    }
}
