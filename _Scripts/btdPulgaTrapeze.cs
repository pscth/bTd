using UnityEngine;
using System.Collections;


public class btdPulgaTrapeze : MonoBehaviour
{
    public float movementSpeed;
    public float movementSpeedTrapeze;
    public float jumpHight;
    public float trapezeForce;

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
        trapezeForce = btdConstants.TRAPEZE_FORCE;
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
                    swingObject = null;
                    break;
                case btdConstants.PULGA_TRAPEZE_SWING:
                    if (swingObject.transform.position.y >= transform.position.y)
                    {
                        rigidbody.AddForce((transform.position - swingObject.transform.position) * trapezeForce * rigidbody.mass * (swingObject.transform.position.y - transform.position.y), ForceMode.Force);
                    }
                    if (Input.GetButtonDown("Jump"))
                    {
                        state = btdConstants.PULGA_TRAPEZE_EXTRA_JUMP;
                        movementSpeed = btdConstants.MOVE_SPEED;
                        rigidbody.AddForce(Vector3.up * jumpHight * 1.0f * rigidbody.mass, ForceMode.Impulse);
                    }
                    transform.RotateAround(swingObject.transform.position, Vector3.forward, Input.GetAxis("Horizontal") * movementSpeedTrapeze * Time.deltaTime);
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
                state = btdConstants.PULGA_TRAPEZE_SWING;
                swingObject = other.gameObject;
                trapezeForce = btdConstants.TRAPEZE_FORCE;
                trapezeForce *= other.GetComponent<CapsuleCollider>().height; //Las dimensiones de collider definen la fuerza de atraccion
                Debug.Log("Swing. Force = " + trapezeForce);
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
