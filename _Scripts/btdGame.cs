using UnityEngine;
using System.Collections;

public class btdGame : MonoBehaviour 
{
    public int pulgaSelected;
    public GameObject pulgaElastic;
    public GameObject pulgaTrapeze;
    public GameObject pulgaForzude;

	// Use this for initialization
	void Start () 
    {
        pulgaSelected = 0;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Trapeze selected");
            pulgaSelected = btdConstants.PULGA_TRAPEZE;
            pulgaElastic.GetComponent<btdPulgaElastic>().setSelected(btdConstants.PULGA_UNSELECTED);
            pulgaTrapeze.GetComponent<btdPulgaTrapeze>().setSelected(btdConstants.PULGA_SELECTED);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Elastic selected");
            pulgaSelected = btdConstants.PULGA_ELASTIC;
            pulgaElastic.GetComponent<btdPulgaElastic>().setSelected(btdConstants.PULGA_SELECTED);
            pulgaTrapeze.GetComponent<btdPulgaTrapeze>().setSelected(btdConstants.PULGA_UNSELECTED);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            pulgaSelected = btdConstants.PULGA_FORZUDE;
        }
	}
}
