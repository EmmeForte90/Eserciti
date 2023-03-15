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

    public Dictionary<string, string> lista_upgrade_perenni_nome = new Dictionary<string, string>();
    public Dictionary<string, int> lista_upgrade_perenni_max_level = new Dictionary<string, int>();
    public Dictionary<string, Dictionary<int, int>> lista_upgrade_perenni_costi = new Dictionary<string, Dictionary<int, int>>();
    public Dictionary<string, Dictionary<int, string>> lista_upgrade_perenni_descrizione = new Dictionary<string, Dictionary<int, string>>();

    private string string_temp;
    void Awake(){
        //traduerree la parte a destra di questo blocco
        lista_upgrade_perenni_nome.Add("proiettili_ignora_armatura","Ignora Armatura");
        lista_upgrade_perenni_nome.Add("proiettili_head_shot","Head Shot");
        lista_upgrade_perenni_nome.Add("proiettili_distanza","Max Distance");
        lista_upgrade_perenni_nome.Add("costi_pupi","Costo Esercito");
        lista_upgrade_perenni_nome.Add("costi_guadagno","Guadagno");
        lista_upgrade_perenni_nome.Add("costi_abilita","Costo Abilità");
        lista_upgrade_perenni_nome.Add("melee_velocita_attacco","Velocità d'attacco");
        lista_upgrade_perenni_nome.Add("melee_colpiti","More hits");
        lista_upgrade_perenni_nome.Add("melee_ignora_attacco","Ignore attacks");
        lista_upgrade_perenni_nome.Add("melee_dono_zanzare","Lifesteal");

        lista_upgrade_perenni_max_level.Add("proiettili_ignora_armatura",4);
        lista_upgrade_perenni_max_level.Add("proiettili_head_shot",5);
        lista_upgrade_perenni_max_level.Add("proiettili_distanza",3);
        lista_upgrade_perenni_max_level.Add("costi_pupi",3);
        lista_upgrade_perenni_max_level.Add("costi_guadagno",5);
        lista_upgrade_perenni_max_level.Add("costi_abilita",4);
        lista_upgrade_perenni_max_level.Add("melee_velocita_attacco",4);
        lista_upgrade_perenni_max_level.Add("melee_colpiti",2);
        lista_upgrade_perenni_max_level.Add("melee_ignora_attacco",3);
        lista_upgrade_perenni_max_level.Add("melee_dono_zanzare",3);

        //settiamo genericamente che ogni upgrade perenne ha un costo di 10 per livello che si vuole ottenere
        foreach(KeyValuePair<string,string> attachStat in lista_upgrade_perenni_nome){
            lista_upgrade_perenni_costi.Add(attachStat.Key,new Dictionary<int, int>());
            lista_upgrade_perenni_descrizione.Add(attachStat.Key,new Dictionary<int, string>());
            for (int i=1;i<=lista_upgrade_perenni_max_level[attachStat.Key];i++){
                lista_upgrade_perenni_costi[attachStat.Key].Add(i,(10*i));
                lista_upgrade_perenni_descrizione[attachStat.Key].Add(i,"Descrizione "+i);
            }
        }
        //traduerree la parte a destra di questi blocchi
        lista_upgrade_perenni_descrizione["proiettili_ignora_armatura"][1]="Le frecce ignorano l'armatura del bersaglio del 25%";
        lista_upgrade_perenni_descrizione["proiettili_ignora_armatura"][2]="Le frecce ignorano l'armatura del bersaglio del 50%";
        lista_upgrade_perenni_descrizione["proiettili_ignora_armatura"][3]="Le frecce ignorano l'armatura del bersaglio del 75%";
        lista_upgrade_perenni_descrizione["proiettili_ignora_armatura"][4]="Le frecce ignorano l'armatura del bersaglio del 100%";

        lista_upgrade_perenni_descrizione["proiettili_head_shot"][1]="Le frecce hanno l'1% di possibilità di uccedere il nemico con un colpo";
        lista_upgrade_perenni_descrizione["proiettili_head_shot"][2]="Le frecce hanno il 2% di possibilità di uccedere il nemico con un colpo";
        lista_upgrade_perenni_descrizione["proiettili_head_shot"][3]="Le frecce hanno il 3% di possibilità di uccedere il nemico con un colpo";
        lista_upgrade_perenni_descrizione["proiettili_head_shot"][4]="Le frecce hanno il 4% di possibilità di uccedere il nemico con un colpo";
        lista_upgrade_perenni_descrizione["proiettili_head_shot"][5]="Le frecce hanno il 5% di possibilità di uccedere il nemico con un colpo";

        lista_upgrade_perenni_descrizione["proiettili_distanza"][1]="Aumenta la distanza massima d'attacco a distanza di +1";
        lista_upgrade_perenni_descrizione["proiettili_distanza"][2]="Aumenta la distanza massima d'attacco a distanza di +2";
        lista_upgrade_perenni_descrizione["proiettili_distanza"][3]="Aumenta la distanza massima d'attacco a distanza di +3";

        lista_upgrade_perenni_descrizione["costi_pupi"][1]="I pupi costano 5 in meno";
        lista_upgrade_perenni_descrizione["costi_pupi"][2]="I pupi costano 10 in meno";
        lista_upgrade_perenni_descrizione["costi_pupi"][3]="I pupi costano 15 in meno";

        lista_upgrade_perenni_descrizione["costi_guadagno"][1]="Guadagni +10 a fine stage";
        lista_upgrade_perenni_descrizione["costi_guadagno"][2]="Guadagni +20 a fine stage";
        lista_upgrade_perenni_descrizione["costi_guadagno"][3]="Guadagni +30 a fine stage";
        lista_upgrade_perenni_descrizione["costi_guadagno"][4]="Guadagni +40 a fine stage";
        lista_upgrade_perenni_descrizione["costi_guadagno"][5]="Guadagni +50 a fine stage";

        lista_upgrade_perenni_descrizione["costi_abilita"][1]="Diminuisce il costo delle abilità di 5";
        lista_upgrade_perenni_descrizione["costi_abilita"][2]="Diminuisce il costo delle abilità di 10";
        lista_upgrade_perenni_descrizione["costi_abilita"][3]="Diminuisce il costo delle abilità di 15";
        lista_upgrade_perenni_descrizione["costi_abilita"][4]="Diminuisce il costo delle abilità di 20";

        lista_upgrade_perenni_descrizione["melee_velocita_attacco"][1]="Aumenta la velocità d'attacco del 5%";
        lista_upgrade_perenni_descrizione["melee_velocita_attacco"][2]="Aumenta la velocità d'attacco del 10%";
        lista_upgrade_perenni_descrizione["melee_velocita_attacco"][3]="Aumenta la velocità d'attacco del 15%";
        lista_upgrade_perenni_descrizione["melee_velocita_attacco"][4]="Aumenta la velocità d'attacco del 20%";

        lista_upgrade_perenni_descrizione["melee_colpiti"][1]="Gli attacchi melee possono colpire 2 pupetti in più contemporaneamente";
        lista_upgrade_perenni_descrizione["melee_colpiti"][2]="Gli attacchi melee possono colpire 3 pupetti in più contemporaneamente";

        lista_upgrade_perenni_descrizione["melee_ignora_attacco"][1]="I pupetti melee ignoreranno il primo colpo ricevuto";
        lista_upgrade_perenni_descrizione["melee_ignora_attacco"][2]="I pupetti melee ignoreranno il primo ed il secondo colpo ricevuti";
        lista_upgrade_perenni_descrizione["melee_ignora_attacco"][3]="I pupetti melee ignoreranno il primo, il secondo e il terzo colpo ricevuti";

        lista_upgrade_perenni_descrizione["melee_dono_zanzare"][1]="I pupetti melee guadagnano il 5% di salute della propria vitalità";
        lista_upgrade_perenni_descrizione["melee_dono_zanzare"][2]="I pupetti melee guadagnano l'8% di salute della propria vitalità";
        lista_upgrade_perenni_descrizione["melee_dono_zanzare"][3]="I pupetti melee guadagnano il 10% di salute della propria vitalità";

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

            //tradurre i seguenti blocchi (a destra)
            //Le descrizione
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

        //traduci nei blocchi le frasi lunghe (sempre a destra)
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
        lista_abilita_cooldown["resurrezione"].Add(1,20);
        lista_abilita_cooldown["resurrezione"].Add(2,30);
        lista_abilita_cooldown["resurrezione"].Add(3,40);

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
