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
    public float velocita_attacco=1f;           //indica la velocità del movimento dell'attacco. Dipende dall'animazione in genere!
    public float ritardo_attacco=1f;            //dopo quanto tempo può ricominciare ad attaccare
    public float raggio_sfera_attacco=0.7f;     //indica la sfera dal punto in cui colpisce; Se qualcuno ha una sfera più grande, potrebbe colpire più pupetti.
    public float danno=1f;                      //indica il danno che fà un pupetto ogni volta che attacca.
    public float armatura_melee=0f;             //indica il valore di danno sottratto quando si viene attaccati corpo a corpo
    public float armatura_distanza=0f;          //indica il valore di danno sottratto quando si viene attaccati a distanza
    public float velocita_proiettile=0f;        //la velocità del proiettile in caso attacca a distanza (0 vuol dire che attacca melee)
    public GameObject proiettile_pf;            //indica la tipologia di proiettile che genera quando attacca a distanza
    public float ritardo_morte=1f;

    //NON TOCCARE
    public bool bool_morto=false;               //se prenderlo in considerazione o meno negli script; Non toccare
    public string stato="idle";                 //idle, wait (riposo cioè ripresa di attacco), attack, move
    private float x_att, y_att;                 //le sue coordinate quando ha attaccato qualcuno; In verità saranno deprecate, credo.
    public GameObject barra_energia_pf;
    public GameObject barra_energia_vuota_pf;
    public GameObject barra_energia;
    public GameObject barra_energia_vuota;
    public GameObject proiettile;
    public float vitalita;
    public bullet_rule valori_proiettile;
    public GameObject mappa;
    public int int_key_pupo=0;
    private SkeletonAnimation skeletonAnimation;
    private bool bool_attivo=false;
    public float x_iniziale_freccia, y_iniziale_freccia;

    //blocco relativo alla direzione del personaggio
    private bool bool_dir_dx=true;
    private float horizontal;
    private float old_x;

    // Start is called before the first frame update
    void Start(){
    }

    public void attiva_pupo(){//perchè ogni pupo che si rispetti, deve essere attivato!
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        barra_energia_vuota=Instantiate(barra_energia_vuota_pf);
        barra_energia_vuota.transform.SetParent(gameObject.transform);
        //barra_energia_vuota.transform.localPosition = new Vector3(0f, 0.6f, -0.5f);
        barra_energia_vuota.transform.localPosition = new Vector3(0f, -0.3f, -0.5f);

        barra_energia=Instantiate(barra_energia_pf);
        barra_energia.transform.SetParent(gameObject.transform);
        //barra_energia.transform.localPosition = new Vector3(0f, 0.6f, -0.6f);
        barra_energia.transform.localPosition = new Vector3(0f, -0.3f, -0.6f);
        vitalita=vitalita_max;

        if (proiettile_pf!=null){
            valori_proiettile=proiettile_pf.GetComponent<bullet_rule>();
            //proiettile=Instantiate(proiettile_pf);
            proiettile=proiettile_pf;

            x_iniziale_freccia=proiettile.transform.position.x;
            y_iniziale_freccia=proiettile.transform.position.y;

            proiettile.transform.SetParent(mappa.transform);
            proiettile.transform.localPosition = new Vector3(0f, 0f, 1f);

            valori_proiettile.velocita=velocita_proiettile;
            valori_proiettile.danno=danno;
            valori_proiettile.bool_fazione_nemica=bool_fazione_nemica;
        }
        old_x=transform.position.x;

        bool_attivo=true;
    }

    // Update is called once per frame
    void Update(){
        if (!bool_attivo){return;}
        if (bool_morto){return;}
        Flip();
        //if (int_key_pupo==1){print (int_key_pupo+" - "+stato);}
        switch (stato){
            case "wait":
            case "idle":{skeletonAnimation.AnimationName="idle";break;}
            case "move":{skeletonAnimation.AnimationName="walk";break;}
            case "attack":{skeletonAnimation.AnimationName="attack";break;}
        }
    }

    public void attiva_proiettile(float xar, float yar, int id_attaccante){
        proiettile.transform.localPosition = new Vector3(transform.position.x+x_iniziale_freccia, transform.position.y+y_iniziale_freccia, 1f);
        valori_proiettile.setta_e_vai(xar,yar,id_attaccante);
    }

    public IEnumerator coroutine_ready() {
        yield return new WaitForSeconds(ritardo_attacco);
        stato="idle";
    }

    public void danneggia(float danni){
        vitalita-=danni;
        if (vitalita<=0){morte_personaggio();return;}
        aggiorna_barra_energia();
    }

    private void morte_personaggio(){
        barra_energia.SetActive(false);
        stato="die";
        skeletonAnimation.AnimationName="die";
        skeletonAnimation.loop=false;
        bool_morto=true;
        StartCoroutine(disattiva_pupo());
    }

    public IEnumerator disattiva_pupo() {
        yield return new WaitForSeconds(ritardo_morte);
        gameObject.SetActive(false);
    }

    private void aggiorna_barra_energia(){
        float percentuale=vitalita*100/vitalita_max;
        percentuale=percentuale*3/100;
        //percentuale*=0.7f;  //uff; Sembra che così non potremmo mai editare bene la grandezza per personaggi diversi
       barra_energia.transform.localScale = new Vector3 (percentuale,0.5f,1);
    }

    void OnTriggerEnter2D(Collider2D col){
        //print ("ho urtato qualcosa: "+col.name+" ("+col.tag+")");
        if (col.tag=="proiettile"){
            bullet_rule br=col.GetComponent<bullet_rule>();
            if (!br.bool_attivo){return;}
            if (int_key_pupo!=br.id_attaccante){
                if (bool_fazione_nemica!=br.bool_fazione_nemica){//appartengono a due fazioni diverse
                    br.attiva_morte_proiettile();
                    danneggia(br.danno);
                }
            }
        }
    }

    /*
    public IEnumerator fine_mov_attacco() {
        yield return new WaitForSeconds(velocita_attacco);
        //print ("ho finito di sferrare l'attacco alle coordinate "+x_att+" - "+y_att);
        if (!bool_attacco_distanza){
            RaycastHit hit;
            Debug.DrawLine(new Vector3(x_att,y_att,10), new Vector3(x_att,y_att,-10), Color.white, 20.5f);
            Ray ray = new Ray(new Vector3(x_att,y_att,10), new Vector3(x_att,y_att,-10));
            if (Physics.Raycast(ray, out hit)) {
                print ("ho colpito qualcosa...");
            }
            if (hit.collider != null){
                print ("ho colpito qualcosa 2");
            }

            if (Physics.Raycast(ray, out hit, 100)){
                print ("ho colpito qualcosa 3");
            }
        }
    }
    */

    public void anim_attacco(float x, float y){
        stato="attack";
        x_att=x;
        y_att=y;
        //StartCoroutine(fine_mov_attacco());
        StartCoroutine(coroutine_ready());
    }

    private void Flip(){
        if (transform.position.x==old_x){return;}
        if (transform.position.x<old_x){horizontal=-1;}
        else {horizontal=1;}
        old_x=transform.position.x;
        if (bool_dir_dx && horizontal < 0f || !bool_dir_dx && horizontal > 0f){
            bool_dir_dx = !bool_dir_dx;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
