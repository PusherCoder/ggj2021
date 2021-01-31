using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupText : MonoBehaviour
{
    private Queue<PickUpScrapLine> lines = new Queue<PickUpScrapLine>();
    private Text text;


    private void Awake()
    {
        BoxController.OnPickUpMetalScrap.AddListener(PickUpScrap);
        text = GetComponent<Text>();
    }

    private void Update()
    {
        if (lines.Count == 0) return;

        PickUpScrapLine oldest = lines.Peek();
        if (Time.time - oldest.CreatedAt > 5)
        {
            lines.Dequeue();
            RefreshText();
        }
    }

    private void PickUpScrap(int amount)
    {
        PickUpScrapLine pickUpScrapLine = new PickUpScrapLine
        {
            Line = $"Picked up {amount} metal scrap",
            CreatedAt = Time.time
        };

        lines.Enqueue(pickUpScrapLine);

        RefreshText();
    }

    private void RefreshText()
    {
        text.text = "";
        int lineNum = 0;
        foreach (PickUpScrapLine line in lines)
        {
            if (lineNum == 0)
                text.text = line.Line;
            else
                text.text = line.Line + "\n" + text.text;

            lineNum++;
        }
    }
}

public struct PickUpScrapLine
{
    public string Line;
    public float CreatedAt;
}