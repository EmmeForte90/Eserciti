using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class basic_rule : MonoBehaviour
{
    public float velocita_movimento=5f;         //la velocità di movimento del personaggio
    public float vitalita_max=10f;              //la sua energia
    public bool bool_fazione_nemica=false;      //
    public float distanza_attacco=1f;            //indica la distanza dalla quale può iniziare ad attaccare un bersaglio (se è a distanza, allora questa distanza sarà più elevata)
    public float ritardo_attacco=1f;            //dopo quanto tempo può ricominciare ad attaccare
    public float raggio_sfera_attacco=0.7f;     //indica la sfera dal punto in cui colpisce; Se qualcuno ha una sfera più grande, potrebbe colpire più pupetti.
    public float danno=1f;                      //indica il danno che fà un pupetto ogni volta che attacca.
    public int ritardo_mov_random_min=2;       //indica il numero di secondi minimo che può passare tra un movimento random ed un altro.
    public int ritardo_mov_random_max=14;       //indica il numero di secondi massimo che può passare tra un movimento random ed un altro.
    public float armatura_melee=0f;             //indica il valore di danno sottratto quando si viene attaccati corpo a corpo
    public float armatura_distanza=0f;          //indica il valore di danno sottratto quando si viene attaccati a distanza
    public float velocita_proiettile=0f;        //la velocità del proiettile in caso attacca a distanza (0 vuol dire che attacca melee)
    public GameObject proiettile_pf;            //indica la tipologia di proiettile che genera quando attacca a distanza
    public bool bool_mago=false;
    public string razza;                        //la razza del pupetto
    public int per_critico=5;
    public int up_proiettili_ignora_armatura=0;
    public int up_proiettili_head_shot=0;
    public int up_pupetti_colpiti_contemporaneamente=0;
    public int up_melee_ignora_attacco=0;
    public int up_melee_dono_zanzare=0;
    public float danno_eroe=0;                  //è il danno che verrebbe inflitto in caso di eroe (o quanto cura)

    public float anim_velocita_attacco=1f;           //indica la velocità del movimento dell'attacco. Dipende dall'animazione in genere!
    public float anim_ritardo_morte=1f;

    //NON TOCCARE                  //la potenza del bump...
    public bool bool_morto=true;               //se prenderlo in considerazione o meno negli script; Non toccare
    public string stato="idle";                 //idle, wait (riposo cioè ripresa di attacco), attack, move
    public GameObject barra_energia_vuota_pf;
    public GameObject barra_energia;
    public GameObject barra_energia_vuota;
    
    public GameObject proiettile;

    //opzioni valide solo per le api
    public GameObject aculeo_pf;                //indica la tipologia dell'aculeo (valido solo per le API)
    public GameObject aculeo;                  
    public float aculeo_x;                      //sono le coordinate di qualcuno da attaccare in caso di morte
    public float aculeo_y;

    public float livello_veleno=0;
    public float vitalita;
    public bullet_rule valori_proiettile;
    public int id_difensore_mago=0;
    public GameObject mappa;
    public int int_key_pupo=0;
    private SkeletonAnimation skeletonAnimation;
    private bool bool_attivo=false;
    private float x_iniziale_freccia, y_iniziale_freccia;
    private string an_movimento_tipo="walk";
    private string an_vittoria_tipo="win";
    private string an_attacco_tipo="attack";

    private Rigidbody2D rb2D;
    private BoxCollider2D col2D;
    private bool bool_movimento_random=false;
    public float thrust=1f;  

    //blocco relativo alla direzione del personaggio
    private bool bool_dir_dx=true;
    private float horizontal;
    public float old_x;
    private Vector3[] waypoints;

    //blocco relativo ai possibili effetti del personaggi
    public bool bool_ragnatele;
    private float secondi_ragnatela=0;
    public bool bool_velocita;
    private float secondi_velocita=0;
    public float velocita_pupo_effetti=1f;
    public bool bool_armatura;
    private float secondi_armatura=0;

    //blocco relativo ai possibili particle dei personaggi
    public GameObject particle_sangue_1;
    public GameObject particle_sangue_2;
    public GameObject particle_sangue_toon;
    public GameObject particle_smoke;
    public GameObject eff_hit_distance;
    public GameObject eff_hit_distance_aculeo;
    public GameObject eff_hit_critico;
    public GameObject eff_cura;
    public GameObject eff_sparizione_eroe_basso;


    public float valore_pupo;
    public float valore_plus_pupo=0;
    public float livello;

    // Start is called before the first frame update
    void Awake(){
        livello_veleno=0;
        //per_critico=100;    //debug;
        bool_armatura=false;
        bool_velocita=false;
        bool_ragnatele=false;
        bool_movimento_random=false;
        thrust=50;
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        col2D = gameObject.GetComponent<BoxCollider2D>();
        bool_morto=true;    //è importante che si capisca che inizia da morto; Poi appena si attiva diventa bool_morto=false
        
        //settaggi globali per non cambiare tutti i pupi
        if (velocita_proiettile!=0){
            //velocita_proiettile*=1.5f;
            velocita_proiettile*=2f;
            distanza_attacco*=1.5f;
            distanza_attacco+=Random.Range(-1f,1f);
        }
        ritardo_attacco+=0.5f;          //in linea generale, bisogna sempre evitare che l'attacco sia più veloce o uguale alla velocità dell'animazione...
        //velocita_movimento*=1.2f;       //così si velocizzano un pò i ragazzi
        //velocita_movimento*=0.8f;

        livello=1;
        if (gameObject.name.Contains("_2")){livello=2;}
        else if (gameObject.name.Contains("_3")){livello=3;}

        if (livello>1){
            velocita_movimento+=(livello*0.8f);
            vitalita_max+=(livello*1.5f);
            danno+=(0.5f*livello);
            armatura_melee+=(0.5f*livello);
            armatura_distanza+=(0.5f*livello);

            //valore_pupo+=(livello*2);
        }

        valore_pupo+=(velocita_movimento*2f);
        valore_pupo+=(vitalita_max*4);
        valore_pupo-=(ritardo_attacco*2);
        valore_pupo+=(raggio_sfera_attacco);
        valore_pupo-=(danno*4);
        valore_pupo+=(armatura_melee*4);
        valore_pupo+=(armatura_distanza*4);
        valore_pupo+=(per_critico/10);

        if (bool_mago){valore_pupo+=(1*livello);}

        valore_pupo+=valore_plus_pupo;

        //print (gameObject.name+" - "+valore_pupo);
    }

    void Start(){
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        switch (razza){
            case "ape":
            case "calabrone":
            case "mosca":
            case "vespa":
            case "zanzara":{
                an_movimento_tipo="fly";
                an_vittoria_tipo="win_fly";
                an_attacco_tipo="attack_fly";
                break;
            }
            case "eroe":{
                skeletonAnimation.loop=false;
                skeletonAnimation.AnimationName="start";

                an_movimento_tipo="move";
                an_vittoria_tipo="idle";
                break;
            }
        }
        StartCoroutine(inizia_percorso_random_coroutine());
    }

    // Update is called once per frame
    void Update(){
        if (!bool_attivo){return;}
        if (bool_morto){return;}
        if (livello_veleno>0){
            danneggia(livello_veleno);
        }
        if (bool_ragnatele){
            if (secondi_ragnatela>0){
                secondi_ragnatela-=Time.deltaTime;
            } else {
                skeletonAnimation.state.GetCurrent(0).TimeScale = 1;
                bool_ragnatele=false;
                secondi_ragnatela=0;
                velocita_pupo_effetti=1;
            }
        } else if (bool_velocita){
            if (secondi_velocita>0){
                secondi_velocita-=Time.deltaTime;
            } else {
                skeletonAnimation.state.GetCurrent(0).TimeScale = 1;
                bool_velocita=false;
                velocita_pupo_effetti=1;
                secondi_velocita=0;
            }
        }
        if (bool_armatura){
            if (secondi_armatura>0){
                secondi_armatura-=Time.deltaTime;
            } else {
                bool_armatura=false;
                secondi_armatura=0;
            }
        }
        Flip();
        //if (int_key_pupo==1){print (int_key_pupo+" - "+stato);}
        switch (stato){
            case "wait":
            case "idle":{skeletonAnimation.AnimationName="idle";break;}
            case "move":{skeletonAnimation.AnimationName=an_movimento_tipo;break;}
            case "attack":{skeletonAnimation.AnimationName=an_attacco_tipo;break;}
        }
        /*
        if (stato!="move"){
            iTween.Stop();
        }
        */

        //proviamo a creare dei BUMP effect semplici per non far muovere il pupo sempre nella stessa direzione...
        if (Input.GetKeyDown("space")){
            inizia_percorso_random();
            //rb2D.AddForce(transform.up * thrust*50, ForceMode2D.Force);
            //rb2D.AddForce(new Vector2(Random.Range(-thrust,thrust),Random.Range(-thrust,thrust)), ForceMode2D.Impulse);
            //rb2D.AddForce(-transform.up * thrust, ForceMode2D.Impulse);
        }
    }

    public void disattiva_eroe(){
        bool_morto=true;
        skeletonAnimation.loop=false;
        skeletonAnimation.AnimationName="fine";
        StartCoroutine(effetto_fumo_basso_eroe());
    }
    private IEnumerator effetto_fumo_basso_eroe(){
        yield return new WaitForSeconds(0.5f);
        GameObject go_temp;
        go_temp=Instantiate(eff_sparizione_eroe_basso);
        go_temp.transform.SetParent(mappa.transform);
        go_temp.transform.localPosition = new Vector3(transform.position.x, transform.position.y, 1f);
        go_temp.SetActive(true);
    }

    public void esulta(){
        skeletonAnimation.AnimationName=an_vittoria_tipo;
        bool_attivo=false;
    }

    private IEnumerator inizia_percorso_random_coroutine(){
        int random=Random.Range(ritardo_mov_random_min,ritardo_mov_random_max);
        yield return new WaitForSeconds(random);
        inizia_percorso_random();
    }

    private IEnumerator attiva_pupo_coroutine(int battaglione, float x, float y){
        yield return new WaitForSeconds((battaglione-1)*1);
        if (razza=="eroe"){skeletonAnimation.loop=true;}
        gameObject.transform.localPosition=new Vector3(x, y, 1f);
        barra_energia_vuota=Instantiate(barra_energia_vuota_pf);
        barra_energia_vuota.transform.SetParent(gameObject.transform);
        barra_energia_vuota.transform.localPosition = new Vector3(-0.5f, -0.3f, 10f);
        barra_energia_vuota.transform.localScale=new Vector3(1f,0.25f,1);

        foreach (Transform child in barra_energia_vuota.transform) {
            if (child.name=="barra"){
                barra_energia=child.gameObject;
                barra_energia.transform.localPosition = new Vector3(1, -0, -9.6f);
                barra_energia.transform.localScale=new Vector3(-1f,1,1);
            }
        }

        vitalita=vitalita_max;

        if (proiettile_pf!=null){
            //proiettile=Instantiate(proiettile_pf);
            proiettile=proiettile_pf;

           //x_iniziale_freccia=proiettile.transform.position.x;
            //y_iniziale_freccia=proiettile.transform.position.y;

            y_iniziale_freccia=0.5f;
            //x_iniziale_freccia=-0.3f;

            proiettile.transform.SetParent(mappa.transform);
            proiettile.transform.localPosition = new Vector3(0f, 0f, 1f);
            if (!bool_mago){
                valori_proiettile=proiettile_pf.GetComponent<bullet_rule>();
                valori_proiettile.razza=razza;
                valori_proiettile.velocita=velocita_proiettile;
                valori_proiettile.danno=danno;
                valori_proiettile.bool_fazione_nemica=bool_fazione_nemica;
                valori_proiettile.per_critico=per_critico;
            } else {
                proiettile.SetActive(false);
            }
        }
        old_x=transform.position.x;
        bool_attivo=true;
        //gameObject.SetActive(true);   //in verità è già attvi, solo che non si vede con la sua z index; Va benissimo così!
        bool_morto=false;
    }

    public void attiva_pupo(int battaglione, float x, float y, bool bool_evocato){//perchè ogni pupo che si rispetti, deve essere attivato!
        float z=-1f;
        if (bool_evocato){z=1f;}
        gameObject.transform.localPosition=new Vector3(x, y, z);  //così sono invisibili al creato.
        gameObject.SetActive(true);
        StartCoroutine(attiva_pupo_coroutine(battaglione,x,y));
    }

    public void attiva_proiettile_mago(int id_difensore){
        proiettile.transform.localPosition = new Vector3(transform.position.x+x_iniziale_freccia, transform.position.y+y_iniziale_freccia, 1f);
        proiettile.SetActive(true);
        id_difensore_mago=id_difensore; //sarà questo che permetterà dall'altra parte (INIT) di proseguire
    }

    public void attiva_proiettile(float xar, float yar, int id_attaccante, int id_difensore){
        proiettile.transform.localPosition = new Vector3(transform.position.x+x_iniziale_freccia, transform.position.y+y_iniziale_freccia, 1f);
        //proiettile.transform.localPosition = new Vector3(transform.position.x, transform.position.y, 1f);
        valori_proiettile.setta_e_vai(xar,yar,id_attaccante);
    }

    public IEnumerator coroutine_ready() {
        yield return new WaitForSeconds(ritardo_attacco/velocita_pupo_effetti);
        stato="idle";
    }

    public void danneggia(float danni){
        if (danni<0){print ("Errore: i danni sono: "+danni);}
        if (bool_morto){return;}
        vitalita-=danni;
        if (vitalita<=0){morte_personaggio();return;}
        aggiorna_barra_energia();
    }

    public void applica_ragnatela(float valore_blocco){
        if (bool_morto){return;}
        bool_ragnatele=true;
        bool_velocita=false;
        velocita_pupo_effetti=0;
        secondi_velocita=0;
        secondi_ragnatela+=valore_blocco;
        skeletonAnimation.state.GetCurrent(0).TimeScale = 0;
    }

    public void applica_velocita(float valore){
        if (bool_morto){return;}
        if (bool_ragnatele){return;}
        bool_velocita=true;
        velocita_pupo_effetti=2;
        secondi_velocita+=valore;
        skeletonAnimation.state.GetCurrent(0).TimeScale = 2;
    }

    public void applica_armatura(float valore){
        if (bool_morto){return;}
        if (bool_ragnatele){return;}
        bool_armatura=true;
        secondi_armatura+=valore;
    }

    public void cura(float cura){
        vitalita+=cura;
        if (vitalita>vitalita_max){
            vitalita=vitalita_max;
        }
        aggiorna_barra_energia();
        
        GameObject go_temp;
        go_temp=Instantiate(eff_cura);
        go_temp.transform.SetParent(mappa.transform);
        go_temp.transform.localPosition = new Vector3(transform.position.x, transform.position.y+0.5f, 1f);
        go_temp.SetActive(true);
    }

    private void morte_personaggio(){
        //debug...
        if (bool_fazione_nemica){
            string id_pupo=gameObject.name;
            if (!PlayerPrefs.HasKey("num_pupi_morti_"+id_pupo)){
                PlayerPrefs.SetInt("num_pupi_morti_"+id_pupo,0);
            }
            int num_pupi_temp=PlayerPrefs.GetInt("num_pupi_morti_"+id_pupo)+1;
            PlayerPrefs.SetInt("num_pupi_morti_"+id_pupo,num_pupi_temp);
        }

        bool_morto=true;
        barra_energia.SetActive(false);
        barra_energia_vuota.SetActive(false);
        stato="die";
        skeletonAnimation.loop=false;
        skeletonAnimation.AnimationName="die";
        skeletonAnimation.state.GetCurrent(0).TimeScale = 1;    //perchè per effetti vari, potrebbe essere bloccato

        col2D.enabled=false;
        GetComponent<MeshRenderer>().sortingOrder-=2000;
        //StartCoroutine(disattiva_pupo());

        if (bool_mago){
            proiettile.SetActive(false);
        }

        if (razza=="ape"){
            aculeo=aculeo_pf;

            y_iniziale_freccia=0.5f;
            //x_iniziale_freccia=-0.3f;

            aculeo.transform.SetParent(mappa.transform);
            aculeo.SetActive(true);

            valori_proiettile=aculeo_pf.GetComponent<bullet_rule>();
            valori_proiettile.razza=razza;
            valori_proiettile.bool_fazione_nemica=bool_fazione_nemica;
            valori_proiettile.bool_aculeo=true;

            aculeo.transform.localPosition = new Vector3(transform.position.x+x_iniziale_freccia, transform.position.y+y_iniziale_freccia, 1f);
            valori_proiettile.setta_e_vai(aculeo_x,aculeo_y,int_key_pupo);
        }
        GameObject go_temp_2;
        go_temp_2=Instantiate(particle_sangue_toon);
        go_temp_2.transform.SetParent(mappa.transform);
        go_temp_2.transform.localPosition = new Vector3(transform.position.x, transform.position.y, 1f);
        go_temp_2.SetActive(true);

        GameObject go_temp;
        go_temp=Instantiate(particle_sangue_1);
        switch (Random.Range(1,3)){
            case 2:{go_temp=Instantiate(particle_sangue_2);break;}
        }
        go_temp.transform.SetParent(mappa.transform);
        go_temp.transform.localPosition = new Vector3(transform.position.x, transform.position.y, 1f);
        go_temp.GetComponent<ParticleSystem>().Play();
    }

    public void effetto_fumo(){
        GameObject go_temp_2;
        go_temp_2=Instantiate(particle_smoke);
        go_temp_2.transform.SetParent(mappa.transform);
        go_temp_2.transform.localPosition = new Vector3(transform.position.x, transform.position.y, 1f);
        go_temp_2.SetActive(true);
    }

    public IEnumerator disattiva_pupo() {
        yield return new WaitForSeconds(anim_ritardo_morte);
        //gameObject.SetActive(false);
    }

    private void aggiorna_barra_energia(){
        float percentuale=vitalita*100/vitalita_max;
        percentuale=percentuale*3/100;
        //percentuale*=0.7f;  //uff; Sembra che così non potremmo mai editare bene la grandezza per personaggi diversi

        //nuovo sistema:
        percentuale=vitalita/vitalita_max;
        barra_energia.transform.localScale = new Vector3 (-percentuale,1,1);
    }

    void OnTriggerEnter2D(Collider2D col){
        if (bool_morto){return;}
        //print ("ho urtato qualcosa: "+col.name+" ("+col.tag+")");
        if (col.tag=="proiettile"){
            bullet_rule br=col.GetComponent<bullet_rule>();
            if (!br.bool_attivo){return;}
            if (int_key_pupo!=br.id_attaccante){
                if (bool_fazione_nemica!=br.bool_fazione_nemica){//appartengono a due fazioni diverse
                    float danno=br.danno;
                    print ("danno prima: "+danno);
                    br.attiva_morte_proiettile();

                    if (Random.Range(1,101)<=br.per_critico){//è un critico!
                        danno*=3;
                        GameObject go_temp_cr;
                        go_temp_cr=Instantiate(eff_hit_critico);
                        go_temp_cr.transform.SetParent(mappa.transform);
                        go_temp_cr.transform.localPosition = new Vector3(transform.position.x, transform.position.y+0.5f, 1f);
                        go_temp_cr.SetActive(true);
                    }

                    bool bool_head_shot=false;
                    if (bool_fazione_nemica){
                        if (up_proiettili_head_shot!=0){
                            float per_random_head_shot=Random.Range(1,101);
                            if (per_random_head_shot<=up_proiettili_head_shot){
                                bool_head_shot=true;
                                print ("incredibile! headshot con "+per_random_head_shot);
                            }
                        }
                    }

                    if (!bool_head_shot){
                        bool bool_armatura=true;
                        if (br.razza!="calabrone"){
                            if (up_proiettili_ignora_armatura!=0){
                                if (bool_fazione_nemica){
                                    float per_random=Random.Range(1,101);
                                    float per_possibilita=(up_proiettili_ignora_armatura*25);
                                    //if (bool_fazione_nemica){print ("ho scagliato una freccia con "+up_proiettili_ignora_armatura+" (tiro: "+per_random+")");}
                                    if (per_random<=per_possibilita){
                                        bool_armatura=false;
                                        //if (bool_fazione_nemica){print ("incredibile! ignora l'armatura del bersaglio!!! ("+br.razza+")");}
                                    }
                                }
                            }
                        } else {bool_armatura=false;}
                        if (bool_armatura){
                            danno-=armatura_distanza;
                            //if (bool_armatura){danno-=1;}
                        }
                        print ("danno dopo: "+danno);
                        if (danno<0.1f){danno=0.1f;}  //farà sempre un minimo di danno.
                        danneggia(danno);
                    } else {morte_personaggio();}

                    if (!br.bool_aculeo){
                        GameObject go_temp;
                        go_temp=Instantiate(eff_hit_distance);
                        go_temp.transform.SetParent(mappa.transform);
                        go_temp.transform.localPosition = new Vector3(transform.position.x, transform.position.y+0.5f, 1f);
                        go_temp.SetActive(true);
                    } else {
                        GameObject go_temp;
                        go_temp=Instantiate(eff_hit_distance_aculeo);
                        go_temp.transform.SetParent(mappa.transform);
                        go_temp.transform.localPosition = new Vector3(transform.position.x, transform.position.y+0.5f, 1f);
                        go_temp.SetActive(true);
                    }
                }
            }
        }
        else if (col.name=="re_mosca_eroe"){
            if ((razza!="mosca")&&(razza!="zanzara")){
                print ("sono stato danneggiato dal re mosca!!!");
                danneggia(danno_eroe);
            }
        }
        else if (col.name=="regina_ape_eroe"){
            if (!bool_fazione_nemica){
                cura(danno_eroe);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.name != "cm_"+int_key_pupo ){
            Physics2D.IgnoreCollision(collision.collider, col2D);
        }
    }

    private void movimento_zigzag(int num){
        switch (razza){
            case "mosca":
            case "zanzara":{
                effetto_fumo();
                float thrust=50;
                rb2D.AddForce(new Vector2(Random.Range(-thrust,thrust),Random.Range(-thrust,thrust)), ForceMode2D.Impulse);
                bool_movimento_random=true;
                if (num!=0){
                    num++;
                    if (Random.Range(0,num)==0){
                        StartCoroutine(coro_prossimo_zigzag(num));
                    }
                    return;
                }
                termina_movimento_random();
                break;
            }
        }
    }

    private IEnumerator coro_prossimo_zigzag(int num){
        int random=Random.Range(ritardo_mov_random_min,ritardo_mov_random_max);
        yield return new WaitForSeconds(0.25f);
        movimento_zigzag(num);
    }

    private void inizia_percorso_random(){
        //if (stato=="attack"){StartCoroutine(inizia_percorso_random_coroutine());return;}        //in verità è più figo commentato
        if (!bool_attivo){return;}
        if (bool_morto){return;}
        if (bool_movimento_random){return;}
        if (bool_ragnatele){return;}

        switch (razza){
            case "mosca":
            case "zanzara":{
                if (Random.Range(1,5)!=4){
                    movimento_zigzag(1);
                    bool_movimento_random=true;
                    return;
                }
                break;
            }
        }

        float x=transform.position.x;
        float y=transform.position.y;
        float z=transform.position.z;
        float velocita_movimento_fattore=5/velocita_movimento*velocita_pupo_effetti;

        string tipo="";
        string direzione="dx";
        int random=Random.Range(1,9);
        float fattore_spostamento_random_pos=0;
        switch (random){
            case 1:
            case 2:
            case 3:
            case 4:{
                fattore_spostamento_random_pos=random*0.5f;
                tipo="random_pos";
                break;
            }
            case 5:{tipo="cerchio_basso";break;}
            case 6:{tipo="cerchio_alto";break;}
            case 7:{tipo="cerchio_basso_lento";break;}
            case 8:{tipo="cerchio_alto_lento";break;}
        }
        tipo="random_pos";
        if (bool_fazione_nemica){direzione="sx";}

        direzione="dx"; if (Random.Range(0,2)==0){direzione="sx";}  //commenta questa riga se vuoi togliere l'aleatorietà
        tipo+="_"+direzione;

        switch (tipo){
            case "random_pos_dx":
            case "random_pos_sx":{
                waypoints=new Vector3[]{
                    new Vector3(x+Random.Range(-fattore_spostamento_random_pos,fattore_spostamento_random_pos),y+Random.Range(-fattore_spostamento_random_pos,fattore_spostamento_random_pos),z),
                    new Vector3(x+Random.Range(-fattore_spostamento_random_pos,fattore_spostamento_random_pos),y+Random.Range(-fattore_spostamento_random_pos,fattore_spostamento_random_pos),z)
                };
                velocita_movimento_fattore*=Random.Range(1f,2f);
                break;
            }
            case "cerchio_basso_dx":{
                waypoints=new Vector3[]{new Vector3(x+0.5f,y-1,z), new Vector3(x+1,y-1.5f,z), new Vector3(x+2,y-2,z)};  //cerchio basso DX
                velocita_movimento_fattore*=Random.Range(0.5f,1.5f);
                break;
            }
            case "cerchio_alto_dx":{
                waypoints=new Vector3[]{new Vector3(x+0.5f,y+1,z), new Vector3(x+1,y+1.5f,z), new Vector3(x+2,y+2,z)};  //cerchio basso DX
                velocita_movimento_fattore*=Random.Range(0.5f,1.5f);
                break;
            }
            case "cerchio_basso_sx":{
                waypoints=new Vector3[]{new Vector3(x-0.5f,y-1,z), new Vector3(x-1,y-1.5f,z), new Vector3(x-2,y-2,z)};  //cerchio basso SX
                velocita_movimento_fattore*=Random.Range(0.5f,1.5f);
                break;
            }
            case "cerchio_alto_sx":{
                waypoints=new Vector3[]{new Vector3(x-0.5f,y+1,z), new Vector3(x-1,y+1.5f,z), new Vector3(x-2,y+2,z)};  //cerchio basso SX
                velocita_movimento_fattore*=Random.Range(0.5f,1.5f);
                break;
            }
            case "cerchio_basso_lento_dx":{
                waypoints=new Vector3[]{new Vector3(x+0.3f,y-0.7f,z), new Vector3(x+0.6f,y-1f,z), new Vector3(x+1.5f,y-1.5f,z)};  //cerchio basso DX
                velocita_movimento_fattore*=Random.Range(0.5f,1.2f);
                break;
            }
            case "cerchio_alto_lento_dx":{
                waypoints=new Vector3[]{new Vector3(x+0.3f,y+0.7f,z), new Vector3(x+0.6f,y+1f,z), new Vector3(x+1.5f,y+1.5f,z)};  //cerchio basso DX
                velocita_movimento_fattore*=Random.Range(0.5f,1.2f);
                break;
            }
            case "cerchio_basso_lento_sx":{
                waypoints=new Vector3[]{new Vector3(x-0.3f,y-0.7f,z), new Vector3(x-0.6f,y-1f,z), new Vector3(x-1.5f,y-1.5f,z)};  //cerchio basso SX
                velocita_movimento_fattore*=Random.Range(0.5f,1.2f);
                break;
            }
            case "cerchio_alto_lento_sx":{
                waypoints=new Vector3[]{new Vector3(x-0.3f,y+0.7f,z), new Vector3(x-0.6f,y+1f,z), new Vector3(x-1.5f,y+1.5f,z)};  //cerchio basso SX
                velocita_movimento_fattore*=Random.Range(0.5f,1.2f);
                break;
            }
        }
        iTween.MoveTo(gameObject, iTween.Hash("path", waypoints, "time", velocita_movimento_fattore, "easetype", iTween.EaseType.linear,"oncomplete", "termina_movimento_random")); 
        bool_movimento_random=true;
    }
    private void termina_movimento_random(){
        bool_movimento_random=false;
        StartCoroutine(inizia_percorso_random_coroutine());
    }

    public void anim_attacco(float x, float y){
        stato="attack";
        StartCoroutine(coroutine_ready());
    }

    private void Flip(){
        if (transform.position.x==old_x){return;}
        if (transform.position.x<old_x){horizontal=1;}
        else {horizontal=-1;}
        if (bool_dir_dx && horizontal < 0f || !bool_dir_dx && horizontal > 0f){
            bool_dir_dx = !bool_dir_dx;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;

            Vector3 localScale_barra = barra_energia_vuota.transform.localScale;
            Vector3 localPosition_barra = barra_energia_vuota.transform.localPosition;


            localScale_barra.x *= -1f;
            barra_energia_vuota.transform.localScale = localScale_barra;
            localPosition_barra.x*=-1f;
            barra_energia_vuota.transform.localPosition = localPosition_barra;

            /*
            if (localScale.x==1){
                localScale_barra.x = -1f;
                barra_energia_vuota.transform.localScale = localScale_barra;
                localPosition_barra.x=0.5f;
                barra_energia_vuota.transform.localPosition = localPosition_barra;
            } else {
                localScale_barra.x *= 1f;
                barra_energia_vuota.transform.localScale = localScale_barra;
                localPosition_barra.x=-0.5f;
                barra_energia_vuota.transform.localPosition = localPosition_barra;
            }
            */
        }
    }
}
