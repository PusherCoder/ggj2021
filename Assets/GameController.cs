using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static UnityEvent CrossedThreshold = new UnityEvent();

    public GameObject Stage1Boxes;
    public GameObject Stage2Boxes;
    public GameObject Stage3Boxes;

    public GameObject Stage2Fence;
    public GameObject Stage3Fence;

    public GameObject Door;

    public Material Stage0Screen;
    public Material Stage1Screen;
    public Material Stage2Screen;
    public Material Stage3Screen;
    public GameObject ComputerScreen;

    public Image ItemHolder;
    public Sprite Nightmare1;
    public Sprite Nightmare2;
    public Sprite Nightmare3;

    public AudioSource HouseMusic;
    public AudioClip[] houseClips;
    public AudioSource ActionMusic;
    public AudioClip[] actionClips;
    public AudioSource CalmMusic;
    public AudioClip[] calmClips;

    public Vector3 BobAmount;

    public int gameStage = 0;
    public bool missionStarted = false;
    private int nextStage = 1;
    private int boxWithPrize = 0;
    private int boxesPickedUp = 0;

    private bool crossfade;
    private AudioSource[] crossfadeClips = new AudioSource[2];
    private float crossfadeVolume = 0.45f;
    private Vector3 initialPosition;

    private void Awake()
    {
        initialPosition = GameObject.Find("Item").transform.localPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        HouseMusic.clip = houseClips[Random.Range(0, houseClips.Length)];
        HouseMusic.Play();
        HouseMusic.volume = crossfadeVolume;
        ActionMusic.clip = actionClips[Random.Range(0, actionClips.Length)];
        ActionMusic.Play();
        CalmMusic.clip = calmClips[Random.Range(0, calmClips.Length)];
        CalmMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        Bob();
        if (crossfade)
        {
            if (crossfadeClips[0].volume == 0.0f)
            {
                crossfadeClips[1].volume = crossfadeVolume;
                crossfade = false;
            }
            else
            {
                crossfadeClips[0].volume -= 0.005f;
                crossfadeClips[1].volume += 0.005f;
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            CrossedThreshold.Invoke();

            if (gameStage != 0)
            {
                if (boxesPickedUp > boxWithPrize)
                {
                    ItemHolder.color = new Color(255, 255, 255, 0);
                    Door.SetActive(true);
                    missionStarted = false;
                    gameStage = 0;
                    ComputerScreen.GetComponent<MeshRenderer>().material = Stage0Screen;

                    crossfadeClips[0] = CalmMusic;
                    crossfadeClips[1] = HouseMusic;
                    crossfade = true;
                }
                else
                {
                    Door.SetActive(false);

                    crossfadeClips[0] = HouseMusic;
                    crossfadeClips[1] = CalmMusic;
                    crossfade = true;
                }
            }
        }
    }

    public void ComputerAccess()
    {
        if (nextStage == 4)
        {
            SceneManager.LoadScene("EndCard", LoadSceneMode.Single);
        }
        gameStage = nextStage++;
        SetupStage();
        if (gameStage == 1) ComputerScreen.GetComponent<MeshRenderer>().material = Stage1Screen;
        if (gameStage == 2) ComputerScreen.GetComponent<MeshRenderer>().material = Stage2Screen;
        if (gameStage == 3) ComputerScreen.GetComponent<MeshRenderer>().material = Stage3Screen;
        missionStarted = true;
    }

    public void PickedUpBox()
    {
        if (++boxesPickedUp > boxWithPrize )
        {
            ItemHolder.color = new Color(255, 255, 255, 1);
            if (gameStage == 1) ItemHolder.sprite = Nightmare1;
            if (gameStage == 2) ItemHolder.sprite = Nightmare2;
            if (gameStage == 3) ItemHolder.sprite = Nightmare3;
        }
    }
    private void Bob()
    {
        GameObject.Find("Item").transform.localPosition = initialPosition + new Vector3(
            Mathf.Cos(Time.time * 9f) * PlayerController.MoveMagnitude * BobAmount.x,
            Mathf.Sin(Time.time * 7f) * PlayerController.MoveMagnitude * BobAmount.y,
            Mathf.Cos(Time.time * 5f) * PlayerController.MoveMagnitude * BobAmount.z);
    }

    void SetupStage()
    {
        int i = 0;
        GameObject[] stage1BoxChildren = new GameObject[Stage1Boxes.transform.childCount];
        GameObject[] stage2BoxChildren = new GameObject[Stage2Boxes.transform.childCount];
        GameObject[] stage3BoxChildren = new GameObject[Stage3Boxes.transform.childCount];

        // Reset box count
        boxesPickedUp = 0;

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

        if (gameStage == 1)
            SpawnManager.SpawnZone1.Invoke(10);
        if (gameStage == 2)
            SpawnManager.SpawnZone1And2.Invoke(40);
        if (gameStage == 3)
            SpawnManager.SpawnZone1And2And3.Invoke(100);

        HUDText.Health = 100;

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

        // Deactivate fences
        if (gameStage > 1) Stage2Fence.SetActive(false);
        if (gameStage > 2) Stage3Fence.SetActive(false);

        // Determine the magic box that contains the nightmare fuel
        boxWithPrize = boxesToKill + Random.Range(0, boxesToKill/3);
        boxWithPrize = 2;
    }
}
