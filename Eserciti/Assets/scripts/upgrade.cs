using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using System.Text;
using System.Xml; //Needed for XML functionality
using System.IO;
using System.Linq;

using Spine;
using Spine.Unity;

public class upgrade : MonoBehaviour
{   
    public Button btn_upgrade_pupetti;
    public Button btn_upgrade_eroe;
    public Button btn_upgrade_generici;

    public GameObject lista_upgrade_generici;
    public GameObject lista_upgrade_pupetti;
    public GameObject lista_upgrade_eroe;

    public info_comuni info_comuni;
    public GameObject pannello_scelta_premio;
    public GameObject pannello_upgrade;

    public GameObject cont_scrigno;
    public TMPro.TextMeshProUGUI testo_reward;


    public GameObject blocco_unita_liv_1;
    public GameObject blocco_unita_liv_2;
    public GameObject blocco_unita_liv_3;
    public GameObject img_scura_blocco_unita_liv_2;
    public GameObject img_scura_blocco_unita_liv_3;
    private Dictionary<string, TMPro.TextMeshProUGUI> lista_txt_unita_esercito = new Dictionary<string, TMPro.TextMeshProUGUI>(); 
    private Dictionary<string, SkeletonGraphic> lista_icona_unita_esercito = new Dictionary<string, SkeletonGraphic>(); 

    public GameObject cont_upgrade_1;
    public GameObject cont_upgrade_2;
    public GameObject cont_upgrade_3;
    public GameObject cont_upgrade_unlock_unity_tier;
    public GameObject cont_upgrade_unity_tier_2;
    public GameObject cont_upgrade_unity_tier_3;
    public GameObject cont_upgrade_random_race;
    public GameObject cont_upgrade_random_spell;


    public Image img_premio_upgrade_1;
    public GameObject img_monete_upgrade_1;
    public TMPro.TextMeshProUGUI titolo_premio_upgrade_1;
    public TMPro.TextMeshProUGUI monete_premio_upgrade_1;
    public TMPro.TextMeshProUGUI descrizione_premio_upgrade_1;
    public Image img_premio_upgrade_2;
    public GameObject img_monete_upgrade_2;
    public TMPro.TextMeshProUGUI titolo_premio_upgrade_2;
    public TMPro.TextMeshProUGUI monete_premio_upgrade_2;
    public TMPro.TextMeshProUGUI descrizione_premio_upgrade_2;
    public Image img_premio_upgrade_3;
    public GameObject img_monete_upgrade_3;
    public TMPro.TextMeshProUGUI titolo_premio_upgrade_3;
    public TMPro.TextMeshProUGUI monete_premio_upgrade_3;
    public TMPro.TextMeshProUGUI descrizione_premio_upgrade_3;
    private Dictionary<int, string> lista_premi_settati = new Dictionary<int, string>();
    private Dictionary<int, int> lista_premi_settati_num_pupi = new Dictionary<int, int>();
    private Dictionary<int, int> lista_premi_settati_monete = new Dictionary<int, int>();

    public TMPro.TextMeshProUGUI txt_next_stage;
    public TMPro.TextMeshProUGUI txt_denaro;
    private int denaro;
    private string path_xml;
    private string id_hero;
    private int num_ondata;
    private int tier_unity_sbloccato;
    private int i;
    private int num_max_abilita=7;
    private float per_potere_eroe;

    //lista_dei costi delle varie abilità
    public TMPro.TextMeshProUGUI txt_u_c_melee_damage;
    public TMPro.TextMeshProUGUI txt_u_c_distance_damage;
    public TMPro.TextMeshProUGUI txt_u_c_spell_damage;
    public TMPro.TextMeshProUGUI txt_u_c_health;
    public TMPro.TextMeshProUGUI txt_u_c_hero_damage;
    public TMPro.TextMeshProUGUI txt_u_c_hero_cooldown;
    public TMPro.TextMeshProUGUI txt_u_c_random_unity_1;
    public TMPro.TextMeshProUGUI txt_u_c_random_spell;
    public TMPro.TextMeshProUGUI txt_u_c_random_race;
    public TMPro.TextMeshProUGUI txt_u_c_random_unity_2;
    public TMPro.TextMeshProUGUI txt_u_c_random_unity_3;
    public TMPro.TextMeshProUGUI txt_u_c_unlock_next_unity_tier;
    public TMPro.TextMeshProUGUI txt_u_c_food;

    //lista bottoni "upgrade" delle abilità
    public Button B_u_b_melee_damage;
    public Button B_u_b_distance_damage;
    public Button B_u_b_spell_damage;
    public Button B_u_b_health;
    public Button B_u_b_hero_damage;
    public Button B_u_b_hero_cooldown;
    public Button B_u_b_random_unity_1;
    public Button B_u_b_random_spell;
    public Button B_u_b_random_race;
    public Button B_u_b_random_unity_2;
    public Button B_u_b_random_unity_3;
    public Button B_u_b_unlock_next_unity_tier;
    public Button B_u_b_food;

