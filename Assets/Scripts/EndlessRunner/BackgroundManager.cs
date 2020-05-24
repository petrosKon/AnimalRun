using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [Header("Backgrounds")]
    public GameObject[] backgrounds;

    public float nextPlatformZ = 50f; //This variable is required in order to show the next platform on top of the other in the Z plane

    private PlayerController thePlayer;
    private int previousNumber;
    private int nextNumber;
    public float backGroundChangeVariable = 500f;
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        nextNumber = 1;
        previousNumber = 0;
        backgrounds[nextNumber].SetActive(true);
        backgrounds[nextNumber].transform.position = new Vector3(backGroundChangeVariable, backgrounds[nextNumber].transform.position.y, backgrounds[previousNumber].transform.position.z - nextPlatformZ);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        BackgroundChange();
    }

    public void BackgroundChange()
    {

        if (nextNumber < 4)
        {
            if (nextNumber == 1)
            {
                if (thePlayer.transform.position.x + 50f >= backgrounds[nextNumber].transform.position.x)
                {
                    LinearBackgroundChange();
                }
            }
            else if (thePlayer.transform.position.x >= backgrounds[nextNumber].transform.position.x)
            {
                LinearBackgroundChange();
            }
        }
    }

    void LinearBackgroundChange()
    {
        //disable the script that allows scrolling behaviour
        foreach (ScrollingBackground scrollingBackground in backgrounds[previousNumber].GetComponentsInChildren<ScrollingBackground>())
        {
            if (scrollingBackground.isActiveAndEnabled)
            {
                scrollingBackground.enabled = false;
            }
        }

        backgrounds[previousNumber].SetActive(false);

        //enable the script that allows scrolling behaviour in the current object
        foreach (ScrollingBackground scrollingBackground in backgrounds[nextNumber].GetComponentsInChildren<ScrollingBackground>())
        {
            if (!scrollingBackground.isActiveAndEnabled)
            {
                scrollingBackground.enabled = true;
            }
        }

        previousNumber = nextNumber;
        nextNumber++;

        backgrounds[nextNumber].SetActive(true);
        backgrounds[nextNumber].transform.position = new Vector3(backgrounds[previousNumber].transform.position.x + backGroundChangeVariable, backgrounds[nextNumber].transform.position.y, backgrounds[previousNumber].transform.position.z - nextPlatformZ);
    }
}
