using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.Text;
using System.Xml; //Needed for XML functionality
using System.IO;
public class init : MonoBehaviour
{
    public info_comuni info_comuni;
    public effetti effetti;
    public TMPro.TextMeshProUGUI txt_desc_abilita;
    public GameObject lista_pupi;
    public GameObject mappa;
    public GameObject pannello_vittoria;
    public GameObject pannello_sconfitta;
    public TMPro.TextMeshProUGUI txt_denaro_guadagnato;
    public TMPro.TextMeshProUGUI txt_ondata_vittoria;
    private int denaro;
    public Dictionary<string, GameObject> lista_obj_pupetti = new Dictionary<string, GameObject>();
    public Dictionary<int, GameObject> lp_totali = new Dictionary<int, GameObject>();
    public Dictionary<int, basic_rule> lp_totali_basic_rule = new Dictionary<int, basic_rule>();
    public Dictionary<int, int> lp_buoni = new Dictionary<int, int>();
    public Dictionary<int, int> lp_cattivi = new Dictionary<int, int>();
    public Dictionary<string, int> lista_pupi_buoni_partenza = new Dictionary<string, int>();
    public GameObject lista_abilita;

    //sezione relativa ai cooldown
    public Dictionary<int, GameObject> lista_abilita_GO = new Dictionary<int, GameObject>();
    public Dictionary<int, Image> lista_abilita_img = new Dictionary<int, Image>();
    public Dictionary<int, Image> lista_abilita_cooldown_img = new Dictionary<int, Image>();
    public Dictionary<int, float> lista_abilita_cooldown_secondi = new Dictionary<int, float>();
    public Dictionary<int, float> lista_abilita_cooldown_secondi_attuale = new Dictionary<int, float>();
    public Dictionary<int, string> lista_abilita_id = new Dictionary<int, string>();
    public Dictionary<int, int> lista_abilita_livello = new Dictionary<int, int>();
    public int int_abilita_scelta=0;
    public int abilita_totali=0;

    private int num_pupi_generati_totali=0;
    private int num_pupi_generati_buoni=0;
    private int num_pupi_generati_cattivi=0;

    private float offset_map_attuale_y=0;

    private bool bool_debug=false;

    private string path;
    private string string_temp;
    private string xml_content;

    public string id_hero="";
    private int i,j,k;
    private bool bool_inizio_partita;
    private float xa=-40,ya=16,y_temp;
    private float xb=-40,yb=-20;
    private float xc=40,yc=16;
    private float xd=40,yd=-20;
    private bool bool_fine_partita=false;
    private int max_pupi_battaglione=20;

    public Dictionary<string, int> lista_upgrade = new Dictionary<string, int>();
    public Dictionary<string, int> lista_razze_sbloccate = new Dictionary<string, int>();

    //da quì in poi le andremo a prendere dalle opzioni della partita in corso
    private int num_ondata=1;
    private int num_battaglione_amico=1;
    private int num_battaglione_nemico=1;
    private string id_arena;

