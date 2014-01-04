using UnityEngine;
using System.Collections;

public class btdPulgaElastic : MonoBehaviour
{
    public float movementSpeed;
    public float jumpHight;

    private Vector3 stretchPosition;
    private float stretchOffset;

    private int state;

    // Use this for initialization
    void Start()
    {
        movementSpeed = 5.0f;
        jumpHight = 300.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float forward = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        transform.Translate(Vector3.right * forward);
        if (Input.GetKeyDown(KeyCode.Space) && state != btdConstants.PULGA_JUMP)
        {
            rigidbody.AddForce(Vector3.up * jumpHight);
            state = btdConstants.PULGA_JUMP;
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl) && state == btdConstants.PULGA_ELASTIC_STRETCH)
        {
            
        }
        else if (state == btdConstants.PULGA_TRAPEZE_SWING)
        {

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ground")
        {
            state = btdConstants.PULGA_WAIT;
        }
    }

    void OnTriggerExit(Collider other)
    {
    }
}
