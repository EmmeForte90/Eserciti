using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using System.Text;
using System.Xml; //Needed for XML functionality
using System.IO;

public class upgrade : MonoBehaviour
{   
    public TMPro.TextMeshProUGUI txt_next_stage;
    public TMPro.TextMeshProUGUI txt_denaro;
    private int denaro;
    private string path_xml;
    private string xml_content;
    private string id_hero;
    private int num_ondata;
    private int i;

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

    // Start is called before the first frame update
    void Start(){
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

        num_ondata++;
        txt_next_stage.SetText("Next: "+num_ondata);

        //settaggi iniziali
        foreach(KeyValuePair<string,Button> attachStat in lista_B_upgrade_bottoni){
            check_abilita(attachStat.Key);
        }
        txt_denaro.SetText(denaro.ToString());
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
                    print ("dovrei avere "+abilita_temp+" di livello "+liv_abilita_temp);

                    lista_abilita.Add(abilita_temp,liv_abilita_temp);
                }
            }
            foreach(XmlElement node_2 in node.SelectNodes("lista_pupetti")){
                foreach(XmlElement node_3 in node_2.SelectNodes("p")){
                    num_pupi_temp=int.Parse(node_3.GetAttribute("num"));
                    tipo_pupo_temp=node_3.InnerText;
                    print ("dovrei avere "+num_pupi_temp+" del tipo "+tipo_pupo_temp);

                    lista_pupetti.Add(tipo_pupo_temp,num_pupi_temp);
                }
            }
            foreach(XmlElement node_2 in node.SelectNodes("lista_upgrade")){
                foreach(XmlElement node_3 in node_2.SelectNodes("u")){
                    livelli_attuali_upgrade[node_3.InnerText]=int.Parse(node_3.GetAttribute("liv"));
                }
            }
        }
    }

    public void salva_e_continua(){
        string string_temp="";
        path_xml=Application.persistentDataPath + "/game_c.xml";
        File.Delete(path_xml);  //eh si, perchè tanto dobbiamo sempre ricrearlo...

        xml_content="";
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
        xml_content+="\n</game>";

        StreamWriter writer = new StreamWriter(path_xml, false);
        writer.Write(xml_content);
        writer.Close();

        //print (xml_content);

        SceneManager.LoadScene("game");
    }
}