    void Awake(){
        pannello_vittoria.SetActive(false);
        pannello_sconfitta.SetActive(false);
        foreach (Transform child in lista_pupi.transform) {
            lista_obj_pupetti.Add(child.name,child.gameObject);
        }
        int i=0,j=0,k=0;

        lista_upgrade.Add("melee_damage",0);
        lista_upgrade.Add("distance_damage",0);
        lista_upgrade.Add("spell_damage",0);
        lista_upgrade.Add("health",0);
        lista_upgrade.Add("hero_damage",0);
        lista_upgrade.Add("hero_cooldown",0);

        foreach (Transform child in lista_abilita.transform) {
            i++;
            lista_abilita_GO.Add(i,child.gameObject);
            lista_abilita_cooldown_secondi.Add(i,0);
            lista_abilita_cooldown_secondi_attuale.Add(i,0);
            foreach (Transform child_2 in child.gameObject.transform) {
                if (child_2.name=="img_cooldown"){
                    lista_abilita_cooldown_img.Add(i,child_2.gameObject.GetComponent<Image>());
                    setta_cooldown_abilita(i,0f);
                } else if (child_2.name=="img_abilita"){
                    lista_abilita_img.Add(i,child_2.gameObject.GetComponent<Image>());
                }
            }
            lista_abilita_id.Add(i,"");
        }
        abilita_totali=i;

        setta_game_da_file();

        //poi andiamo a prendere i settaggi per ogni cooldown che ha salvato da qualche parte nella partita
        foreach(KeyValuePair<int,string> attachStat in lista_abilita_id){
            if (attachStat.Value!=""){
                lista_abilita_cooldown_secondi[attachStat.Key]=info_comuni.lista_abilita_cooldown[attachStat.Value][lista_abilita_livello[attachStat.Key]];

            } else {
                lista_abilita_GO[attachStat.Key].SetActive(false);
            }
        }

        switch (num_ondata){
            case 1:{for (i=1;i<=10;i++){genera_pupo("coccinella_warrior");}break;}
            case 2:{for (i=1;i<=20;i++){genera_pupo("coccinella_warrior");}break;}
            default:{//da quì generiamo i nemici nemici che ci interessano; Perchè quelli amici lo facciamo da setta_game_da_file
                for (i=1;i<=10;i++){genera_pupo("coccinella_warrior");}
                //genera_pupo("ape_warrior");
                //genera_pupo("ape_arcer");
                //genera_pupo("pupetto_standard_nemico");
                break;
            }
        }
        //a questo punto abbiamo tutti i pupi creati che sono basicamente invisibili...
        i=0;j=0;
        float per_10;
        foreach(KeyValuePair<int,GameObject> attachStat in lp_totali){
            lp_totali_basic_rule[attachStat.Key].int_key_pupo=attachStat.Key;
            if (lp_totali_basic_rule[attachStat.Key].bool_fazione_nemica){
                num_pupi_generati_cattivi++;
                lp_cattivi.Add(num_pupi_generati_cattivi,attachStat.Key);

                j++;
                //lp_totali[attachStat.Key].transform.localPosition = new Vector3(xc, (j*-2)+15, 1f);
                if (j>max_pupi_battaglione){//dobbiamo attivare il prossimo battaglione
                    j=1;
                    num_battaglione_nemico++;
                }
                lp_totali_basic_rule[attachStat.Key].attiva_pupo(num_battaglione_nemico,xc,(j*-2)+15,false);

            } else {
                num_pupi_generati_buoni++;
                lp_buoni.Add(num_pupi_generati_buoni,attachStat.Key);

                i++;
                //lp_totali[attachStat.Key].transform.localPosition = new Vector3(xa, (i*-2)+15, 1f);
                if (i>max_pupi_battaglione){//dobbiamo attivare il prossimo battaglione
                    i=1;
                    num_battaglione_amico++;
                }
                lp_totali_basic_rule[attachStat.Key].attiva_pupo(num_battaglione_amico,xa,(i*-2)+15,false);

                if (lp_totali_basic_rule[attachStat.Key].velocita_proiettile==0){
                    lp_totali_basic_rule[attachStat.Key].danno+=lista_upgrade["melee_damage"];
                }
                else {
                    if (!lp_totali_basic_rule[attachStat.Key].bool_mago){
                        lp_totali_basic_rule[attachStat.Key].danno+=lista_upgrade["distance_damage"];
                    }
                    else {
                        lp_totali_basic_rule[attachStat.Key].danno+=lista_upgrade["spell_damage"];
                    }
                }
                if (lista_upgrade["health"]>0){
                    per_10=lp_totali_basic_rule[attachStat.Key].vitalita_max*lista_upgrade["health"]/10;
                    lp_totali_basic_rule[attachStat.Key].vitalita_max+=per_10;
                }
            }
        }
        txt_desc_abilita.SetText("");
        bool_inizio_partita=true;
        StartCoroutine(start_partita());
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (int_abilita_scelta!=0){
                int_abilita_scelta=0;
                txt_desc_abilita.SetText("");
            }
        }
        if (!bool_inizio_partita){
            if (bool_fine_partita){return;}
        }
        //andiamo a prendere i cooldown e li abbassiamo se c'è ne sono...
        for (int i=1;i<=abilita_totali;i++){
            if (lista_abilita_cooldown_secondi_attuale[i]>0){
                lista_abilita_cooldown_secondi_attuale[i]-=Time.deltaTime;
                if (lista_abilita_cooldown_secondi_attuale[i]>0){
                    setta_cooldown_abilita(i,(lista_abilita_cooldown_secondi_attuale[i]/lista_abilita_cooldown_secondi[i]));
                }
            }
        }

