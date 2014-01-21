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
        KeyboardSelectionMode();
        GamePadSelecionMode();
        Selection();
    }

    void Selection()
    {
        switch (pulgaSelected)
        {
            case btdConstants.PULGA_FORZUDE:
                Debug.Log("Forzude selected");
                pulgaElastic.GetComponent<btdPulgaElastic>().setSelected(btdConstants.PULGA_UNSELECTED);
                pulgaTrapeze.GetComponent<btdPulgaTrapeze>().setSelected(btdConstants.PULGA_UNSELECTED);
                pulgaForzude.GetComponent<btdPulgaForzuda>().setSelected(btdConstants.PULGA_SELECTED);
                // Camera
                mainCamera.GetComponent<btdCamera>().newTarget(pulgaForzude);
                break;
            case btdConstants.PULGA_CLOWN:
                break;
            case btdConstants.PULGA_TRAPEZE:
                Debug.Log("Trapeze selected");
                pulgaElastic.GetComponent<btdPulgaElastic>().setSelected(btdConstants.PULGA_UNSELECTED);
                pulgaTrapeze.GetComponent<btdPulgaTrapeze>().setSelected(btdConstants.PULGA_SELECTED);
                pulgaForzude.GetComponent<btdPulgaForzuda>().setSelected(btdConstants.PULGA_UNSELECTED);
                // Camera
                mainCamera.GetComponent<btdCamera>().newTarget(pulgaTrapeze);
                break;
            case btdConstants.PULGA_ELASTIC:
                Debug.Log("Elastic selected");
                pulgaElastic.GetComponent<btdPulgaElastic>().setSelected(btdConstants.PULGA_SELECTED);
                pulgaTrapeze.GetComponent<btdPulgaTrapeze>().setSelected(btdConstants.PULGA_UNSELECTED);
                pulgaForzude.GetComponent<btdPulgaForzuda>().setSelected(btdConstants.PULGA_UNSELECTED);
                // Camera
                mainCamera.GetComponent<btdCamera>().newTarget(pulgaElastic);
                break;
            case btdConstants.PULGA_FAQUIR:
                break;
            case btdConstants.PULGA_GROUP:
                break;
        }
    }

    void GamePadSelecionMode()
    {
        int dir = 0;
        if (Input.GetButtonDown("SelectRight")) dir = 1;
        if (Input.GetButtonDown("SelectLeft")) dir = -1;
        pulgaSelected = pulgaSelected + dir;

        while (pulgaSelected == 0 || pulgaSelected == btdConstants.PULGA_CLOWN || pulgaSelected == btdConstants.PULGA_FAQUIR || pulgaSelected == btdConstants.PULGA_GROUP)
        {
            pulgaSelected = pulgaSelected + dir;
            if (pulgaSelected < 1) pulgaSelected = 6;
            else if (pulgaSelected > 6) pulgaSelected = 1;
        }
    }

    void KeyboardSelectionMode()
    {
        if      (Input.GetKeyDown(KeyCode.Alpha1))          pulgaSelected = btdConstants.PULGA_FORZUDE;
        else if (Input.GetKeyDown(KeyCode.Alpha2))          { }
        else if (Input.GetKeyDown(KeyCode.Alpha3))          pulgaSelected = btdConstants.PULGA_TRAPEZE;
        else if (Input.GetKeyDown(KeyCode.Alpha4))          pulgaSelected = btdConstants.PULGA_ELASTIC;
        else if (Input.GetKeyDown(KeyCode.Alpha5))          { }
        else if (Input.GetKeyDown(KeyCode.Alpha6))          { }
	}
}
