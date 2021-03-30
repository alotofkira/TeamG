//------------------------------------------------------------------------------
//
// File Name:	RandomSpawner.cs
// Author(s):	Jeremy Kings (j.kings) - Unity Project
//              Nathan Mueller - original Zero Engine project
//              Alex Dzius - Tech Lead on Team G in Endless Runner Project
// Project:		Endless Runner
// Course:		WANIC VGP
//
// Copyright © 2021 DigiPen (USA) Corporation.
//
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    // Object spawn timing
    public Vector2 SpawnInterval = new Vector2(0, 6); // likely increase immensly
    // Whether to align object spawn positions to grid
    public bool AlignSpawnPosition = false;

    // Object prefabs and spawn chances
    public GameObject Object1Prefab = null;
    public int Object1SpawnChance = 100;
    public GameObject Object2Prefab = null;
    public int Object2SpawnChance = 100;
    public GameObject Object3Prefab = null;
    public int Object3SpawnChance = 100;
    public GameObject Object4Prefab = null;
    public int Object4SpawnChance = 1;
    public GameObject Object5Prefab = null;
    public int Object5SpawnChance = 100;
    public GameObject Object6Prefab = null;
    public int Object6SpawnChance = 100;

    // Private variables
    private int totalChance = 0;
    private float timeTilNextSpawn = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        totalChance = Object1SpawnChance + Object2SpawnChance + Object3SpawnChance
            + Object4SpawnChance + Object5SpawnChance + Object6SpawnChance;
    }

    // Update is called once per frame
    void Update()
    {
        timeTilNextSpawn -= Time.deltaTime;

        if(timeTilNextSpawn <= 0.0f)
        {
            timeTilNextSpawn += Random.Range(SpawnInterval.x, SpawnInterval.y);
            SpawnRandomObject();
            // A frequency where at about 10,000m the game would become exponentially hard
            // Remove a 0 to have said situation be the case at 1000m
            SpawnInterval.y = 1.69f - PlayerSaveData.DistanceRun / 10000; 
        }
    }

    void SpawnRandomObject()
    {
        // Roll die to determine which object to spawn
        int dieRoll = Random.Range(1, totalChance + 1);

        // Determine spawn position [[[BELOW IS WITH RANDOM POSITION, NOT NEEDED FOR ZOMBIES]
        /* Vector3 spawnPosition = transform.position 
             + new Vector3(0, Random.Range(0, transform.localScale.y), 0);*/
        Vector3 spawnPosition = transform.position;
        GameObject spawnedObject = null;

        // Align to grid
        if(AlignSpawnPosition)
        {
            spawnPosition.x = Mathf.Floor(spawnPosition.x);
            spawnPosition.y = Mathf.Floor(spawnPosition.y) + 0.5f;
        }
        // no more holes nor duplicates
        RaycastHit2D hitdown = Physics2D.Raycast(new Vector2(spawnPosition.x, spawnPosition.y), new Vector2(0, -1f), 0.8f);
        RaycastHit2D duplicate = Physics2D.Raycast(new Vector2(spawnPosition.x, spawnPosition.y), new Vector2(-2f, 0), 1f);
        // weird edgecase but it is needed to ensure that we dont have things hanging off the level 2 cliffs
        RaycastHit2D leftedge = Physics2D.Raycast(new Vector2(spawnPosition.x, spawnPosition.y), new Vector2(0.5f, -1f), 1f);
        RaycastHit2D rightedge = Physics2D.Raycast(new Vector2(spawnPosition.x, spawnPosition.y), new Vector2(-0.5f, -1f), 1f);
        if (!hitdown.collider || duplicate.collider || !leftedge.collider || !rightedge.collider )
        {
            return;
        }
        // Create object based on die roll
        if (dieRoll <= Object1SpawnChance)
        {
            spawnedObject = Instantiate(Object1Prefab);
        }
        else if(dieRoll <= Object2SpawnChance + Object1SpawnChance)
        {
            spawnedObject = Instantiate(Object2Prefab);
        }
        else if (dieRoll <= Object3SpawnChance + Object2SpawnChance 
            + Object1SpawnChance)
        {
            spawnedObject = Instantiate(Object3Prefab);
        }
        else if (dieRoll <= Object4SpawnChance + Object3SpawnChance 
            + Object2SpawnChance + Object1SpawnChance)
        {
            //spawnedObject = Instantiate(Object4Prefab);
            // Object 4 has been chosen to correspond to acid rain.
            if(!AcidRainController.RainInProgress)
                AcidRainController.announceRain = true;
        }
        else if (dieRoll <= Object5SpawnChance + Object4SpawnChance 
            + Object3SpawnChance + Object2SpawnChance + Object1SpawnChance)
        {
            spawnedObject = Instantiate(Object5Prefab);
        }
        else if (dieRoll <= totalChance)
        {
            spawnedObject = Instantiate(Object6Prefab);
        }

        // Set object position, and adjust certain things to be in the right place. Will likely be removed once assets flow in
        if (!spawnedObject.name.Contains("Zombie"))
        {
            spawnPosition.y -= 0.23f;
        }
        spawnedObject.transform.position = spawnPosition;
    }
}
