using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class info_comuni : MonoBehaviour
{
    public Dictionary<string, string> lista_abilita_nome = new Dictionary<string, string>();
    public Dictionary<string, string> lista_abilita_descrizione = new Dictionary<string, string>();
    public Dictionary<string, Dictionary<int, int>> lista_abilita_cooldown = new Dictionary<string, Dictionary<int, int>>();
    // Start is called before the first frame update
    void Awake()
    {
        lista_abilita_nome.Add("evoca_formiche","Summon Warrior Ants");
        lista_abilita_descrizione.Add("evoca_formiche","Summon 5 warrior ants for each level. Cooldown is really long and you can use only few times every match.");
        lista_abilita_cooldown.Add("evoca_formiche",new Dictionary<int, int>());
        lista_abilita_cooldown["evoca_formice"].Add(1,30);
        lista_abilita_cooldown["evoca_formice"].Add(2,30);
        lista_abilita_cooldown["evoca_formice"].Add(3,30);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
