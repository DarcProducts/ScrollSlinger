using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int PlotsTillSubscribers;
    [SerializeField] LandPlotGenerator landPlotGenerator;
    [SerializeField, Range(0f, 1f)] float subscriberChance;
    [SerializeField] int plotsTillSubscribers;
    [SerializeField] float horseSpeed;
    [SerializeField] GlobalBool StartMovingLands;
    [SerializeField] GlobalFloat LandMoveSpeed;
    [SerializeField] float speedResetTime;

    void Start()
    {
        Subscriptions.chanceForSubscriber = subscriberChance;
        StartMovingLands.Value = true;
        LandMoveSpeed.Value = 10;
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(speedResetTime);
        LandMoveSpeed.Value = horseSpeed;
    }

    void OnValidate() => PlotsTillSubscribers = plotsTillSubscribers; 
}
