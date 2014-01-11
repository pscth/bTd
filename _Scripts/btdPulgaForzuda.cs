using UnityEngine;
using System.Collections;

public class btdPulgaForzuda : MonoBehaviour
{
    public float movementSpeed;
    public float jumphight;

    private int pullDirection;

    public int state;
	public int pullstate;
	public int pullAngle;
	
    private int selected;
	
	private GameObject LHand; //brazos
	private GameObject RHand;
	public GameObject Grabber;
	public GameObject GrabPoint;
	public	GameObject Pullable;

    // Use this for initialization
    void Start()
    {
		// Common
        movementSpeed = btdConstants.MOVE_SPEED;
        jumphight = btdConstants.JUMP_HIGHT;
        rigidbody.mass = btdConstants.PULGA_MASS;
        selected = btdConstants.PULGA_UNSELECTED;
        state = btdConstants.PULGA_WAIT;
		// Forzude
		pullstate = btdConstants.PULGA_FORZUDE_NO_PULL;
		LHand =GameObject.Find ("LArm");
		RHand =GameObject.Find ("RArm");
		Grabber =GameObject.Find ("Grabber");
		GrabPoint =GameObject.Find ("GrabPoint");
		pullAngle= btdConstants.PULGA_FORZUDE_ANGLE_NO_PULL;			
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (selected == btdConstants.PULGA_SELECTED)
        {
            // Movimiento lateral ----
       		float forward = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
			if(forward>0) transform.localRotation = Quaternion.Euler(0,0,0);
			if(forward<0) {transform.localRotation = Quaternion.Euler(0,180,0);  forward=-forward;}
        	transform.Translate(Vector3.right * forward);
			
            //Salto
			if (Input.GetButton("Jump") && state != btdConstants.PULGA_JUMP)				
            {
                rigidbody.AddForce(Vector3.up * jumphight);
                state = btdConstants.PULGA_JUMP;
            }
			//Coger ------
			if (Input.GetButton("Fire1") && pullstate==btdConstants.PULGA_FORZUDE_NO_PULL && state==btdConstants.PULGA_FORZUDE_PULLUP) 
        	{  					
				pullstate=btdConstants.PULGA_FORZUDE_PULLING;
				pullAngle=btdConstants.PULGA_FORZUDE_START_ANGLE_PULL;					
        	}
			//Dejar
			if (Input.GetButton("Fire2") && pullstate==btdConstants.PULGA_FORZUDE_PULL) 
			{				 
				pullstate=btdConstants.PULGA_FORZUDE_DROP;
			}
			//Lanzar
			if (Input.GetButton("Fire3") && pullstate==btdConstants.PULGA_FORZUDE_PULL) 
			{				
				pullstate=btdConstants.PULGA_FORZUDE_THROW;
			}	
			//Estados de objeto pull
			switch(pullstate)
			{
				case btdConstants.PULGA_FORZUDE_PULLING:
					// Brazos
					LHand.transform.localRotation = Quaternion.Euler (0,0,pullAngle);
					RHand.transform.localRotation = Quaternion.Euler (0,0,pullAngle);
					Grabber.transform.localRotation = Quaternion.Euler (0,0,pullAngle);
					// Object
					Pullable.transform.localPosition = GrabPoint.transform.position ;								
					//PullAngle
					if(pullAngle<btdConstants.PULGA_FORZUDE_FINAL_ANGLE_PULL)	pullAngle=pullAngle+btdConstants.PULGA_FORZUDE_INCREASE_PULL_ANGLE;				
					else 														pullstate=btdConstants.PULGA_FORZUDE_PULL;					
					break;								
				case btdConstants.PULGA_FORZUDE_DROP:					
					// Object
					Pullable.transform.localPosition = GrabPoint.transform.position ;
					//PullAngle				
					if(pullAngle>btdConstants.PULGA_FORZUDE_START_ANGLE_PULL)	pullAngle=pullAngle-btdConstants.PULGA_FORZUDE_INCREASE_PULL_ANGLE;				
					else 														
					{	
						pullstate=btdConstants.PULGA_FORZUDE_NO_PULL;
						pullAngle = btdConstants.PULGA_FORZUDE_ANGLE_NO_PULL;
					}
					// Brazos
				    LHand.transform.localRotation = Quaternion.Euler (0,0,pullAngle);
					RHand.transform.localRotation = Quaternion.Euler (0,0,pullAngle);
					Grabber.transform.localRotation = Quaternion.Euler (0,0,pullAngle);
					break;								
				case btdConstants.PULGA_FORZUDE_THROW:					
					pullstate= btdConstants.PULGA_FORZUDE_NO_PULL;
					LHand.transform.localRotation = Quaternion.Euler (0,0,pullAngle);
					RHand.transform.localRotation = Quaternion.Euler (0,0,pullAngle);
					Grabber.transform.localRotation = Quaternion.Euler (0,0,pullAngle);
					break;
				case btdConstants.PULGA_FORZUDE_PULL:	
					Pullable.transform.localPosition = GrabPoint.transform.position ;
					break;
					
			}			
			
        }
    }

    void OnTriggerEnter(Collider other)
    {
		switch(other.tag)
		{
			//------------------------------------------------------------
			case "ground":
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
				break;
			//------------------------------------------------------------
			/*case "pullUpLeft":
				pullDirection = btdConstants.LEFT;
            	state = btdConstants.PULGA_FORZUDE_PULLUP;
            	renderer.material.color = Color.yellow;
            	Debug.Log("PullLeft");
				break;
			//------------------------------------------------------------
			case "PullUpRight":
				pullDirection = btdConstants.RIGHT;
            	state = btdConstants.PULGA_FORZUDE_PULLUP;
            	renderer.material.color = Color.yellow;
            	Debug.Log("PullRight");
				break;*/	
			//------------------------------------------------------------
			case "PullUpObj":		
				if(pullstate==btdConstants.PULGA_FORZUDE_NO_PULL)
				{
					pullDirection = btdConstants.CENTER;
					state = btdConstants.PULGA_FORZUDE_PULLUP;
            		renderer.material.color = Color.yellow;
					Pullable=GameObject.Find(other.gameObject.name);
					Debug.Log("PullCenter");	
			    }							
				break;
			//------------------------------------------------------------
			default:				
				break;			
		}   
   
    }

    void OnTriggerExit(Collider other)
    {
       // if (other.tag == "pullUpRight" || other.tag == "pullUpLeft")
        if (other.tag == "PullUpObj" )
		{
            renderer.material.color = Color.green;
			state = btdConstants.PULGA_WAIT;
        }
    }

    public void setSelected(int _selected)
    {
        selected = _selected;
    }
}
