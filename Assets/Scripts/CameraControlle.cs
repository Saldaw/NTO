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

    private float minZoomPlanet = -25;
    private float maxZoom = 4;

    const int ziroCord = 1500;

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
                if (targetZoom + mw * wheel_speed * Time.deltaTime * 300 > minZoomPlanet) targetZoom += mw * wheel_speed * Time.deltaTime * 300; else targetZoom = minZoomPlanet;
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
                if(targetZoom - mw * wheel_speed * Time.deltaTime * 300>maxZoom)targetZoom -= mw * wheel_speed * Time.deltaTime * 300;else targetZoom = maxZoom;
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
        if(cameraMain.transform.position.x + x * movingSpead * Time.deltaTime * cameraMainCamera.transform.localPosition.y / 10>ziroCord+mapScale/2)cameraMain.transform.position = new Vector3(cameraMain.transform.position.x + x * movingSpead * Time.deltaTime * cameraMainCamera.transform.localPosition.y / 10-mapScale, 0, cameraMain.transform.position.z + y * movingSpead * Time.deltaTime * cameraMainCamera.transform.localPosition.y / 10);
        else if (cameraMain.transform.position.x + x * movingSpead * Time.deltaTime * cameraMainCamera.transform.localPosition.y / 10 < ziroCord - mapScale / 2) cameraMain.transform.position = new Vector3(cameraMain.transform.position.x + x * movingSpead * Time.deltaTime * cameraMainCamera.transform.localPosition.y / 10 + mapScale, 0, cameraMain.transform.position.z + y * movingSpead * Time.deltaTime * cameraMainCamera.transform.localPosition.y / 10);
        if (cameraMain.transform.position.z + y * movingSpead * Time.deltaTime* cameraMainCamera.transform.localPosition.y / 10 > ziroCord + mapScale / 2) cameraMain.transform.position = new Vector3(cameraMain.transform.position.x + x * movingSpead * Time.deltaTime * cameraMainCamera.transform.localPosition.y / 10, 0, cameraMain.transform.position.z + y * movingSpead * Time.deltaTime * cameraMainCamera.transform.localPosition.y / 10- mapScale);
        else if (cameraMain.transform.position.z + y * movingSpead * Time.deltaTime * cameraMainCamera.transform.localPosition.y / 10 < ziroCord - mapScale / 2) cameraMain.transform.position = new Vector3(cameraMain.transform.position.x + x * movingSpead * Time.deltaTime * cameraMainCamera.transform.localPosition.y / 10, 0, cameraMain.transform.position.z + y * movingSpead * Time.deltaTime * cameraMainCamera.transform.localPosition.y / 10+ mapScale);
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

            if (cameraPlanet.transform.eulerAngles.x<90) mainCameraPosition.y = ziroCord + mapScale / 2 * (cameraPlanet.transform.eulerAngles.x / 90f);
            else mainCameraPosition.y = ziroCord + mapScale / 2 * ((cameraPlanet.transform.eulerAngles.x-360f) / 90f);

            cameraMainCamera.transform.localPosition = new Vector3(0, changeModePlanet - 1, 0);
            cameraMain.transform.position = new Vector3(mainCameraPosition.x, 0, mainCameraPosition.y);
            PlanetVive.SetActive(cameraPlanetMode);
            targetZoom = 60;
        }
        
        
    }
    void ChangeModPlanet()
    {
        cameraPlanetMode = !cameraPlanetMode;
        cameraPlanet.transform.eulerAngles = new Vector3(0, 0, 0);
        cameraPlanet.transform.Rotate((cameraMain.transform.position.z - ziroCord) / (mapScale / 2)*90f, -90,0);
        targetZoom = -15;
        cameraPlanetCamera.transform.localPosition = new Vector3(0, 0, changeMode-1);
        Planet.transform.eulerAngles = new Vector3(0, 0, 0);
        Planet.transform.Rotate(0, 360f / mapScale * (cameraMain.transform.position.x-ziroCord), 0);
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
