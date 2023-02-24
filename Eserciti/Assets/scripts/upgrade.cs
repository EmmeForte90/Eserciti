using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using System.Text;
using System.Xml; //Needed for XML functionality
using System.IO;
using System.Linq;

public class upgrade : MonoBehaviour
{   
    public info_comuni info_comuni;
    public GameObject pannello_scelta_premio;
    public GameObject pannello_upgrade;

    public GameObject blocco_unita_liv_1;
    private Dictionary<string, TMPro.TextMeshProUGUI> lista_txt_unita_esercito = new Dictionary<string, TMPro.TextMeshProUGUI>(); 

    public Image img_premio_upgrade_1;
    public TMPro.TextMeshProUGUI titolo_premio_upgrade_1;
    public TMPro.TextMeshProUGUI monete_premio_upgrade_1;
    public TMPro.TextMeshProUGUI descrizione_premio_upgrade_1;
    public Image img_premio_upgrade_2;
    public TMPro.TextMeshProUGUI titolo_premio_upgrade_2;
    public TMPro.TextMeshProUGUI monete_premio_upgrade_2;
    public TMPro.TextMeshProUGUI descrizione_premio_upgrade_2;
    public Image img_premio_upgrade_3;
    public TMPro.TextMeshProUGUI titolo_premio_upgrade_3;
    public TMPro.TextMeshProUGUI monete_premio_upgrade_3;
    public TMPro.TextMeshProUGUI descrizione_premio_upgrade_3;
    private Dictionary<int, string> lista_premi_settati = new Dictionary<int, string>();
    private Dictionary<int, int> lista_premi_settati_num_pupi = new Dictionary<int, int>();

    public TMPro.TextMeshProUGUI txt_next_stage;
    public TMPro.TextMeshProUGUI txt_denaro;
    private int denaro;
    private string path_xml;
    private string id_hero;
    private int num_ondata;
    private int i;
    private int num_max_abilita=6;

    //lista_dei costi delle varie abilità
    public TMPro.TextMeshProUGUI txt_u_c_melee_damage;
    public TMPro.TextMeshProUGUI txt_u_c_distance_damage;
    public TMPro.TextMeshProUGUI txt_u_c_spell_damage;
    public TMPro.TextMeshProUGUI txt_u_c_health;
    public TMPro.TextMeshProUGUI txt_u_c_hero_damage;
    public TMPro.TextMeshProUGUI txt_u_c_hero_cooldown;
    public TMPro.TextMeshProUGUI txt_u_c_random_unity;
    public TMPro.TextMeshProUGUI txt_u_c_random_spell;
    public TMPro.TextMeshProUGUI txt_u_c_food;

    //lista bottoni "upgrade" delle abilità
    public Button B_u_b_melee_damage;
    public Button B_u_b_distance_damage;
    public Button B_u_b_spell_damage;
    public Button B_u_b_health;
    public Button B_u_b_hero_damage;
    public Button B_u_b_hero_cooldown;
    public Button B_u_b_random_unity;
    public Button B_u_b_random_spell;
    public Button B_u_b_food;

    //lista descrizioni delle abilità (cambiano a seconda del livello)
    public TMPro.TextMeshProUGUI txt_u_d_melee_damage;
    public TMPro.TextMeshProUGUI txt_u_d_distance_damage;
    public TMPro.TextMeshProUGUI txt_u_d_spell_damage;
    public TMPro.TextMeshProUGUI txt_u_d_health;
    public TMPro.TextMeshProUGUI txt_u_d_hero_damage;
    public TMPro.TextMeshProUGUI txt_u_d_hero_cooldown;
    public TMPro.TextMeshProUGUI txt_u_d_random_unity;
    public TMPro.TextMeshProUGUI txt_u_d_random_spell;
    public TMPro.TextMeshProUGUI txt_u_d_food;

    private Dictionary<string, int> lista_pupetti = new Dictionary<string, int>(); 
    private Dictionary<string, int> lista_abilita = new Dictionary<string, int>(); 

    private Dictionary<string, TMPro.TextMeshProUGUI> lista_txt_upgrade_costi = new Dictionary<string, TMPro.TextMeshProUGUI>();
    private Dictionary<string, Button> lista_B_upgrade_bottoni = new Dictionary<string, Button>();
    private Dictionary<string, TMPro.TextMeshProUGUI> lista_txt_upgrade_descrizione = new Dictionary<string, TMPro.TextMeshProUGUI>();