    //lista descrizioni delle abilità (cambiano a seconda del livello)
    public TMPro.TextMeshProUGUI txt_u_d_melee_damage;
    public TMPro.TextMeshProUGUI txt_u_d_distance_damage;
    public TMPro.TextMeshProUGUI txt_u_d_spell_damage;
    public TMPro.TextMeshProUGUI txt_u_d_health;
    public TMPro.TextMeshProUGUI txt_u_d_hero_damage;
    public TMPro.TextMeshProUGUI txt_u_d_hero_cooldown;
    public TMPro.TextMeshProUGUI txt_u_d_random_unity_1;
    public TMPro.TextMeshProUGUI txt_u_d_random_spell;
    public TMPro.TextMeshProUGUI txt_u_d_random_race;
    public TMPro.TextMeshProUGUI txt_u_d_random_unity_2;
    public TMPro.TextMeshProUGUI txt_u_d_random_unity_3;
    public TMPro.TextMeshProUGUI txt_u_d_unlock_next_unity_tier;
    public TMPro.TextMeshProUGUI txt_u_d_food;

    private Dictionary<string, int> lista_pupetti = new Dictionary<string, int>(); 
    private Dictionary<string, int> lista_abilita = new Dictionary<string, int>(); 

    private Dictionary<string, TMPro.TextMeshProUGUI> lista_txt_upgrade_costi = new Dictionary<string, TMPro.TextMeshProUGUI>();
    private Dictionary<string, Button> lista_B_upgrade_bottoni = new Dictionary<string, Button>();
    private Dictionary<string, TMPro.TextMeshProUGUI> lista_txt_upgrade_descrizione = new Dictionary<string, TMPro.TextMeshProUGUI>();

    private Dictionary<string, int> livelli_attuali_upgrade = new Dictionary<string, int>();
    private Dictionary<string, int> lista_random = new Dictionary<string, int>();

    public Dictionary<string, int> lista_razze_sbloccate = new Dictionary<string, int>();

    private int valore_monete_premio=100;

    private Dictionary<string, int> lista_upgrade_perenni_liv = new Dictionary<string, int>();

    // Start is called before the first frame update
    void Start(){
        carica_info_partite();
        get_all_blocchi_testo_unita();
        riempi_blocchi_testo_unita();
        
        path_xml=Application.persistentDataPath + "/game_c.xml";

        lista_txt_upgrade_costi.Add("melee_damage",txt_u_c_melee_damage);
        lista_txt_upgrade_costi.Add("distance_damage",txt_u_c_distance_damage);
        lista_txt_upgrade_costi.Add("spell_damage",txt_u_c_spell_damage);
        lista_txt_upgrade_costi.Add("health",txt_u_c_health);
        lista_txt_upgrade_costi.Add("hero_damage",txt_u_c_hero_damage);
        lista_txt_upgrade_costi.Add("hero_cooldown",txt_u_c_hero_cooldown);
        lista_txt_upgrade_costi.Add("random_unity_1",txt_u_c_random_unity_1);
        lista_txt_upgrade_costi.Add("random_spell",txt_u_c_random_spell);
        lista_txt_upgrade_costi.Add("random_race",txt_u_c_random_race);
        lista_txt_upgrade_costi.Add("random_unity_2",txt_u_c_random_unity_2);
        lista_txt_upgrade_costi.Add("random_unity_3",txt_u_c_random_unity_3);
        lista_txt_upgrade_costi.Add("unlock_next_unity_tier",txt_u_c_unlock_next_unity_tier);
        lista_txt_upgrade_costi.Add("food",txt_u_c_food);

        lista_B_upgrade_bottoni.Add("melee_damage",B_u_b_melee_damage);
        lista_B_upgrade_bottoni.Add("distance_damage",B_u_b_distance_damage);
        lista_B_upgrade_bottoni.Add("spell_damage",B_u_b_spell_damage);
        lista_B_upgrade_bottoni.Add("health",B_u_b_health);
        lista_B_upgrade_bottoni.Add("hero_damage",B_u_b_hero_damage);
        lista_B_upgrade_bottoni.Add("hero_cooldown",B_u_b_hero_cooldown);
        lista_B_upgrade_bottoni.Add("random_unity_1",B_u_b_random_unity_1);
        lista_B_upgrade_bottoni.Add("random_spell",B_u_b_random_spell);
        lista_B_upgrade_bottoni.Add("random_race",B_u_b_random_race);
        lista_B_upgrade_bottoni.Add("random_unity_2",B_u_b_random_unity_2);
        lista_B_upgrade_bottoni.Add("random_unity_3",B_u_b_random_unity_3);
        lista_B_upgrade_bottoni.Add("unlock_next_unity_tier",B_u_b_unlock_next_unity_tier);
        lista_B_upgrade_bottoni.Add("food",B_u_b_food);

        lista_txt_upgrade_descrizione.Add("melee_damage",txt_u_d_melee_damage);
        lista_txt_upgrade_descrizione.Add("distance_damage",txt_u_d_distance_damage);
        lista_txt_upgrade_descrizione.Add("spell_damage",txt_u_d_spell_damage);
        lista_txt_upgrade_descrizione.Add("health",txt_u_d_health);
        lista_txt_upgrade_descrizione.Add("hero_damage",txt_u_d_hero_damage);
        lista_txt_upgrade_descrizione.Add("hero_cooldown",txt_u_d_hero_cooldown);
        lista_txt_upgrade_descrizione.Add("random_unity_1",txt_u_d_random_unity_1);
        lista_txt_upgrade_descrizione.Add("random_spell",txt_u_d_random_spell);
        lista_txt_upgrade_descrizione.Add("random_race",txt_u_d_random_race);
        lista_txt_upgrade_descrizione.Add("random_unity_2",txt_u_d_random_unity_2);
        lista_txt_upgrade_descrizione.Add("random_unity_3",txt_u_d_random_unity_3);
        lista_txt_upgrade_descrizione.Add("unlock_next_unity_tier",txt_u_d_unlock_next_unity_tier);
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

        check_full_abilita();
        check_full_race();

        pannello_scelta_premio.SetActive(true);
        pannello_upgrade.SetActive(false);

        get_premi_upgrade();
        //riempi_blocchi_testo_unita();     //viene già fatto al momento della scelta del premio...

        check_img_nere_upgrade_unity_tier();

        foreach(KeyValuePair<string,int> attachStat in lista_razze_sbloccate){
            sblocca_unita_razza(attachStat.Key,attachStat.Value);
        }
        btn_upgrade_pupetti.onClick.Invoke();
    }

