using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class upgrade : MonoBehaviour
{   
    public TMPro.TextMeshProUGUI txt_denaro;
    private int denaro;

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


    private Dictionary<string, TMPro.TextMeshProUGUI> lista_txt_upgrade_costi = new Dictionary<string, TMPro.TextMeshProUGUI>();
    private Dictionary<string, Button> lista_B_upgrade_bottoni = new Dictionary<string, Button>();
    private Dictionary<string, TMPro.TextMeshProUGUI> lista_txt_upgrade_descrizione = new Dictionary<string, TMPro.TextMeshProUGUI>();

    private Dictionary<string, int> livelli_attuali_abilita = new Dictionary<string, int>();

    // Start is called before the first frame update
    void Start(){
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
            livelli_attuali_abilita.Add(attachStat.Key,0);
        }

        //da quì, andremo a prendere da dove si trovano, tutte le altre informazioni
        denaro=20800;    //in verità lo andremo a prendere dove si trova...

        //settaggi iniziali
        foreach(KeyValuePair<string,Button> attachStat in lista_B_upgrade_bottoni){
            check_abilita(attachStat.Key);
        }
        txt_denaro.SetText(denaro.ToString());
    }

    public void check_abilita(string abilita){
        int livello=livelli_attuali_abilita[abilita];
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
            livelli_attuali_abilita[abilita]++;
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
        int livello=livelli_attuali_abilita[abilita];
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

    public void salva_e_continua(){
        SceneManager.LoadScene("game");
    }
}
