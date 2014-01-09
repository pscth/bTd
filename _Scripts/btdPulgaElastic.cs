using UnityEngine;
using System.Collections;

public class btdPulgaElastic : MonoBehaviour
{
    public float movementSpeed;
    public float jumpHight;

    private float gorgeDimension;
    private int gorgeDirection;

    private int state;
    private int selected;

    // Use this for initialization
    void Start()
    {
        movementSpeed = 5.0f;
        jumpHight = 25.0f;
        rigidbody.mass = 0.1f;
        selected = btdConstants.PULGA_UNSELECTED;
    }

    // Update is called once per frame
    void Update()
    {
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
                if (gorgeDirection == btdConstants.GORGE_LEFT)
                {
                    GetComponent<BoxCollider>().size += new Vector3(gorgeDimension * 0.1f, 0.0f, 0.0f);
                    GetComponent<BoxCollider>().center += new Vector3(gorgeDimension * 0.05f, 0.0f, 0.0f);
                }
                else if (gorgeDirection == btdConstants.GORGE_RIGHT)
                {
                    GetComponent<BoxCollider>().size += new Vector3(gorgeDimension * 0.1f, 0.0f, 0.0f);
                    GetComponent<BoxCollider>().center -= new Vector3(gorgeDimension * 0.05f, 0.0f, 0.0f);
                }
                rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                if (GetComponent<BoxCollider>().size.x >= gorgeDimension + 0.5f)
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
                if (gorgeDirection == btdConstants.GORGE_LEFT)
                {
                    GetComponent<BoxCollider>().size -= new Vector3(gorgeDimension * 0.1f, 0.0f, 0.0f);
                    GetComponent<BoxCollider>().center -= new Vector3(gorgeDimension * 0.05f, 0.0f, 0.0f);
                    transform.Translate(new Vector3(gorgeDimension * 0.1f, 0.0f, 0.0f));
                    if (GetComponent<BoxCollider>().center == new Vector3(0.0f, 0.0f, 0.0f))
                    {
                        state = btdConstants.PULGA_ELASTIC_STRETCH;
                        Debug.Log("Normalidad");
                    }
                }
                else if (gorgeDirection == btdConstants.GORGE_RIGHT)
                {
                    GetComponent<BoxCollider>().size -= new Vector3(gorgeDimension * 0.1f, 0.0f, 0.0f);
                    GetComponent<BoxCollider>().center += new Vector3(gorgeDimension * 0.05f, 0.0f, 0.0f);
                    transform.Translate(new Vector3(-gorgeDimension * 0.1f, 0.0f, 0.0f));
                    if (GetComponent<BoxCollider>().center == new Vector3(0.0f, 0.0f, 0.0f))
                    {
                        state = btdConstants.PULGA_ELASTIC_STRETCH;
                        Debug.Log("Normalidad");
                    }
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ground" && state == btdConstants.PULGA_JUMP)
        {
            state = btdConstants.PULGA_WAIT;
        }
        else if (other.tag == "gorgeTrigger" && state != btdConstants.PULGA_JUMP)
        {
            gorgeDimension = other.GetComponent<BoxCollider>().size.x;
            state = btdConstants.PULGA_ELASTIC_STRETCH;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "gorgeTrigger")
        {
            state = btdConstants.PULGA_WAIT;
            renderer.material.color = Color.blue;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "gorgeLeft" && state == btdConstants.PULGA_WAIT)
        {
            gorgeDirection = btdConstants.GORGE_LEFT;
        }
        else if (other.tag == "gorgeRight" && state == btdConstants.PULGA_WAIT)
        {
            gorgeDirection = btdConstants.GORGE_RIGHT;
        }
        if (other.tag == "gorgeTrigger")
        {
            renderer.material.color = Color.yellow;
        }
    }

    public void setSelected(int _selected)
    {
        selected = _selected;
    }
}
