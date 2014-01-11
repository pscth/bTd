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
            Debug.Log("Trapeze selected");
            pulgaSelected = btdConstants.PULGA_TRAPEZE;
            pulgaElastic.GetComponent<btdPulgaElastic>().setSelected(btdConstants.PULGA_UNSELECTED);
            pulgaTrapeze.GetComponent<btdPulgaTrapeze>().setSelected(btdConstants.PULGA_SELECTED);
            pulgaForzude.GetComponent<btdPulgaForzuda>().setSelected(btdConstants.PULGA_UNSELECTED);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Elastic selected");
            pulgaSelected = btdConstants.PULGA_ELASTIC;
            pulgaElastic.GetComponent<btdPulgaElastic>().setSelected(btdConstants.PULGA_SELECTED);
            pulgaTrapeze.GetComponent<btdPulgaTrapeze>().setSelected(btdConstants.PULGA_UNSELECTED);
            pulgaForzude.GetComponent<btdPulgaForzuda>().setSelected(btdConstants.PULGA_UNSELECTED);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("Forzude selected");
            pulgaSelected = btdConstants.PULGA_FORZUDE;
            pulgaElastic.GetComponent<btdPulgaElastic>().setSelected(btdConstants.PULGA_UNSELECTED);
            pulgaTrapeze.GetComponent<btdPulgaTrapeze>().setSelected(btdConstants.PULGA_UNSELECTED);
            pulgaForzude.GetComponent<btdPulgaForzuda>().setSelected(btdConstants.PULGA_SELECTED);
        }
        switch (pulgaSelected)
        {
            case btdConstants.PULGA_ELASTIC:
                mainCamera.transform.position = new Vector3(pulgaElastic.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
                break;
            case btdConstants.PULGA_TRAPEZE:
                mainCamera.transform.position = new Vector3(pulgaTrapeze.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
                break;
            case btdConstants.PULGA_FORZUDE:
                mainCamera.transform.position = new Vector3(pulgaForzude.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
                break;
        }
	}
}
