using UnityEngine;
using UnityEngine.Networking;

public class PlayerClient : NetworkBehaviour
{
    public GameObject heroPrefab;

    [HideInInspector]
    public GameObject hero;

    void Start()
    {
        if (isLocalPlayer)
        {
            Cmd_SpawnHero();
        }
    }

    [Command]
    void Cmd_SpawnHero()
    {
        var spawnPoint = GameObject.Find("Sunny1");
        hero = Instantiate(heroPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        NetworkServer.SpawnWithClientAuthority(hero, connectionToClient);
    }
}
