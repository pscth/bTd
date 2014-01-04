using UnityEngine;
using System.Collections;


public class btdPulgaTrapeze : MonoBehaviour
{
    public float movementSpeed;
    public float jumpHight;
    public float jumpHightTrapeze;

    private Vector3 swingPosition;
    
    private int state;

    // Use this for initialization
    void Start()
    {
        movementSpeed = 5.0f;
        jumpHight = 300.0f;
        jumpHightTrapeze = 550.0f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        float forward = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        transform.Translate(Vector3.right * forward);
        if (Input.GetKeyDown(KeyCode.Space) && state != btdConstants.PULGA_JUMP)
        {
            rigidbody.AddForce(Vector3.up * jumpHight);
            state = btdConstants.PULGA_JUMP;
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl) && state == btdConstants.PULGA_TRAPEZE_JUMP)
        {
            rigidbody.AddForce(Vector3.up * jumpHightTrapeze);
        }
        else if (state == btdConstants.PULGA_TRAPEZE_SWING)
        {
            rigidbody.AddForce((transform.position - swingPosition).normalized * btdConstants.TRAPEZE_FORCE * rigidbody.mass, ForceMode.Impulse);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ground")
        {
            state = btdConstants.PULGA_WAIT;
        }
        else if (other.tag == "trapezeJump")
        {
            Debug.Log("In " + other.tag);
            state = btdConstants.PULGA_TRAPEZE_JUMP;
        }
        else if (other.tag == "trapezeSwing")
        {
            Debug.Log("In " + other.tag);
            state = btdConstants.PULGA_TRAPEZE_SWING;
            swingPosition = other.GetComponent<BoxCollider>().transform.position;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "trapezeJump")
        {
            Debug.Log("Out " + other.tag);
            state = btdConstants.PULGA_WAIT;
        }
    }
}
