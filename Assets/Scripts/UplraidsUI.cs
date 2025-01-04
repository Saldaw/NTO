using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UplraidsUI : MonoBehaviour
{
    [SerializeField] GameObject Branchs;
    Vector2 startpos = Vector2.zero;
    static float maxScale = 3f;
    static float minScale = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            if (startpos == Vector2.zero)
            {
                startpos = Input.mousePosition;
            }
            Vector2 targetpos = Input.mousePosition - new Vector3(startpos.x, startpos.y, 0);
            if (Branchs.transform.position.x + targetpos.x < Screen.width * 1.5f && Branchs.transform.position.x + targetpos.x > 0 - Screen.width * 0.5f)
            {
                Branchs.transform.position += new Vector3(targetpos.x, 0, 0);
            }
            if (Branchs.transform.position.y + targetpos.y < Screen.height * 1.5f && Branchs.transform.position.y + targetpos.y > 0 - Screen.height * 0.5f)
            {
                Branchs.transform.position += new Vector3(0,targetpos.y, 0);
            }
            startpos = Input.mousePosition;
        }
        else
        {
            startpos = Vector2.zero;
        }
        float mw = Input.GetAxis("Mouse ScrollWheel");
        if (mw != 0)
        {
            if (Branchs.transform.localScale.x + mw < maxScale && Branchs.transform.localScale.x + mw > minScale)
            {
                Branchs.transform.localScale += new Vector3(mw, 0, 0);
            }
            if (Branchs.transform.localScale.y + mw < maxScale && Branchs.transform.localScale.y + mw > minScale)
            {
                Branchs.transform.localScale += new Vector3(0, mw, 0);
            }
        }
    }
}
