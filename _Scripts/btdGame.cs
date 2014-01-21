using UnityEngine;
using System.Collections;

public class btdGame : MonoBehaviour 
{
    public int pulgaSelected;
    public GameObject pulgaElastic;
    public GameObject pulgaTrapeze;
    public GameObject pulgaForzude;
    public Camera mainCamera;

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
            Debug.Log("Forzude selected");
            pulgaSelected = btdConstants.PULGA_FORZUDE;
            pulgaElastic.GetComponent<btdPulgaElastic>().setSelected(btdConstants.PULGA_UNSELECTED);
            pulgaTrapeze.GetComponent<btdPulgaTrapeze>().setSelected(btdConstants.PULGA_UNSELECTED);
            pulgaForzude.GetComponent<btdPulgaForzuda>().setSelected(btdConstants.PULGA_SELECTED);
        } 
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("Trapeze selected");
            pulgaSelected = btdConstants.PULGA_TRAPEZE;
            pulgaElastic.GetComponent<btdPulgaElastic>().setSelected(btdConstants.PULGA_UNSELECTED);
            pulgaTrapeze.GetComponent<btdPulgaTrapeze>().setSelected(btdConstants.PULGA_SELECTED);
            pulgaForzude.GetComponent<btdPulgaForzuda>().setSelected(btdConstants.PULGA_UNSELECTED);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("Elastic selected");
            pulgaSelected = btdConstants.PULGA_ELASTIC;
            pulgaElastic.GetComponent<btdPulgaElastic>().setSelected(btdConstants.PULGA_SELECTED);
            pulgaTrapeze.GetComponent<btdPulgaTrapeze>().setSelected(btdConstants.PULGA_UNSELECTED);
            pulgaForzude.GetComponent<btdPulgaForzuda>().setSelected(btdConstants.PULGA_UNSELECTED);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
        }
        switch (pulgaSelected)
        {
            case btdConstants.PULGA_FORZUDE:
                mainCamera.GetComponent<btdCamera>().newTarget(pulgaForzude);
                break;
            case btdConstants.PULGA_CLOWN:
                break;
            case btdConstants.PULGA_TRAPEZE:
                mainCamera.GetComponent<btdCamera>().newTarget(pulgaTrapeze);
                break;
            case btdConstants.PULGA_ELASTIC:
                mainCamera.GetComponent<btdCamera>().newTarget(pulgaElastic);
                break;
            case btdConstants.PULGA_FAQUIR:
                break;
            case btdConstants.PULGA_GROUP:
                break;
        }
	}
}
