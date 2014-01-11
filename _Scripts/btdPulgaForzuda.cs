using UnityEngine;
using System.Collections;

public class btdPulgaForzuda : MonoBehaviour
{
    public float movementSpeed;
    public float jumphight;

    private int pullDirection;

    public int state;
    private int selected;

    // Use this for initialization
    void Start()
    {
        movementSpeed = btdConstants.MOVE_SPEED;
        jumphight = btdConstants.JUMP_HIGHT;
        rigidbody.mass = btdConstants.PULGA_MASS;
        selected = btdConstants.PULGA_UNSELECTED;
        state = btdConstants.PULGA_WAIT;
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
                rigidbody.AddForce(Vector3.up * jumphight);
                state = btdConstants.PULGA_JUMP;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ground")
        {
            if (state == btdConstants.PULGA_JUMP)
            {
                state = btdConstants.PULGA_WAIT;
            }
            else if (state == btdConstants.PULGA_FORZUDE_PULLUP_BUT_JUMPING)
            {
                state = btdConstants.PULGA_FORZUDE_PULLUP;
                renderer.material.color = Color.yellow;
            }
            Debug.Log("Ground");
        }
        else if (other.tag == "pullUpLeft")
        {
            pullDirection = btdConstants.LEFT;
            state = btdConstants.PULGA_FORZUDE_PULLUP;
            renderer.material.color = Color.yellow;
            Debug.Log("PullLeft");
        }
        else if (other.tag == "pullUpRight")
        {
            pullDirection = btdConstants.RIGHT;
            state = btdConstants.PULGA_FORZUDE_PULLUP;
            renderer.material.color = Color.yellow;
            Debug.Log("PullRight");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "pullUpRight" || other.tag == "pullUpLeft")
        {
            renderer.material.color = Color.green;
        }
    }

    public void setSelected(int _selected)
    {
        selected = _selected;
    }
}
