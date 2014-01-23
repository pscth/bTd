using UnityEngine;
using System.Collections;

public class btdLuciernaga : MonoBehaviour {

	public int countx;
	public int countr;
	public int county;
	public bool enabley;
	public float forcey;
	// Use this for initialization
	void Start () 
	{
		forcey = 0;
		enabley=true;
		countx = 0;
		countr = 0;
		county = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (countx >= 300 && countx < 320) 
		{
			//enabley=false;//NOTHING				
		} 
		else if (countx >= 320 && countx < 500) 
		{
			//enabley=false;
			countr = countr + 1;
			transform.localRotation = Quaternion.Euler (0, countr, 0);
		}
		else if (countx >= 800 && countx < 820) 
		{
			//enabley=false;
			//NOTHING
		}
		else if (countx >= 820 && countx < 1000) 
		{
			//enabley=false;
			countr = countr - 1;
			transform.localRotation = Quaternion.Euler (0, countr, 0);
		}
		else if (countx == 1000)
		{		
			//enabley=false;
			countx = 0;		
			countr=0;
		}
		else 
		{
			//enabley=true;
			transform.Translate (Vector3.right * 5.0f * Time.deltaTime);
		} 
			countx = countx + 1;

		if (enabley)
		{
			if (county < 10)    	transform.Translate (Vector3.up * -forcey * Time.deltaTime);
			else if (county < 20)	transform.Translate (Vector3.up * forcey * Time.deltaTime);
			else	county = 0;
			if ((county%10)<5) forcey = forcey + 1;
			else 		        forcey = forcey - 1;
			county = county + 1;
		}
		else forcey=0;

	}
}
