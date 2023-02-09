using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Text;
using System.Xml; //Needed for XML functionality
using System.IO;
public class init : MonoBehaviour
{
    public GameObject lista_pupi;
    public GameObject mappa;
    public Dictionary<string, GameObject> lista_obj_pupetti = new Dictionary<string, GameObject>();
    public Dictionary<int, GameObject> lp_totali = new Dictionary<int, GameObject>();
    public Dictionary<int, basic_rule> lp_totali_basic_rule = new Dictionary<int, basic_rule>();
    public Dictionary<int, int> lp_buoni = new Dictionary<int, int>();
    public Dictionary<int, int> lp_cattivi = new Dictionary<int, int>();
    public GameObject lista_abilita;

    //sezione relativa ai cooldown
    public Dictionary<int, GameObject> lista_abilita_GO = new Dictionary<int, GameObject>();
    public Dictionary<int, Image> lista_abilita_img = new Dictionary<int, Image>();
    public Dictionary<int, Image> lista_abilita_cooldown_img = new Dictionary<int, Image>();
    public Dictionary<int, float> lista_abilita_cooldown_secondi = new Dictionary<int, float>();
    public Dictionary<int, float> lista_abilita_cooldown_secondi_attuale = new Dictionary<int, float>();
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
    private float xa=-30,ya=16,y_temp;
    private float xb=-30,yb=-20;
    private float xc=30,yc=16;
    private float xd=30,yd=-20;
    private bool bool_fine_partita=false;
    private int max_pupi_battaglione=20;

    public Dictionary<string, int> lista_upgrade = new Dictionary<string, int>();

    //da quì in poi le andremo a prendere dalle opzioni della partita in corso
    private int num_ondata=1;
    private int num_battaglione_amico=1;
    private int num_battaglione_nemico=1;
    private string id_arena;

    void Awake(){
        foreach (Transform child in lista_pupi.transform) {
            lista_obj_pupetti.Add(child.name,child.gameObject);
        }
        i=0;

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
        }
        abilita_totali=i;

        setta_game_da_file();

        //poi andiamo a prendere i settaggi per ogni cooldown che ha salvato da qualche parte nella partita
        lista_abilita_cooldown_secondi[1]=4;

        switch (num_ondata){
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
        foreach(KeyValuePair<int,GameObject> attachStat in lp_totali){
            lp_totali_basic_rule[attachStat.Key].int_key_pupo=attachStat.Key;
            //lp_totali_basic_rule[attachStat.Key].attiva_pupo();             //il tutto basicamente dipenderebbe da quì...
            if (lp_totali_basic_rule[attachStat.Key].bool_fazione_nemica){
                num_pupi_generati_cattivi++;
                lp_cattivi.Add(num_pupi_generati_cattivi,attachStat.Key);

                j++;
                //lp_totali[attachStat.Key].transform.localPosition = new Vector3(xc, (j*-2)+15, 1f);
                if (j>max_pupi_battaglione){//dobbiamo attivare il prossimo battaglione
                    j=1;
                    num_battaglione_nemico++;
                }
                lp_totali_basic_rule[attachStat.Key].attiva_pupo(num_battaglione_nemico,xc,(j*-2)+15);

            } else {
                num_pupi_generati_buoni++;
                lp_buoni.Add(num_pupi_generati_buoni,attachStat.Key);

                i++;
                //lp_totali[attachStat.Key].transform.localPosition = new Vector3(xa, (i*-2)+15, 1f);
                if (i>max_pupi_battaglione){//dobbiamo attivare il prossimo battaglione
                    i=1;
                    num_battaglione_amico++;
                }
                lp_totali_basic_rule[attachStat.Key].attiva_pupo(num_battaglione_amico,xa,(i*-2)+15);
            }
        }

        /*
        foreach(KeyValuePair<int,int> attachStat in lp_buoni){
            i++;
            lp_totali[attachStat.Value].transform.localPosition = new Vector3(xa, (i*2)-5, 1f);
            lp_totali_basic_rule[attachStat.Key].attiva_pupo();
            
        }
        foreach(KeyValuePair<int,int> attachStat in lp_cattivi){
            //lp_totali[attachStat.Value].transform.localPosition = new Vector3(15, (attachStat.Key*-2)+5, 1f);
            lp_totali[attachStat.Value].transform.localPosition = new Vector3(xc, (attachStat.Key*2)+5, 1f);
        }
        */
        bool_inizio_partita=true;
        StartCoroutine(start_partita());
    }

