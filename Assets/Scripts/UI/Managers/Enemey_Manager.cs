using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using UnityEngine.AddressableAssets;
using FishNet.Object.Synchronizing;
public class Enemey_Manager : NetworkBehaviour
{
    float ticktimer;
    int tick;
    List<GameObject> enemies;
    int wave = 0;
    int spawning = 2;
    int lasttick;
    GameObject enemy;
    AudioSource Audio;
    public AudioClip newwave;

    [SyncVar]
    public bool start = false;

    public static Enemey_Manager Instance { get; private set; }

    public override void OnStartNetwork() //set pawn as reference when spawned
    {
        base.OnStartNetwork();

        

    enemy = Addressables.LoadAssetAsync<GameObject>("Enemi").WaitForCompletion();
        tick = 0;
        lasttick = 0;
        enemies = new List<GameObject>();

        Audio = GetComponent<AudioSource>();
    }


    private void Awake()
    {
        Instance = this;
    }



        public void desself(GameObject sel)
    {
        enemies.Remove(sel);
    }

    void FixedUpdate()
    {
        if (start == false)
            return;


        ticktimer += Time.deltaTime;

        if (ticktimer > 1)
        {
            ticktimer -= 1;
            tick++;
            Debug.Log("tick" + tick);
            Debug.Log("lastick" + lasttick);
            

                if (tick >= lasttick + 8)
                {
                spawning += 2;

                if (IsServer)
                {


                    List<Vector3> spawns = new List<Vector3>();

                    for (int i = 0; i < spawning; i++)
                    {
                        spawns.Add(new Vector3(Random.Range(-30f, 30f), Random.Range(-30f, 30f), Random.Range(0, 0)));
                    }

                    spawnWaveClients(spawns);
                }
                    lasttick = tick;

                }
            

            if (enemies.Count > 0)
            {

                for (int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].GetComponent<Enemy_Input>().FindClosestPlayer();
                }
            }

        }
    }

    [ObserversRpc]
    void spawnWaveClients(List<Vector3> spawns)
    {
        wave += 2;
        
        
        for (int i = 0; i < spawning; i++)
        {
            //Vector3 myVector = new Vector3(Random.Range(-30f, 30f), Random.Range(-30f, 30f), Random.Range(0, 0));
            //spawnWaveClients(myVector);
            GameObject enemyInstantce = Instantiate(enemy, spawns[i], Quaternion.identity);
            Spawn(enemyInstantce);
            enemies.Add(enemyInstantce);
        }

        Audio.Play(0);
    }

    [ServerRpc]
    void SpawnWave()
    {
        Debug.Log("spawning" + wave);
        

    }
    

}
