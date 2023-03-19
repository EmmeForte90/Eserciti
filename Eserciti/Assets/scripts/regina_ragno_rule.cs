using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class regina_ragno_rule : MonoBehaviour
{
    public init init;
    public GameObject mappa;
    private SkeletonAnimation skeletonAnimation;
    private Rigidbody2D rb2D;
    private BoxCollider2D col2D;
    public GameObject bomba_pf;
    public Dictionary<int, float> lista_bombe_attive = new Dictionary<int,float>();
    public Dictionary<int, GameObject> lista_bombe_GO = new Dictionary<int, GameObject>(); 
    public Dictionary<int, Vector3> lista_bombe_destinazione = new Dictionary<int, Vector3>();
    public Dictionary<int, Vector3> lista_bombe_mid_destinazione = new Dictionary<int, Vector3>(); 
    private bool bool_termina_devastazione=false;
    private int num_bombe_lanciate=0;
    private Vector3 pos_iniziale_bombe;
    float t;
    
    // Start is called before the first frame update
    void Start()
    {   
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        col2D = gameObject.GetComponent<BoxCollider2D>();
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        gameObject.SetActive(false);

        t=10f;
        Vector3 vor=new Vector3(-38,0.05f,-1);
        Vector3 var=new Vector3(-4,-1,-1);
        Vector3 vin=vor +(var -vor)/2 +Vector3.up *t;
        print ("vin: "+vin);
        Vector3 m1,m2;
        for (float i=0;i<=1;i+=0.1f){
            print (punto_parabola(vor,var,vin,t,i));
        }
        //lancio la bomba n. 1: (-38.00, 0.05, -1.00) - (-21.00, 0.02, -1.00) - (-4.00, -1.00, -1.00)
    }

    void Update(){
        if (num_bombe_lanciate!=0){
            foreach(KeyValuePair<int,GameObject> attachStat in lista_bombe_GO){
                if (lista_bombe_attive[attachStat.Key]<1){
                    lista_bombe_attive[attachStat.Key]+=0.01f;
                    //print ("bomba: "+attachStat.Key+" - "+lista_bombe_attive[attachStat.Key]);
                    attachStat.Value.transform.position=punto_parabola(pos_iniziale_bombe,lista_bombe_destinazione[attachStat.Key],lista_bombe_mid_destinazione[attachStat.Key],t,lista_bombe_attive[attachStat.Key]);

                    if (lista_bombe_attive[attachStat.Key]>=1){
                        disattiva_bomba(attachStat.Key);
                    }
                }
            }
        }
    }

    private void disattiva_bomba(int int_key_bomba){
        print ("disattivo la bomba "+int_key_bomba);
        lista_bombe_GO[int_key_bomba].SetActive(false);
    }

    private Vector3 punto_parabola(Vector3 start_point, Vector3 end_point, Vector3 mid_point, float t, float count){
        Vector3 vor=start_point;
        Vector3 var=end_point;
        Vector3 vin=mid_point;
        Vector3 m1,m2,v_temp;

        m1 = Vector3.Lerp(vor, vin, count);
        m2 = Vector3.Lerp(vin, var, count);
        v_temp = Vector3.Lerp(m1, m2, count);
        return v_temp;
    }


    public void disattiva(){
        print ("stò disattivando il mostro...");
        bool_termina_devastazione=true;
        skeletonAnimation.loop=false;
        skeletonAnimation.AnimationName="fine";
        StartCoroutine(disattiva_totale());
    }

    private IEnumerator disattiva_totale(){
        yield return new WaitForSeconds(1);
        //gameObject.SetActive(false);
    }

    public void attiva(){
        pos_iniziale_bombe=new Vector3(transform.position.x,transform.position.y+4,-1);
        skeletonAnimation.loop=false;
        skeletonAnimation.state.ClearTracks();
        skeletonAnimation.AnimationName="start";
        gameObject.SetActive(true);
        StartCoroutine(inizia_azione_ragno());
    }

    private IEnumerator inizia_azione_ragno(){
        yield return new WaitForSeconds(2);
        bool_termina_devastazione=false;

        num_bombe_lanciate=0;
        lista_bombe_GO.Clear();
        lista_bombe_attive.Clear();
        lista_bombe_destinazione.Clear();
        lista_bombe_mid_destinazione.Clear();

        StartCoroutine(lancia_bomba_anim());
    }

    private IEnumerator lancia_bomba_anim(){
        if (!bool_termina_devastazione){
            StartCoroutine(return_idle());
            skeletonAnimation.loop=false;
            skeletonAnimation.AnimationName="attack";
            yield return new WaitForSeconds(0.4f);
            if (!bool_termina_devastazione){
                lancia_bomba();
            }
        }
    }

    private IEnumerator return_idle(){
        if (!bool_termina_devastazione){
            yield return new WaitForSeconds(1);
            if (!bool_termina_devastazione){
                skeletonAnimation.loop=true;
                skeletonAnimation.AnimationName="idle";
            }
        }
    }

    private void lancia_bomba(){
        if (!bool_termina_devastazione){
            num_bombe_lanciate++;
            GameObject go_temp;
            go_temp=(GameObject)Instantiate(bomba_pf);
            go_temp.transform.SetParent(mappa.transform);
            go_temp.transform.localPosition = pos_iniziale_bombe;
            go_temp.SetActive(true);

            Vector3 destinazione=new Vector3(Random.Range(-5,15),Random.Range(-5,6),-1); //questo sistema forse deve cambiare...
            Vector3 vin=pos_iniziale_bombe+(destinazione-pos_iniziale_bombe)/2 +Vector3.up *t;
            lista_bombe_mid_destinazione.Add(num_bombe_lanciate,vin);
            lista_bombe_GO.Add(num_bombe_lanciate,go_temp);
            lista_bombe_destinazione.Add(num_bombe_lanciate,destinazione);
            lista_bombe_attive.Add(num_bombe_lanciate,0);


            print ("lancio la bomba n. "+num_bombe_lanciate+": "+pos_iniziale_bombe+" - "+vin+" - "+destinazione);

            StartCoroutine(lancia_next_bomba());
        }
    }
    private IEnumerator lancia_next_bomba(){
        if (!bool_termina_devastazione){
            yield return new WaitForSeconds(3);
            if (!bool_termina_devastazione){
                StartCoroutine(lancia_bomba_anim());
            }
        }
    }
}
