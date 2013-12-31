using UnityEngine;
using System.Collections;

public class btdPulgaForzuda : MonoBehaviour
{
    public float movementSpeed;
    public float jumphight;
    private bool jumping;

    // Use this for initialization
    void Start()
    {
        movementSpeed = 5.0f;
        jumphight = 300.0f;
        jumping = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        float forward = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        transform.Translate(Vector3.left * forward);
        if (Input.GetKeyDown("space") && !jumping)
        {
            rigidbody.AddForce(Vector3.up * jumphight);
            jumping = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            jumping = false;
        }
    }
}
