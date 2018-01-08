using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster GM;

    public GameObject player;

    public List<GameObject> shootingAreas;
    private int actualArea = 0;

    void Awake ()
    {
        if (GM != null)
            GameObject.Destroy(GM);
        else
            GM = this;

        player = GameObject.Find("Player");
	}

    public void NewShootingArea()
    {
        foreach (GameObject area in shootingAreas)
        {
            area.SetActive(false);
        }

        int areaSelector =  Random.Range(0, 6);

        while(areaSelector == actualArea)
            areaSelector = Random.Range(0, 6);
           
        actualArea = areaSelector;
        shootingAreas[areaSelector].SetActive(true);
    }
}
