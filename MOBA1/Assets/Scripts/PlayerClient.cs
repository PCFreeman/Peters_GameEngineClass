using UnityEngine;
using UnityEngine.Networking;

public class PlayerClient : NetworkBehaviour
{
    public static PlayerClient local = null;

    void Start()
    {
        if (isLocalPlayer)
        {
            local = this;
            GameManager.Instance.CmdRegisterPlayer("close","far",connectionToClient.connectionId);
        }
    }

   
}