    private Dictionary<string, int> livelli_attuali_upgrade = new Dictionary<string, int>();
    private Dictionary<string, int> lista_random = new Dictionary<string, int>();

    public Dictionary<string, int> lista_razze_sbloccate = new Dictionary<string, int>();

    // Start is called before the first frame update
    void Start(){
        get_all_blocchi_testo_unita();
        riempi_blocchi_testo_unita();
        
        path_xml=Application.persistentDataPath + "/game_c.xml";

        lista_txt_upgrade_costi.Add("melee_damage",txt_u_c_melee_damage);
        lista_txt_upgrade_costi.Add("distance_damage",txt_u_c_distance_damage);
        lista_txt_upgrade_costi.Add("spell_damage",txt_u_c_spell_damage);
        lista_txt_upgrade_costi.Add("health",txt_u_c_health);
        lista_txt_upgrade_costi.Add("hero_damage",txt_u_c_hero_damage);
        lista_txt_upgrade_costi.Add("hero_cooldown",txt_u_c_hero_cooldown);
        lista_txt_upgrade_costi.Add("random_unity",txt_u_c_random_unity);
        lista_txt_upgrade_costi.Add("random_spell",txt_u_c_random_spell);
        lista_txt_upgrade_costi.Add("food",txt_u_c_food);

        lista_B_upgrade_bottoni.Add("melee_damage",B_u_b_melee_damage);
        lista_B_upgrade_bottoni.Add("distance_damage",B_u_b_distance_damage);
        lista_B_upgrade_bottoni.Add("spell_damage",B_u_b_spell_damage);
        lista_B_upgrade_bottoni.Add("health",B_u_b_health);
        lista_B_upgrade_bottoni.Add("hero_damage",B_u_b_hero_damage);
        lista_B_upgrade_bottoni.Add("hero_cooldown",B_u_b_hero_cooldown);
        lista_B_upgrade_bottoni.Add("random_unity",B_u_b_random_unity);
        lista_B_upgrade_bottoni.Add("random_spell",B_u_b_random_spell);
        lista_B_upgrade_bottoni.Add("food",B_u_b_food);

        lista_txt_upgrade_descrizione.Add("melee_damage",txt_u_d_melee_damage);
        lista_txt_upgrade_descrizione.Add("distance_damage",txt_u_d_distance_damage);
        lista_txt_upgrade_descrizione.Add("spell_damage",txt_u_d_spell_damage);
        lista_txt_upgrade_descrizione.Add("health",txt_u_d_health);
        lista_txt_upgrade_descrizione.Add("hero_damage",txt_u_d_hero_damage);
        lista_txt_upgrade_descrizione.Add("hero_cooldown",txt_u_d_hero_cooldown);
        lista_txt_upgrade_descrizione.Add("random_unity",txt_u_d_random_unity);
        lista_txt_upgrade_descrizione.Add("random_spell",txt_u_d_random_spell);
        lista_txt_upgrade_descrizione.Add("food",txt_u_d_food);

        foreach(KeyValuePair<string,Button> attachStat in lista_B_upgrade_bottoni){
            livelli_attuali_upgrade.Add(attachStat.Key,0);
        }

        denaro=10000;   //è un fake perchè tanto andrà a prenderlo da xml...puoi cancellare quando vuoi

        //da quì, andremo a prendere da dove si trovano, tutte le altre informazioni
        carica_info_xml();  //effettivamente e da quì che carichiamo le info importanti...

        txt_next_stage.SetText("Next: "+num_ondata);

        //settaggi iniziali
        foreach(KeyValuePair<string,Button> attachStat in lista_B_upgrade_bottoni){
            check_abilita(attachStat.Key);
        }
        txt_denaro.SetText(denaro.ToString());

        pannello_scelta_premio.SetActive(true);
        pannello_upgrade.SetActive(false);

        get_premi_upgrade();
        //riempi_blocchi_testo_unita();     //viene già fatto al momento della scelta del premio...
    }

    private void get_all_blocchi_testo_unita(){
        string string_temp="";
        foreach (Transform child in blocco_unita_liv_1.transform.GetComponentsInChildren<Transform>()) {
            print ("blocco: "+child.name);
            if (child.name.Contains("num_")){
                string_temp=child.name.Replace("num_","");
                lista_txt_unita_esercito.Add(string_temp,child.GetComponent<TMPro.TextMeshProUGUI>());
                lista_txt_unita_esercito[string_temp].SetText("0");
            }
        }
    }

