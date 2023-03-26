using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Spine;
using Spine.Unity;

using System.Text;
using System.Xml; //Needed for XML functionality
using System.IO;
public class init : MonoBehaviour
{
    public abilita_balestra abilita_balestra;
    public abilita_bombo abilita_bombo;
    private basic_rule br_eroe;
    private int int_key_eroe=0;
    private bool bool_eroe_in_azione=false;
    public GameObject cont_eroi_pupi;
    private GameObject GO_pupo_eroe;
    private SkeletonGraphic SkeletonGraphic_hero;
    public GameObject cont_eroi;
    public Image img_skull;
    public GameObject GO_anim_fiamma;
    public Image img_BarraEroe_piena;
    private bool bool_potere_eroe_pieno=false;
    private Color color_img_BarraEroe_piena;
    private float per_potere_eroe=0;
    private float incr_potere_eroe=5;
    private float decr_potere_eroe=0.1f;
    public Texture2D cursore_abilita;

    public SpriteRenderer sfondo;
    public TMPro.TextMeshProUGUI testo_gemme_guadagnate;
    private Dictionary<string, int> lista_upgrade_perenni_liv = new Dictionary<string, int>();
    private int num_gemme=0;
    private int num_gemme_totali=0;
    private int num_partite=0;

    public GameObject cont_descrizione_volante;
    public Text txt_descrizione_volante;
    private int abilita_temp_mouse;

    public info_comuni info_comuni;
    public effetti effetti;
    //public TMPro.TextMeshProUGUI txt_desc_abilita;    //presto disattiverai tutto ciò che concerne stà cosa!
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

    public Dictionary<int, GameObject> lista_cerchietti_rossi_GO = new Dictionary<int, GameObject>();
    public Dictionary<int, GameObject> lista_cerchietti_blu_GO = new Dictionary<int, GameObject>();
    public Dictionary<int, GameObject> lista_cerchietti_verdi_GO = new Dictionary<int, GameObject>();

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
    private float xa=-40,ya_start=19;
    private float xc=40;
    private bool bool_fine_partita=false;
    private int max_pupi_battaglione=15;

    public Dictionary<string, int> lista_upgrade = new Dictionary<string, int>();
    public Dictionary<string, int> lista_razze_sbloccate = new Dictionary<string, int>();

    //da quì in poi le andremo a prendere dalle opzioni della partita in corso
    private int num_ondata=1;
    private int num_battaglione_amico=1;
    private int num_battaglione_nemico=1;
    private string id_arena;
    private int tier_unity_sbloccato=1;

    //cose legate agli effetti
    public GameObject particle_mosche;
    private bool bool_mosche_fastidiose;
    private int liv_mosche_fastidiose;

    private int valore_iniziale_ondata=130;
    private int valore_incrementale_ondata=45;

    public GameObject insetto_esplosivo_pf;
    public GameObject insetto_esplosivo_velenoso_pf;

