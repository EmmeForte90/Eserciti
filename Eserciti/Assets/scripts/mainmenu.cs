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
    public GameObject sch_upgrade_perenni;
    private int denaro=0;
    private int num_gemme=0;
    private int num_gemme_totali=0;
    private int num_partite=0;

    private Dictionary<string, int> lista_upgrade_perenni_liv = new Dictionary<string, int>();
    public GameObject cont_singoli_upgrade;
    //public Dictionary<string, TMPro.TextMeshProUGUI> lista_txt_nome_upgrade_perenne = new Dictionary<string, TMPro.TextMeshProUGUI>();    //il nome non cambierà mai
    public Dictionary<string, TMPro.TextMeshProUGUI> lista_txt_descrizione_upgrade_perenne = new Dictionary<string, TMPro.TextMeshProUGUI>();
    public Dictionary<string, TMPro.TextMeshProUGUI> lista_txt_costo_upgrade_perenne = new Dictionary<string, TMPro.TextMeshProUGUI>();
    public Dictionary<string, GameObject> lista_obj_cont_num_romani_img_upgrade_perenne = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> lista_obj_cont_num_romani_txt_upgrade_perenne = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> lista_obj_bottone_upgrade_perenne = new Dictionary<string, GameObject>();
    public Dictionary<string, Button> lista_btn_bottone_upgrade_perenne = new Dictionary<string, Button>();
    public TMPro.TextMeshProUGUI txt_num_gemme;

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1920, 1080, true);

        carica_info_partite();

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
        if (Input.GetKeyDown(KeyCode.Escape)){
            torna_indietro();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1)){
            click_eroe("regina_formica_nera");
            inizia_nuova_partita();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)){
            click_eroe("re_mosca");
            inizia_nuova_partita();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)){
            click_eroe("regina_ape");
            inizia_nuova_partita();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4)){
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
                lista_pupetti.Add("formica_warrior_1",4);
                lista_pupetti.Add("formica_arcer_1",2);
                lista_razze_sbloccate.Add("formiche_1",1);
                
                denaro=30;
                break;
            }
            case "re_mosca":{
                id_abilita="mosche_fastidiose";
                nome="King Fly";
                descrizione="Una dozzina sono fastidiose. Una dozzina di dozzine diventano incontrollabili.";
                lista_pupetti.Add("mosca_warrior_1",10);
                lista_pupetti.Add("mosca_arce_1r",6);
                lista_pupetti.Add("mosca_wizard_1",2);
                lista_razze_sbloccate.Add("mosche_1",1);
                break;
            }
            case "regina_ape":{
                id_abilita="miele";
                nome="Queen Bee";
                descrizione="Un sacrificio è salvare poco. Un intero sacrificio è salvare la regina.";
                lista_pupetti.Add("ape_warrior_1",4);
                lista_pupetti.Add("ape_arcer_1",1);
                lista_razze_sbloccate.Add("api_1",1);
                break;
            }
            case "regina_ragno":{
                id_abilita="ragnatele";
                nome="Queen Spider";
                descrizione="Non vuoi rilassarti? Ci pensano le loro ragnatele";
                lista_pupetti.Add("ragnetto_warrior_1",4);
                lista_pupetti.Add("ragnetto_arcer_1",2);
                lista_pupetti.Add("ragnetto_wizard_1",1);
                lista_razze_sbloccate.Add("ragnetti_1",1);
                break;
            }
            case "re_cavalletta":{
                id_abilita="velocita";
                nome="King Grasshopper";
                descrizione="Saltellano veloci e a volte incontrollabili";
                lista_pupetti.Add("cavalletta_warrior_1",6);
                lista_pupetti.Add("cavalletta_arcer_1",2);
                lista_razze_sbloccate.Add("cavallette_1",1);
                break;
            }
            case "re_scarabeo":{
                id_abilita="armatura";
                nome="King Beetle";
                descrizione="Non sono solo semplici e nobili insetti. Prova a mettere un dito tra le loro chele";
                lista_pupetti.Add("scarabeo_warrior_1",8);
                lista_razze_sbloccate.Add("scarabei_1",1);
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

    private void disattiva_pannelli(){
        sch_sel_personaggio.SetActive(false);
        sch_upgrade_perenni.SetActive(false);
        sch_mainmenu.SetActive(false);
    }

    public void attiva_pannello(string pannello){
        disattiva_pannelli();
    }

    public void torna_indietro(){
        if ((sch_sel_personaggio.activeSelf)||(sch_upgrade_perenni.activeSelf)){
            disattiva_pannelli();
            sch_mainmenu.SetActive(true);
        }
    }

    public void continue_game(){
        string string_temp="";
        string path_xml=Application.persistentDataPath + "/game_c.xml";

        XmlDocument xml_game = new XmlDocument ();
        string_temp=System.IO.File.ReadAllText(path_xml);
        //string_temp=f_comuni.decripta(string_temp, "munimuni");
        xml_game.LoadXml(string_temp);

        foreach(XmlElement node in xml_game.SelectNodes("game")){
            SceneManager.LoadScene(node.GetAttribute("posizione"));
        }
    }

    public void pannello_upgrade_perenni(){
        sch_sel_personaggio.SetActive(false);
        sch_mainmenu.SetActive(false);
        sch_upgrade_perenni.SetActive(true);
    }

    public void btn_compra_upgrade_perenne(GameObject GO_upgrade){
        string upgrade=GO_upgrade.name;
        int livello=lista_upgrade_perenni_liv[upgrade];
        int costo=info_comuni.lista_upgrade_perenni_costi[upgrade][livello+1];
        num_gemme-=costo;
        lista_upgrade_perenni_liv[upgrade]++;
        txt_num_gemme.SetText(num_gemme.ToString());
        aggiorna_upgrade_perenne(upgrade);
        salva_file_info_partite();
    }

    private void salva_file_info_partite(){
        string xml_content="";
        string path_xml=Application.persistentDataPath + "/info_partite_c.xml";
        xml_content="<info_partite num_partite='"+num_partite+"' num_gemme='"+num_gemme+"' num_gemme_totali='"+num_gemme_totali+"'>";
        xml_content+="\n\t<upgrade_perenni>";
        foreach(KeyValuePair<string,int> attachStat in lista_upgrade_perenni_liv){
            xml_content+="\n\t\t<u liv='"+attachStat.Value+"'>"+attachStat.Key+"</u>";
        }
        xml_content+="\n\t</upgrade_perenni>";
        xml_content+="\n</info_partite>";


        //print (xml_content);
        StreamWriter writer = new StreamWriter(path_xml, false);
        writer.Write(xml_content);
        writer.Close();
    }

    private void carica_info_partite(){
        foreach(KeyValuePair<string,string> attachStat in info_comuni.lista_upgrade_perenni_nome){
            lista_upgrade_perenni_liv.Add(attachStat.Key,0);
        }

        string xml_content="";
        string path_xml=Application.persistentDataPath + "/info_partite_c.xml";
        //File.Delete(path_xml);  //stiamo in pieno e totale debug
        if (!System.IO.File.Exists(path_xml)){//se NON esiste questo file, vuol dire che è la prima volta che gioca a questo gioco
            xml_content="<info_partite num_partite='0' num_gemme='0' num_gemme_totali='0'>";
            xml_content+="\n\t<upgrade_perenni>";
            foreach(KeyValuePair<string,int> attachStat in lista_upgrade_perenni_liv){
                xml_content+="\n\t\t<u liv='0'>"+attachStat.Key+"</u>";
            }
            xml_content+="\n\t</upgrade_perenni>";
            xml_content+="\n</info_partite>";


            //print (xml_content);
            StreamWriter writer = new StreamWriter(path_xml, false);
            writer.Write(xml_content);
            writer.Close();
        }

        XmlDocument xml_game = new XmlDocument ();
        string string_temp=System.IO.File.ReadAllText(path_xml);
        //string_temp=f_comuni.decripta(string_temp, "munimuni");
        xml_game.LoadXml(string_temp);

        string upgrade_temp="";
        int liv_upgrade_temp=0;
        foreach(XmlElement node in xml_game.SelectNodes("info_partite")){
            num_partite=int.Parse(node.GetAttribute("num_partite"));
            num_gemme=int.Parse(node.GetAttribute("num_gemme"));
            num_gemme_totali=int.Parse(node.GetAttribute("num_gemme_totali"));

            foreach(XmlElement node_2 in node.SelectNodes("upgrade_perenni")){
                foreach(XmlElement node_3 in node_2.SelectNodes("u")){
                    liv_upgrade_temp=int.Parse(node_3.GetAttribute("liv"));
                    upgrade_temp=node_3.InnerText;
                    lista_upgrade_perenni_liv[upgrade_temp]=liv_upgrade_temp;
                }
            }
        }

        foreach (Transform g in cont_singoli_upgrade.transform){
            upgrade_temp=g.name;
            liv_upgrade_temp=lista_upgrade_perenni_liv[upgrade_temp];
            foreach (Transform g2 in g.transform){
                switch (g2.name){
                    case "nome_upgrade":{//mettiamolo diretto perchè il nome non cambierà mai
                        g2.transform.GetComponent<TMPro.TextMeshProUGUI>().SetText(info_comuni.lista_upgrade_perenni_nome[upgrade_temp]);
                        break;
                    }
                    case "descrizione_upgrade":{
                        lista_txt_descrizione_upgrade_perenne.Add(upgrade_temp,g2.transform.GetComponent<TMPro.TextMeshProUGUI>());
                        //g2.transform.GetComponent<TMPro.TextMeshProUGUI>().SetText(info_comuni.lista_upgrade_perenni_descrizione[upgrade_temp][liv_upgrade_temp+1]);
                        break;
                    }
                    case "costo_upgrade":{
                        lista_txt_costo_upgrade_perenne.Add(upgrade_temp,g2.transform.GetComponent<TMPro.TextMeshProUGUI>());
                        //g2.transform.GetComponent<TMPro.TextMeshProUGUI>().SetText("Cost:\n"+info_comuni.lista_upgrade_perenni_costi[upgrade_temp][liv_upgrade_temp+1].ToString());
                        break;
                    }
                    case "cont_num_romani_img_upgrade":{
                        lista_obj_cont_num_romani_img_upgrade_perenne.Add(upgrade_temp,g2.gameObject);
                        break;
                    }
                    case "cont_num_romani_txt_upgrade":{
                        lista_obj_cont_num_romani_txt_upgrade_perenne.Add(upgrade_temp,g2.gameObject);
                        break;
                    }
                    case "bottone_compra":{
                        lista_obj_bottone_upgrade_perenne.Add(upgrade_temp,g2.gameObject);
                        lista_btn_bottone_upgrade_perenne.Add(upgrade_temp,g2.gameObject.GetComponent<Button>());
                        break;
                    }
                }
            }
        }
        foreach(KeyValuePair<string,string> attachStat in info_comuni.lista_upgrade_perenni_nome){
            aggiorna_upgrade_perenne(attachStat.Key);
        }
        txt_num_gemme.SetText(num_gemme.ToString());
    }

    private void aggiorna_upgrade_perenne(string upgrade){
        int livello=lista_upgrade_perenni_liv[upgrade];
        if (livello<info_comuni.lista_upgrade_perenni_max_level[upgrade]){
            lista_txt_descrizione_upgrade_perenne[upgrade].SetText(info_comuni.lista_upgrade_perenni_descrizione[upgrade][livello+1]);
            lista_txt_costo_upgrade_perenne[upgrade].SetText("Cost:\n"+info_comuni.lista_upgrade_perenni_costi[upgrade][livello+1].ToString());
            if (info_comuni.lista_upgrade_perenni_costi[upgrade][livello+1]>num_gemme){
                lista_btn_bottone_upgrade_perenne[upgrade].interactable=false;
            }
        } else {
            lista_txt_descrizione_upgrade_perenne[upgrade].SetText("Hai raggiunto il livello massimo");
            lista_txt_costo_upgrade_perenne[upgrade].SetText("");
            lista_obj_bottone_upgrade_perenne[upgrade].SetActive(false);
        }

        string[] splitArray;
        int num_romano;

        foreach (Transform child in lista_obj_cont_num_romani_img_upgrade_perenne[upgrade].transform){
            splitArray=child.name.Split(char.Parse("_"));
            num_romano=int.Parse(splitArray[3]);

            if (num_romano>info_comuni.lista_upgrade_perenni_max_level[upgrade]){
                child.gameObject.SetActive(false);
            }
        }
        foreach (Transform child in lista_obj_cont_num_romani_txt_upgrade_perenne[upgrade].transform){
            splitArray=child.name.Split(char.Parse("_"));
            num_romano=int.Parse(splitArray[3]);

            if (num_romano>info_comuni.lista_upgrade_perenni_max_level[upgrade]){
                child.gameObject.SetActive(false);
            }
            else {
                if (num_romano>livello){
                    child.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.1f, 0.1f, 0.1f, 1);
                } else {
                    child.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1, 0.8f, 0f, 1);
                }
            }   
        }
    }

    public void inizia_nuova_partita(){
        denaro=0;
        livelli_upgrade.Add("melee_damage",0);
        livelli_upgrade.Add("distance_damage",0);
        livelli_upgrade.Add("spell_damage",0);
        livelli_upgrade.Add("health",0);
        livelli_upgrade.Add("hero_damage",0);
        livelli_upgrade.Add("hero_cooldown",0);
        livelli_upgrade.Add("random_unity_1",0);
        livelli_upgrade.Add("random_spell",0);
        livelli_upgrade.Add("random_race",0);
        livelli_upgrade.Add("random_unity_2",0);
        livelli_upgrade.Add("random_unity_3",0);
        livelli_upgrade.Add("food",0);

        //magari fare uno switch a seconda dell'eroe scelto cosicchè da poter scegliere in santa pace poi i vari livelli di upgrade ed anche per il denaro scelto.

        string xml_content="";
        string path_xml=Application.persistentDataPath + "/game_c.xml";
        File.Delete(path_xml);  //eh si, perchè tanto dobbiamo sempre ricrearlo...

        xml_content="";
        xml_content="<game id_hero='"+id_eroe_scelto+"' num_ondata='1' denaro='"+denaro+"' tier_unity_sbloccato='1' posizione='game'>";
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
