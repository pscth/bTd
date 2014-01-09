using UnityEngine;
using System.Collections;


public class btdPulgaTrapeze : MonoBehaviour
{
    public float movementSpeed;
    public float jumpHight;
    public float jumpHightTrapeze;

    private Vector3 swingPosition;

    private int state;
    private int selected;

    // Use this for initialization
    void Start()
    {
        movementSpeed = 5.0f;
        jumpHight = 25.0f;
        jumpHightTrapeze = 50.0f;
        rigidbody.mass = 0.1f;
        selected = btdConstants.PULGA_UNSELECTED;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (selected == btdConstants.PULGA_SELECTED)
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
                if (swingPosition.y >= transform.position.y)
                {
                    rigidbody.AddForce((transform.position - swingPosition).normalized * btdConstants.TRAPEZE_FORCE * rigidbody.mass, ForceMode.Impulse);
                }
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ground")
        {
            state = btdConstants.PULGA_WAIT;
        }
        else if (other.tag == "trapezeSwing")
        {
            state = btdConstants.PULGA_TRAPEZE_SWING;
            swingPosition = other.GetComponent<BoxCollider>().transform.position;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "trapezeJump")
        {
            state = btdConstants.PULGA_WAIT;
        }
        if (other.tag == "trapezeJump" || other.tag == "trapezeSwing")
        {
            renderer.material.color = Color.red;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "trapezeJump")
        {
            state = btdConstants.PULGA_TRAPEZE_JUMP;
        }
        if (other.tag == "trapezeJump" || other.tag == "trapezeSwing")
        {
            renderer.material.color = Color.yellow;
        }
    }

    public void setSelected(int _selected)
    {
        selected = _selected;
    }
}
