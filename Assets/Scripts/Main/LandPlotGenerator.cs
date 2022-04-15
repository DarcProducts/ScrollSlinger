using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class LandPlotGenerator : MonoBehaviour
{
    [SerializeField] GlobalInt TotalPlotsGenerated;
    [SerializeField] int maxLandPlots;
    [SerializeField] bool spawnLandAtStart;
    [SerializeField] int spawnDistance;
    [SerializeField] ObjectPool landPlot;
    [SerializeField] GlobalFloat LandMaxZLocation;
    [SerializeField] GlobalBool StartMovingLand;
    readonly List<LandPlot> landPlots = new List<LandPlot>();
    float _nextZSpawnLocation;

    void Start()
    {
        if (spawnLandAtStart)
            InitializeLandPlots();
    }

    [ContextMenu("Spawn Land Plots"), Button]
    public void InitializeLandPlots()
    {
        for (int i = 0; i < maxLandPlots; i++)
        {
            GameObject lP = landPlot.GetObject();
            landPlots.Add(lP.GetComponent<LandPlot>());
            if (i - 1 < 0)
                lP.transform.position = Vector3.zero;
            else
            {
                _nextZSpawnLocation = landPlots[i - 1].transform.position.z + spawnDistance;
                lP.transform.position = new Vector3(0, 0, _nextZSpawnLocation);
                _nextZSpawnLocation += LandMaxZLocation.Value + spawnDistance;
            }
            lP.SetActive(true);
            TotalPlotsGenerated.Value++;
        }
    }

    public void ResetLandPlot()
    {
        GameObject newPlot = landPlot.GetObject();
        newPlot.transform.localPosition = new Vector3(0, 0, _nextZSpawnLocation);
        newPlot.SetActive(true);
    }
}