    private void riempi_blocchi_testo_unita(){
        foreach(KeyValuePair<string,int> attachStat in lista_pupetti){
            lista_txt_unita_esercito[attachStat.Key].SetText(attachStat.Value.ToString());
        }
    }

    public void click_upgrade_number(int numero){
        string premio=lista_premi_settati[numero];
        string tipo;
        string[] splitArray;

        int monete=0;

        splitArray=premio.Split(char.Parse("-"));
        tipo=splitArray[0];
        switch (tipo){
            case "pupo":{
                string razza_pupo = splitArray[1];
                int num_pupi=lista_premi_settati_num_pupi[numero];
                string classe_pupo = splitArray[2];
                int livello_pupo = int.Parse(splitArray[3]);
                monete=(int)(info_comuni.lista_costo_unita_razza[razza_pupo]*num_pupi);
                monete=30-monete;

                string pupo_xml=info_comuni.lista_razze_totale[razza_pupo]+"_"+classe_pupo;

                if (!lista_pupetti.ContainsKey(pupo_xml)){lista_pupetti.Add(pupo_xml,0);}
                lista_pupetti[pupo_xml]+=num_pupi;
                break;
            }
            case "nrazza":{
                int livello_razza = int.Parse(splitArray[2]);
                string razza_pupo = splitArray[1];
                lista_razze_sbloccate.Add(razza_pupo,livello_razza);
                break;
            }
            case "abilita":{
                string abilita=splitArray[1];
                int livello_abilita = int.Parse(splitArray[2]);
                if (livello_abilita==1){lista_abilita.Add(abilita,1);}
                else {lista_abilita[abilita]++;}
                break;
            }
            case "denaro":{
                int livello = int.Parse(splitArray[1]);
                switch (livello){
                    default:{monete=50;break;}
                }
                break;
            }
        }
        if (monete>0){
            denaro+=monete;
            txt_denaro.SetText(denaro.ToString());
        }
        pannello_scelta_premio.SetActive(false);
        pannello_upgrade.SetActive(true);

        riempi_blocchi_testo_unita();
    }

