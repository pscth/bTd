using UnityEngine;
using System.Collections;


public class btdPulgaForzuda : MonoBehaviour
{
    public float movementSpeed;
    public float jumpHight;
    public bool jumpHold = false;
    public float jumpForce;



    public int state;
    public int selected;

    // ATRIBUTOS
	public int pullstate;
	public int pullAngle;
    public float throwAngle;
    public bool canPull;  
	public bool isHeavy;
	private int incrAngleThrow;
	public GameObject Grabber;
	public GameObject GrabPoint;
	public	GameObject Pullable;
    public GameObject Thrower;   

    //--------------------------------
    // EVENTS
    //--------------------------------
    void Start()
    {
        // Common		
        movementSpeed = btdConstants.MOVE_SPEED;
        jumpHight = btdConstants.JUMP_HIGHT;
        rigidbody.mass = btdConstants.PULGA_MASS;
        selected = btdConstants.PULGA_UNSELECTED;
        state = btdConstants.PULGA_WAIT;
		// Forzude
		pullstate = btdConstants.PULGA_FORZUDE_NO_PULL;
		Grabber =GameObject.Find ("Grabber");
		GrabPoint =GameObject.Find ("GrabPoint");        
		pullAngle= btdConstants.PULGA_FORZUDE_ANGLE_NO_PULL;
        canPull = false;
        //pruebas        
        Thrower = GameObject.Find("Thrower");			
        throwAngle = btdConstants.PULGA_FORZUDE_INIT_THROW_ANGLE; 
		incrAngleThrow=1;
		isHeavy = false;
		faced = 1;
    }	
	void Update ()
    {
        if (selected == btdConstants.PULGA_SELECTED)
        {
            ProcessInput();
            ProcessState();
        }
    }
	void OnTriggerEnter(Collider other)
    {       
		switch(other.tag)
		{            
			case "ground":
                state = btdConstants.PULGA_WAIT;
                break;
			case "PullUpObj":		
				if(pullstate==btdConstants.PULGA_FORZUDE_NO_PULL)
				{
                    canPull = true; 	
					isHeavy=false;
					Pullable=GameObject.Find(other.gameObject.name);
					Debug.Log("PullObj");	
			    }							
				break;
			case "PullUpHeavyObj":		
				if(pullstate==btdConstants.PULGA_FORZUDE_NO_PULL)
				{
					canPull = true;		
					isHeavy=true;
					Pullable=GameObject.Find(other.gameObject.name);
					Debug.Log("PullHeavyObj");	
				}							
				break;
		}   
   
    }
	void OnTriggerExit(Collider other)
    {     
		switch(other.tag)
		{            
			/*case "ground":
				state = btdConstants.PULGA_JUMP;
				break;*/
			case "PullUpObj":		
				canPull = false;						
				break;
			case "PullUpHeavyObj":		
				canPull = false;				
				break;									
		}   
    }
    //---------------------------------------------------------------------------------
    // METHODS   
    //---------------------------------------------------------------------------------
    //Analizar Entrada-------
    void ProcessInput()
    {
        // Mover

		if(pullstate!=btdConstants.PULGA_FORZUDE_PULLING_HEAVY && pullstate!=btdConstants.PULGA_FORZUDE_PULL_HEAVY)
		{
			float forward = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
			if (forward > 0) {transform.localRotation = Quaternion.Euler(0, 0, 0); faced=1;}
			if (forward < 0) {transform.localRotation = Quaternion.Euler(0, 180, 0); forward = -forward; faced=-1; }
			transform.Translate(Vector3.right * forward);
			// Saltar
			if (Input.GetButtonDown("Jump"))
			{
				if (state != btdConstants.PULGA_JUMP)
				{
					jumpForce = 1;
					jumpHold = true;
					rigidbody.AddForce(Vector3.up * 0.2f,ForceMode.Impulse);				
					state = btdConstants.PULGA_JUMP;
				}
			}
			if (Input.GetButton("Jump") && jumpHold && jumpForce > 0 && state == btdConstants.PULGA_JUMP)
			{   
				rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
				jumpForce = jumpForce - 0.1f;
			}   
			if(Input.GetButtonUp ("Jump") && state == btdConstants.PULGA_JUMP)
			{
				jumpHold=false;	
			}
		}
        //Coger ------
        if (Input.GetButton("Fire1") && pullstate == btdConstants.PULGA_FORZUDE_NO_PULL && canPull)
        {

            if(isHeavy) pullstate = btdConstants.PULGA_FORZUDE_PULLING_HEAVY;
			else 		pullstate = btdConstants.PULGA_FORZUDE_PULLING;
            pullAngle = btdConstants.PULGA_FORZUDE_START_ANGLE_PULL;
            Pullable.rigidbody.isKinematic = true;
        }
        //Dejar y Lanzar          
		if (Input.GetButton("Fire2") && (pullstate == btdConstants.PULGA_FORZUDE_PULL || pullstate == btdConstants.PULGA_FORZUDE_PULL_HEAVY))
        {
            pullstate = btdConstants.PULGA_FORZUDE_DROP;
        }
        if (Input.GetButton("Fire3") && (pullstate == btdConstants.PULGA_FORZUDE_PULL || pullstate == btdConstants.PULGA_FORZUDE_HOLD_THROW))
        {
            Debug.Log("Pressing Fire 3");			
			if(throwAngle==90) incrAngleThrow=-1;
			else if(throwAngle==0) incrAngleThrow=1;
			throwAngle=throwAngle + incrAngleThrow;            
            Thrower.transform.localRotation = Quaternion.Euler(0, 0, throwAngle);
            pullstate = btdConstants.PULGA_FORZUDE_HOLD_THROW;                
        }
        if (Input.GetButtonUp("Fire3") && pullstate == btdConstants.PULGA_FORZUDE_HOLD_THROW)
        {
            Debug.Log("Releasing Fire 3");
            pullstate = btdConstants.PULGA_FORZUDE_THROW;   
        }        
    }