        bool_fine_partita=true;
        foreach(KeyValuePair<int,int> attachStat in lp_buoni){
            if (!lp_totali_basic_rule[attachStat.Value].bool_morto){
                bool_fine_partita=false;
                cerca_prossimo_bersaglio(attachStat.Value);
            }
        }
        if (!bool_inizio_partita){
            if (bool_fine_partita){
                fine_partita("sconfitta");
                return;
            }
        }

        //decommenta il blocco per far muovere anche i nemici
        bool_fine_partita=true;
        foreach(KeyValuePair<int,int> attachStat in lp_cattivi){
            if (!lp_totali_basic_rule[attachStat.Value].bool_morto){
                bool_fine_partita=false;
                cerca_prossimo_bersaglio(attachStat.Value);
            }
        }
        if (!bool_inizio_partita){
            if (bool_fine_partita){
                fine_partita("vittoria");
                return;
            }
        }
    }

    private IEnumerator start_partita(){
        yield return new WaitForSeconds(3);
        bool_inizio_partita=false;
    }

    public void fine_partita(string esito){
        if (esito=="vittoria"){
            int denaro_guadagnato=0;
            if (num_ondata<=5){denaro_guadagnato=30;}
            else if (num_ondata<=10){denaro_guadagnato=50;}
            else if (num_ondata<=15){denaro_guadagnato=70;}
            else if (num_ondata<=20){denaro_guadagnato=100;}
            else {denaro_guadagnato=150;}
            txt_ondata_vittoria.SetText("Stage "+num_ondata+" clear!");
            txt_denaro_guadagnato.SetText("You have earned "+denaro_guadagnato+" gold!");
            foreach(KeyValuePair<int,int> attachStat in lp_buoni){
                if (!lp_totali_basic_rule[attachStat.Value].bool_morto){
                    lp_totali_basic_rule[attachStat.Value].esulta();
                }
            }

            path=Application.persistentDataPath + "/game_c.xml";
            //File.Delete(path);
            num_ondata++;

            xml_content="<game id_hero='"+id_hero+"' num_ondata='"+num_ondata+"'";
            denaro+=denaro_guadagnato;
            xml_content+=" denaro='"+denaro+"'>";

            xml_content+="\n\t<lista_abilita>";
            foreach(KeyValuePair<int,string> attachStat in lista_abilita_id){
                if (attachStat.Value!=""){
                    xml_content+="\n\t\t<a liv='"+lista_abilita_livello[attachStat.Key]+"'>"+attachStat.Value+"</a>";
                }
            }
            xml_content+="\n\t</lista_abilita>";

            xml_content+="\n\t<lista_pupetti>";
            foreach(KeyValuePair<string,int> attachStat in lista_pupi_buoni_partenza){
                xml_content+="\n\t\t<p num='"+attachStat.Value+"'>"+attachStat.Key+"</p>";
            }
            xml_content+="\n\t</lista_pupetti>";
            xml_content+="\n\t<lista_upgrade>";
            foreach(KeyValuePair<string,int> attachStat in lista_upgrade){
                xml_content+="\n\t\t<u liv='"+attachStat.Value+"'>"+attachStat.Key+"</u>";
            }
            xml_content+="\n\t</lista_upgrade>";

            xml_content+="\n\t<lista_razze_sbloccate>";
            foreach(KeyValuePair<string,int> attachStat in lista_razze_sbloccate){
                xml_content+="\n\t\t<r liv='"+attachStat.Value+"'>"+attachStat.Key+"</r>";
            }
            xml_content+="\n\t</lista_razze_sbloccate>";

            xml_content+="\n</game>";

            StreamWriter writer = new StreamWriter(path, false);
            writer.Write(xml_content);
            writer.Close();
            print (xml_content);

            pannello_vittoria.SetActive(true);
        } else {
            print ("vincono i nemici");
        }
    }

    public void next_stage(){
        SceneManager.LoadScene("upgrade");
    }

    public void setta_cooldown_abilita(int abilita, float settaggio){
        lista_abilita_cooldown_img[abilita].fillAmount=settaggio;
    }

    public void click_abilita(GameObject go_abilita){
        if (bool_fine_partita){return;}
        int int_abilita=int.Parse(go_abilita.name.Replace("abilita_",""));
        if (lista_abilita_cooldown_secondi_attuale[int_abilita]<=0){
            int_abilita_scelta=int_abilita;
            switch (int_abilita_scelta){
                default:{
                    string testo="";
                    testo+="Select where want to use "+info_comuni.lista_abilita_nome[lista_abilita_id[int_abilita_scelta]];
                    testo+="\n\nPress ESC to cancel";
                    txt_desc_abilita.SetText(testo);
                    break;
                }
            }
        } else {
            print ("è in cooldown");
        }
    }

    public void mouse_click(GameObject obj, string tipo){
        //print ("mouse: ho cliccato su "+obj.name+" (del tipo "+tipo);
        print (Camera.main.ScreenToWorldPoint(Input.mousePosition)+" - "+Input.mousePosition+" - "+Camera.main.ScreenToWorldPoint(Input.mousePosition).x);
        if (obj.name.Contains("abilita_")){click_abilita(obj);}
        else if (obj.name=="area_cliccabile"){
            if (int_abilita_scelta!=0){
                attiva_abilita_coordinate(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            }

            /*
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.SetParent(mappa.transform);
            cube.transform.localPosition = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 1f);
            */
        }
    }

    public void evoca_pupo(string id_pupo, float xar, float yar){
        genera_pupo(id_pupo);
        if (!lp_totali_basic_rule[num_pupi_generati_totali].bool_fazione_nemica){
            num_pupi_generati_buoni++;  //num_pupi_generati_totali viene uincrementato dentro genera_pupo
            lp_buoni.Add(num_pupi_generati_buoni,num_pupi_generati_totali);
            float per_10;

            //lp_totali[num_pupi_generati_totali].transform.localPosition = new Vector3(xa, (i*-2)+15, 1f);
            lp_totali_basic_rule[num_pupi_generati_totali].attiva_pupo(2,xar,yar,true);

            if (lp_totali_basic_rule[num_pupi_generati_totali].velocita_proiettile==0){
                lp_totali_basic_rule[num_pupi_generati_totali].danno+=lista_upgrade["melee_damage"];
            }
            else {
                if (!lp_totali_basic_rule[num_pupi_generati_totali].bool_mago){
                    lp_totali_basic_rule[num_pupi_generati_totali].danno+=lista_upgrade["distance_damage"];
                }
                else {
                    lp_totali_basic_rule[num_pupi_generati_totali].danno+=lista_upgrade["spell_damage"];
                }
            }
            if (lista_upgrade["health"]>0){
                per_10=lp_totali_basic_rule[num_pupi_generati_totali].vitalita_max*lista_upgrade["health"]/10;
                lp_totali_basic_rule[num_pupi_generati_totali].vitalita_max+=per_10;
            }
        }
        effetti.eff_evocazione_brown(xar,yar);
    }

    public void attiva_abilita_coordinate(float xar, float yar){
        if (int_abilita_scelta!=0){
            print ("attivo l'abilita numero "+int_abilita_scelta+" alle coordinate "+xar+"-"+yar+" ... durata cooldown: "+lista_abilita_cooldown_secondi[int_abilita_scelta]);
            lista_abilita_cooldown_secondi_attuale[int_abilita_scelta]=lista_abilita_cooldown_secondi[int_abilita_scelta];
            setta_cooldown_abilita(int_abilita_scelta,1);
            txt_desc_abilita.SetText("");
            int liv=lista_abilita_livello[int_abilita_scelta];
            switch (lista_abilita_id[int_abilita_scelta]){
                case "evoca_formiche":{
                    float random_x;
                    float random_y;
                    for (int i=1;i<=2*liv;i++){
                        random_x=Random.Range(-1f,2f)+xar;
                        random_y=Random.Range(-1f,2f)+yar;
                        evoca_pupo("formica_warrior",random_x,random_y);
                    }
                    break;
                }
            }


            int_abilita_scelta=0;
            /*
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.SetParent(mappa.transform);
            cube.transform.localPosition = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 1f);
            */
        }
    }

    public void mouse_exit(GameObject obj){
        if (obj.name.Contains("abilita_")){
            if (int_abilita_scelta==0){
                txt_desc_abilita.SetText("");
            }
        }
        //print ("mouse: sono uscito da "+obj.name);
        //switch 
    }
    public void mouse_enter(GameObject obj){
        if (obj.name.Contains("abilita_")){
            if (int_abilita_scelta==0){
                int int_abilita=int.Parse(obj.name.Replace("abilita_",""));
                string testo=info_comuni.lista_abilita_descrizione[lista_abilita_id[int_abilita]];
                txt_desc_abilita.SetText(testo);
            }
        }
    }

    public void cerca_prossimo_bersaglio(int id_attaccante){
        float distanza_difensore=10000;
        float distanza=0;
        int id_pupo_difensore=0;
        if (lp_totali_basic_rule[id_attaccante].id_difensore_mago!=0){
            int id_difensore_mago=lp_totali_basic_rule[id_attaccante].id_difensore_mago;
            lp_totali_basic_rule[id_attaccante].proiettile.transform.position=Vector3.MoveTowards(lp_totali_basic_rule[id_attaccante].proiettile.transform.position, lp_totali_basic_rule[id_difensore_mago].transform.position,lp_totali_basic_rule[id_attaccante].velocita_proiettile * Time.deltaTime);
            if(Vector3.Distance(lp_totali_basic_rule[id_attaccante].proiettile.transform.position, lp_totali_basic_rule[id_difensore_mago].transform.position) < 0.1f){
                calcola_danno_combattimento(id_attaccante,id_difensore_mago);
                lp_totali_basic_rule[id_attaccante].id_difensore_mago=0;
                lp_totali_basic_rule[id_attaccante].proiettile.SetActive(false);
            }
        }

        if (!lp_totali_basic_rule[id_attaccante].bool_fazione_nemica){
            foreach(KeyValuePair<int,int> attachStat in lp_cattivi){
                if (!lp_totali_basic_rule[attachStat.Value].bool_morto){
                    distanza=calcola_distanza_due_pupi(id_attaccante,attachStat.Value);
                    if (distanza<distanza_difensore){
                        distanza_difensore=distanza;
                        id_pupo_difensore=attachStat.Value;
                    }
                }
            }
            if (id_pupo_difensore!=0){
                avvicinati_prossimo_bersaglio(id_attaccante, id_pupo_difensore);
            }
        } else {
            foreach(KeyValuePair<int,int> attachStat in lp_buoni){
                if (!lp_totali_basic_rule[attachStat.Value].bool_morto){
                    distanza=calcola_distanza_due_pupi(id_attaccante,attachStat.Value);
                    if (distanza<distanza_difensore){
                        distanza_difensore=distanza;
                        id_pupo_difensore=attachStat.Value;
                    }
                }
            }
            if (id_pupo_difensore!=0){
                avvicinati_prossimo_bersaglio(id_attaccante, id_pupo_difensore);
            }
        }
    }

    public float calcola_distanza_due_pupi(int id_pupo_a, int id_pupo_b){
        return calcola_distanza(lp_totali[id_pupo_a].transform.position.x,lp_totali[id_pupo_a].transform.position.y,lp_totali[id_pupo_b].transform.position.x,lp_totali[id_pupo_b].transform.position.y);
    }

    public void avvicinati_prossimo_bersaglio(int id_attaccante, int id_difensore){
        if (!lp_totali_basic_rule[id_attaccante].bool_morto){
            if (!lp_totali_basic_rule[id_difensore].bool_morto){
                float distanza=calcola_distanza_due_pupi(id_attaccante,id_difensore);
                lp_totali_basic_rule[id_attaccante].old_x=lp_totali[id_difensore].transform.position.x;
                if (distanza>lp_totali_basic_rule[id_attaccante].distanza_attacco){
                    lp_totali_basic_rule[id_attaccante].stato="move";
                    lp_totali[id_attaccante].transform.position=Vector3.MoveTowards(lp_totali[id_attaccante].transform.position,lp_totali[id_difensore].transform.position,lp_totali_basic_rule[id_attaccante].velocita_movimento * Time.deltaTime);
        
                } else {
                    pupo_attacca_bersaglio(id_attaccante, id_difensore);
                }
            }
        }
    }

    public void pupo_attacca_bersaglio(int id_attaccante, int id_difensore){
        if ((lp_totali_basic_rule[id_attaccante].stato=="idle")||(lp_totali_basic_rule[id_attaccante].stato=="move")){
            float x_att=lp_totali[id_difensore].transform.position.x;
            float y_att=lp_totali[id_difensore].transform.position.y;
            lp_totali_basic_rule[id_attaccante].anim_attacco(x_att,y_att);
            StartCoroutine(fine_mov_attacco(id_attaccante,id_difensore,x_att,y_att));
        }
    }

    public IEnumerator fine_mov_attacco(int id_attaccante, int id_difensore, float x_att, float y_att) {
        yield return new WaitForSeconds(lp_totali_basic_rule[id_attaccante].anim_velocita_attacco);
        if (!lp_totali_basic_rule[id_attaccante].bool_morto){//beh potrebbe capitare che è stato colpito prima che sferrasse l'attaccp finale...
            if (!lp_totali_basic_rule[id_difensore].bool_morto){//beh potrebbe capitare che è stato colpito il suo bersaglio...
                lp_totali_basic_rule[id_attaccante].stato="wait";
                if (lp_totali_basic_rule[id_attaccante].velocita_proiettile==0){
                    float distanza=0;
                    float raggio_sfera_attacco=lp_totali_basic_rule[id_attaccante].raggio_sfera_attacco;
                    foreach(KeyValuePair<int,GameObject> attachStat in lp_totali){
                        if (id_attaccante!=attachStat.Key){
                            if (controlla_punto_attacco(x_att,y_att,lp_totali[attachStat.Key].transform.position.x,lp_totali[attachStat.Key].transform.position.y,raggio_sfera_attacco)){
                                calcola_danno_combattimento(id_attaccante, attachStat.Key);
                            }
                        }
                    }
                } else {
                    if (!lp_totali_basic_rule[id_attaccante].bool_mago){
                        //y_att++;    //sembrerebbe essere un allegro standard per quanto riguarda il colpire al petto
                        lp_totali_basic_rule[id_attaccante].attiva_proiettile(x_att, y_att, id_attaccante, id_difensore);
                    } else {
                        lp_totali_basic_rule[id_attaccante].attiva_proiettile_mago(id_difensore);
                    }
                }
            }
        }
    }

    public void calcola_danno_combattimento(int id_attaccante, int id_difensore){
        float valore_danno=lp_totali_basic_rule[id_attaccante].danno;
        if (lp_totali_basic_rule[id_attaccante].velocita_proiettile==0){
            valore_danno-=lp_totali_basic_rule[id_difensore].armatura_melee;
        } else {
            valore_danno-=lp_totali_basic_rule[id_difensore].armatura_distanza;
        }
        if (valore_danno<0.1f){valore_danno=0.1f;}  //farà sempre un minimo di danno.
        lp_totali_basic_rule[id_difensore].danneggia(valore_danno);
    }

    private bool controlla_punto_attacco(float xor, float yor, float xar, float yar, float distanza){
        float distanza_due_punti=calcola_distanza(xor, yor, xar, yar);
        if (distanza_due_punti<distanza){return true;}
        return false;
    }

    public void genera_pupo(string id_pupo){
        num_pupi_generati_totali++;
        GameObject go_temp;
        go_temp=Instantiate(lista_obj_pupetti[id_pupo]);
        go_temp.transform.SetParent(mappa.transform);
        go_temp.transform.localPosition = new Vector3(0, 0, 1f);
        go_temp.name=id_pupo;
        lp_totali.Add(num_pupi_generati_totali,go_temp);
        lp_totali_basic_rule.Add(num_pupi_generati_totali,go_temp.GetComponent<basic_rule>());
        go_temp.GetComponent<MeshRenderer>().sortingOrder = (num_pupi_generati_totali+2000);
        go_temp.SetActive(false);
    }

    public float calcola_distanza(float xor, float yor, float xar, float yar){
        float distanza=0f;
        float dist_x=Mathf.Abs(xor - xar);
        float dist_y=Mathf.Abs(yor - yar);
        distanza=Mathf.Sqrt((dist_x*dist_x) + (dist_y*dist_y));
        return distanza;
    }

    private void setta_game_da_file(){
        string string_temp="";
        path=Application.persistentDataPath + "/game_c.xml";

        //arrivati a questo punto, il file deve esistere per forza ed andiamo a prendere tutte le cose che serviranno al giocatore.
        XmlDocument xml_game = new XmlDocument ();
        string_temp=System.IO.File.ReadAllText(path);
        //string_temp=f_comuni.decripta(string_temp, "munimuni");
        xml_game.LoadXml(string_temp);

        int num_pupi_temp;
        string tipo_pupo_temp;
        int num_abilita=0;
        foreach(XmlElement node in xml_game.SelectNodes("game")){
            id_hero=node.GetAttribute("id_hero");
            num_ondata=int.Parse(node.GetAttribute("num_ondata"));
            denaro=int.Parse(node.GetAttribute("denaro"));
            foreach(XmlElement node_2 in node.SelectNodes("lista_abilita")){
                foreach(XmlElement node_3 in node_2.SelectNodes("a")){
                    num_abilita++;
                    lista_abilita_id[num_abilita]=node_3.InnerText;
                    lista_abilita_livello[num_abilita]=int.Parse(node_3.GetAttribute("liv"));
                }
            }
            foreach(XmlElement node_2 in node.SelectNodes("lista_pupetti")){
                foreach(XmlElement node_3 in node_2.SelectNodes("p")){
                    num_pupi_temp=int.Parse(node_3.GetAttribute("num"));
                    tipo_pupo_temp=node_3.InnerText;
                    print ("dovrei avere "+num_pupi_temp+" del tipo "+tipo_pupo_temp);
                    lista_pupi_buoni_partenza.Add(tipo_pupo_temp,num_pupi_temp);            //così almeno c'è li abbiamo e buonanotte per quando salviamo...

                    for (i=1;i<=num_pupi_temp;i++){
                        genera_pupo(tipo_pupo_temp);
                    }
                }
            }
            foreach(XmlElement node_2 in node.SelectNodes("lista_upgrade")){
                foreach(XmlElement node_3 in node_2.SelectNodes("u")){
                    lista_upgrade[node_3.InnerText]=int.Parse(node_3.GetAttribute("liv"));
                }
            }
            foreach(XmlElement node_2 in node.SelectNodes("lista_razze_sbloccate")){
                foreach(XmlElement node_3 in node_2.SelectNodes("r")){
                    lista_razze_sbloccate[node_3.InnerText]=int.Parse(node_3.GetAttribute("liv"));
                }
            }
        }
    }
}
