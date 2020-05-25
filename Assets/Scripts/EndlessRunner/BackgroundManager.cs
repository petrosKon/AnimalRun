using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [Header("Backgrounds")]
    public GameObject[] backgrounds;

    [Header("Z position between platforms")]
    public float nextPlatformZ = 30f; //This variable is required in order to show the next platform on top of the other in the Z plane

    [Header("Distance Between new Backgrounds")]
    public float backgroundX = 200f;
    public float nextBackgroundX = 250f;

    [Header("Speed of the parallax Background")]
    public const float parallaxSpeed = 1.3f;

    private PlayerController thePlayer;
    private int previousNumber;
    private int nextNumber;

    private float backgroundZ = 900f;
    private float changePoint;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        previousNumber = 0;
        nextNumber = previousNumber + 1;
        backgrounds[nextNumber].SetActive(true);
        backgroundZ -= nextPlatformZ;
        backgrounds[nextNumber].transform.position = new Vector3(backgroundX, backgrounds[nextNumber].transform.position.y, backgroundZ);

        FindNextBackgroundStartingPoint();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LinearBackgroundChange();
    }

    void LinearBackgroundChange()
    {
        if (backgroundZ >= 30)
        {

            if (thePlayer.transform.position.x >= changePoint)
            {
                //enable the script that allows scrolling behaviour in the current object
                foreach (ScrollingBackground scrollingBackground in backgrounds[nextNumber].GetComponentsInChildren<ScrollingBackground>())
                {
                    if (!scrollingBackground.isActiveAndEnabled)
                    {
                        scrollingBackground.enabled = true;
                        scrollingBackground.parallaxSpeed = parallaxSpeed;
                    }
                }

                //disable the script that allows scrolling behaviour
                foreach (ScrollingBackground scrollingBackground in backgrounds[previousNumber].GetComponentsInChildren<ScrollingBackground>())
                {
                    if (scrollingBackground.isActiveAndEnabled)
                    {
                        scrollingBackground.enabled = false;
                        scrollingBackground.parallaxSpeed = 0f;

                    }
                }

                backgrounds[previousNumber].SetActive(false);

                previousNumber = nextNumber;
                nextNumber++;

                if(nextNumber > 3)
                {
                    nextNumber = 0;
                }

                backgroundX += nextBackgroundX;
                backgrounds[nextNumber].transform.position = new Vector3(backgroundX, backgrounds[nextNumber].transform.position.y, backgrounds[previousNumber].transform.position.z - nextPlatformZ);
                backgrounds[nextNumber].SetActive(true);

                FindNextBackgroundStartingPoint();
            }
        }

    }
    void FindNextBackgroundStartingPoint()
    {

        List<Transform> backgroundTransforms = new List<Transform>();
        changePoint = Mathf.Infinity;

        foreach (Transform child in backgrounds[nextNumber].GetComponentsInChildren<Transform>())
        {
            if (child.name.Equals("Background"))
            {
                foreach (Transform backgroundTrans in child)
                {
                    backgroundTransforms.Add(backgroundTrans);
                }
                break;
            }
        }

        foreach (Transform back in backgroundTransforms)
        {
            if (back.position.x < changePoint)
            {
                changePoint = back.position.x;
            }
        }

        Debug.Log(changePoint);

    }
}
