using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventroyUi : MonoBehaviour
{
    [SerializeField] private GameObject inventroy;
    [SerializeField] private bool isPop = false;


    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if(isPop)
            {
                isPop = false;
            }
            else isPop = true;
                PopUp();
        }
    }

    public void PopUp()
    {
        inventroy.gameObject.SetActive(isPop);
    }
}