    private void get_premi_upgrade(){
        string premio="";
        string pupo_singolo="";
        int livello=1;  //ben presto dovrà cambiare a seconda delle circostanze (o dell'ondata)

        switch (num_ondata){
            case 3:{//scegli una nuova razza da sbloccare
                foreach(KeyValuePair<string,string> attachStat in info_comuni.lista_razze_totale){
                    if (!lista_razze_sbloccate.ContainsKey(attachStat.Key)){
                        lista_random.Add("nrazza-"+attachStat.Key+"-"+livello+"-a",1);
                    }
                }
                premio=lista_random.ElementAt(Random.Range(0, lista_random.Count)).Key;
                crea_premio_upgrade(premio,1);
                lista_random.Remove(premio);
                premio=lista_random.ElementAt(Random.Range(0, lista_random.Count)).Key;
                crea_premio_upgrade(premio,2);
                lista_random.Remove(premio);
                premio=lista_random.ElementAt(Random.Range(0, lista_random.Count)).Key;
                crea_premio_upgrade(premio,3);
                lista_random.Remove(premio);
                break;
            }
            case 2:
            case 4:{//solo pupetti normali
                foreach(KeyValuePair<string,int> attachStat in lista_razze_sbloccate){
                    //pupo_singolo=info_comuni.lista_razze_totale[attachStat.Key];      //no: Tanto saranno sempre al plurale...
                    pupo_singolo=attachStat.Key;
                    lista_random.Add("pupo-"+pupo_singolo+"-warrior-"+livello,1);
                    lista_random.Add("pupo-"+pupo_singolo+"-arcer-"+livello,1);
                    lista_random.Add("pupo-"+pupo_singolo+"-wizard-"+livello,1);
                }
                premio=lista_random.ElementAt(Random.Range(0, lista_random.Count)).Key;
                crea_premio_upgrade(premio,1);
                lista_random.Remove(premio);
                premio=lista_random.ElementAt(Random.Range(0, lista_random.Count)).Key;
                crea_premio_upgrade(premio,2);
                lista_random.Remove(premio);
                premio=lista_random.ElementAt(Random.Range(0, lista_random.Count)).Key;
                crea_premio_upgrade(premio,3);
                lista_random.Remove(premio);
                break;
            }
            case 5:{
                foreach(KeyValuePair<string,string> attachStat in info_comuni.lista_abilita_nome){
                    if (!lista_abilita.ContainsKey(attachStat.Key)){livello=1;}
                    else {livello=lista_abilita[attachStat.Key]+1;}
                    lista_random.Add("abilita-"+attachStat.Key+"-"+livello,1);
                }
                premio=lista_random.ElementAt(Random.Range(0, lista_random.Count)).Key;
                crea_premio_upgrade(premio,1);
                lista_random.Remove(premio);
                premio=lista_random.ElementAt(Random.Range(0, lista_random.Count)).Key;
                crea_premio_upgrade(premio,2);
                lista_random.Remove(premio);
                premio=lista_random.ElementAt(Random.Range(0, lista_random.Count)).Key;
                crea_premio_upgrade(premio,3);
                lista_random.Remove(premio);
                break;
            }
            default:{
                //prima parte: Diamo uno dei pupetti sbloccati
                foreach(KeyValuePair<string,int> attachStat in lista_razze_sbloccate){
                    //pupo_singolo=info_comuni.lista_razze_totale[attachStat.Key];      //no: Tanto saranno sempre al plurale...
                    pupo_singolo=attachStat.Key;
                    lista_random.Add("pupo-"+pupo_singolo+"-warrior-"+livello,1);
                    lista_random.Add("pupo-"+pupo_singolo+"-arcer-"+livello,1);
                    lista_random.Add("pupo-"+pupo_singolo+"-wizard-"+livello,1);
                }
                premio=lista_random.ElementAt(Random.Range(0, lista_random.Count)).Key;
                crea_premio_upgrade(premio,1);

                //secondo premio
                lista_random.Remove(premio);
                foreach(KeyValuePair<string,string> attachStat in info_comuni.lista_razze_totale){
                    if (!lista_razze_sbloccate.ContainsKey(attachStat.Key)){
                        lista_random.Add("nrazza-"+attachStat.Key+"-"+livello+"-a",1);
                    }
                }
                premio=lista_random.ElementAt(Random.Range(0, lista_random.Count)).Key;
                crea_premio_upgrade(premio,2);

                //terzo premio
                lista_random.Remove(premio);
                lista_random.Add("denaro-"+livello,1);
                if (lista_abilita.Count<num_max_abilita){
                    foreach(KeyValuePair<string,string> attachStat in info_comuni.lista_abilita_nome){
                        if (!lista_abilita.ContainsKey(attachStat.Key)){livello=1;}
                        else {livello=lista_abilita[attachStat.Key]+1;}
                        lista_random.Add("abilita-"+attachStat.Key+"-"+livello,1);
                    }
                }
                premio=lista_random.ElementAt(Random.Range(0, lista_random.Count)).Key;
                crea_premio_upgrade(premio,3);
                break;
            }
        }
    }

    private void crea_premio_upgrade(string premio, int num_cont_premio){
        print ("è entrato "+premio);
        lista_premi_settati.Add(num_cont_premio,premio);
        string tipo;
        string[] splitArray;

        string titolo="";
        string descrizione="";
        int monete=0;
        string txt_monete="";

        splitArray=premio.Split(char.Parse("-"));
        tipo=splitArray[0];
        switch (tipo){
            case "pupo":{
                string razza_pupo = splitArray[1];
                int num_pupi=Random.Range(1,4);
                string classe_pupo = splitArray[2];
                int livello_pupo = int.Parse(splitArray[3]);
                monete=(int)(info_comuni.lista_costo_unita_razza[razza_pupo]*num_pupi);
                monete=100-monete;
                txt_monete="+ "+monete+" gold";
                lista_premi_settati_num_pupi.Add(num_cont_premio,num_pupi);

                titolo="+"+num_pupi+" "+info_comuni.lista_classi_nome[classe_pupo]+" "+info_comuni.lista_razza_pupi_nome[razza_pupo];
                descrizione=info_comuni.lista_pupi_descrizione[razza_pupo];
                break;
            }
            case "nrazza":{
                int livello_razza = int.Parse(splitArray[2]);
                string razza_pupo = splitArray[1];
                titolo="Unlock "+info_comuni.lista_razza_pupi_nome[razza_pupo]+" (Level "+livello_razza+")";
                descrizione=info_comuni.lista_pupi_descrizione[razza_pupo];
                break;
            }
            case "abilita":{
                string abilita=splitArray[1];
                int livello_abilita = int.Parse(splitArray[2]);
                if (livello_abilita==1){
                    titolo="New ability: "+info_comuni.lista_abilita_nome[abilita];
                } else {
                    titolo="Unlock "+info_comuni.lista_abilita_nome[abilita]+" level "+livello_abilita;
                }
                descrizione="Upgrade the ability "+info_comuni.lista_abilita_nome[abilita]+" to level 2";
                break;
            }
            case "denaro":{
                monete=0;
                int livello = int.Parse(splitArray[1]);
                switch (livello){
                    default:{monete=50;break;}
                }
                titolo="Gold!";
                descrizione="Un grande tributo al morale della truppa!";
                break;
            }
            default:{
                print ("non ho trovato questo: "+tipo);break;
            }
        }

        switch (num_cont_premio){
            case 1:{
                titolo_premio_upgrade_1.SetText(titolo);
                monete_premio_upgrade_1.SetText(txt_monete);
                descrizione_premio_upgrade_1.SetText(descrizione);
                break;
            }
            case 2:{
                titolo_premio_upgrade_2.SetText(titolo);
                monete_premio_upgrade_2.SetText(txt_monete);
                descrizione_premio_upgrade_2.SetText(descrizione);
                break;
            }
            case 3:{
                titolo_premio_upgrade_3.SetText(titolo);
                monete_premio_upgrade_3.SetText(txt_monete);
                descrizione_premio_upgrade_3.SetText(descrizione);
                break;
            }
        }

    }