    // Update is called once per frame
    void Update(){
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
            print ("vince il giocatore");
        } else {
            print ("vincono i nemici");
        }
    }

    public void setta_cooldown_abilita(int abilita, float settaggio){
        lista_abilita_cooldown_img[abilita].fillAmount=settaggio;
    }

    public void click_abilita(GameObject go_abilita){
        int int_abilita=int.Parse(go_abilita.name.Replace("abilita_",""));
        if (lista_abilita_cooldown_secondi_attuale[int_abilita]<=0){
            int_abilita_scelta=int_abilita;
            print ("stò cercando di attivare l'abilità "+int_abilita_scelta+" ... deve cliccare sulla mappa");
        } else {
            print ("è in cooldown");
        }
    }

    public void mouse_click(GameObject obj, string tipo){
        print ("mouse: ho cliccato su "+obj.name+" (del tipo "+tipo);
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

    public void attiva_abilita_coordinate(float xar, float yar){
        if (int_abilita_scelta!=0){
            print ("attivo l'abilita numero "+int_abilita_scelta+" alle coordinate "+xar+"-"+yar+" ... durata cooldown: "+lista_abilita_cooldown_secondi[int_abilita_scelta]);
            lista_abilita_cooldown_secondi_attuale[int_abilita_scelta]=lista_abilita_cooldown_secondi[int_abilita_scelta];
            setta_cooldown_abilita(int_abilita_scelta,1);
            int_abilita_scelta=0;

            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.SetParent(mappa.transform);
            cube.transform.localPosition = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 1f);
        }
    }

    public void mouse_exit(GameObject obj){
        //print ("mouse: sono uscito da "+obj.name);
    }
    public void mouse_enter(GameObject obj){
        //print ("mouse: sono entrato su "+obj.name);
    }

    public void cerca_prossimo_bersaglio(int id_attaccante){
        float distanza_difensore=10000;
        float distanza=0;
        int id_pupo_difensore=0;
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
                    spostamento_pupo_random(id_attaccante, id_difensore);
                } else {
                    pupo_attacca_bersaglio(id_attaccante, id_difensore);
                }
            }
        }
    }

    public void spostamento_pupo_random(int id_attaccante, int id_difensore){
        lp_totali[id_attaccante].transform.position=Vector3.MoveTowards(lp_totali[id_attaccante].transform.position,lp_totali[id_difensore].transform.position,lp_totali_basic_rule[id_attaccante].velocita_movimento * Time.deltaTime);
        if (!lp_totali_basic_rule[id_attaccante].bool_movimento_cerchio){
            int random=Random.Range(0, 10);
            if (random<4){
                lp_totali_basic_rule[id_attaccante].inizia_percorso_arco();
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
                    //y_att++;    //sembrerebbe essere un allegro standard per quanto riguarda il colpire al petto
                    lp_totali_basic_rule[id_attaccante].attiva_proiettile(x_att, y_att, id_attaccante);
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
        //File.Delete(path);
        if (!System.IO.File.Exists(path)){//se NON esiste questo file, vuol dire che stà iniziando una partita da 0
            int denaro;
            if (!PlayerPrefs.HasKey("new_game_id_hero")){
                PlayerPrefs.SetString("new_game_id_hero","formica_nera");
            }
            if (bool_debug){
                PlayerPrefs.SetString("new_game_id_hero","formica_nera");
            }
            id_hero=PlayerPrefs.GetString("new_game_id_hero");
            xml_content="<game id_hero='"+id_hero+"' num_ondata='1'";
            switch (id_hero){
                default:{
                    denaro=2000;
                    break;
                }
            }
            xml_content+=" denaro='"+denaro+"'>";
            xml_content+="\n\t<lista_abilita>";
            switch (id_hero){
                case "formica_nera":{
                    xml_content+="\n\t\t<a liv='1'>ragnatele</a>";
                    break;
                }
            }
            xml_content+="\n\t</lista_abilita>";
            xml_content+="\n\t<lista_pupetti>";
            switch (id_hero){
                case "formica_nera":{
                    xml_content+="\n\t\t<p num='6'>formica_warrior</p>";
                    xml_content+="\n\t\t<p num='2'>formica_arcer</p>";
                    break;
                }
            }
            xml_content+="\n\t</lista_pupetti>";
            xml_content+="\n\t<lista_upgrade>";
            foreach(KeyValuePair<string,int> attachStat in lista_upgrade){
                xml_content+="\n\t\t<u liv='"+attachStat.Value+"'>"+attachStat.Key+"</u>";
            }
            xml_content+="\n\t</lista_upgrade>";
            xml_content+="\n</game>";

            StreamWriter writer = new StreamWriter(path, false);
            writer.Write(xml_content);
            writer.Close();
        }
        //arrivati a questo punto, il file deve esistere per forza ed andiamo a prendere tutte el cose che serviranno al giocatore.

        XmlDocument xml_game = new XmlDocument ();
        string_temp=System.IO.File.ReadAllText(path);
        //string_temp=f_comuni.decripta(string_temp, "munimuni");
        xml_game.LoadXml(string_temp);

        int num_pupi_temp;
        string tipo_pupo_temp;
        foreach(XmlElement node in xml_game.SelectNodes("game")){
            id_hero=node.GetAttribute("id_hero");
            num_ondata=int.Parse(node.GetAttribute("num_ondata"));
            foreach(XmlElement node_2 in node.SelectNodes("lista_pupetti")){
                foreach(XmlElement node_3 in node_2.SelectNodes("p")){
                    num_pupi_temp=int.Parse(node_3.GetAttribute("num"));
                    tipo_pupo_temp=node_3.InnerText;
                    print ("dovrei avere "+num_pupi_temp+" del tipo "+tipo_pupo_temp);

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
        }
    }
}
