using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class regina_ragno_rule : MonoBehaviour
{
    private SkeletonAnimation skeletonAnimation;
    private Rigidbody2D rb2D;
    private BoxCollider2D col2D;
    
    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        col2D = gameObject.GetComponent<BoxCollider2D>();
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        gameObject.SetActive(false);
    }


    public void disattiva(){
        iTween.Stop();
        skeletonAnimation.loop=false;
        skeletonAnimation.AnimationName="fine";
        StartCoroutine(disattiva_totale());
    }

    private IEnumerator disattiva_totale(){
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }

    public void attiva(){
        print ("attivo!");
        skeletonAnimation.loop=false;
        skeletonAnimation.state.ClearTracks();
        skeletonAnimation.AnimationName="start";
        gameObject.SetActive(true);
        StartCoroutine(inizia_azione_ragno());
    }

    private IEnumerator inizia_azione_ragno(){
        yield return new WaitForSeconds(2);
        print ("inizierò a fare casino...");


    }
}
