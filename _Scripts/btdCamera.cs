using UnityEngine;
using System.Collections;

public class btdCamera : MonoBehaviour 
{
    public float speed;
    public float zoomDistance;

    private GameObject target;

	// Use this for initialization
	void Start () 
    {
        speed = 2.0f;
        zoomDistance = 0.0f;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (target != null)
        {
            transform.Translate(new Vector3(target.transform.position.x - transform.position.x, 0.0f, 0.0f) * speed * Time.deltaTime);
            //Tambien se deberia subir la camara y tener en cuenta el nivel del suelo en el que esta el target
            if (zoomDistance != transform.position.z && zoomDistance != 0.0f)
            {
                transform.Translate(new Vector3(0.0f, 0.0f, zoomDistance - transform.position.z) * speed * Time.deltaTime);
            }
        }
	}

    public void newTarget(GameObject _target) {target = _target;}

    public void zoom(float distance)
    {
        zoomDistance = distance;
    }
}
