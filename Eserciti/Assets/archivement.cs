using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class archivement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(SteamManager.Initialized) {
            string name = SteamFriends.GetPersonaName();
            Debug.Log(name);
        }
        /*
        try{
            Steamworks.SteamClient.Init(2369250);
            Debug.Log("My Steam State: " + SteamClient.State);
            Debug.Log("My Steam is valid: " + SteamClient.IsValid );
            Debug.Log("My Steam is loggedon: " + SteamClient.IsLoggedOn );
        }
        catch (System.Exception e){
            print (e);
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            archivia_premio("end_hero_regina_formica_nera");
        }
    }

    public void archivia_premio(string id_premio){
        if (!SteamManager.Initialized){print ("errore: Archive non pronto");return;}
        string name = SteamFriends.GetPersonaName();
        Debug.Log("nome: "+name);
        SteamUserStats.SetAchievement(id_premio);
        SteamUserStats.StoreStats();
    }
}
