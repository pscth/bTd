using UnityEngine;
using System.Collections;


public abstract class btdPulgaController : MonoBehaviour
{
	public float movementSpeed;
	public float jumpHight;

	public  int state;
	public int selected;
	
	// Use this for initialization
	public void PulgaCommonStart()
	{
		movementSpeed = btdConstants.MOVE_SPEED;
		jumpHight = btdConstants.JUMP_HIGHT;
		rigidbody.mass = btdConstants.PULGA_MASS;
		selected = btdConstants.PULGA_UNSELECTED;
		state = btdConstants.PULGA_WAIT;
	}
	
	// Update is called once per frame
	public bool PulgaCommonUpdate ()
	{
			//float forward = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
			//transform.Translate(Vector3.right * forward);

			// Movimiento lateral ----
			float forward = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
			if(forward>0) transform.localRotation = Quaternion.Euler(0,0,0);
			if(forward<0) {transform.localRotation = Quaternion.Euler(0,180,0);  forward=-forward;}
			transform.Translate(Vector3.right * forward);

			if (Input.GetButton("Jump") && state != btdConstants.PULGA_JUMP)	
			{
				rigidbody.AddForce(Vector3.up * jumpHight);
				state = btdConstants.PULGA_JUMP;
				return true;	
			}
			return false;
	}
	public bool PulgaCommonOnTriggerEnter(Collider other)
	{
		if (other.tag == "ground")
		{
			state = btdConstants.PULGA_WAIT;
			return true;
		}
		return false;
	}
	public bool PulgaCommonOnTriggerExit(Collider other)
	{
		return false;
		// --- AUN NADA TU!
	}
	public void setSelected(int _selected)
	{
				selected = _selected;	
	}
}