    public void check_abilita(string abilita){
        int livello=livelli_attuali_upgrade[abilita];
        string testo="";
        int costo=ritorna_costo_abilita(abilita);   //perchè è stato aggiornato al nuovo costo
        switch (abilita){
            case "melee_damage":{testo="Your units hit +"+(livello+1)+" on melee attacks";break;}
            case "distance_damage":{testo="Your units hit +"+(livello+1)+" on distance attacks";break;}
            case "spell_damage":{testo="Your units hit +"+(livello+1)+" on spell attacks";break;}
            case "health":{testo="Your units have +"+((livello+1)*10)+"% of health more";break;}
            case "hero_damage":{testo="The abilities of your hero hit +"+((livello+1)*10)+"%";break;}
            case "hero_cooldown":{testo="The cooldown of your hero is -"+((livello+1)*10)+"% reduced";break;}
            case "random_unity":{testo="Choose a random unit of three";break;}
            case "random_spell":{testo="Choose a random ability of three";break;}
            case "food":{testo="Add +20 space units";break;}
        }
        lista_txt_upgrade_descrizione[abilita].SetText(testo);
        lista_txt_upgrade_costi[abilita].SetText("Cost: "+costo.ToString());

        switch (abilita){
            case "melee_damage":
            case "distance_damage":
            case "spell_damage":
            case "health":{
                if (livello>=10){
                    lista_B_upgrade_bottoni[abilita].interactable=false;
                    lista_txt_upgrade_descrizione[abilita].SetText("You have reach the max level for this upgrade!");
                    lista_txt_upgrade_costi[abilita].SetText("Cost: "+costo.ToString("/"));
                }
                break;
            }
            case "hero_damage":
            case "hero_cooldown":{
                if (livello>=5){
                    lista_B_upgrade_bottoni[abilita].interactable=false;
                    lista_txt_upgrade_descrizione[abilita].SetText("You have reach the max level for this upgrade!");
                    lista_txt_upgrade_costi[abilita].SetText("Cost: "+costo.ToString("/"));
                }
                break;
            }
        }
    }

    public void btn_compra(string abilita){
        int costo=ritorna_costo_abilita(abilita);
        if (denaro>=costo){
            denaro-=costo;
            livelli_attuali_upgrade[abilita]++;
            check_abilita(abilita);
            txt_denaro.SetText(denaro.ToString());

            //a questo punto controlla ogni abilità
            foreach(KeyValuePair<string,Button> attachStat in lista_B_upgrade_bottoni){
                costo=ritorna_costo_abilita(attachStat.Key);
                if (costo>denaro){
                    lista_B_upgrade_bottoni[attachStat.Key].interactable=false;
                }
            }

        } else {
            print ("eh no: non posso comprare più di cosi...");
        }
    }

