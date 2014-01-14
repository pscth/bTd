using UnityEngine;
using System.Collections;

public class btdPulgaElastic : MonoBehaviour
{
    public float movementSpeed;
    public float jumpHight;

    private float gorgeDimension;
    private int gorgeDirection;
    //public GameObject pullUp;

    public int state;
    private int selected;

    // Use this for initialization
    void Start()
    {
        movementSpeed = btdConstants.MOVE_SPEED;
        jumpHight = btdConstants.JUMP_HIGHT;
        rigidbody.mass = btdConstants.PULGA_MASS;
        selected = btdConstants.PULGA_UNSELECTED;
        state = btdConstants.PULGA_WAIT;
    }

    // Update is called once per frame
    void Update()
    {
        //pullUp.transform.position = transform.position;
        if (selected == btdConstants.PULGA_SELECTED)
        {
            if (state != btdConstants.PULGA_ELASTIC_STRETCH_ON)
            {
                float forward = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
                transform.Translate(Vector3.right * forward);
            }
            if (Input.GetKeyDown(KeyCode.Space) && state != btdConstants.PULGA_JUMP)
            {
                rigidbody.AddForce(Vector3.up * jumpHight);
                state = btdConstants.PULGA_JUMP;
            }
            else if (Input.GetKeyDown(KeyCode.LeftControl) && state == btdConstants.PULGA_ELASTIC_STRETCH)
            {
                Debug.Log("Estirar");
                state = btdConstants.PULGA_ELASTIC_TO_STRETCH_ON;
            }
            else if (state == btdConstants.PULGA_ELASTIC_TO_STRETCH_ON)
            {
                rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                transform.localScale += new Vector3(gorgeDimension * 0.1f, 0.0f, 0.0f);
                if (gorgeDirection == btdConstants.LEFT)
                {
                    transform.Translate(new Vector3(gorgeDimension / 2 * 0.1f, 0.0f, 0.0f));
                }
                else if (gorgeDirection == btdConstants.RIGHT)
                {
                    transform.Translate(new Vector3(-gorgeDimension / 2 * 0.1f, 0.0f, 0.0f));
                }
                if (transform.localScale.x >= gorgeDimension + 0.5f)
                {
                    state = btdConstants.PULGA_ELASTIC_STRETCH_ON;
                    Debug.Log("Estirada");
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftControl) && state == btdConstants.PULGA_ELASTIC_STRETCH_ON)
            {
                Debug.Log("Volver a la normalidad");
                state = btdConstants.PULGA_ELASTIC_TO_WAIT;
            }
            else if (state == btdConstants.PULGA_ELASTIC_TO_WAIT)
            {
                rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
                transform.localScale -= new Vector3(gorgeDimension * 0.1f, 0.0f, 0.0f);
                if (gorgeDirection == btdConstants.LEFT)
                {
                    transform.Translate(new Vector3(gorgeDimension / 2 * 0.1f, 0.0f, 0.0f));
                }
                else if (gorgeDirection == btdConstants.RIGHT)
                {
                    transform.Translate(new Vector3(-gorgeDimension / 2 * 0.1f, 0.0f, 0.0f));
                }
                if (transform.localScale.x <= 1.0f)
                {
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    state = btdConstants.PULGA_ELASTIC_STRETCH;
                    Debug.Log("Normalidad");
                }
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
            else if (state == btdConstants.PULGA_ELASTIC_STRETCH_BUT_JUMPING)
            {
                state = btdConstants.PULGA_ELASTIC_STRETCH;
                renderer.material.color = Color.yellow;
            }
            Debug.Log("Ground");
        }
        else if (other.tag == "gorgeTrigger")
        {
            if (state == btdConstants.PULGA_JUMP)
            {
                state = btdConstants.PULGA_ELASTIC_STRETCH_BUT_JUMPING;
            }
            else
            {
                state = btdConstants.PULGA_ELASTIC_STRETCH;
                renderer.material.color = Color.yellow;
            }
            gorgeDimension = other.GetComponent<BoxCollider>().size.x;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "gorgeTrigger")
        {
            if (state != btdConstants.PULGA_JUMP)
            {
                state = btdConstants.PULGA_WAIT;
            }
            renderer.material.color = Color.blue;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "gorgeLeft" && (state == btdConstants.PULGA_WAIT || state == btdConstants.PULGA_ELASTIC_STRETCH))
        {
            gorgeDirection = btdConstants.LEFT;
        }
        else if (other.tag == "gorgeRight" && (state == btdConstants.PULGA_WAIT || state == btdConstants.PULGA_ELASTIC_STRETCH))
        {
            gorgeDirection = btdConstants.RIGHT;
        }
    }

    public void setSelected(int _selected)
    {
        selected = _selected;
    }
}