    // Actuar segun estado--------
    void ProcessState()
    {
        //Estados de objeto pull
        switch (pullstate)
        {
			case btdConstants.PULGA_FORZUDE_PULLING_HEAVY:
            case btdConstants.PULGA_FORZUDE_PULLING:
                // Brazos               
                Grabber.transform.localRotation = Quaternion.Euler(0, 0, pullAngle);
                // Object
                Pullable.transform.localPosition = GrabPoint.transform.position;
                //PullAngle
                if (pullAngle < btdConstants.PULGA_FORZUDE_FINAL_ANGLE_PULL) pullAngle = pullAngle + btdConstants.PULGA_FORZUDE_INCREASE_PULL_ANGLE;
                else if(!isHeavy) pullstate = btdConstants.PULGA_FORZUDE_PULL;
				else 			 pullstate = btdConstants.PULGA_FORZUDE_PULL_HEAVY;
                break;
            case btdConstants.PULGA_FORZUDE_DROP:
                // Object
                Pullable.transform.localPosition = GrabPoint.transform.position;
                //PullAngle				
                if (pullAngle > btdConstants.PULGA_FORZUDE_START_ANGLE_PULL) pullAngle = pullAngle - btdConstants.PULGA_FORZUDE_INCREASE_PULL_ANGLE;
                else
                {
                    Pullable.rigidbody.isKinematic = false;
                    pullstate = btdConstants.PULGA_FORZUDE_NO_PULL;
                    pullAngle = btdConstants.PULGA_FORZUDE_ANGLE_NO_PULL;					
                }
                // Brazos                
                Grabber.transform.localRotation = Quaternion.Euler(0, 0, pullAngle);
                break;
            case btdConstants.PULGA_FORZUDE_THROW:      
                
                Pullable.rigidbody.isKinematic = false;
                Pullable.rigidbody.AddForce(Thrower.transform.right * 500);       
                pullstate = btdConstants.PULGA_FORZUDE_NO_PULL;               
                pullAngle = btdConstants.PULGA_FORZUDE_ANGLE_NO_PULL;
                throwAngle = btdConstants.PULGA_FORZUDE_INIT_THROW_ANGLE;               
                Grabber.transform.localRotation = Quaternion.Euler(0, 0, pullAngle);
                Thrower.transform.localRotation = Quaternion.Euler(0, 0, throwAngle);                
                Debug.Log("Throw Complete");
                break;
            case btdConstants.PULGA_FORZUDE_HOLD_THROW:                
            case btdConstants.PULGA_FORZUDE_PULL:
				//NO FUNCIONA COMO DEBERIA.
                Pullable.transform.localPosition = GrabPoint.transform.position;
                if (state == btdConstants.PULGA_JUMP) Pullable.rigidbody.AddForce(Vector3.up * jumpHight);
                break;
			case btdConstants.PULGA_FORZUDE_PULL_HEAVY:
				break;

        }	
    }
    public void setSelected(int _selected)
    {
        selected = _selected;
    }
}
