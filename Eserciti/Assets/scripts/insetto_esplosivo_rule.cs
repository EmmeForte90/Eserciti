using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class insetto_esplosivo_rule : MonoBehaviour
{
    public init init;
    private SkeletonAnimation skeletonAnimation;
    public string insetto_esplosivo_tipo="";

    //blocco relativo alla direzione del personaggio
    private bool bool_dir_dx=true;
    private float horizontal;
    public float old_x;
    private Vector3[] waypoints;

    // Update is called once per frame
    void Update()
    {
        Flip();
    }

    public void attiva(){
        old_x=transform.position.x;
        gameObject.SetActive(false);
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        skeletonAnimation.loop=false;
        skeletonAnimation.state.ClearTracks();
        skeletonAnimation.AnimationName="appear";
        gameObject.SetActive(true);

        StartCoroutine(inizia_movimenti_random());
    }

    private IEnumerator inizia_movimenti_random(){
        yield return new WaitForSeconds(0.5f);
        skeletonAnimation.loop=true;
        skeletonAnimation.AnimationName="walk";
        int time=5;
        float random_x;
        float random_y;
        float z=1;
        Vector3[] waypoints;
        int num_path=time*2;
        waypoints=new Vector3[num_path];
        random_x=transform.position.x;
        random_y=transform.position.y;

        float limite_x_sx=-23f;
        float limite_x_dx=30f;
        float limite_y_up=14f;
        float limite_y_down=-12f;

        for (int i=0;i<num_path;i++){
            random_x+=Random.Range(-1f,1f);
            random_y+=Random.Range(-1f,1f);

            if (random_x<limite_x_sx){random_x=limite_x_sx;}
            else if (random_x>limite_x_dx){random_x=limite_x_dx;}
            if (random_y>limite_y_up){random_y=limite_y_up;}
            else if (random_y<limite_y_down){random_y=limite_y_down;}
            
            waypoints[i]=new Vector3(random_x,random_y,1f);
        }

        //gameObject.transform.localPosition = new Vector3(0, 0, -11f);
        iTween.MoveTo(gameObject, iTween.Hash("path", waypoints, "time", time, "easetype", iTween.EaseType.linear));
        StartCoroutine(termina_effetto(time));
    }

    private IEnumerator termina_effetto(float time){
        yield return new WaitForSeconds(time);
        skeletonAnimation.loop=false;
        skeletonAnimation.state.ClearTracks();
        skeletonAnimation.AnimationName="explode";

        StartCoroutine(esplosione());
    }

    private IEnumerator esplosione(){
        yield return new WaitForSeconds(1.5f);
        init.insetto_esplode(insetto_esplosivo_tipo,transform.position.x,transform.position.y);
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