    public void bottone_dx_lista_upgrade(string tipo){
        //btn_upgrade_pupetti;
        //btn_upgrade_eroe;
        //btn_upgrade_generici;

        lista_upgrade_generici.SetActive(false);
        lista_upgrade_pupetti.SetActive(false);
        lista_upgrade_eroe.SetActive(false);

        switch (tipo){
            case "generici":{lista_upgrade_generici.SetActive(true);break;}
            case "pupetti":{lista_upgrade_pupetti.SetActive(true);break;}
            case "eroe":{lista_upgrade_eroe.SetActive(true);break;}
        }
    }

    private void check_img_nere_upgrade_unity_tier(){
        if (tier_unity_sbloccato<2){
            cont_upgrade_unity_tier_2.SetActive(false);
            cont_upgrade_unity_tier_3.SetActive(false);
        }
        else {
            cont_upgrade_unity_tier_2.SetActive(true);
            img_scura_blocco_unita_liv_2.SetActive(false);
            if (tier_unity_sbloccato<3){
                cont_upgrade_unity_tier_3.SetActive(false);
            } else {
                cont_upgrade_unity_tier_3.SetActive(true);
                img_scura_blocco_unita_liv_3.SetActive(false);
            }
        }
    }

    private void get_all_blocchi_testo_unita(){
        string string_temp="";
        foreach (Transform child in blocco_unita_liv_1.transform.GetComponentsInChildren<Transform>()) {
            if (child.name.Contains("num_")){
                string_temp=child.name.Replace("num_","");
                lista_txt_unita_esercito.Add(string_temp,child.GetComponent<TMPro.TextMeshProUGUI>());
            }
            else if (child.name.Contains("icona_")){
                string_temp=child.name.Replace("num_","");
                lista_icona_unita_esercito.Add(string_temp,child.GetComponent<SkeletonGraphic>());
            }
        }

        foreach (Transform child in blocco_unita_liv_2.transform.GetComponentsInChildren<Transform>()) {
            if (child.name.Contains("num_")){
                string_temp=child.name.Replace("num_","");
                lista_txt_unita_esercito.Add(string_temp,child.GetComponent<TMPro.TextMeshProUGUI>());
            }
            else if (child.name.Contains("icona_")){
                string_temp=child.name.Replace("num_","");
                lista_icona_unita_esercito.Add(string_temp,child.GetComponent<SkeletonGraphic>());
            }
        }

        foreach (Transform child in blocco_unita_liv_3.transform.GetComponentsInChildren<Transform>()) {
            if (child.name.Contains("num_")){
                string_temp=child.name.Replace("num_","");
                lista_txt_unita_esercito.Add(string_temp,child.GetComponent<TMPro.TextMeshProUGUI>());
            }
            else if (child.name.Contains("icona_")){
                string_temp=child.name.Replace("num_","");
                lista_icona_unita_esercito.Add(string_temp,child.GetComponent<SkeletonGraphic>());
            }
        }
    }

    private void sblocca_unita_razza(string razza_plurare, int livello){
        print ("dovrei sbloccare "+razza_plurare+" - "+livello);
        string razza=info_comuni.lista_razze_totale[razza_plurare];
        string[] splitArray;
        splitArray=razza_plurare.Split(char.Parse("_"));   
        livello=int.Parse(splitArray[1]);
        print ("dovrei sbloccare "+razza_plurare+" - "+livello);

        string string_temp;

        string_temp="icona_"+razza+"_warrior_"+livello;
        lista_icona_unita_esercito[string_temp].color=new Color(1, 1, 1, 1f);
        string_temp=razza+"_warrior_"+livello;
        lista_txt_unita_esercito[string_temp].SetText("0");

        string_temp="icona_"+razza+"_arcer_"+livello;
        lista_icona_unita_esercito[string_temp].color=new Color(1, 1, 1, 1f);
        string_temp=razza+"_arcer_"+livello;
        lista_txt_unita_esercito[string_temp].SetText("0");

        string_temp="icona_"+razza+"_wizard_"+livello;
        lista_icona_unita_esercito[string_temp].color=new Color(1, 1, 1, 1f);
        string_temp=razza+"_wizard_"+livello;
        lista_txt_unita_esercito[string_temp].SetText("0");
    }

