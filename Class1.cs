using System.Collections.Generic;
using TheForest.Utils.Settings;
using UnityEngine;

namespace HarderMoreEnemies
{


    public class MoreEnemiesMain : spawnMutants
    {
        protected override void checkSpawn()
        {
            //GameSettings.Ai.Refresh();
            if (! enabled)
            {
                return;
            }
            if (TheForest.Utils.Scene.SceneTracker.allPlayers.Count == 0)
            {
                return;
            }
             allPlayers = new List<GameObject>(TheForest.Utils.Scene.SceneTracker.allPlayers);
             allPlayers.RemoveAll((GameObject o) => o == null);
            if ( allPlayers[0] == null)
            {
                return;
            }
            if ( allPlayers.Count > 1)
            {
                 allPlayers.Sort((GameObject c1, GameObject c2) => Vector3.Distance( transform.position, c1.transform.position).CompareTo(Vector3.Distance( transform.position, c2.transform.position)));
            }
            if (Clock.Day <  daysTillSpawn)
            {
                return;
            }
            if (!this)
            {
                return;
            }
             checkPlayerDist = Vector3.Distance( allPlayers[0].transform.position,  transform.position);
            if ( spawnInCave && TheForest.Utils.Scene.SceneTracker.allPlayersInCave.Count > 0 &&  checkPlayerDist < 130f && ! alreadySpawned)
            {
                for (int i = 0; i < 5; i++)
                {
                    StartCoroutine("doSpawn");
                }
                 alreadySpawned = true;
            }
            else if ( sinkholeSpawn &&  checkPlayerDist < 200f && ! alreadySpawned)
            {
                for (int i = 0; i < 6; i++)
                {
                    StartCoroutine("doSpawn");
                }
                 alreadySpawned = true;
            }
            else if (!TheForest.Utils.LocalPlayer.IsInCaves && ! spawnInCave && ! sinkholeSpawn)
            {
                
                 StartCoroutine("doSpawn");
                
                 CancelInvoke("checkSpawn");
            }
            float num = 160f;
            if ( sinkholeSpawn)
            {
                num = 225f;
            }
            if (( spawnInCave ||  sinkholeSpawn) &&  checkPlayerDist > num &&  alreadySpawned)
            {
                bool flag = false;
                if (TheForest.Utils.Scene.SceneTracker.allPlayersInCave.Count > 0 &&  spawnInCave)
                {
                    flag = true;
                }
                if ( sinkholeSpawn)
                {
                    flag = true;
                }
                if (flag)
                {
                    bool flag2 = false;
                    foreach (GameObject gameObject in  allMembers)
                    {
                        if (gameObject && gameObject.activeSelf && Vector3.Distance(gameObject.transform.position,  allPlayers[0].transform.position) < 160f)
                        {
                            flag2 = true;
                        }
                    }
                    if (!flag2)
                    {
                         StartCoroutine("despawnAll");
                         alreadySpawned = false;
                    }
                }
            }
        }
    }
    //class MaxMutantIncrease : mutantController
    //{
    //    protected override void Start()
    //    {
    //        base.Start();
    //        maxActiveMutants *= 10;
    //        currentMaxActiveMutants *= 10;
    //        maxActiveSpawners *= 10;
    //    }
    //    protected override void setDayConditions()
    //    {
    //        base.setDayConditions();
    //        maxActiveMutants *= 10;
    //        currentMaxActiveMutants *= 10;
    //        maxActiveSpawners *= 10;
    //    }
    //}
}

