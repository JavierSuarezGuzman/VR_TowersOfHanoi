using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logic : MonoBehaviour
{
    public GameObject[] legalCubes = new GameObject[2];
    public GameObject platformObject;
    internal bool isDone;

    GameManager gameManager;
    OVRGrabbable grabbable;

    private void Awake()
    {
        grabbable = GetComponent<OVRGrabbable>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    //first frame
    void Start()
    {
        grabbable.grabEnd_Event.AddListener(Dropped);
        grabbable.grabBegin_Event.AddListener(Grabbed);
    }

    void Dropped()
    {
        gameManager.Moved();
    }

    void Grabbed(bool isLeft)
    {
        if (isLeft)
            OVRInput.SetControllerVibration(0.1f, 0.3f, OVRInput.Controller.LTouch);
        else
            OVRInput.SetControllerVibration(0.1f, 0.3f, OVRInput.Controller.RTouch);

        StartCoroutine(ExecuteAfterSeconds(0.2f));
    }
    
    IEnumerator ExecuteAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        OVRInput.SetControllerVibration(0.0f, 0.0f, OVRInput.Controller.LTouch);
        OVRInput.SetControllerVibration(0.0f, 0.0f, OVRInput.Controller.RTouch);
    }

    //once per every fram
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.position.y > transform.position.y)
        {
            bool legal = false;
            foreach (GameObject legalCube in legalCubes)
            {
                if (legalCube.Equals(collision.gameObject))
                {
                    legal = true; //bounce
                    break;
                }
            }

            if (!legal)
            {
                Rigidbody r = collision.gameObject.GetComponent<Rigidbody>();
                if (r)
                {
                    r.AddForce(3 * Vector3.up, ForceMode.Impulse);
                    collision.gameObject.GetComponent<AudioSource>().Play();
                }
            }
        }

        if (isDone)
            return;

        if (collision.transform.position.y < transform.position.y)
            if (collision.gameObject.Equals(platformObject))
                isDone = true;

    }


    private void OnCollisionExit(Collision colllision)
    {
        if (colllision.gameObject.Equals(platformObject))
            isDone = false;

    }

    internal void Reset(float yDelta)
    {
        transform.position = new Vector3(-0.4f, 0.5f + yDelta, 0.5f);
        isDone = false;
    }
    internal void setAlpha(float alphaLevel)
    {
        Color cubeColor = gameObject.GetComponent<Renderer>().material.color;
        cubeColor.a = alphaLevel;
        gameObject.GetComponent<Renderer>().material.color = cubeColor;
    }

    internal void showmarker(bool markerActive)
    {
        Transform crossHair = transform.GetChild(0);
        crossHair.gameObject.SetActive(markerActive);
    }
    
}
