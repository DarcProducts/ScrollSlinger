using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] LandPlotGenerator landPlotGenerator;
    [SerializeField, Range(0f, 1f)] float subscriberChance;
    [SerializeField] float horseSpeed;
    [SerializeField] GlobalBool StartMovingLands;
    [SerializeField] GlobalFloat LandMoveSpeed;

    void Start()
    {
        Subscriptions.chanceForSubscriber = 0;
        StartMovingLands.Value = true;
        LandMoveSpeed.Value = 10;
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(5);
        Subscriptions.chanceForSubscriber = subscriberChance;
        LandMoveSpeed.Value = horseSpeed;
    }
}
