using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject Stage1Boxes;
    public GameObject Stage2Boxes;
    public GameObject Stage3Boxes;

    private int gameStage = 1;

    private void Awake()
    {
        SetupStage();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetupStage()
    {
        int i = 0;
        GameObject[] stage1BoxChildren = new GameObject[Stage1Boxes.transform.childCount];
        Debug.Log("There are " + Stage1Boxes.transform.childCount + " children.");

        if (gameStage==1)
        {
            // Get the box threshold numbers
            int boxesToKill = Stage1Boxes.transform.childCount / 3;
            // Get the list of boxes
            foreach ( Transform child in Stage1Boxes.transform )
            {
                stage1BoxChildren[i] = child.gameObject;
                i++;
            }
            // Deactivate 1/3 of the boxes
            for (i = 0; i < boxesToKill; )
            {
                int boxToKill = Random.Range(0, Stage1Boxes.transform.childCount);
                if( stage1BoxChildren[boxToKill].activeInHierarchy == true )
                {
                    stage1BoxChildren[boxToKill].SetActive(false);
                    i++;
                }
            }
        }
    }
}
