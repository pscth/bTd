using UnityEngine;
using System.Collections;

public class btdPulgaElastic : MonoBehaviour
{
    public float movementSpeed;
    public float jumpHight;

    private float gorgeDimension;
    private int gorgeDirection;
    public bool canClimb;

    public int state;
    private int selected;

    // Use this for initialization
    void Start()
    {
        canClimb = false;
        movementSpeed = btdConstants.MOVE_SPEED;
        jumpHight = btdConstants.JUMP_HIGHT;
        rigidbody.mass = btdConstants.PULGA_MASS;
        selected = btdConstants.PULGA_UNSELECTED;
        state = btdConstants.PULGA_WAIT;
    }

    // Update is called once per frame
    void Update()
    {
        if (selected == btdConstants.PULGA_SELECTED)
        {
            // MOVEMENT
            if (state != btdConstants.PULGA_ELASTIC_STRETCH_ON)
            {
                float forward = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
                //
                if (state == btdConstants.PULGA_ELASTIC_CLIMB_RDY || state == btdConstants.PULGA_ELASTIC_CLIMBING)
                {
                    if (canClimb)
                    {
                        rigidbody.useGravity = false;
                        if (forward > 0)
                        {
                            transform.localRotation = Quaternion.Euler(0, 0, 0);
                            renderer.material.color = Color.blue;
                        }
                        else if (forward < 0)
                        {
                            transform.localRotation = Quaternion.Euler(0, 0, 180);
                            forward *= -1;
                            renderer.material.color = Color.cyan;
                        }
                        forward *= btdConstants.ELASTIC_CLIMB_SPEED / movementSpeed;
                        transform.Translate(Vector3.up * forward);
                    }
                }
                else
                {
                    rigidbody.useGravity = true;
                    //
                    if (forward > 0)
                    {
                        transform.localRotation = Quaternion.Euler(0, 0, 0);
                        renderer.material.color = Color.blue;
                    }
                    else if (forward < 0)
                    {
                        transform.localRotation = Quaternion.Euler(0, 180, 0);
                        forward *= -1;
                        renderer.material.color = Color.cyan;
                    }
                    transform.Translate(Vector3.right * forward);
                }
            }
            //SM
            switch (state)
            {
                case btdConstants.PULGA_WAIT:
                    if (Input.GetButtonDown("Jump"))
                    {
                        rigidbody.AddForce(Vector3.up * jumpHight * rigidbody.mass, ForceMode.Impulse);
                        state = btdConstants.PULGA_JUMP;
                    }
                    break;
                case btdConstants.PULGA_JUMP:
                    break;
                case btdConstants.PULGA_ELASTIC_STRETCH:
                    if (Input.GetAxis("Horizontal") == 0.0f)
                    {
                        renderer.material.color = Color.yellow;
                        if (Input.GetButtonDown("Fire1"))
                        {
                            Debug.Log("Estirar");
                            state = btdConstants.PULGA_ELASTIC_TO_STRETCH_ON;
                        }
                        else if (Input.GetButtonDown("Jump"))
                        {
                            rigidbody.AddForce(Vector3.up * jumpHight * rigidbody.mass, ForceMode.Impulse);
                            state = btdConstants.PULGA_ELASTIC_STRETCH_BUT_JUMPING;
                        }
                    }
                    break;
                case btdConstants.PULGA_ELASTIC_STRETCH_BUT_JUMPING:
                    break;
                case btdConstants.PULGA_ELASTIC_TO_STRETCH_ON:
                    rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                    transform.localScale += new Vector3(gorgeDimension * 0.1f, 0.0f, 0.0f);
                    if (gorgeDirection == btdConstants.LEFT)
                    {
                        transform.localRotation = Quaternion.Euler(0, 0, 0);
                    }
                    else if (gorgeDirection == btdConstants.RIGHT)
                    {
                        transform.localRotation = Quaternion.Euler(0, 180, 0);
                    }
                    transform.Translate(new Vector3(gorgeDimension / 2 * 0.1f, 0.0f, 0.0f));
                    if (transform.localScale.x >= gorgeDimension + 1.5f)
                    {
                        state = btdConstants.PULGA_ELASTIC_STRETCH_ON;
                        Debug.Log("Estirada");
                    }
                    break;
                case btdConstants.PULGA_ELASTIC_STRETCH_ON:
                    if (Input.GetButtonDown("Fire1"))
                    {
                        Debug.Log("Volver a la normalidad");
                        state = btdConstants.PULGA_ELASTIC_TO_WAIT;
                    }
                    break;
                case btdConstants.PULGA_ELASTIC_TO_WAIT:
                    rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
                    transform.localScale -= new Vector3(gorgeDimension * 0.1f, 0.0f, 0.0f);
                    transform.Translate(new Vector3(gorgeDimension / 2 * 0.1f, 0.0f, 0.0f));
                    if (transform.localScale.x <= 1.0f)
                    {
                        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                        state = btdConstants.PULGA_ELASTIC_STRETCH;
                        Debug.Log("Normalidad");
                    }
                    break;
                case btdConstants.PULGA_ELASTIC_CLIMB:
                    renderer.material.color = Color.yellow;
                    if (Input.GetButtonDown("Fire1"))
                    {
                        state = btdConstants.PULGA_ELASTIC_CLIMB_RDY;
                    }
                    break;
                case btdConstants.PULGA_ELASTIC_CLIMB_RDY:
                    rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                    renderer.material.color = Color.yellow;
                    if (Input.GetButtonDown("Fire1"))
                    {
                        state = btdConstants.PULGA_ELASTIC_CLIMB;
                        rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
                    }
                    else if (Input.GetButtonDown("Jump"))
                    {
                        rigidbody.AddForce(Vector3.up * jumpHight * rigidbody.mass, ForceMode.Impulse);
                        state = btdConstants.PULGA_JUMP;
                        rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
                    }
                    break;
                case btdConstants.PULGA_ELASTIC_CLIMBING:
                    break;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "ground":
                if (state == btdConstants.PULGA_JUMP)
                {
                    state = btdConstants.PULGA_WAIT;
                }
                else if (state == btdConstants.PULGA_ELASTIC_STRETCH_BUT_JUMPING)
                {
                    state = btdConstants.PULGA_ELASTIC_STRETCH;
                }
                break;
            case "gorgeTrigger":
                if (state == btdConstants.PULGA_WAIT)
                {
                    state = btdConstants.PULGA_ELASTIC_STRETCH;
                }
                else if (state == btdConstants.PULGA_JUMP)
                {
                    state = btdConstants.PULGA_ELASTIC_STRETCH_BUT_JUMPING;
                }
                gorgeDimension = other.GetComponent<BoxCollider>().size.x;
                break;
                //
            case "climb":
                if (state != btdConstants.PULGA_ELASTIC_CLIMBING)
                {
                    state = btdConstants.PULGA_ELASTIC_CLIMB;
                }
                else
                {
                    state = btdConstants.PULGA_ELASTIC_CLIMB_RDY;
                }
                break;
            case "climbZone":
                canClimb = true;
                break;
        }
    }

    void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case "gorgeTrigger":
                if (state != btdConstants.PULGA_JUMP && state != btdConstants.PULGA_ELASTIC_TO_STRETCH_ON && state != btdConstants.PULGA_ELASTIC_TO_WAIT)
                {
                    state = btdConstants.PULGA_WAIT;
                }
                break;
            //
            case "climb":
                if (state != btdConstants.PULGA_ELASTIC_CLIMB_RDY)
                {
                    state = btdConstants.PULGA_WAIT;
                }
                else
                {
                    state = btdConstants.PULGA_ELASTIC_CLIMBING;
                }
                break;
            case "climbZone":
                canClimb = false;
                break;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (state == btdConstants.PULGA_WAIT || state == btdConstants.PULGA_ELASTIC_STRETCH)
        {
            switch (other.tag)
            {
                case "gorgeLeft":
                    gorgeDirection = btdConstants.LEFT;
                    break;
                case "gorgeRight":
                    gorgeDirection = btdConstants.RIGHT;
                    break;
            }
        }
    }

    public void setSelected(int _selected) {selected = _selected;}
}
