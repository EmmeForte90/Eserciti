using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class info_comuni : MonoBehaviour
{
    public Dictionary<string, string> lista_abilita_nome = new Dictionary<string, string>();
    public Dictionary<string, string> lista_abilita_descrizione = new Dictionary<string, string>();
    public Dictionary<string, Dictionary<int, int>> lista_abilita_cooldown = new Dictionary<string, Dictionary<int, int>>();
    public Dictionary<string, Dictionary<int, int>> lista_abilita_cooldown_partenza = new Dictionary<string, Dictionary<int, int>>();

    public Dictionary<string, string> lista_razze_totale = new Dictionary<string, string>();
    public Dictionary<string, string> lista_razza_pupi_nome = new Dictionary<string, string>();
    public Dictionary<string, string> lista_pupi_descrizione = new Dictionary<string, string>();
    public Dictionary<string, string> lista_classi_nome = new Dictionary<string, string>();
    public Dictionary<string, int> lista_costo_unita_razza = new Dictionary<string, int>();
    // Start is called before the first frame update

    private string string_temp;
    void Awake(){
        lista_classi_nome.Add("warrior","Warrior");
        lista_classi_nome.Add("arcer","Arcer");
        lista_classi_nome.Add("wizard","Wizard");
        
        //praticamente essendo di tre livelli diversi, tanto vale fare direttamente così; Come fossero tre razze diverse a seconda del livello.
        //Domani faremo la stessa cosa per le cavalcature
        for (int i=1;i<=3;i++){
            //NB: Il singolare a destra è importante per definire la tipologia del pupo negli upgrade
            lista_razze_totale.Add("formiche_"+i,"formica");
            lista_razze_totale.Add("mosche_"+i,"mosca");
            lista_razze_totale.Add("api_"+i,"ape");
            lista_razze_totale.Add("ragnetti_"+i,"ragnetto");
            //lista_razze_totale.Add("cavallette_"+i,"cavalletta");
            //lista_razze_totale.Add("scarabei_"+i,"scarabeo");

            //Le descrizioni
            lista_pupi_descrizione.Add("formiche_"+i,"Most commons soldiers");
            lista_pupi_descrizione.Add("mosche_"+i,"Cheap cost but frails");
            lista_pupi_descrizione.Add("api_"+i,"Quando muoiono lanciano il loro pungiglione contro qualche nemico.");
            lista_pupi_descrizione.Add("ragnetti_"+i,"Ogni volta che colpiscono, rallentano il bersaglio. Sono immuni da questo effetto da parte di altri ragni");

            //NB: I Nomi e le descrizioni, saranno sempre al plurale
            lista_razza_pupi_nome.Add("formiche_"+i,"Ants");     
            lista_razza_pupi_nome.Add("mosche_"+i,"Flies");      
            lista_razza_pupi_nome.Add("api_"+i,"Bees");          
            lista_razza_pupi_nome.Add("ragnetti_"+i,"Spiders");  

            //i costi dei vari pupetti
            lista_costo_unita_razza.Add("formiche_"+i,30*i);
            lista_costo_unita_razza.Add("mosche_"+i,30*i);
            lista_costo_unita_razza.Add("api_"+i,30*i);
            lista_costo_unita_razza.Add("ragnetti_"+i,30*i);
        }

        //La lista di tutte le abilita!
        lista_abilita_nome.Add("evoca_formiche","Summon Warrior Ants");
        lista_abilita_descrizione.Add("evoca_formiche","Summon 2 warrior ants for each level. Cooldown is really long and you can use only few times every match.");
        lista_abilita_cooldown.Add("evoca_formiche",new Dictionary<int, int>());
        lista_abilita_cooldown["evoca_formiche"].Add(1,18);
        lista_abilita_cooldown["evoca_formiche"].Add(2,18);
        lista_abilita_cooldown["evoca_formiche"].Add(3,18);

        lista_abilita_nome.Add("mosche_fastidiose","Swarm of flies");
        lista_abilita_descrizione.Add("mosche_fastidiose","Crea uno sciamo che si muove in maniera random che danneggia tutto ciò che trova al suo passaggio. Mosche e zanzare sono immuni.");
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
        lista_abilita_descrizione.Add("ragnatele","Create a spiderweb in the area. All enemy hitten can't move or permorf attacks until spiderweb end. Spiders are immune.");
        lista_abilita_cooldown.Add("ragnatele",new Dictionary<int, int>());
        lista_abilita_cooldown["ragnatele"].Add(1,20);
        lista_abilita_cooldown["ragnatele"].Add(2,20);
        lista_abilita_cooldown["ragnatele"].Add(3,20);

        lista_abilita_nome.Add("velocita","Speed");
        lista_abilita_descrizione.Add("velocita","Increase the speed of all friendly hitten unities in the area for 5 seconds. It doesn't work on webbed targets.");
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

        lista_abilita_nome.Add("zombie","Zombie");
        lista_abilita_descrizione.Add("zombie","Evoca degli insetti zombie casuali. Al primo livello ne evoca 5. Al secondo ne evoca 8. Al terzo ne evoca 10");
        lista_abilita_cooldown.Add("zombie",new Dictionary<int, int>());
        lista_abilita_cooldown["zombie"].Add(1,30);
        lista_abilita_cooldown["zombie"].Add(2,30);
        lista_abilita_cooldown["zombie"].Add(3,30);

        lista_abilita_nome.Add("resurrezione","resurrezione");
        lista_abilita_descrizione.Add("resurrezione","Rievoca un pupetto morto alleato. Il livello del pupo evocato è uguale a quello del livello dell'abilità. Non ha effetto se non ci sono stati pupetti morti.");
        lista_abilita_cooldown.Add("resurrezione",new Dictionary<int, int>());
        lista_abilita_cooldown["resurrezione"].Add(1,4);   //40, 50 e 60
        lista_abilita_cooldown["resurrezione"].Add(2,5);
        lista_abilita_cooldown["resurrezione"].Add(3,6);

        //settiamo genericamente che ogni abilità ha un cooldown di partenza uguale alla metà di un normale cooldown
        foreach(KeyValuePair<string,Dictionary<int,int>> attachStat in lista_abilita_cooldown){
            lista_abilita_cooldown_partenza.Add(attachStat.Key,new Dictionary<int, int>());
            for (int i=1;i<=3;i++){
                lista_abilita_cooldown_partenza[attachStat.Key].Add(i,attachStat.Value[i]/2);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