    public int ritorna_costo_abilita(string abilita){
        int costo=0;
        int livello=livelli_attuali_upgrade[abilita];
        switch (abilita){
            case "melee_damage":
            case "distance_damage":
            case "spell_damage":
            case "health":
            case "hero_damage":
            case "hero_cooldown":
            {
                costo=200*(livello+1);
                break;
            }
            default:{
                costo=200;
                break;
            }
        }
        return costo;
        //lista_txt_upgrade_costi[abilita].SetText("<mspace=20>"+costo+"</mspace>");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void carica_info_xml(){
        string string_temp;
        path_xml=Application.persistentDataPath + "/game_c.xml";
        XmlDocument xml_game = new XmlDocument ();
        string_temp=System.IO.File.ReadAllText(path_xml);
        //string_temp=f_comuni.decripta(string_temp, "munimuni");
        xml_game.LoadXml(string_temp);

        //negli upgrade, non si può entrare se il file game_c non è stato ancora creato; E come detto, in verità verrà automaticamente creato al primo combattimento.
        int num_pupi_temp, liv_abilita_temp;
        string tipo_pupo_temp, abilita_temp;

        foreach(XmlElement node in xml_game.SelectNodes("game")){
            id_hero=node.GetAttribute("id_hero");
            num_ondata=int.Parse(node.GetAttribute("num_ondata"));
            denaro=int.Parse(node.GetAttribute("denaro"));
            foreach(XmlElement node_2 in node.SelectNodes("lista_abilita")){
                foreach(XmlElement node_3 in node_2.SelectNodes("a")){
                    liv_abilita_temp=int.Parse(node_3.GetAttribute("liv"));
                    abilita_temp=node_3.InnerText;
                    lista_abilita.Add(abilita_temp,liv_abilita_temp);
                }
            }
            foreach(XmlElement node_2 in node.SelectNodes("lista_pupetti")){
                foreach(XmlElement node_3 in node_2.SelectNodes("p")){
                    num_pupi_temp=int.Parse(node_3.GetAttribute("num"));
                    tipo_pupo_temp=node_3.InnerText;
                    lista_pupetti.Add(tipo_pupo_temp,num_pupi_temp);
                }
            }
            foreach(XmlElement node_2 in node.SelectNodes("lista_upgrade")){
                foreach(XmlElement node_3 in node_2.SelectNodes("u")){
                    livelli_attuali_upgrade[node_3.InnerText]=int.Parse(node_3.GetAttribute("liv"));
                }
            }
            foreach(XmlElement node_2 in node.SelectNodes("lista_razze_sbloccate")){
                foreach(XmlElement node_3 in node_2.SelectNodes("r")){
                    lista_razze_sbloccate[node_3.InnerText]=int.Parse(node_3.GetAttribute("liv"));
                }
            }
        }
    }

    public void salva_e_continua(){
        string string_temp="";
        path_xml=Application.persistentDataPath + "/game_c.xml";
        File.Delete(path_xml);  //eh si, perchè tanto dobbiamo sempre ricrearlo...

        string xml_content="";
        xml_content="<game id_hero='"+id_hero+"' num_ondata='"+num_ondata+"' denaro='"+denaro+"'>";
        xml_content+="\n\t<lista_abilita>";
        foreach(KeyValuePair<string,int> attachStat in lista_abilita){
            xml_content+="\n\t\t<a liv='"+attachStat.Value+"'>"+attachStat.Key+"</a>";
        }
        xml_content+="\n\t</lista_abilita>";
        xml_content+="\n\t<lista_pupetti>";
        foreach(KeyValuePair<string,int> attachStat in lista_pupetti){
            xml_content+="\n\t\t<p num='"+attachStat.Value+"'>"+attachStat.Key+"</p>";
        }
        xml_content+="\n\t</lista_pupetti>";
        xml_content+="\n\t<lista_upgrade>";
        foreach(KeyValuePair<string,int> attachStat in livelli_attuali_upgrade){
            xml_content+="\n\t\t<u liv='"+attachStat.Value+"'>"+attachStat.Key+"</u>";
        }
        xml_content+="\n\t</lista_upgrade>";

        xml_content+="\n\t<lista_razze_sbloccate>";
        foreach(KeyValuePair<string,int> attachStat in lista_razze_sbloccate){
            xml_content+="\n\t\t<r liv='"+attachStat.Value+"'>"+attachStat.Key+"</r>";
        }
        xml_content+="\n\t</lista_razze_sbloccate>";

        xml_content+="\n</game>";

        StreamWriter writer = new StreamWriter(path_xml, false);
        writer.Write(xml_content);
        writer.Close();

        //print (xml_content);

        SceneManager.LoadScene("game");
    }
}
