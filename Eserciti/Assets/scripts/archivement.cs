using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if !UNITY_ANDROID
    using Steamworks;
#endif

public class archivement : MonoBehaviour
{
    void Start()
    {
#if !UNITY_ANDROID
        if(SteamManager.Initialized) {
            string name = SteamFriends.GetPersonaName();
            //Debug.Log(name);
        }
#endif
    }

    public void archivia_premio(string id_premio){
#if !UNITY_ANDROID
        if (!SteamManager.Initialized){print ("errore: Archive non pronto");return;}
        string name = SteamFriends.GetPersonaName();
        Debug.Log("nome giocatore: "+name+" - "+id_premio);
        SteamUserStats.SetAchievement(id_premio);
        SteamUserStats.StoreStats();
#endif
    }
}
