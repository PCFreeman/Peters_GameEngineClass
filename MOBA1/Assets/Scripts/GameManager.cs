using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour
{
    [System.Serializable]
    public struct TeamColor
    {
       public Color slot1;
       public Color slot2;
       public Color slot3;
       public Color slot4;
       public Color slot5;
    }

    public TeamColor teamSunny;
    public TeamColor teamGloomy;

    [System.Serializable]
    public struct PlayerInfo
    {
        public string playerName;
        public string heroSelected;
        public NetworkConnection clientConection;
    }
    [SyncVar]
    List<PlayerInfo> sunnyPlayers = new List<PlayerInfo>();
    List<NetworkConnection> sunnConnection = new List<NetworkConnection>();
    [SyncVar]
    List<PlayerInfo> gloomyPlayers = new List<PlayerInfo>();
    List<NetworkConnection> gloomyConnection = new List<NetworkConnection>();
    [SyncVar]
    private bool gameStarted = false;

    public static GameManager Instance;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }

    }

    public void StartMatch()
    {
        gameStarted = true;

     for(int i =0;i<sunnyPlayers.Count;++i)
     {
        var playerInfo = sunnyPlayers[i];
        var spawnPoint = GameObject.Find("Sunny1");
        var heroPrefab = HeroCataloge.Instance.FindHeroByName(playerInfo.heroSelected);
        var hero = Instantiate(heroPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        NetworkServer.SpawnWithClientAuthority(hero, playerInfo.clientConection);
     }

        for (int i = 0; i < gloomyPlayers.Count; ++i)
        {
            var playerInfo = gloomyPlayers[i];
            var spawnPoint = GameObject.Find("Gloomy1");
            var heroPrefab = HeroCataloge.Instance.FindHeroByName(playerInfo.heroSelected);
            var hero = Instantiate(heroPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
            NetworkServer.SpawnWithClientAuthority(hero, playerInfo.clientConection);
        }
    }
    void OnGUI()
    {
        GUI.BeginGroup(new Rect(Screen.width - 200, 0, 200, 400));
        if(NetworkServer.active&&sunnyPlayers.Count>0)
        {
            if (sunnyPlayers.Count > 0)
            {
                if (GUILayout.Button("Star Match"))
                {
                    StartMatch();
                }
            }
        }
      

        GUILayout.Label("Team Sunny");
        foreach(var player in sunnyPlayers)
        {
            GUILayout.Label(player.playerName + " : " + player.heroSelected);
        }

        GUILayout.Label("Team Gloomy");
        foreach (var player in gloomyPlayers)
        {
            GUILayout.Label(player.playerName + " : " + player.heroSelected);
        }

        GUI.EndGroup();
    }

    public bool IsGameStarted()
    {
        return gameStarted;
    }

    [Command]
    public void CmdRegisterPlayer(string playerName,string heroSelected, int connectionId)
    {
        if(sunnyPlayers.Count >= 5&& gloomyPlayers.Count >= 5)
        {
            return;
        }

        NetworkConnection clientConnection = null;
        foreach(var connection in NetworkServer.connections)
        {
            if(connection.connectionId==connectionId)
            {
                clientConnection = connection;
                break;
            }
        }

        if(sunnyPlayers.Count <= gloomyPlayers.Count)
        {
            sunnyPlayers.Add(new PlayerInfo { playerName = playerName, heroSelected = heroSelected,clientConection = connectionToClient });
        }
        else
        {
            gloomyPlayers.Add(new PlayerInfo { playerName = playerName, heroSelected = heroSelected, clientConection = connectionToClient });
        }

        return;
    }

    [Command]
    void Cmd_SpawnHero()
    {
        //var spawnPoint = GameObject.Find("Sunny1");
        //hero = Instantiate(heroPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        //NetworkServer.SpawnWithClientAuthority(hero, connectionToClient);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
