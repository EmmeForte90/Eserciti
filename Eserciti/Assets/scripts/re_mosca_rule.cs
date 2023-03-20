using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class re_mosca_rule : MonoBehaviour
{
    private SkeletonAnimation skeletonAnimation;
    private Rigidbody2D rb2D;
    private BoxCollider2D col2D;

    //blocco relativo alla direzione del personaggio
    private bool bool_dir_dx=true;
    private float horizontal;
    public float old_x;
    private Vector3[] waypoints;

    public GameObject ps_eroe_mosca;
    public GameObject ps_eroe_mosca_escape;
    public GameObject particle_poche_mosche;

    private bool bool_mosche_poche_attive=false;
    
    // Start is called before the first frame update
    void Start()
    {
        bool_mosche_poche_attive=false;
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        col2D = gameObject.GetComponent<BoxCollider2D>();
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        old_x=transform.position.x;
        gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        Flip();
        if (bool_mosche_poche_attive){
            GameObject go_temp;
            go_temp=Instantiate(particle_poche_mosche);
            go_temp.transform.SetParent(gameObject.transform);
            go_temp.transform.localPosition = new Vector3(0, 0, -10f);
            go_temp.SetActive(true);
        }
    }

    public void attiva_ps_azzurro_evocazione(){
        GameObject go_temp;
        go_temp=Instantiate(ps_eroe_mosca);
        go_temp.transform.SetParent(gameObject.transform);
        go_temp.transform.localPosition = new Vector3(0, 0, -10f);
        go_temp.GetComponent<ParticleSystem>().Play();
    }

    public void disattiva(){
        bool_mosche_poche_attive=false;
        iTween.Stop(gameObject);
        skeletonAnimation.loop=false;
        skeletonAnimation.AnimationName="fine";

        GameObject go_temp;
        go_temp=Instantiate(ps_eroe_mosca_escape);
        go_temp.transform.SetParent(gameObject.transform);
        go_temp.transform.localPosition = new Vector3(0, 0, -10f);
        go_temp.GetComponent<ParticleSystem>().Play();

        StartCoroutine(disattiva_totale());
    }

    private IEnumerator disattiva_totale(){
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }

    public void attiva(){
        skeletonAnimation.loop=false;
        skeletonAnimation.state.ClearTracks();
        skeletonAnimation.AnimationName="start";
        gameObject.SetActive(true);
        attiva_ps_azzurro_evocazione();
        StartCoroutine(inizia_movimenti_random());
        bool_mosche_poche_attive=true;
    }

    private IEnumerator inizia_movimenti_random(){
        yield return new WaitForSeconds(1);
        skeletonAnimation.loop=true;
        skeletonAnimation.AnimationName="skill";
        int time=20;
        float random_x;
        float random_y;
        float z=1;
        Vector3[] waypoints;
        int num_path=time*2;
        waypoints=new Vector3[num_path];
        random_x=0;
        random_y=0;

        float limite_x_sx=-23f;
        float limite_x_dx=30f;
        float limite_y_up=14f;
        float limite_y_down=-12f;

        for (int i=0;i<num_path;i++){
            random_x+=Random.Range(-5f,5f);
            random_y+=Random.Range(-5f,5f);

            if (random_x<limite_x_sx){random_x=limite_x_sx;}
            else if (random_x>limite_x_dx){random_x=limite_x_dx;}
            if (random_y>limite_y_up){random_y=limite_y_up;}
            else if (random_y<limite_y_down){random_y=limite_y_down;}

            //metodo secondo, a cazzum
            random_x=Random.Range(limite_x_sx,limite_x_dx);
            random_y=Random.Range(limite_y_up,limite_y_down);
            

            waypoints[i]=new Vector3(random_x,random_y,1f);
        }

        //gameObject.transform.localPosition = new Vector3(0, 0, -11f);
        iTween.MoveTo(gameObject, iTween.Hash("path", waypoints, "time", time, "easetype", iTween.EaseType.linear));

        //StartCoroutine(termina_effetto(time));
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
        }
    }
}