using UnityEngine;
using System.Collections;


public class btdPulgaForzuda : btdPulgaController
{
	public int pullstate;
	public int pullAngle;	   

	private GameObject LHand; //brazos
	private GameObject RHand;
	public GameObject Grabber;
	public GameObject GrabPoint;
	public	GameObject Pullable;

    // Use this for initialization
    void Start()
    {        
		PulgaCommonStart ();
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
			PulgaCommonUpdate ();
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
		PulgaCommonOnTriggerEnter (other);
		switch(other.tag)
		{
			case "PullUpObj":		
				if(pullstate==btdConstants.PULGA_FORZUDE_NO_PULL)
				{
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
		PulgaCommonOnTriggerExit (other);
        if (other.tag == "PullUpObj" )
		{
            renderer.material.color = Color.green;
			state = btdConstants.PULGA_WAIT;
        }
    }
}
