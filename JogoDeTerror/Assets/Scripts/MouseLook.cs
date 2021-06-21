using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class MouseLook : MonoBehaviour
{
    [Range(0.01f,10f)]public float mouseSensivity = 2f;

    public Transform pointTransform;
    public MultiAimConstraint multiAim;
    public Camera mainCamera;
    public Transform playerbody;
    public LayerMask layerHit;

    float xRotation = 0;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(pointTransform,transform.position,Quaternion.identity);

        multiAim.data.worldUpObject=pointTransform;
        mainCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        
        float mouseX = Input.GetAxis("Mouse X") * mouseSensivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation,-90,90);

        transform.localRotation = Quaternion.Euler(xRotation,0f,0f);
       
        playerbody.Rotate(Vector3.up*mouseX);

        Aim();
    }

    private  void Aim()
    {
        Ray cameraray = mainCamera.ScreenPointToRay(Input.mousePosition);

        Plane groundplane = new Plane(Vector3.up, Vector3.zero);



        if (Physics.Raycast(cameraray, out RaycastHit raycast, Mathf.Infinity, layerHit))
        {

            Vector3 pointToLook = cameraray.GetPoint(raycast.distance);

            Debug.DrawLine(cameraray.origin, pointToLook, Color.blue);

            pointTransform.localPosition = pointToLook;


        }


    }

    /*
    private (bool success,Vector3 position) GetMousePosition()
    {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray,out var hitinfo, Mathf.Infinity,layerHit))
        {
            return (success: true, position:hitinfo.point);
        }else
        {
            return (success: false, position: Vector3.zero);
        }
    }*/
}
