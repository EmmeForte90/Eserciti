using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.Text;
using System.Xml; //Needed for XML functionality
using System.IO;

public class mainmenu : MonoBehaviour
{
    public info_comuni info_comuni;
    public TMPro.TextMeshProUGUI txt_nome_eroe;
    public TMPro.TextMeshProUGUI txt_descrizione_eroe;
    public TMPro.TextMeshProUGUI txt_nome_abilita;
    public TMPro.TextMeshProUGUI txt_descrizione_abilita;
    public GameObject lista_eroi;
    public Dictionary<string, GameObject> lista_obj_eroi = new Dictionary<string, GameObject>();
    private string id_eroe_scelto="";
    private Dictionary<string, int> lista_pupetti = new Dictionary<string, int>();
    private Dictionary<string, int> lista_abilita = new Dictionary<string, int>();
    private Dictionary<string, int> livelli_upgrade = new Dictionary<string, int>();
    private Dictionary<string, int> lista_razze_sbloccate = new Dictionary<string, int>();

    public GameObject sch_sel_personaggio;
    public GameObject sch_mainmenu;
    private int denaro=0;
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1920, 1080, true);

        foreach (Transform child in lista_eroi.transform) {
            lista_obj_eroi.Add(child.name,child.gameObject);
        }

        nascondi_eroi();
    }

    public void new_game(){
        sch_mainmenu.SetActive(false);
        sch_sel_personaggio.SetActive(true);
    }

    private void nascondi_eroi(){
        foreach(KeyValuePair<string,GameObject> attachStat in lista_obj_eroi){
            attachStat.Value.SetActive(false);
        }
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            click_eroe("regina_formica_nera");
            inizia_nuova_partita();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)){
            click_eroe("re_mosca");
            inizia_nuova_partita();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)){
            click_eroe("regina_ape");
            inizia_nuova_partita();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)){
            click_eroe("regina_ragno");
            inizia_nuova_partita();
        }
    }

    public void click_eroe(string id_eroe){
        string nome="";
        string descrizione="";
        string id_abilita="";

        id_eroe_scelto=id_eroe;
        lista_pupetti.Clear();
        lista_abilita.Clear();
        lista_razze_sbloccate.Clear();
        switch (id_eroe){
            case "regina_formica_nera":{
                id_abilita="evoca_formiche";
                nome="Black Ant";
                descrizione="Sinonimo d'invasione! Sembrano sempre poche ed innocenti ed invece...";
                lista_pupetti.Add("formica_warrior",6);
                lista_pupetti.Add("formica_arcer",2);
                lista_razze_sbloccate.Add("formiche",1);
                
                denaro=30;
                break;
            }
            case "re_mosca":{
                id_abilita="mosche_fastidiose";
                nome="King Fly";
                descrizione="Una dozzina sono fastidiose. Una dozzina di dozzine diventano incontrollabili.";
                lista_pupetti.Add("mosca_warrior",10);
                lista_pupetti.Add("mosca_arcer",6);
                lista_pupetti.Add("mosca_wizard",2);
                lista_razze_sbloccate.Add("mosche",1);
                break;
            }
            case "regina_ape":{
                id_abilita="miele";
                nome="Queen Bee";
                descrizione="Un sacrificio è salvare poco. Un intero sacrificio è salvare la regina.";
                lista_pupetti.Add("ape_warrior",4);
                lista_pupetti.Add("ape_arcer",1);
                lista_razze_sbloccate.Add("api",1);
                break;
            }
            case "regina_ragno":{
                id_abilita="ragnatele";
                nome="Queen Spider";
                descrizione="Non vuoi rilassarti? Ci pensano le loro ragnatele";
                lista_pupetti.Add("spider_warrior",4);
                lista_pupetti.Add("spider_arcer",2);
                lista_pupetti.Add("spider_wizard",1);
                lista_razze_sbloccate.Add("ragnetti",1);
                break;
            }
            case "re_cavalletta":{
                id_abilita="velocita";
                nome="King Grasshopper";
                descrizione="Saltellano veloci e a volte incontrollabili";
                lista_pupetti.Add("cavalletta_warrior",6);
                lista_pupetti.Add("cavalletta_arcer",2);
                lista_razze_sbloccate.Add("cavallette",1);
                break;
            }
            case "re_scarabeo":{
                id_abilita="armatura";
                nome="King Beetle";
                descrizione="Non sono solo semplici e nobili insetti. Prova a mettere un dito tra le loro chele";
                lista_pupetti.Add("scarabeo_warrior",8);
                lista_razze_sbloccate.Add("scarabei",1);
                break;
            }
        }

        lista_abilita.Add(id_abilita,1);
        txt_nome_eroe.SetText(nome);
        txt_descrizione_eroe.SetText(descrizione);

        txt_nome_abilita.SetText(info_comuni.lista_abilita_nome[id_abilita]);
        txt_descrizione_abilita.SetText(info_comuni.lista_abilita_descrizione[id_abilita]);

        nascondi_eroi();
        lista_obj_eroi[id_eroe].SetActive(true);
    }

    public void inizia_nuova_partita(){
        denaro=30;
        livelli_upgrade.Add("melee_damage",0);
        livelli_upgrade.Add("distance_damage",0);
        livelli_upgrade.Add("spell_damage",0);
        livelli_upgrade.Add("health",0);
        livelli_upgrade.Add("hero_damage",0);
        livelli_upgrade.Add("hero_cooldown",0);
        livelli_upgrade.Add("random_unity",0);
        livelli_upgrade.Add("random_spell",0);
        livelli_upgrade.Add("food",0);

        //magari fare uno switch a seconda dell'eroe scelto cosicchè da poter scegliere in santa pace poi i vari livelli di upgrade ed anche per il denaro scelto.

        string xml_content="";
        string path_xml=Application.persistentDataPath + "/game_c.xml";
        File.Delete(path_xml);  //eh si, perchè tanto dobbiamo sempre ricrearlo...

        xml_content="";
        xml_content="<game id_hero='"+id_eroe_scelto+"' num_ondata='1' denaro='"+denaro+"'>";
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
        foreach(KeyValuePair<string,int> attachStat in livelli_upgrade){
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

        print (xml_content);

        SceneManager.LoadScene("game");
    }
}
