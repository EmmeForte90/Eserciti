using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class info_comuni : MonoBehaviour
{
    public Dictionary<string, string> lista_abilita_nome = new Dictionary<string, string>();
    public Dictionary<string, string> lista_abilita_descrizione = new Dictionary<string, string>();
    public Dictionary<string, Dictionary<int, int>> lista_abilita_cooldown = new Dictionary<string, Dictionary<int, int>>();

    public Dictionary<string, string> lista_razze_totale = new Dictionary<string, string>();
    public Dictionary<string, string> lista_razza_pupi_nome = new Dictionary<string, string>();
    public Dictionary<string, string> lista_pupi_descrizione = new Dictionary<string, string>();
    public Dictionary<string, string> lista_classi_nome = new Dictionary<string, string>();
    public Dictionary<string, float> lista_costo_unita_razza = new Dictionary<string, float>();
    // Start is called before the first frame update
    void Awake()
    {
        lista_costo_unita_razza.Add("formiche",1f);
        lista_costo_unita_razza.Add("mosche",0.7f);
        lista_costo_unita_razza.Add("api",1.5f);
        lista_costo_unita_razza.Add("ragnetti",1.5f);

        lista_classi_nome.Add("warrior","Warrior");
        lista_classi_nome.Add("arcer","Arcer");
        lista_classi_nome.Add("wizard","Wizard");

        //NB: I Nomi e le descrizioni, saranno sempre al plurale
        lista_razza_pupi_nome.Add("formiche","Ants");
        lista_razza_pupi_nome.Add("mosche","Flies");
        lista_razza_pupi_nome.Add("api","Bees");
        lista_razza_pupi_nome.Add("ragnetti","Spiders");

        lista_pupi_descrizione.Add("formiche","Most commons soldiers");
        lista_pupi_descrizione.Add("mosche","Cheap cost but frails");
        lista_pupi_descrizione.Add("api","Quando muoiono lanciano il loro pungiglione contro qualche nemico.");
        lista_pupi_descrizione.Add("ragnetti","Ogni volta che colpiscono, rallentano il bersaglio. Sono immuni da questo effetto da parte di altri ragni");


        //NB: Il singolare a destra è importante per definire la tipologia del pupo negli upgrade
        lista_razze_totale.Add("formiche","formica");
        lista_razze_totale.Add("mosche","mosca");
        lista_razze_totale.Add("api","ape");
        lista_razze_totale.Add("ragnetti","ragnetto");
        //lista_razze_totale.Add("cavallette","cavalletta");
        //lista_razze_totale.Add("scarabei","scarabeo");

        //La lista di tutte le abilita!
        lista_abilita_nome.Add("evoca_formiche","Summon Warrior Ants");
        lista_abilita_descrizione.Add("evoca_formiche","Summon 3 warrior ants for each level. Cooldown is really long and you can use only few times every match.");
        lista_abilita_cooldown.Add("evoca_formiche",new Dictionary<int, int>());
        lista_abilita_cooldown["evoca_formiche"].Add(1,30);
        lista_abilita_cooldown["evoca_formiche"].Add(2,30);
        lista_abilita_cooldown["evoca_formiche"].Add(3,30);

        lista_abilita_nome.Add("mosche_fastidiose","Swarm of flies");
        lista_abilita_descrizione.Add("mosche_fastidiose","Crea uno sciamo che si muove in maniera random che danneggia tutto ciò che trova al suo passaggio.");
        lista_abilita_cooldown.Add("mosche_fastidiose",new Dictionary<int, int>());
        lista_abilita_cooldown["mosche_fastidiose"].Add(1,20);
        lista_abilita_cooldown["mosche_fastidiose"].Add(2,20);
        lista_abilita_cooldown["mosche_fastidiose"].Add(3,20);

        lista_abilita_nome.Add("miele","Honey");
        lista_abilita_descrizione.Add("miele","Regenerate all friendly unities hitten in the area.");
        lista_abilita_cooldown.Add("miele",new Dictionary<int, int>());
        lista_abilita_cooldown["miele"].Add(1,20);
        lista_abilita_cooldown["miele"].Add(2,20);
        lista_abilita_cooldown["miele"].Add(3,20);

        lista_abilita_nome.Add("ragnatele","Spiderweb");
        lista_abilita_descrizione.Add("ragnatele","Create a spiderweb in the area. All enemy hitten can't move or permorf attacks until spiderweb end.");
        lista_abilita_cooldown.Add("ragnatele",new Dictionary<int, int>());
        lista_abilita_cooldown["ragnatele"].Add(1,20);
        lista_abilita_cooldown["ragnatele"].Add(2,20);
        lista_abilita_cooldown["ragnatele"].Add(3,20);

        lista_abilita_nome.Add("velocita","Speed");
        lista_abilita_descrizione.Add("velocita","Increase the speed of all friendly hitten unities in the area for 5 seconds.");
        lista_abilita_cooldown.Add("velocita",new Dictionary<int, int>());
        lista_abilita_cooldown["velocita"].Add(1,30);
        lista_abilita_cooldown["velocita"].Add(2,30);
        lista_abilita_cooldown["velocita"].Add(3,30);

        lista_abilita_nome.Add("armatura","Armour");
        lista_abilita_descrizione.Add("armatura","Increase the armour of all friendly hitten unities in the area for 5 seconds.");
        lista_abilita_cooldown.Add("armatura",new Dictionary<int, int>());
        lista_abilita_cooldown["armatura"].Add(1,30);
        lista_abilita_cooldown["armatura"].Add(2,30);
        lista_abilita_cooldown["armatura"].Add(3,30);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
