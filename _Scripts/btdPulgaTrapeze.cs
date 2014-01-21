using UnityEngine;
using System.Collections;


public class btdPulgaTrapeze : MonoBehaviour
{
    public float movementSpeed;
    public float movementSpeedTrapeze;
    public float jumpHight;

    private Vector3 swingPosition;
    private GameObject swingObject;

    public int state;
    private int selected;

    // Use this for initialization
    void Start()
    {
        movementSpeedTrapeze = btdConstants.TRAPEZE_MOVE_SPEED;
        movementSpeed = btdConstants.MOVE_SPEED;
        jumpHight = btdConstants.JUMP_HIGHT;
        rigidbody.mass = btdConstants.PULGA_MASS;
        selected = btdConstants.PULGA_UNSELECTED;
        state = btdConstants.PULGA_WAIT;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (selected == btdConstants.PULGA_SELECTED)
        {
            //MOVEMENT
            float forward = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
            if (forward > 0)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                renderer.material.color = Color.red;
            }
            else if (forward < 0) 
            { 
                transform.localRotation = Quaternion.Euler(0, 180, 0);
                forward *= -1;
                renderer.material.color = Color.magenta;
            }
            if (state != btdConstants.PULGA_TRAPEZE_SWING) transform.Translate(Vector3.right * forward);
            //SM
            switch (state)
            {
                case btdConstants.PULGA_WAIT:
                    if (Input.GetButtonDown("Jump"))
                    {
                        state = btdConstants.PULGA_JUMP; 
                        rigidbody.AddForce(Vector3.up * jumpHight * rigidbody.mass, ForceMode.Impulse);
                    }
                    break;
                case btdConstants.PULGA_JUMP:
                    if (Input.GetButtonDown("Jump"))
                    {
                        state = btdConstants.PULGA_TRAPEZE_EXTRA_JUMP;
                        rigidbody.AddForce(Vector3.up * jumpHight * rigidbody.mass, ForceMode.Impulse);
                    }
                    break;
                case btdConstants.PULGA_TRAPEZE_EXTRA_JUMP:
                    swingPosition = new Vector3(0.0f, 0.0f, 0.0f);
                    break;
                case btdConstants.PULGA_TRAPEZE_SWING:
                    if (swingPosition.y >= transform.position.y)
                    {
                        swingPosition = swingObject.transform.position;
                        rigidbody.AddForce((transform.position - swingPosition) * btdConstants.TRAPEZE_FORCE * rigidbody.mass * (swingPosition.y - transform.position.y), ForceMode.Force);
                    }
                    if (Input.GetButtonDown("Jump"))
                    {
                        state = btdConstants.PULGA_TRAPEZE_EXTRA_JUMP;
                        movementSpeed = btdConstants.MOVE_SPEED;
                        rigidbody.AddForce(Vector3.up * jumpHight * 1.0f * rigidbody.mass, ForceMode.Impulse);
                    }
                    transform.RotateAround(swingPosition, Vector3.forward, Input.GetAxis("Horizontal") * movementSpeedTrapeze * Time.deltaTime);
                    break;
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "ground":
                state = btdConstants.PULGA_WAIT;
                break;
            case "trapezeSwing":
                Debug.Log("Swing");
                state = btdConstants.PULGA_TRAPEZE_SWING;
                swingPosition = other.transform.position;
                swingObject = other.gameObject;
                break;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "trapezeSwing") state = btdConstants.PULGA_TRAPEZE_EXTRA_JUMP;
    }

    void OnTriggerStay(Collider other) {}

    public void setSelected(int _selected) {selected = _selected;}
}
