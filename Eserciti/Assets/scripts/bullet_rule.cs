using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_rule : MonoBehaviour
{
    //queste variabili vengono settate dal pupo nel momento che viene creato; Vengono settati sempre dal pupo, non dalla freccia
    public float velocita=0f;               //la velocità del proiettile
    public float danno=0f;                  //il danno
    public bool bool_fazione_nemica=true;   //la tipologia di fazione a cui fà parte
    public bool bool_attivo=false;          //se è attivo o meno
    public float xar=0f;                    //le coordinate di arrivo
    public float yar=0f;
    public int id_attaccante=0;             //int key del pupo che attacca
    private float angolo_proiettile;
    public bool bool_aculeo=false;
    public int per_critico=0;

    // Start is called before the first frame update
    void Start(){
        gameObject.SetActive(false);
    }

    public void setta_e_vai(float xar_f, float yar_f,int id_attaccante_f){
        angolo_proiettile=calcola_angolo(transform.position.x,transform.position.y,xar_f,yar_f);
        angolo_proiettile-=135f;
        gameObject.transform.rotation = Quaternion.Euler(0,0,angolo_proiettile);

        id_attaccante=id_attaccante_f;
        gameObject.SetActive(true);
        xar=xar_f;
        yar=yar_f;
        bool_attivo=true;
    }

    public void attiva_morte_proiettile(){
        bool_attivo=false;
        gameObject.SetActive(false);
    }

    /*
    void OnTriggerEnter2D(Collider2D col){
        print ("collido triggerando freccia con tag: "+col.tag+" ed il mio ID_ATTACCANTE è uguale a "+id_attaccante);
    }
    */

    // Update is called once per frame
    void Update()
    {
        if (bool_attivo){
            transform.position=Vector3.MoveTowards(transform.position, new Vector3(xar, yar, transform.position.z),velocita * Time.deltaTime);
            if(Vector3.Distance(transform.position, new Vector3(xar, yar, transform.position.z)) < 0.1f){
                attiva_morte_proiettile();
            }
        }
    }

    public float calcola_angolo(float xor, float yor, float xar, float yar){
        Vector2 vec1, vec2; 
        vec1.x=xor;         
        vec1.y=yor;         
        vec2.x=xar;         
        vec2.y=yar;         
        Vector2 diference = vec2 - vec1;
        float sign = (vec2.y < vec1.y)? -1.0f : 1.0f;
        return Vector2.Angle(Vector2.right, diference) * sign;
    }
}
