using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUp: MonoBehaviour
{
    [SerializeField] LayerMask PickupMask;
    [SerializeField] LayerMask DropOffMask;
    [SerializeField] Camera PlayerCamera;
    [SerializeField] Transform  PickupTarget;
    [SerializeField] Transform []  DropOffTargets;
    [Space]
    [SerializeField] float PickupRange;

    [SerializeField] float speed;
    //Rigidbody CurrentObject;
    GameObject []  currentPlates;
    public string currentPlateName;
    public float MinDistance;
    public GameObject Player;
    public bool placed;

    private void Start()
    {
        placed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            PickUpMethod();
        }
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            DropoffMethod();
        }

    }
    
    public void PickUpMethod()
    {
        Ray CameraRay = PlayerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(CameraRay, out RaycastHit hitInfo, PickupRange, PickupMask))
        {

            currentPlates = GameObject.FindGameObjectsWithTag("PickUp");


            foreach (GameObject currentPlate in currentPlates)
            {
                float dist = Vector3.Distance(currentPlate.transform.position, Player.transform.position);

                if (dist < MinDistance)
                {
  
                    currentPlate.transform.position = PickupTarget.position;
                    currentPlate.transform.parent = PickupTarget;

                    placed = false;



                    currentPlate.tag = "DropOff";
                    currentPlate.layer = 14;

                    break;
                }
                
            }
        }

    }

    public void DropoffMethod()
    {
        currentPlates = GameObject.FindGameObjectsWithTag("DropOff");


        foreach (Transform DropOffTarget in DropOffTargets)
        {

            float dist = Vector3.Distance(DropOffTarget.position, Player.transform.position);

            foreach (GameObject currentPlate in currentPlates)
            {

                if (dist < MinDistance)
                {



                    currentPlate.transform.position = DropOffTarget.position;
                    currentPlate.transform.parent = DropOffTarget;

                    currentPlateName = currentPlate.name;
                    Debug.Log(currentPlateName);

                    placed = true;

                    currentPlate.tag = "PickUp";
                    currentPlate.layer = 15;

                }

            }
        }

        
            
    }


}
