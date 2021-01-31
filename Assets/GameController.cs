using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject Stage1Boxes;
    public GameObject Stage2Boxes;
    public GameObject Stage3Boxes;

    public GameObject Stage2Fence;
    public GameObject Stage3Fence;

    private int gameStage = 0;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ComputerAccess()
    {
        gameStage++;
        SetupStage();
    }

    void SetupStage()
    {
        int i = 0;
        GameObject[] stage1BoxChildren = new GameObject[Stage1Boxes.transform.childCount];
        GameObject[] stage2BoxChildren = new GameObject[Stage2Boxes.transform.childCount];
        GameObject[] stage3BoxChildren = new GameObject[Stage3Boxes.transform.childCount];

        // Calculate the number of boxes that need to be disposed of
        int boxesToKill = Stage1Boxes.transform.childCount / 3;
        boxesToKill += (gameStage > 1) ? Stage2Boxes.transform.childCount / 3 : 0;
        boxesToKill += (gameStage > 2) ? Stage3Boxes.transform.childCount / 3 : 0;

        // Generate the list of boxes for all three stages
        foreach (Transform child in Stage1Boxes.transform) stage1BoxChildren[i++] = child.gameObject;
        i = 0;
        foreach (Transform child in Stage2Boxes.transform) stage2BoxChildren[i++] = child.gameObject;
        i = 0;
        foreach (Transform child in Stage3Boxes.transform) stage3BoxChildren[i++] = child.gameObject;

        // Concatenate the arrays based on the current stage
        GameObject[] boxArray = new GameObject[Stage1Boxes.transform.childCount + Stage2Boxes.transform.childCount + Stage3Boxes.transform.childCount];
        int boxArraySize = Stage1Boxes.transform.childCount;
        stage1BoxChildren.CopyTo(boxArray, 0);
        if (gameStage > 1)
        {
            boxArraySize += Stage2Boxes.transform.childCount;
            stage2BoxChildren.CopyTo(boxArray, Stage1Boxes.transform.childCount);
        }
        if (gameStage > 2)
        {
            boxArraySize += Stage3Boxes.transform.childCount;
            stage3BoxChildren.CopyTo(boxArray, (Stage1Boxes.transform.childCount + Stage2Boxes.transform.childCount));
        }
        
        // Activate all the boxes
        for (i = 0; i < boxArraySize; i++)
        {
            boxArray[i].SetActive(true);
        }

        // Deactive the appropriate number of boxes
        for (i = 0; i < boxesToKill; )
        {
            int boxToKill = Random.Range(0, boxArraySize);
            if (boxArray[boxToKill].activeInHierarchy == true)
            {
                boxArray[boxToKill].SetActive(false);
                i++;
            }
        }
    }
}
