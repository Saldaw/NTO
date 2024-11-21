using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class CameraControlle : MonoBehaviour
{
    [SerializeField] private GameObject cameraMain;
    [SerializeField] private GameObject cameraMainCamera;
    [SerializeField] private GameObject cameraPlanet;
    [SerializeField] private GameObject cameraPlanetCamera;
    [SerializeField] private GameObject Planet;
    [SerializeField] private GameObject PlanetVive;
    [Range(2, 200)] public int Spead = 2;
    [SerializeField] private float movingSpead;
    private bool cameraPlanetMode = true;
    private Vector2 mainCameraPosition;
    private float planetCameraRotation;
    private float planetRotation;
    public float wheel_speed = 10000f;
    [SerializeField] private int mapScale;
    [SerializeField] private float changeMode;
    [SerializeField] private float changeModePlanet;

    private float zoomSpead = 3f;
    private float targetZoom = -19;
    private bool isZooming = false;

    const int ziroCord = 100;

    private Vector2 previousMousePosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraPlanetMode)
        {
            if (Input.GetMouseButtonDown(1)) previousMousePosition = Input.mousePosition;
            if (Input.GetMouseButton(1))
            {
                Vector2 currentMousePosition = Input.mousePosition;
                Vector2 mousDelta = currentMousePosition - previousMousePosition;
                previousMousePosition = currentMousePosition;
                if (mousDelta.y != 0) RotateCameraY(mousDelta.y/ Screen.height/2);
                if (mousDelta.x != 0) RotatePlanetX(mousDelta.x / Screen.width);
            }
            float mw = Input.GetAxis("Mouse ScrollWheel");
            if (mw != 0)
                targetZoom += mw * wheel_speed*Time.deltaTime*1000;
                isZooming = true;
            if (isZooming) UpdateZooming();
        }
        else
        {
            previousMousePosition = Input.mousePosition;
            if (previousMousePosition.x / Screen.width > 0.9f) MoveCamera(1,0);
            if (previousMousePosition.x / Screen.width < 0.1f) MoveCamera(-1, 0);
            if (previousMousePosition.y / Screen.height > 0.9f) MoveCamera(0, 1);
            if (previousMousePosition.y / Screen.height < 0.1f) MoveCamera(0, -1);
            float mw = Input.GetAxis("Mouse ScrollWheel");
            if (mw != 0)
                targetZoom -= mw * wheel_speed * Time.deltaTime * 300;
            isZooming = true;
            if (isZooming) UpdateZoomingPlanet();
        }
    }
    private void RotateCameraY(float y)
    {
        if(Mathf.Abs(y * Spead) < 90) {
            if (cameraPlanet.transform.eulerAngles.x - y * Spead > 75 && cameraPlanet.transform.eulerAngles.x - y * Spead < 180) cameraPlanet.transform.rotation = Quaternion.Euler(75, cameraPlanet.transform.eulerAngles.y, cameraPlanet.transform.eulerAngles.z);
            else if (cameraPlanet.transform.eulerAngles.x - y * Spead < 285 && cameraPlanet.transform.eulerAngles.x - y * Spead > 180) cameraPlanet.transform.rotation = Quaternion.Euler(285, cameraPlanet.transform.eulerAngles.y, cameraPlanet.transform.eulerAngles.z);
            else cameraPlanet.transform.Rotate(-y * Spead, 0, 0);
        }
        
    }
    private void MoveCamera(int x, int y)
    {
        cameraMain.transform.position = new Vector3(cameraMain.transform.position.x + x*movingSpead*Time.deltaTime*cameraMainCamera.transform.localPosition.y/10, 0, cameraMain.transform.position.z + y * movingSpead * Time.deltaTime* cameraMainCamera.transform.localPosition.y / 10);
    }
    private void RotatePlanetX(float x)
    {
        Planet.transform.Rotate(0, -x * Spead, 0);
    }
    void ChangeMode()
    {
        
        if (cameraPlanetMode)
        {
            cameraPlanetMode = !cameraPlanetMode;
            if (Planet.transform.eulerAngles.y > 180) mainCameraPosition.x = ziroCord + mapScale / 2 * -(360 - Planet.transform.eulerAngles.y) / 180;
            else mainCameraPosition.x = ziroCord + mapScale / 2 * Planet.transform.eulerAngles.y / 180;
            mainCameraPosition.y = ziroCord + mapScale / 2 * Mathf.Sin(Mathf.PI / 180f * cameraPlanet.transform.eulerAngles.x);
            cameraMain.transform.position = new Vector3(mainCameraPosition.x, 0, mainCameraPosition.y);
            PlanetVive.SetActive(cameraPlanetMode);
            targetZoom = 0;
        }
        
        
    }
    void ChangeModPlanet()
    {
        cameraPlanetMode = !cameraPlanetMode;
        cameraPlanet.transform.eulerAngles = new Vector3(Mathf.Asin((cameraMain.transform.position.z - ziroCord)/(mapScale/2)) * 180 / Mathf.PI, -90, 0);
        targetZoom = -20;
        Planet.transform.eulerAngles = new Vector3(0, 0, 0);
        Planet.transform.Rotate(0, 360 / mapScale * (cameraMain.transform.position.x-ziroCord), 0);
        PlanetVive.SetActive(cameraPlanetMode);
    }
    void UpdateZooming()
    {
        float posX = cameraPlanetCamera.transform.localPosition.z;
        posX = Mathf.Lerp(posX, targetZoom, zoomSpead * Time.deltaTime);
        cameraPlanetCamera.transform.localPosition = new Vector3(cameraPlanetCamera.transform.localPosition.x, cameraPlanetCamera.transform.localPosition.y, posX);
        if (Mathf.Abs(targetZoom - posX) <= 1f) targetZoom = posX;  isZooming = false;
        if (posX > changeMode) ChangeMode();
    }
    void UpdateZoomingPlanet()
    {
        float posY = cameraMainCamera.transform.localPosition.y;
        posY = Mathf.Lerp(posY, targetZoom, zoomSpead * Time.deltaTime);
        cameraMainCamera.transform.localPosition = new Vector3(cameraMainCamera.transform.localPosition.x, posY, cameraMainCamera.transform.localPosition.z);
        if (Mathf.Abs(targetZoom - posY) <= 1f) targetZoom = posY; isZooming = false;
        if (posY > changeModePlanet) ChangeModPlanet();
    }
    }