    void Awake(){
        carica_info_partite();  //semplicemente per prendere gli upgrade della partita

        GO_anim_fiamma.SetActive(false);
        particle_mosche.SetActive(false);
        bool_mosche_fastidiose=false;
        color_img_BarraEroe_piena=img_BarraEroe_piena.color;

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
                switch (child_2.name){
                    case "img_cooldown":{
                        lista_abilita_cooldown_img.Add(i,child_2.gameObject.GetComponent<Image>());
                        setta_cooldown_abilita(i,0f);
                        break;
                    }
                    case "img_abilita":{
                        lista_abilita_img.Add(i,child_2.gameObject.GetComponent<Image>());
                        break;
                    }
                    case "img_cerchietto_bianco_rosso":{lista_cerchietti_rossi_GO.Add(i,child_2.gameObject);break;}
                    case "img_cerchietto_bianco_blu":{lista_cerchietti_blu_GO.Add(i,child_2.gameObject);break;}
                    case "img_cerchietto_bianco_verde":{lista_cerchietti_verdi_GO.Add(i,child_2.gameObject);break;}
                }
            }
            lista_abilita_id.Add(i,"");
        }
        abilita_totali=i;
        setta_sfondo();

        setta_game_da_file();

        incr_potere_eroe=info_comuni.lista_incremento_potere_eroe[id_hero];
        decr_potere_eroe=info_comuni.lista_decremento_potere_eroe[id_hero];


        //incr_potere_eroe=5;   //debug
        decr_potere_eroe=0.1f;  //debug

        //ora che abbiamo l'id hero, andiamo a disattivare tutti gli altri
        foreach (Transform child in cont_eroi.transform) {
            if (child.name!=id_hero){child.gameObject.SetActive(false);}
            else {SkeletonGraphic_hero=child.gameObject.GetComponent<SkeletonGraphic>();}
        }

        foreach (Transform child in cont_eroi_pupi.transform) {
            if (child.name==id_hero+"_eroe"){
                GO_pupo_eroe=child.gameObject;
                lista_obj_pupetti.Add("eroe",GO_pupo_eroe);
                break;
            }
        }

        //poi andiamo a prendere i settaggi per ogni cooldown che ha salvato da qualche parte nella partita
        foreach(KeyValuePair<int,string> attachStat in lista_abilita_id){
            if (attachStat.Value!=""){
                lista_abilita_cooldown_secondi[attachStat.Key]=info_comuni.lista_abilita_cooldown[attachStat.Value][lista_abilita_livello[attachStat.Key]];
                setta_img_abilita(attachStat.Key);
                lista_abilita_cooldown_secondi_attuale[attachStat.Key]=info_comuni.lista_abilita_cooldown_partenza[attachStat.Value][lista_abilita_livello[attachStat.Key]];

                if (lista_upgrade["hero_cooldown"]>0){
                    lista_abilita_cooldown_secondi[attachStat.Key]=lista_abilita_cooldown_secondi[attachStat.Key]*(100-(lista_upgrade["hero_cooldown"]*10))/100;
                    lista_abilita_cooldown_secondi_attuale[attachStat.Key]=lista_abilita_cooldown_secondi_attuale[attachStat.Key]*(100-(lista_upgrade["hero_cooldown"]*10))/100;
                }
                setta_cerchietto_abilita(attachStat.Key,"rosso");

                if (attachStat.Value=="mosche_fastidiose"){
                    liv_mosche_fastidiose=lista_abilita_livello[attachStat.Key];
                }
            } else {
                lista_abilita_GO[attachStat.Key].SetActive(false);
            }
        }

        generazione_nemici(num_ondata);

        
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
                //lp_totali_basic_rule[attachStat.Key].vitalita_max*=0.8f;    //diminuire un pò l'eenrgia dei pupi nemici
                lp_totali_basic_rule[attachStat.Key].attiva_pupo(num_battaglione_nemico,xc,(j*-2)+ya_start,false);

            } else {
                num_pupi_generati_buoni++;
                lp_buoni.Add(num_pupi_generati_buoni,attachStat.Key);

                i++;
                //lp_totali[attachStat.Key].transform.localPosition = new Vector3(xa, (i*-2)+15, 1f);
                if (i>max_pupi_battaglione){//dobbiamo attivare il prossimo battaglione
                    i=1;
                    num_battaglione_amico++;
                }
                lp_totali_basic_rule[attachStat.Key].attiva_pupo(num_battaglione_amico,xa,(i*-2)+ya_start,false);

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
        //txt_desc_abilita.SetText("");
        bool_inizio_partita=true;
        StartCoroutine(start_partita());

        aggiorna_img_abilita_eroe();
        if (per_potere_eroe>=100){
            attiva_bottone_potere_eroe();
        }

        switch (id_hero){
            case "re_mosca":{//il danno potrebbe variare a seconda di eventuali abilità prese
                foreach(KeyValuePair<int,GameObject> attachStat in lp_totali){
                    //if (lp_totali_basic_rule[attachStat.Key].bool_fazione_nemica){
                        lp_totali_basic_rule[attachStat.Key].danno_eroe=5f;
                    //}
                }
                break;
            }
            case "regina_ape":{//il danno potrebbe variare a seconda di eventuali abilità prese
                foreach(KeyValuePair<int,GameObject> attachStat in lp_totali){
                    if (!lp_totali_basic_rule[attachStat.Key].bool_fazione_nemica){
                        lp_totali_basic_rule[attachStat.Key].danno_eroe=1f;
                    }
                }
                break;
            }
        }
    }

    private void decrementa_potere_eroe(){
        per_potere_eroe-=decr_potere_eroe;
        aggiorna_img_abilita_eroe();

        if (per_potere_eroe<=0){
            disattiva_eroe();
        }
    }

    private void attiva_bottone_potere_eroe(){
        bool_potere_eroe_pieno=true;
        GO_anim_fiamma.SetActive(true);
    }

    private void setta_sfondo(){
        Sprite sprite_temp  = Resources.Load<Sprite>("arene/tt"+(Random.Range(1,6).ToString()));
        sfondo.sprite = sprite_temp;
    }

    private void setta_img_abilita(int int_abilita){
        string abilita=lista_abilita_id[int_abilita];
        Sprite sprite_temp  = Resources.Load<Sprite>("icone_abilita/"+abilita);
        lista_abilita_img[int_abilita].sprite = sprite_temp;
    }

    private void descrizione_follow_mouse(){
        //Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mousePosition = Input.mousePosition;

        float x_plus=400;
        float y_plus=150;

        x_plus-=(abilita_temp_mouse*70);

        mousePosition.x+=x_plus;
        mousePosition.y+=y_plus;
        mousePosition.z=0;
        cont_descrizione_volante.transform.position=mousePosition;
    }

    private void aggiorna_img_abilita_eroe(){
        img_skull.fillAmount=1-(per_potere_eroe/100);
        color_img_BarraEroe_piena.a=(1-img_skull.fillAmount);
        img_BarraEroe_piena.color=color_img_BarraEroe_piena;
    }

    // Update is called once per frame
    void Update(){

        if (Input.GetKeyDown(KeyCode.Z)){
            foreach(KeyValuePair<int,GameObject> attachStat in lp_totali){
                effetti.effetto_hit_melee(lp_totali[attachStat.Key].transform.position.x,lp_totali[attachStat.Key].transform.position.y+0.5f);
                //effetti.eff_armatura(lp_totali[attachStat.Key].transform.position.x,lp_totali[attachStat.Key].transform.position.y);
                //effetti.eff_ragnatele(lp_totali[attachStat.Key].transform.position.x,lp_totali[attachStat.Key].transform.position.y);

            }
        }
        if (Input.GetKeyDown(KeyCode.Q)){check_click_abilita(1);}
        if (Input.GetKeyDown(KeyCode.W)){check_click_abilita(2);}
        if (Input.GetKeyDown(KeyCode.E)){check_click_abilita(3);}
        if (Input.GetKeyDown(KeyCode.R)){check_click_abilita(4);}
        if (Input.GetKeyDown(KeyCode.T)){check_click_abilita(5);}
        if (Input.GetKeyDown(KeyCode.Y)){check_click_abilita(6);}
        if (Input.GetKeyDown(KeyCode.U)){check_click_abilita(7);}
        if (Input.GetKeyDown(KeyCode.I)){check_click_abilita(8);}

        if (Input.GetKeyDown(KeyCode.Escape)){
            if (int_abilita_scelta!=0){
                setta_cursore("default");
                setta_cerchietto_abilita(int_abilita_scelta,"verde");
                int_abilita_scelta=0;
                //txt_desc_abilita.SetText("");
            }
        }
        if (!bool_inizio_partita){
            if (bool_fine_partita){return;}
        }

        if (!bool_eroe_in_azione){
            if (!bool_potere_eroe_pieno){
                if (per_potere_eroe<100){
                    per_potere_eroe+=incr_potere_eroe;
                    aggiorna_img_abilita_eroe();
                    if (per_potere_eroe>=100){attiva_bottone_potere_eroe();}
                }
            }
        } else {//essendo in azione, dobbiamo farlo decrementare
            if (per_potere_eroe>0){
                decrementa_potere_eroe();
            }
        }
        descrizione_follow_mouse();

        //parte relativa alle mosche fastidiose
        if (bool_mosche_fastidiose){
            foreach(KeyValuePair<int,int> attachStat in lp_cattivi){
                if (!lp_totali_basic_rule[attachStat.Value].bool_morto){
                    calcola_eventuale_danno_mosche_fastidiose(attachStat.Value);
                }
            }
            foreach(KeyValuePair<int,int> attachStat in lp_buoni){
                if (!lp_totali_basic_rule[attachStat.Value].bool_morto){
                    calcola_eventuale_danno_mosche_fastidiose(attachStat.Value);
                }
            }
        }
        //andiamo a prendere i cooldown e li abbassiamo se c'è ne sono...
        for (int i=1;i<=abilita_totali;i++){
            if (lista_abilita_cooldown_secondi_attuale[i]>0){
                lista_abilita_cooldown_secondi_attuale[i]-=Time.deltaTime;
                if (lista_abilita_cooldown_secondi_attuale[i]>0){
                    setta_cooldown_abilita(i,(lista_abilita_cooldown_secondi_attuale[i]/lista_abilita_cooldown_secondi[i]));
                } else {
                    setta_cerchietto_abilita(i,"verde");
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

    private void check_click_abilita(int int_abilita){
        if (bool_fine_partita){return;}
        if ((!lista_abilita_id.ContainsKey(int_abilita))||(lista_abilita_id[int_abilita]=="")){return;}
        if (lista_abilita_cooldown_secondi_attuale[int_abilita]<=0){
            setta_cursore("abilita");
            click_abilita(int_abilita);
        }
    }

    private void calcola_eventuale_danno_mosche_fastidiose(int id_pupo){
        if ((lp_totali_basic_rule[id_pupo].razza!="mosca")&&(lp_totali_basic_rule[id_pupo].razza!="zanzara")){
            float distanza_temp=calcola_distanza(
                lp_totali[id_pupo].transform.position.x,
                lp_totali[id_pupo].transform.position.y,
                particle_mosche.transform.position.x,particle_mosche.transform.position.y);
            if (distanza_temp<=3.5f){
                //print ("colpisco "+attachStat.Value+" - "+distanza_temp);
                lp_totali_basic_rule[id_pupo].danneggia(0.025f*liv_mosche_fastidiose);
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

            denaro_guadagnato+=(lista_upgrade_perenni_liv["costi_guadagno"]*10);

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

            xml_content="<game id_hero='"+id_hero+"' num_ondata='"+num_ondata+"' tier_unity_sbloccato='"+tier_unity_sbloccato+"' per_potere_eroe='"+per_potere_eroe+"'";
            denaro+=denaro_guadagnato;
            xml_content+=" denaro='"+denaro+"' posizione='upgrade'>";

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
            path=Application.persistentDataPath + "/game_c.xml";
            //File.Delete(path);        //poi lo cancelleremo quando non saremo in pieno debug

            int num_gemme_guadagnate=num_ondata;
            testo_gemme_guadagnate.SetText("Hai guadagnato "+num_gemme_guadagnate+" gemme");

            num_gemme+=num_gemme_guadagnate;
            num_gemme_totali+=num_gemme_guadagnate;
            num_partite++;
            salva_file_info_partite();

            PlayerPrefs.SetString("ultima_posizione","gioco_sconfitta");
            pannello_sconfitta.SetActive(true);
        }
    }

    public void btn_ricomincia(){
        SceneManager.LoadScene("mainmenu");
    }

    public void next_stage(){
        SceneManager.LoadScene("upgrade");
    }

    public void setta_cooldown_abilita(int abilita, float settaggio){
        lista_abilita_cooldown_img[abilita].fillAmount=settaggio;
    }

    public void click_abilita(int int_abilita){
        if (bool_fine_partita){return;}
        if ((!lista_abilita_id.ContainsKey(int_abilita))||(lista_abilita_id[int_abilita]=="")){return;}
        if (lista_abilita_cooldown_secondi_attuale[int_abilita]<=0){
            if (int_abilita_scelta!=0){
                setta_cerchietto_abilita(int_abilita_scelta,"verde");
            }
            int_abilita_scelta=int_abilita;
            setta_cerchietto_abilita(int_abilita_scelta,"blu");

            switch (lista_abilita_id[int_abilita]){
                default:{
                    string testo="";
                    testo+="Select where want to use "+info_comuni.lista_abilita_nome[lista_abilita_id[int_abilita]];
                    testo+="\n\nPress ESC to cancel";
                    //txt_desc_abilita.SetText(testo);
                    break;
                }
            }
        } else {
            print ("è in cooldown");
        }
    }

    public void mouse_click(GameObject obj, string tipo){
        //print ("mouse: ho cliccato su "+obj.name+" (del tipo "+tipo);
        //print (Camera.main.ScreenToWorldPoint(Input.mousePosition)+" - "+Input.mousePosition+" - "+Camera.main.ScreenToWorldPoint(Input.mousePosition).x);
        if (obj.name.Contains("abilita_")){
            int int_abilita=int.Parse(obj.name.Replace("abilita_",""));
            click_abilita(int_abilita);
        }
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
        else if (obj.name=="img_skull"){
            if (bool_eroe_in_azione){return;}
            if (per_potere_eroe>=100){
                GO_anim_fiamma.SetActive(false);
                bool_eroe_in_azione=true;
                SkeletonGraphic_hero.AnimationState.SetAnimation(0, "start", false);    //se metti a true andrà in loop
                StartCoroutine(aggiungi_eroe_scena());
            }
        }
    }

    public void bomba(string tipo, float xar, float yar){
        //print ("Una bomba del tipo "+tipo+" ha colpito alle coordinate "+xar+" - "+yar);
        switch (tipo){
            case "bomba_balestra":
            case "bomba_bombo":{
                effetti.effetto_esplosione_piccola(xar,yar);
                float distanza_temp;
                float valore_abilita=2;
                float valore_danno=valore_abilita*0.5f+Random.Range(0.5f,1f);

                foreach(KeyValuePair<int,int> attachStat in lp_cattivi){
                    if (!lp_totali_basic_rule[attachStat.Value].bool_morto){
                        distanza_temp=calcola_distanza(lp_totali[attachStat.Value].transform.position.x,lp_totali[attachStat.Value].transform.position.y,xar,yar);
                        if (distanza_temp<=2){
                            valore_danno+=(2-distanza_temp);
                            lp_totali_basic_rule[attachStat.Value].danneggia(valore_danno);
                        }
                    }
                }
                break;
            }
            case "bomba_eroe_ragnatele":{
                effetti.eff_ragnatele(xar,yar);
                float distanza_temp;
                float valore_abilita=6;
                foreach(KeyValuePair<int,int> attachStat in lp_cattivi){
                    if (!lp_totali_basic_rule[attachStat.Value].bool_morto){
                        distanza_temp=calcola_distanza(lp_totali[attachStat.Value].transform.position.x,lp_totali[attachStat.Value].transform.position.y,xar,yar);
                        if (distanza_temp<=4){
                            lp_totali_basic_rule[attachStat.Value].applica_ragnatela(valore_abilita);
                        }
                    }
                }
                break;
            }
            case "bomba_eroe_cavalletta":{
                effetti.effetto_esplosione(xar,yar);
                float distanza_temp;
                float valore_abilita=6;
                float valore_danno=valore_abilita*0.5f+Random.Range(0.5f,1f);

                foreach(KeyValuePair<int,int> attachStat in lp_cattivi){
                    if (!lp_totali_basic_rule[attachStat.Value].bool_morto){
                        distanza_temp=calcola_distanza(lp_totali[attachStat.Value].transform.position.x,lp_totali[attachStat.Value].transform.position.y,xar,yar);
                        if (distanza_temp<=4){
                            valore_danno+=(4-distanza_temp);
                            lp_totali_basic_rule[attachStat.Value].danneggia(valore_danno);
                        }
                    }
                }
                break;
            }
        }
    }

    private IEnumerator aggiungi_eroe_scena() {
        int num_secondi=2;
        yield return new WaitForSeconds(num_secondi);
        switch (id_hero){
            case "re_cavalletta":{
                num_pupi_generati_totali++;
                GO_pupo_eroe.transform.SetParent(mappa.transform);
                GO_pupo_eroe.transform.localPosition = new Vector3(-38, 0, 11f);
                GO_pupo_eroe.GetComponent<MeshRenderer>().sortingOrder = (num_pupi_generati_totali+2001);
                GO_pupo_eroe.GetComponent<re_cavalletta_rule>().attiva();

                break;
            }
            case "regina_ragno":{
                num_pupi_generati_totali++;
                GO_pupo_eroe.transform.SetParent(mappa.transform);
                GO_pupo_eroe.transform.localPosition = new Vector3(-38, 0, 11f);
                GO_pupo_eroe.GetComponent<MeshRenderer>().sortingOrder = (num_pupi_generati_totali+2001);
                GO_pupo_eroe.GetComponent<regina_ragno_rule>().attiva();

                break;
            }
            case "regina_ape":{
                num_pupi_generati_totali++;
                GO_pupo_eroe.transform.SetParent(mappa.transform);
                GO_pupo_eroe.transform.localPosition = new Vector3(0, 0, 11f);
                GO_pupo_eroe.GetComponent<MeshRenderer>().sortingOrder = (num_pupi_generati_totali+2001);
                GO_pupo_eroe.GetComponent<regina_ape_rule>().attiva();

                break;
            }
            case "re_mosca":{
                num_pupi_generati_totali++;
                GO_pupo_eroe.transform.SetParent(mappa.transform);
                GO_pupo_eroe.transform.localPosition = new Vector3(0, 0, 11f);
                GO_pupo_eroe.GetComponent<MeshRenderer>().sortingOrder = (num_pupi_generati_totali+2001);
                GO_pupo_eroe.GetComponent<re_mosca_rule>().attiva();

                break;
            }
            default:{evoca_eroe();break;}
        }
    }

    private void disattiva_eroe(){
        //prima faremo fare altre scenette al nostro eroe
        switch (id_hero){
            case "re_cavalletta":{
                GO_pupo_eroe.GetComponent<MeshRenderer>().sortingOrder = (num_pupi_generati_totali+2001);
                GO_pupo_eroe.GetComponent<re_cavalletta_rule>().disattiva();
                break;
            }
            case "regina_ragno":{
                GO_pupo_eroe.GetComponent<MeshRenderer>().sortingOrder = (num_pupi_generati_totali+2001);
                GO_pupo_eroe.GetComponent<regina_ragno_rule>().disattiva();
                break;
            }
            case "re_mosca":{
                GO_pupo_eroe.GetComponent<MeshRenderer>().sortingOrder = (num_pupi_generati_totali+2001);
                GO_pupo_eroe.GetComponent<re_mosca_rule>().disattiva();
                break;
            }
            case "regina_ape":{
                GO_pupo_eroe.GetComponent<MeshRenderer>().sortingOrder = (num_pupi_generati_totali+2001);
                GO_pupo_eroe.GetComponent<regina_ape_rule>().disattiva();
                break;
            }
            default:{
                if (!lp_totali_basic_rule[int_key_eroe].bool_morto){
                    lp_totali_basic_rule[int_key_eroe].disattiva_eroe();
                }
                break;
            }
        }
        StartCoroutine(anim_rimetti_eroe_a_posto());
    }

    private IEnumerator anim_rimetti_eroe_a_posto() {
        int num_secondi=2;
        yield return new WaitForSeconds(num_secondi);
        SkeletonGraphic_hero.AnimationState.SetAnimation(0, "fine", false);    //se metti a true andrà in loop
        StartCoroutine(setta_eroe_normale());
    }

    private IEnumerator setta_eroe_normale(){
        yield return new WaitForSeconds(1);
        SkeletonGraphic_hero.AnimationState.SetAnimation(0, "animation", true);    //se metti a true andrà in loop
        per_potere_eroe=0;
        bool_eroe_in_azione=false;
        bool_potere_eroe_pieno=false;
    }

    private void evoca_eroe(){
        float xar=0;
        float yar=0;
        int tempo_evocazione=3;
        genera_pupo("eroe");
        num_pupi_generati_buoni++;  //num_pupi_generati_totali viene uincrementato dentro genera_pupo
        int_key_eroe=num_pupi_generati_totali;
        lp_buoni.Add(num_pupi_generati_buoni,num_pupi_generati_totali);
        lp_totali_basic_rule[num_pupi_generati_totali].attiva_pupo(tempo_evocazione,xar,yar,true);

        effetti.eff_evocazione_eroe(xar,yar,id_hero);
    }

    public void evoca_pupo(string id_pupo, float xar, float yar, string tipo_evocazione){
        int tempo_evocazione=2;
        switch (tipo_evocazione){
            case "resurrezione":{tempo_evocazione=4;break;}
            case "zombie":{tempo_evocazione=3;break;}
        }
        genera_pupo(id_pupo);
        if (!lp_totali_basic_rule[num_pupi_generati_totali].bool_fazione_nemica){
            num_pupi_generati_buoni++;  //num_pupi_generati_totali viene uincrementato dentro genera_pupo
            lp_buoni.Add(num_pupi_generati_buoni,num_pupi_generati_totali);
            float per_10;
            lp_totali_basic_rule[num_pupi_generati_totali].attiva_pupo(tempo_evocazione,xar,yar,true);
            if (id_pupo.Contains("zombie")){
                lp_totali_basic_rule[num_pupi_generati_totali].danno*=1.5f;
                lp_totali_basic_rule[num_pupi_generati_totali].vitalita_max/=2;
                lp_totali_basic_rule[num_pupi_generati_totali].velocita_movimento/=2;
                lp_totali_basic_rule[num_pupi_generati_totali].distanza_attacco/=2;
                lp_totali_basic_rule[num_pupi_generati_totali].ritardo_attacco*=2;
            }
            else {
                //lp_totali[num_pupi_generati_totali].transform.localPosition = new Vector3(xa, (i*-2)+15, 1f);
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
        }
        effetti.eff_evocazione_brown(xar,yar);
    }

    private void evoca_insetto_esplosivo(GameObject go_insetto, float xar, float yar){
        int tempo_evocazione=2;
        GameObject go_temp;
        go_temp=Instantiate(go_insetto);
        go_temp.transform.SetParent(mappa.transform);
        go_temp.transform.localPosition = new Vector3(xar, yar, 1f);
        effetti.eff_evocazione_brown(xar,yar);
        go_temp.GetComponent<MeshRenderer>().sortingOrder = (num_pupi_generati_totali+2000);
        go_temp.GetComponent<insetto_esplosivo_rule>().attiva();

    }

    public void insetto_esplode(string tipo, float xar, float yar){
        switch (tipo){
            case "insetto_esplosivo":{
                effetti.effetto_esplosione_piccola(xar,yar);
                float distanza_temp;
                float valore_abilita=6;
                float valore_danno=valore_abilita*0.5f+Random.Range(0.5f,1f);

                foreach(KeyValuePair<int,GameObject> attachStat in lp_totali){
                    if (!lp_totali_basic_rule[attachStat.Key].bool_morto){
                        distanza_temp=calcola_distanza(lp_totali[attachStat.Key].transform.position.x,lp_totali[attachStat.Key].transform.position.y,xar,yar);
                        if (distanza_temp<=4){
                            valore_danno+=(4-distanza_temp);
                            lp_totali_basic_rule[attachStat.Key].danneggia(valore_danno);
                        }
                    }
                }
                break;
            }
            case "insetto_esplosivo_velenoso":{
                effetti.effetto_esplosione_velenosa(xar,yar);
                float distanza_temp;
                float livello_veleno=0.0075f;

                foreach(KeyValuePair<int,GameObject> attachStat in lp_totali){
                    if (!lp_totali_basic_rule[attachStat.Key].bool_morto){
                        distanza_temp=calcola_distanza(lp_totali[attachStat.Key].transform.position.x,lp_totali[attachStat.Key].transform.position.y,xar,yar);
                        if (distanza_temp<=3){
                            lp_totali_basic_rule[attachStat.Key].livello_veleno+=livello_veleno;
                        }
                    }
                }
                break;
            }
        }
    }



    public void attiva_abilita_coordinate(float xar, float yar){
        setta_cursore("default");
        if (bool_fine_partita){int_abilita_scelta=0;return;}
        if (int_abilita_scelta!=0){
            //print ("attivo l'abilita numero "+int_abilita_scelta+" alle coordinate "+xar+"-"+yar+" ... durata cooldown: "+lista_abilita_cooldown_secondi[int_abilita_scelta]);
            lista_abilita_cooldown_secondi_attuale[int_abilita_scelta]=lista_abilita_cooldown_secondi[int_abilita_scelta];
            setta_cerchietto_abilita(int_abilita_scelta,"rosso");
            setta_cooldown_abilita(int_abilita_scelta,1);
            //txt_desc_abilita.SetText("");
            int liv=lista_abilita_livello[int_abilita_scelta];
            switch (lista_abilita_id[int_abilita_scelta]){
                case "balestra":{
                    int num_bombo=liv*5;
                    float random_x;
                    float random_y;
                    string zombie_temp="";

                    abilita_balestra.resetta();

                    for (int i=1;i<=num_bombo;i++){
                        random_x=Random.Range(-5f,5f)+xar;
                        random_y=Random.Range(-5f,5f)+yar;

                        abilita_balestra.aggiungi(i,liv,random_x, random_y);
                    }

                    abilita_balestra.avvia();
                    break;
                }
                case "bombo":{
                    int num_bombo=liv*5;
                    float random_x;
                    float random_y;
                    string zombie_temp="";

                    abilita_bombo.resetta();

                    for (int i=1;i<=num_bombo;i++){
                        random_x=Random.Range(-5f,5f)+xar;
                        random_y=Random.Range(-5f,5f)+yar;

                        abilita_bombo.aggiungi(i,liv,random_x, random_y);
                    }

                    abilita_bombo.avvia();
                    break;
                }
                case "resurrezione":{
                    float distanza_temp;
                    int id_pupo_risveglio=0;
                    float distanza_pupo=10000;
                    foreach(KeyValuePair<int,int> attachStat in lp_buoni){
                        if (lp_totali_basic_rule[attachStat.Value].bool_morto){
                            if (lp_totali[attachStat.Value].name.Contains("_"+liv)){
                                distanza_temp=calcola_distanza(lp_totali[attachStat.Value].transform.position.x,lp_totali[attachStat.Value].transform.position.y,xar,yar);
                                if (distanza_temp<distanza_pupo){
                                    distanza_pupo=distanza_temp;
                                    id_pupo_risveglio=attachStat.Value;
                                }
                            }
                        }
                    }
                    if (id_pupo_risveglio!=0){
                        float x=lp_totali[id_pupo_risveglio].transform.position.x;
                        float y=lp_totali[id_pupo_risveglio].transform.position.y;
                        effetti.effetto_resurrezione(x,y);
                        evoca_pupo(lp_totali[id_pupo_risveglio].name,x,y, "resurrezione");
                    } else {print ("non è ancora morto nessuno di quel livello");}
                    break;
                }
                case "zombie":{
                    float random_x;
                    float random_y;
                    int num_zombie=5;
                    if (liv==2){num_zombie=7;}
                    else if (liv==3){num_zombie=10;}
                    string zombie_temp="";
                    for (int i=1;i<=num_zombie;i++){
                        random_x=Random.Range(-2f,2f)+xar;
                        random_y=Random.Range(-2f,2f)+yar;
                        zombie_temp="";
                        switch (Random.Range(1,5)){
                            case 1:{zombie_temp+="formica";break;}
                            case 2:{zombie_temp+="ape";break;}
                            case 3:{zombie_temp+="mosca";break;}
                            case 4:{zombie_temp+="ragnetto";break;}
                        }
                        zombie_temp+="_";
                        switch (Random.Range(1,4)){
                            case 1:{zombie_temp+="warrior";break;}
                            case 2:{zombie_temp+="arcer";break;}
                            case 3:{zombie_temp+="wizard";break;}
                        }
                        zombie_temp+="_1_zombie";
                        evoca_pupo(zombie_temp,random_x,random_y,"zombie");
                    }
                    break;
                }
                case "evoca_formiche":{
                    float random_x;
                    float random_y;
                    for (int i=1;i<=2*liv;i++){
                        random_x=Random.Range(-1f,2f)+xar;
                        random_y=Random.Range(-1f,2f)+yar;
                        evoca_pupo("formica_warrior_1",random_x,random_y,"evocazione");
                    }
                    break;
                }
                case "insetto_esplosivo":{
                    float random_x;
                    float random_y;
                    for (int i=1;i<=liv;i++){
                        random_x=Random.Range(-1f,1f)+xar;
                        random_y=Random.Range(-1f,1f)+yar;
                        evoca_insetto_esplosivo(insetto_esplosivo_pf,random_x,random_y);
                    }
                    break;
                }
                case "insetto_esplosivo_velenoso":{
                    float random_x;
                    float random_y;
                    for (int i=1;i<=liv;i++){
                        random_x=Random.Range(-1f,1f)+xar;
                        random_y=Random.Range(-1f,1f)+yar;
                        evoca_insetto_esplosivo(insetto_esplosivo_velenoso_pf,random_x,random_y);
                    }
                    break;
                }
                case "mosche_fastidiose":{
                    bool_mosche_fastidiose=true;
                    int time=5;
                    float random_x;
                    float random_y;
                    float z=1;
                    Vector3[] waypoints;
                    int num_path=time*2;
                    waypoints=new Vector3[num_path];
                    random_x=xar;
                    random_y=yar;
                    for (int i=0;i<num_path;i++){
                        random_x+=Random.Range(-2f,2f);
                        random_y+=Random.Range(-2f,2f);
                        waypoints[i]=new Vector3(random_x,random_y,-11f);
                    }

                    particle_mosche.SetActive(true);
                    particle_mosche.transform.localPosition = new Vector3(xar, yar, -11f);
                    iTween.MoveTo(particle_mosche, iTween.Hash("path", waypoints, "time", time, "easetype", iTween.EaseType.linear));

                    StartCoroutine(termina_mosche_fastidiose(time));

                    break;
                }
                case "miele":{
                    effetti.eff_miele(xar,yar);
                    float distanza_temp;
                    float valore_abilita=3+(liv);    //anche questo potrebbe dipendere dal livello
                    foreach(KeyValuePair<int,int> attachStat in lp_buoni){
                        if (!lp_totali_basic_rule[attachStat.Value].bool_morto){
                            distanza_temp=calcola_distanza(lp_totali[attachStat.Value].transform.position.x,lp_totali[attachStat.Value].transform.position.y,xar,yar);
                            if (distanza_temp<=4){
                                lp_totali_basic_rule[attachStat.Value].cura(valore_abilita);
                            }
                        }
                    }
                    break;
                }
                case "ragnatele":{
                    effetti.eff_ragnatele(xar,yar);
                    float distanza_temp;
                    float valore_abilita=3+liv;
                    foreach(KeyValuePair<int,int> attachStat in lp_cattivi){
                        if (!lp_totali_basic_rule[attachStat.Value].bool_morto){
                            distanza_temp=calcola_distanza(lp_totali[attachStat.Value].transform.position.x,lp_totali[attachStat.Value].transform.position.y,xar,yar);
                            if (distanza_temp<=4){
                                lp_totali_basic_rule[attachStat.Value].applica_ragnatela(valore_abilita);
                            }
                        }
                    }
                    break;
                }
                case "velocita":{
                    effetti.eff_velocita(xar,yar);
                    float distanza_temp;
                    float valore_abilita=3+liv;
                    foreach(KeyValuePair<int,int> attachStat in lp_buoni){
                        if (!lp_totali_basic_rule[attachStat.Value].bool_morto){
                            distanza_temp=calcola_distanza(lp_totali[attachStat.Value].transform.position.x,lp_totali[attachStat.Value].transform.position.y,xar,yar);
                            if (distanza_temp<=4){
                                lp_totali_basic_rule[attachStat.Value].applica_velocita(valore_abilita);
                            }
                        }
                    }
                    break;
                }
                case "armatura":{
                    effetti.eff_armatura(xar,yar);
                    float distanza_temp;
                    float valore_abilita=3+liv;
                    foreach(KeyValuePair<int,int> attachStat in lp_buoni){
                        if (!lp_totali_basic_rule[attachStat.Value].bool_morto){
                            distanza_temp=calcola_distanza(lp_totali[attachStat.Value].transform.position.x,lp_totali[attachStat.Value].transform.position.y,xar,yar);
                            if (distanza_temp<=4){
                                lp_totali_basic_rule[attachStat.Value].applica_armatura(valore_abilita);
                            }
                        }
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
    public IEnumerator termina_mosche_fastidiose(int secondi) {
        yield return new WaitForSeconds(secondi);
        bool_mosche_fastidiose=false;
        particle_mosche.SetActive(false);
    }

    private void setta_cursore(string tipo){
        switch (tipo){
            default:{Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);break;}
            case "abilita":{Cursor.SetCursor(cursore_abilita, new Vector2(100,50), CursorMode.ForceSoftware);break;}
        }
    }

    public void mouse_exit(GameObject obj){
        if (obj.name=="area_cliccabile"){
            if (int_abilita_scelta!=0){
                setta_cursore("default");
            }
        }
        else if (obj.name.Contains("abilita_")){
            if (int_abilita_scelta==0){
                //txt_desc_abilita.SetText("");
            }
            int int_abilita=int.Parse(obj.name.Replace("abilita_",""));
            if (lista_abilita_cooldown_secondi_attuale[int_abilita]<=0){
                if (int_abilita_scelta!=int_abilita){
                    setta_cerchietto_abilita(int_abilita,"verde");
                }
            } else {
                setta_cerchietto_abilita(int_abilita,"rosso");
            }
            cont_descrizione_volante.SetActive(false);
        } else if (obj.name=="img_skull"){
            cont_descrizione_volante.SetActive(false);
        }
        //print ("mouse: sono uscito da "+obj.name);
    }
    public void mouse_enter(GameObject obj){
        if (obj.name=="area_cliccabile"){
            if (int_abilita_scelta!=0){
                setta_cursore("abilita");
            }
        }
        else if (obj.name.Contains("abilita_")){
            int int_abilita=int.Parse(obj.name.Replace("abilita_",""));
            if (int_abilita_scelta==0){
                string testo=info_comuni.lista_abilita_descrizione[lista_abilita_id[int_abilita]];
                cont_descrizione_volante.SetActive(true);
                abilita_temp_mouse=int_abilita;
                //txt_desc_abilita.SetText(testo);
                //testo = testo.Replace(".", ".\n");
                txt_descrizione_volante.text=testo;
            }
            if (lista_abilita_cooldown_secondi_attuale[int_abilita]<=0){
                setta_cerchietto_abilita(int_abilita,"blu");
            }
        } else if (obj.name=="img_skull"){
            txt_descrizione_volante.text=info_comuni.lista_powerhero_descrizione[id_hero];
            cont_descrizione_volante.SetActive(true);
        }
    }

    private void setta_cerchietto_abilita(int int_abilita, string colore){
        lista_cerchietti_rossi_GO[int_abilita].SetActive(false);
        lista_cerchietti_blu_GO[int_abilita].SetActive(false);
        lista_cerchietti_verdi_GO[int_abilita].SetActive(false);

        switch (colore){
            case "rosso":{lista_cerchietti_rossi_GO[int_abilita].SetActive(true);break;}
            case "blu":{lista_cerchietti_blu_GO[int_abilita].SetActive(true);break;}
            case "verde":{lista_cerchietti_verdi_GO[int_abilita].SetActive(true);break;}
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
        if (lp_totali_basic_rule[id_attaccante].bool_ragnatele){return;}

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
                if (lp_totali_basic_rule[id_attaccante].razza=="ape"){
                    lp_totali_basic_rule[id_attaccante].aculeo_x=lp_totali[id_difensore].transform.position.x;
                    lp_totali_basic_rule[id_attaccante].aculeo_y=lp_totali[id_difensore].transform.position.y;
                }
                if (distanza>lp_totali_basic_rule[id_attaccante].distanza_attacco){
                    lp_totali_basic_rule[id_attaccante].stato="move";
                    lp_totali[id_attaccante].transform.position=Vector3.MoveTowards(lp_totali[id_attaccante].transform.position,lp_totali[id_difensore].transform.position,lp_totali_basic_rule[id_attaccante].velocita_movimento * lp_totali_basic_rule[id_attaccante].velocita_pupo_effetti *Time.deltaTime);
        
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
        yield return new WaitForSeconds(lp_totali_basic_rule[id_attaccante].anim_velocita_attacco/lp_totali_basic_rule[id_attaccante].velocita_pupo_effetti);
        //lp_totali_basic_rule[id_attaccante].bool_stop_hit=false;
        if (!lp_totali_basic_rule[id_attaccante].bool_morto){//beh potrebbe capitare che è stato colpito prima che sferrasse l'attaccp finale...
            if (!lp_totali_basic_rule[id_difensore].bool_morto){//beh potrebbe capitare che è stato colpito il suo bersaglio...
                lp_totali_basic_rule[id_attaccante].stato="wait";
                if (lp_totali_basic_rule[id_attaccante].velocita_proiettile==0){
                    float distanza=0;
                    float raggio_sfera_attacco=lp_totali_basic_rule[id_attaccante].raggio_sfera_attacco;
                    int num_colpire=1+lp_totali_basic_rule[id_attaccante].up_pupetti_colpiti_contemporaneamente;
                    foreach(KeyValuePair<int,GameObject> attachStat in lp_totali){
                        if (!lp_totali_basic_rule[attachStat.Key].bool_morto){
                            if (id_attaccante!=attachStat.Key){
                                if (controlla_punto_attacco(x_att,y_att,lp_totali[attachStat.Key].transform.position.x,lp_totali[attachStat.Key].transform.position.y,raggio_sfera_attacco)){
                                    calcola_danno_combattimento(id_attaccante, attachStat.Key);
                                    if (lp_totali_basic_rule[id_difensore].up_melee_ignora_attacco==0){
                                        calcola_danno_combattimento(id_attaccante, attachStat.Key);
                                    } else {lp_totali_basic_rule[id_difensore].up_melee_ignora_attacco--;
                                        print ("incredibile! ignoro l'attacco!!! ("+lp_totali_basic_rule[id_difensore].up_melee_ignora_attacco+") --- ("+lp_totali[id_difensore].name+")");
                                    }
                                    num_colpire--;
                                    //lp_totali_basic_rule[id_attaccante].bool_stop_hit=true;
                                    if (num_colpire<=0){break;}
                                    //else {print ("incredibile! posso riattaccare ("+num_colpire+") --- ("+lp_totali[id_attaccante].name+")");}
                                }
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
        if (id_difensore==int_key_eroe){decrementa_potere_eroe();return;}
        float valore_danno=lp_totali_basic_rule[id_attaccante].danno;
        if (Random.Range(1,101)<=lp_totali_basic_rule[id_attaccante].per_critico){//è un critico!
            //print ("critico! on melee");
            valore_danno*=3;
            effetti.effetto_hit_critico(lp_totali[id_difensore].transform.position.x,lp_totali[id_difensore].transform.position.y+0.5f);
        }

        if (lp_totali_basic_rule[id_attaccante].velocita_proiettile==0){
            effetti.effetto_hit_melee(lp_totali[id_difensore].transform.position.x,lp_totali[id_difensore].transform.position.y+0.5f);
            if (lp_totali_basic_rule[id_attaccante].razza!="calabrone"){
                valore_danno-=lp_totali_basic_rule[id_difensore].armatura_melee;
            }
            if (lp_totali_basic_rule[id_attaccante].up_melee_dono_zanzare!=0){
                int percentuale=5;
                if (lp_totali_basic_rule[id_attaccante].up_melee_dono_zanzare==2){percentuale=8;}
                else if (lp_totali_basic_rule[id_attaccante].up_melee_dono_zanzare==3){percentuale=10;}
                //print ("curo con zanzare di: "+(lp_totali_basic_rule[id_attaccante].vitalita_max*percentuale/100)+" - "+lp_totali_basic_rule[id_attaccante].vitalita_max+"*"+percentuale+"/100");
                lp_totali_basic_rule[id_attaccante].cura(lp_totali_basic_rule[id_attaccante].vitalita_max*percentuale/100);
            }
        } else {//solo ai maghi succede di calcolare il danno da combattimento a distanza...
             effetti.effetto_hit_magic_sfera(lp_totali[id_difensore].transform.position.x,lp_totali[id_difensore].transform.position.y+0.5f);
        }
        if (lp_totali_basic_rule[id_attaccante].razza!="calabrone"){
            if (lp_totali_basic_rule[id_difensore].bool_armatura){valore_danno-=1;}
        }
        if (valore_danno<0.1f){valore_danno=0.1f;}  //farà sempre un minimo di danno.
        lp_totali_basic_rule[id_difensore].danneggia(valore_danno);

        switch (lp_totali_basic_rule[id_attaccante].razza){
            case "ragno":
            case "ragnetto":{
                lp_totali_basic_rule[id_difensore].applica_ragnatela(0.5f);
                break;
            }
            case "zanzara":{
                lp_totali_basic_rule[id_attaccante].cura(lp_totali_basic_rule[id_attaccante].vitalita_max/10);
                break;
            }
        }
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
        lp_totali_basic_rule[num_pupi_generati_totali].up_proiettili_ignora_armatura=lista_upgrade_perenni_liv["proiettili_ignora_armatura"];
        lp_totali_basic_rule[num_pupi_generati_totali].up_proiettili_head_shot=lista_upgrade_perenni_liv["proiettili_head_shot"];
        if (!lp_totali_basic_rule[num_pupi_generati_totali].bool_fazione_nemica){
            if (!lp_totali_basic_rule[num_pupi_generati_totali].bool_mago){
                if (lp_totali_basic_rule[num_pupi_generati_totali].velocita_proiettile!=0){
                    lp_totali_basic_rule[num_pupi_generati_totali].distanza_attacco+=lista_upgrade_perenni_liv["proiettili_distanza"];
                } else {//è un guerriero
                    if (lista_upgrade_perenni_liv["melee_velocita_attacco"]>0){                   
                        lp_totali_basic_rule[num_pupi_generati_totali].ritardo_attacco-=(lp_totali_basic_rule[num_pupi_generati_totali].ritardo_attacco/10*lista_upgrade_perenni_liv["melee_velocita_attacco"]);
                    }
                    lp_totali_basic_rule[num_pupi_generati_totali].up_pupetti_colpiti_contemporaneamente=lista_upgrade_perenni_liv["melee_colpiti"];
                    lp_totali_basic_rule[num_pupi_generati_totali].up_melee_ignora_attacco=lista_upgrade_perenni_liv["melee_ignora_attacco"];
                    lp_totali_basic_rule[num_pupi_generati_totali].up_melee_dono_zanzare=lista_upgrade_perenni_liv["melee_dono_zanzare"];
                }
            }
        }
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
            tier_unity_sbloccato=int.Parse(node.GetAttribute("tier_unity_sbloccato"));
            try{per_potere_eroe=float.Parse(node.GetAttribute("per_potere_eroe"));} catch{print ("Aspettiamo il prossimo update");}
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

    private void generazione_nemici(int num_ondata){
        Dictionary<string, float> punteggio_pupi = new Dictionary<string, float>();
        Dictionary<int, string> lista_pupi_temp = new Dictionary<int, string>();
        int num_pupi_g=0;
        basic_rule br_temp;

        string string_temp="";

        foreach (Transform child in lista_pupi.transform) {
            if ((!child.name.Contains("horseman"))&&(!child.name.Contains("zombie"))){
                if (
                    (!child.name.Contains("formica"))&&
                    (!child.name.Contains("ape"))&&
                    (!child.name.Contains("ragnetto"))&&
                    (!child.name.Contains("mosca"))&&
                    (!child.name.Contains("cavalletta"))&&
                    (!child.name.Contains("scarabeo"))
                ){
                    num_pupi_g++;
                    lista_pupi_temp.Add(num_pupi_g,child.name);
                    punteggio_pupi.Add(child.name,child.gameObject.GetComponent<basic_rule>().valore_pupo);
                    string_temp+="\n"+child.name+"-"+punteggio_pupi[child.name];
                } else if (child.name.Contains("formica_rossa")){
                    num_pupi_g++;
                    lista_pupi_temp.Add(num_pupi_g,child.name);
                    punteggio_pupi.Add(child.name,child.gameObject.GetComponent<basic_rule>().valore_pupo);
                    string_temp+="\n"+child.name+"-"+punteggio_pupi[child.name];
                }
            }
        }

        //print (string_temp);

        float punteggio_residuo=valore_iniziale_ondata+(valore_incrementale_ondata*num_ondata);
        int num_random=0;

        if (num_ondata<11){
            while(punteggio_residuo>0){
                num_random=Random.Range(1,(num_pupi_g+1));
                if (lista_pupi_temp[num_random].Contains("_1")){
                    genera_pupo(lista_pupi_temp[num_random]);
                    punteggio_residuo-=punteggio_pupi[lista_pupi_temp[num_random]];
                }
            }
        }
        else if (num_ondata<21){
            while(punteggio_residuo>0){
                num_random=Random.Range(1,(num_pupi_g+1));
                if ((lista_pupi_temp[num_random].Contains("_1"))||(lista_pupi_temp[num_random].Contains("_2"))){
                    genera_pupo(lista_pupi_temp[num_random]);
                    punteggio_residuo-=punteggio_pupi[lista_pupi_temp[num_random]];
                }
            }
        }
        else {//gran bollito
            while(punteggio_residuo>0){
                num_random=Random.Range(1,(num_pupi_g+1));
                genera_pupo(lista_pupi_temp[num_random]);
                punteggio_residuo-=punteggio_pupi[lista_pupi_temp[num_random]];
            }
        }
        //print ("punteggio residuo: "+punteggio_residuo);
    }
}
