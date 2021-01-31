using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoxController : MonoBehaviour
{
    public static PickupEvent OnPickUpMetalScrap = new PickupEvent();

    private void OnTriggerEnter(Collider other)
    {
        transform.gameObject.SetActive(false);
        OnPickUpMetalScrap.Invoke(Random.Range(2, 5));
        GameObject.Find("Game Controller").GetComponent<GameController>().PickedUpBox();
    }
}

public class PickupEvent : UnityEvent<int> { }