    private void riempi_blocchi_testo_unita(){
        foreach(KeyValuePair<string,int> attachStat in lista_pupetti){
            lista_txt_unita_esercito[attachStat.Key].SetText(attachStat.Value.ToString());
        }
    }

    public void click_upgrade_number(int numero){
        cont_scrigno.SetActive(false);
        testo_reward.SetText("Choose Your Upgrade");
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

                string[] splitArray_razza;
                splitArray_razza=razza_pupo.Split(char.Parse("_"));
                int livello_pupo = int.Parse(splitArray_razza[1]);
                monete=lista_premi_settati_monete[numero];

                string pupo_xml=info_comuni.lista_razze_totale[razza_pupo]+"_"+classe_pupo+"_"+livello_pupo;
                print (pupo_xml);

                if (!lista_pupetti.ContainsKey(pupo_xml)){lista_pupetti.Add(pupo_xml,0);}
                lista_pupetti[pupo_xml]+=num_pupi;
                break;
            }
            case "nrazza":{
                int livello_razza = int.Parse(splitArray[2]);
                string razza = splitArray[1];
                print ("inserisco la razza: "+razza);
                lista_razze_sbloccate.Add(razza,livello_razza);
                sblocca_unita_razza(razza,livello_razza);
                check_full_race();
                break;
            }
            case "abilita":{
                string abilita=splitArray[1];
                int livello_abilita = int.Parse(splitArray[2]);
                if (livello_abilita==1){lista_abilita.Add(abilita,1);}
                else {lista_abilita[abilita]++;}
                check_full_abilita();
                break;
            }
            case "denaro":{
                int livello = int.Parse(splitArray[1]);
                switch (livello){
                    default:{monete=valore_monete_premio;break;}
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
        check_all_button();
    }

    private void check_all_button(){
        int costo;
        foreach(KeyValuePair<string,Button> attachStat in lista_B_upgrade_bottoni){
            costo=ritorna_costo_abilita(attachStat.Key);
            if (costo>denaro){
                lista_B_upgrade_bottoni[attachStat.Key].interactable=false;
                lista_B_upgrade_bottoni[attachStat.Key].gameObject.SetActive(false);
            }
            else {
                lista_B_upgrade_bottoni[attachStat.Key].interactable=true;
                lista_B_upgrade_bottoni[attachStat.Key].gameObject.SetActive(true);
            }
        }
    }

    private void get_premi_upgrade(){
        string premio="";
        string pupo_singolo="";
        int livello=1;  //ben presto dovrà cambiare a seconda delle circostanze (o dell'ondata)

        lista_random.Clear();
        switch (num_ondata){
            case 3:{//scegli una nuova razza da sbloccare
                string[] splitArray_razza;
                foreach(KeyValuePair<string,string> attachStat in info_comuni.lista_razze_totale){
                    splitArray_razza=attachStat.Key.Split(char.Parse("_"));
                    livello=int.Parse(splitArray_razza[1]);
                    if (livello<=tier_unity_sbloccato){
                        if (!lista_razze_sbloccate.ContainsKey(attachStat.Key)){
                            lista_random.Add("nrazza-"+attachStat.Key+"-"+livello+"-a",1);
                        }
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
                    pupo_singolo=attachStat.Key;
                    lista_random.Add("pupo-"+pupo_singolo+"-warrior-plus_monete",1);
                    lista_random.Add("pupo-"+pupo_singolo+"-arcer-plus_monete",1);
                    lista_random.Add("pupo-"+pupo_singolo+"-wizard-plus_monete",1);
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
                    if (!lista_abilita.ContainsKey(attachStat.Key)){
                        if (!info_comuni.lista_bool_abilita_classe[attachStat.Key]){
                            if (lista_abilita.Count<num_max_abilita){lista_random.Add("abilita-"+attachStat.Key+"-1",1);}
                        }
                    }
                    else {
                        livello=lista_abilita[attachStat.Key]+1;
                        if (livello<4){lista_random.Add("abilita-"+attachStat.Key+"-"+livello,1);}
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
            default:{
                //prima parte: Diamo uno dei pupetti sbloccati
                foreach(KeyValuePair<string,int> attachStat in lista_razze_sbloccate){
                    //pupo_singolo=info_comuni.lista_razze_totale[attachStat.Key];      //no: Tanto saranno sempre al plurale...
                    pupo_singolo=attachStat.Key;
                    lista_random.Add("pupo-"+pupo_singolo+"-warrior-plus_monete",1);
                    lista_random.Add("pupo-"+pupo_singolo+"-arcer-plus_monete",1);
                    lista_random.Add("pupo-"+pupo_singolo+"-wizard-plus_monete",1);
                }
                premio=lista_random.ElementAt(Random.Range(0, lista_random.Count)).Key;
                crea_premio_upgrade(premio,1);

                //secondo premio (inseriamo anche la possibilità di sbloccare una razza)
                lista_random.Remove(premio);

                string[] splitArray_razza;
                foreach(KeyValuePair<string,string> attachStat in info_comuni.lista_razze_totale){
                    splitArray_razza=attachStat.Key.Split(char.Parse("_"));
                    livello=int.Parse(splitArray_razza[1]);
                    if (livello<=tier_unity_sbloccato){
                        if (!lista_razze_sbloccate.ContainsKey(attachStat.Key)){
                            lista_random.Add("nrazza-"+attachStat.Key+"-"+livello+"-a",1);
                        }
                    }
                }

                premio=lista_random.ElementAt(Random.Range(0, lista_random.Count)).Key;
                crea_premio_upgrade(premio,2);

                //terzo premio (inseriamo anche la possibilità di sbloccare una abilità)
                lista_random.Remove(premio);
                lista_random.Add("denaro-"+livello,1);
                
                foreach(KeyValuePair<string,string> attachStat in info_comuni.lista_abilita_nome){
                    if (!lista_abilita.ContainsKey(attachStat.Key)){
                        if (!info_comuni.lista_bool_abilita_classe[attachStat.Key]){
                            if (lista_abilita.Count<num_max_abilita){lista_random.Add("abilita-"+attachStat.Key+"-1",1);}
                        }
                    }
                    else {
                        livello=lista_abilita[attachStat.Key]+1;
                        if (livello<4){lista_random.Add("abilita-"+attachStat.Key+"-"+livello,1);}
                    }
                }

                premio=lista_random.ElementAt(Random.Range(0, lista_random.Count)).Key;
                crea_premio_upgrade(premio,3);
                break;
            }
        }
    }

    private void check_full_abilita(){
        if (lista_abilita.Count>=num_max_abilita){
            foreach(KeyValuePair<string,int> attachStat in lista_abilita){
                if (attachStat.Value<3){return;}
            }
            cont_upgrade_random_spell.SetActive(false);
            lista_B_upgrade_bottoni["random_spell"].interactable=false;
            lista_B_upgrade_bottoni["random_spell"].gameObject.SetActive(false);
            lista_txt_upgrade_descrizione["random_spell"].SetText("You have unlocked all abilities or don't have space for new one.");
            lista_txt_upgrade_costi["random_spell"].SetText("Cost: /");
        }
    }

    private void check_full_race(){
        int da_sbloccare=0;
        int sbloccate=0;
        int livello=0;
        string[] splitArray;
        foreach(KeyValuePair<string,string> attachStat in info_comuni.lista_razze_totale){
            splitArray=attachStat.Key.Split(char.Parse("_"));
            livello=int.Parse(splitArray[1]);
            if (livello<=tier_unity_sbloccato){da_sbloccare++;}
        }
        //print (lista_razze_sbloccate.Count+" - "+info_comuni.lista_razze_totale.Count);
        if (lista_razze_sbloccate.Count>=da_sbloccare){
            cont_upgrade_random_race.SetActive(false);
            lista_B_upgrade_bottoni["random_race"].interactable=false;
            lista_B_upgrade_bottoni["random_race"].gameObject.SetActive(false);
            string testo="You have unlocked all races of level";
            switch (tier_unity_sbloccato){
                case 1:{testo+=" 1";break;}
                case 2:{testo+="s 1 and 2";break;}
                case 3:{testo+="s 1, 2 and 3";break;}
            }
            lista_txt_upgrade_descrizione["random_race"].SetText(testo);
        }
        else {cont_upgrade_random_race.SetActive(true);}
    }

    private void crea_premio_upgrade(string premio, int num_cont_premio){
        string tipo="";
        string titolo="";
        string descrizione="";
        int monete=0;
        string txt_monete="";
        print ("è entrato "+premio);
        if (premio!=""){
            string[] splitArray;
            string[] splitArray_razza;
            splitArray=premio.Split(char.Parse("-"));
            tipo=splitArray[0];
            lista_premi_settati.Add(num_cont_premio,premio);
            switch (tipo){
                case "pupo":{
                    string razza_pupo = splitArray[1];
                    string classe_pupo = splitArray[2];
                    splitArray_razza=razza_pupo.Split(char.Parse("_"));
                    int livello_pupo = int.Parse(splitArray_razza[1]);
                    int num_pupi=Random.Range(1,4);
                    //monete=(int)(info_comuni.lista_costo_unita_razza[razza_pupo]*num_pupi);
                    //monete=valore_monete_premio-monete;

                    //nuovo sistema:
                    num_pupi=1;
                    if (razza_pupo.Contains("mosche")){num_pupi=2;}
                    
                    if (premio.Contains("plus_monete")){
                        monete=valore_monete_premio-(info_comuni.lista_costo_unita_razza[razza_pupo]);
                        txt_monete="+ "+monete+" gold";
                        txt_monete="+"+monete;
                    }
                    lista_premi_settati_num_pupi.Add(num_cont_premio,num_pupi);
                    lista_premi_settati_monete.Add(num_cont_premio,monete);

                    titolo="+"+num_pupi+" "+info_comuni.lista_classi_nome[classe_pupo]+" "+info_comuni.lista_razza_pupi_nome[razza_pupo]+" lvl "+livello_pupo;
                    descrizione=info_comuni.lista_pupi_descrizione[razza_pupo];

                    string string_temp_img="reward_pupi_2/skeleton-Lv"+livello_pupo+"_";
                    switch (classe_pupo){
                        case "warrior":{string_temp_img+="Warrior";break;}
                        case "arcer":{string_temp_img+="Bow";break;}
                        case "wizard":{string_temp_img+="Wizard";break;}
                    }
                    string_temp_img+="-"+char.ToUpper(info_comuni.lista_razze_totale[razza_pupo][0])+info_comuni.lista_razze_totale[razza_pupo].Substring(1);
                    if (razza_pupo.Contains("formiche")){string_temp_img+="N";}

                    riempi_img_premio(num_cont_premio,string_temp_img);
                    break;
                }
                case "nrazza":{
                    int livello_razza = int.Parse(splitArray[2]);
                    string razza_pupo = splitArray[1];
                    titolo="Unlock "+info_comuni.lista_razza_pupi_nome[razza_pupo]+" (Level "+livello_razza+")";
                    descrizione=info_comuni.lista_pupi_descrizione[razza_pupo];

                    riempi_img_premio(num_cont_premio,"upgrade_nrazza/"+info_comuni.lista_razze_totale[razza_pupo]+"_"+livello_razza);
                    break;
                }
                case "abilita":{
                    string abilita=splitArray[1];
                    int livello_abilita = int.Parse(splitArray[2]);
                    if (livello_abilita==1){
                        titolo="New ability: "+info_comuni.lista_abilita_nome[abilita];
                    } else {
                        titolo=info_comuni.lista_abilita_nome[abilita]+" level "+livello_abilita;
                    }
                    descrizione=info_comuni.lista_abilita_descrizione[abilita];

                    riempi_img_premio(num_cont_premio,"reward_abilita/"+abilita+"_"+livello_abilita);
                    
                    break;
                }
                case "denaro":{
                    monete=0;
                    int livello = int.Parse(splitArray[1]);
                    switch (livello){
                        default:{monete=valore_monete_premio;break;}
                    }
                    txt_monete="+ "+monete+" gold";
                    txt_monete="+"+monete;
                    titolo="Gold!";
                    descrizione="Un grande tributo al morale della truppa!";
                    riempi_img_premio(num_cont_premio,"reward_gold");
                    break;
                }
                case "":{break;}
                default:{
                    print ("non ho trovato questo: "+tipo);break;
                }
            }

            switch (num_cont_premio){
                case 1:{
                    if (txt_monete==""){img_monete_upgrade_1.SetActive(false);} else {img_monete_upgrade_1.SetActive(true);}
                    cont_upgrade_1.SetActive(true);
                    titolo_premio_upgrade_1.SetText(titolo);
                    monete_premio_upgrade_1.SetText(txt_monete);
                    descrizione_premio_upgrade_1.SetText(descrizione);
                    break;
                }
                case 2:{
                    if (txt_monete==""){img_monete_upgrade_2.SetActive(false);} else {img_monete_upgrade_2.SetActive(true);}
                    cont_upgrade_2.SetActive(true);
                    titolo_premio_upgrade_2.SetText(titolo);
                    monete_premio_upgrade_2.SetText(txt_monete);
                    descrizione_premio_upgrade_2.SetText(descrizione);
                    break;
                }
                case 3:{
                    if (txt_monete==""){img_monete_upgrade_3.SetActive(false);} else {img_monete_upgrade_3.SetActive(true);}
                    cont_upgrade_3.SetActive(true);
                    titolo_premio_upgrade_3.SetText(titolo);
                    monete_premio_upgrade_3.SetText(txt_monete);
                    descrizione_premio_upgrade_3.SetText(descrizione);
                    break;
                }
            }
        }
        else {
            switch (num_cont_premio){
                case 1:{cont_upgrade_1.SetActive(false);break;}
                case 2:{cont_upgrade_2.SetActive(false);break;}
                case 3:{cont_upgrade_3.SetActive(false);break;}
            }
        }

    }

    private void riempi_img_premio(int num_cont_premio, string sprite){
        print (sprite);
        Sprite sprite_temp  = Resources.Load<Sprite>(sprite);
        switch (num_cont_premio){
            case 1:{img_premio_upgrade_1.sprite = sprite_temp;break;}
            case 2:{img_premio_upgrade_2.sprite = sprite_temp;break;}
            case 3:{img_premio_upgrade_3.sprite = sprite_temp;break;}
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
            case "hero_damage":{testo="The abilities of your hero that deal damage, do so by +"+((livello+1)*10)+"%";break;}
            case "hero_cooldown":{testo="The cooldown of your abilities is reduced by -"+((livello+1)*10)+"%";break;}
            case "random_unity_1":{testo="Choose one of three randomly selected level 1 units from the unlocked races";break;}
            case "random_spell":{testo="Choose between three new abilities or an existing one at a higher level";break;}
            case "random_race":{testo="Choose from three new races to unlock";break;}
            case "random_unity_2":{testo="Choose one of three randomly selected level 2 units from the unlocked races";break;}
            case "random_unity_3":{testo="Choose one of three randomly selected level 3 units from the unlocked races";break;}
            case "unlock_next_unity_tier":{
                if (tier_unity_sbloccato<2){testo="Unlock the ability to produce level 2 units";}
                else if (tier_unity_sbloccato<3){testo="Unlock the ability to produce level 23 units";}
                else {cont_upgrade_unlock_unity_tier.SetActive(false);}
                break;
            }
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
                    lista_B_upgrade_bottoni[abilita].gameObject.SetActive(false);
                    lista_txt_upgrade_descrizione[abilita].SetText("You have reach the max level for this upgrade!");
                    lista_txt_upgrade_costi[abilita].SetText("Cost: /");
                }
                break;
            }
            case "hero_damage":
            case "hero_cooldown":{
                if (livello>=5){
                    lista_B_upgrade_bottoni[abilita].interactable=false;
                    lista_B_upgrade_bottoni[abilita].gameObject.SetActive(false);
                    lista_txt_upgrade_descrizione[abilita].SetText("You have reach the max level for this upgrade!");
                    lista_txt_upgrade_costi[abilita].SetText("Cost: /");
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
                    lista_B_upgrade_bottoni[attachStat.Key].gameObject.SetActive(false);
                }
            }

            switch (abilita){
                case "unlock_next_unity_tier":{
                    tier_unity_sbloccato++;
                    check_all_button();
                    check_abilita("unlock_next_unity_tier");
                    check_img_nere_upgrade_unity_tier();
                    switch (id_hero){
                        case "regina_formica_nera":{lista_razze_sbloccate.Add("formiche_"+tier_unity_sbloccato,tier_unity_sbloccato);break;}
                    }
                    foreach(KeyValuePair<string,int> attachStat in lista_razze_sbloccate){
                        sblocca_unita_razza(attachStat.Key,attachStat.Value);
                    }
                    check_full_race();
                    riempi_blocchi_testo_unita();
                    break;
                }
                case "random_unity_3":
                case "random_unity_2":
                case "random_unity_1":{
                    string pupo_singolo="";
                    int livello_scelto=1;
                    if (abilita=="random_unity_2"){livello_scelto=2;}
                    else if (abilita=="random_unity_3"){livello_scelto=3;}
                    string premio="";
                    string[] splitArray;
                    int livello_temp=1;
                    lista_random.Clear();
                    lista_premi_settati.Clear();
                    lista_premi_settati_num_pupi.Clear();
                    lista_premi_settati_monete.Clear();
                    foreach(KeyValuePair<string,int> attachStat in lista_razze_sbloccate){
                        splitArray=attachStat.Key.Split(char.Parse("_"));
                        livello_temp=int.Parse(splitArray[1]);
                        if (livello_temp==livello_scelto){
                            print ("posso mettere nel random "+attachStat.Key+" - "+attachStat.Value);
                            pupo_singolo=attachStat.Key;
                            lista_random.Add("pupo-"+pupo_singolo+"-warrior-"+livello_scelto+"-0",1);
                            lista_random.Add("pupo-"+pupo_singolo+"-arcer-"+livello_scelto+"-0",1);
                            lista_random.Add("pupo-"+pupo_singolo+"-wizard-"+livello_scelto+"-0",1);
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

                    pannello_scelta_premio.SetActive(true);
                    pannello_upgrade.SetActive(false);
                    break;
                }
                case "random_spell":{
                    int livello=1;
                    string premio="";
                    lista_random.Clear();
                    lista_premi_settati.Clear();
                    foreach(KeyValuePair<string,string> attachStat in info_comuni.lista_abilita_nome){
                        if (!lista_abilita.ContainsKey(attachStat.Key)){
                            if (!info_comuni.lista_bool_abilita_classe[attachStat.Key]){
                                if (lista_abilita.Count<num_max_abilita){lista_random.Add("abilita-"+attachStat.Key+"-1",1);}
                            }
                        }
                        else {
                            livello=lista_abilita[attachStat.Key]+1;
                            if (livello<4){lista_random.Add("abilita-"+attachStat.Key+"-"+livello,1);}
                        }
                    }
                    switch (lista_random.Count()){
                        case 1:{
                            premio=lista_random.ElementAt(0).Key;
                            crea_premio_upgrade(premio,1);
                            crea_premio_upgrade("",2);
                            crea_premio_upgrade("",3);
                            break;
                        }
                        case 2:{
                            premio=lista_random.ElementAt(0).Key;
                            crea_premio_upgrade(premio,1);
                            lista_random.Remove(premio);
                            premio=lista_random.ElementAt(0).Key;
                            crea_premio_upgrade(premio,2);
                            crea_premio_upgrade("",3);
                            break;
                        }
                        default:{
                            premio=lista_random.ElementAt(Random.Range(0, lista_random.Count)).Key;
                            crea_premio_upgrade(premio,1);
                            lista_random.Remove(premio);
                            premio=lista_random.ElementAt(Random.Range(0, lista_random.Count)).Key;
                            crea_premio_upgrade(premio,2);
                            lista_random.Remove(premio);
                            premio=lista_random.ElementAt(Random.Range(0, lista_random.Count)).Key;
                            crea_premio_upgrade(premio,3);
                            break;
                        }
                    }
                    pannello_scelta_premio.SetActive(true);
                    pannello_upgrade.SetActive(false);
                    break;
                }
                case "random_race":{
                    string premio="";
                    int livello=1;
                    lista_random.Clear();
                    lista_premi_settati.Clear();
                    string[] splitArray;
                    foreach(KeyValuePair<string,string> attachStat in info_comuni.lista_razze_totale){
                        if (!lista_razze_sbloccate.ContainsKey(attachStat.Key)){
                            splitArray=attachStat.Key.Split(char.Parse("_"));
                            livello=int.Parse(splitArray[1]);
                            if (livello<=tier_unity_sbloccato){
                                print ("inserisco la razza "+attachStat.Key+" - di livello "+livello);
                                lista_random.Add("nrazza-"+attachStat.Key+"-"+livello+"-a",1);
                            }
                        }
                    }
                    switch (lista_random.Count()){
                        case 1:{
                            premio=lista_random.ElementAt(0).Key;
                            crea_premio_upgrade(premio,1);
                            crea_premio_upgrade("",2);
                            crea_premio_upgrade("",3);
                            break;
                        }
                        case 2:{
                            premio=lista_random.ElementAt(0).Key;
                            crea_premio_upgrade(premio,1);
                            lista_random.Remove(premio);
                            premio=lista_random.ElementAt(0).Key;
                            crea_premio_upgrade(premio,2);
                            crea_premio_upgrade("",3);
                            break;
                        }
                        default:{
                            premio=lista_random.ElementAt(Random.Range(0, lista_random.Count)).Key;
                            crea_premio_upgrade(premio,1);
                            lista_random.Remove(premio);
                            premio=lista_random.ElementAt(Random.Range(0, lista_random.Count)).Key;
                            crea_premio_upgrade(premio,2);
                            lista_random.Remove(premio);
                            premio=lista_random.ElementAt(Random.Range(0, lista_random.Count)).Key;
                            crea_premio_upgrade(premio,3);
                            break;
                        }
                    }
                    pannello_scelta_premio.SetActive(true);
                    pannello_upgrade.SetActive(false);
                    break;
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
            case "unlock_next_unity_tier":{
                if (tier_unity_sbloccato<2){costo=200;}
                else if (tier_unity_sbloccato<3){costo=500;}
                break;
            }
            case "melee_damage":
            case "distance_damage":
            case "spell_damage":
            case "health":
            case "hero_damage":
            case "hero_cooldown":
            {
                costo=100*(livello+1);
                break;
            }
            case "random_unity_1":{costo=30+(10*livello)-(lista_upgrade_perenni_liv["costi_pupi"]*5);break;}
            case "random_unity_2":{costo=60+(10*livello)-(lista_upgrade_perenni_liv["costi_pupi"]*5);break;}
            case "random_unity_3":{costo=90+(10*livello)-(lista_upgrade_perenni_liv["costi_pupi"]*5);break;}
            case "random_race":{costo=100;break;}
            case "random_spell":{
                costo=100;
                costo-=(lista_upgrade_perenni_liv["costi_abilita"]*5);
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
            tier_unity_sbloccato=int.Parse(node.GetAttribute("tier_unity_sbloccato"));
            try{per_potere_eroe=float.Parse(node.GetAttribute("per_potere_eroe"));} catch{print ("Aspettiamo il prossimo update");}
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
        xml_content="<game id_hero='"+id_hero+"' num_ondata='"+num_ondata+"' denaro='"+denaro+"' tier_unity_sbloccato='"+tier_unity_sbloccato+"' per_potere_eroe='"+per_potere_eroe+"' posizione='game'>";
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

    private void carica_info_partite(){
        foreach(KeyValuePair<string,string> attachStat in info_comuni.lista_upgrade_perenni_nome){
            lista_upgrade_perenni_liv.Add(attachStat.Key,0);
        }

        string xml_content="";
        string path_xml=Application.persistentDataPath + "/info_partite_c.xml";

        XmlDocument xml_game = new XmlDocument ();
        string string_temp=System.IO.File.ReadAllText(path_xml);
        //string_temp=f_comuni.decripta(string_temp, "munimuni");
        xml_game.LoadXml(string_temp);

        string upgrade_temp="";
        int liv_upgrade_temp=0;
        foreach(XmlElement node in xml_game.SelectNodes("info_partite")){
            /*
            num_partite=int.Parse(node.GetAttribute("num_partite"));
            num_gemme=int.Parse(node.GetAttribute("num_gemme"));
            num_gemme_totali=int.Parse(node.GetAttribute("num_gemme_totali"));
            */

            foreach(XmlElement node_2 in node.SelectNodes("upgrade_perenni")){
                foreach(XmlElement node_3 in node_2.SelectNodes("u")){
                    liv_upgrade_temp=int.Parse(node_3.GetAttribute("liv"));
                    upgrade_temp=node_3.InnerText;
                    lista_upgrade_perenni_liv[upgrade_temp]=liv_upgrade_temp;
                }
            }
        }
    }
}
