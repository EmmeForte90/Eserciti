using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class abilita_balestra : MonoBehaviour
{
    public init init;
    public GameObject bombo_pf;
    public Dictionary<int, float> lista_bombo_attivi = new Dictionary<int,float>();
    public Dictionary<int, GameObject> lista_bombo_GO = new Dictionary<int, GameObject>(); 
    public Dictionary<int, Vector3> lista_bombo_origine = new Dictionary<int, Vector3>();
    public Dictionary<int, Vector3> lista_bombo_destinazione = new Dictionary<int, Vector3>();
    public Dictionary<int, Vector3> lista_bombo_mid_destinazione = new Dictionary<int, Vector3>(); 
    public Dictionary<int, float> lista_bombo_rotazione = new Dictionary<int,float>();
    public Dictionary<int, bool> lista_bombo_ritardo = new Dictionary<int,bool>();
    private bool bool_attivo=false;
    private float t;

    // Start is called before the first frame update
    void Start()
    {
        t=5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (bool_attivo){
            foreach(KeyValuePair<int,Vector3> attachStat in lista_bombo_destinazione){
                if (lista_bombo_ritardo[attachStat.Key]){
                    if (lista_bombo_attivi[attachStat.Key]<1){
                        lista_bombo_attivi[attachStat.Key]+=0.01f;
                        lista_bombo_GO[attachStat.Key].transform.position=punto_parabola(lista_bombo_origine[attachStat.Key],lista_bombo_destinazione[attachStat.Key],lista_bombo_mid_destinazione[attachStat.Key],t,lista_bombo_attivi[attachStat.Key]);
                        //lista_bombo_GO[attachStat.Key].transform.Rotate(0,0,6*lista_bombo_rotazione[attachStat.Key]*Time.deltaTime);

                        if (lista_bombo_attivi[attachStat.Key]>=1){
                            lista_bombo_GO[attachStat.Key].SetActive(false);
                            init.bomba("bomba_balestra",lista_bombo_destinazione[attachStat.Key].x,lista_bombo_destinazione[attachStat.Key].y);
                        }
                    } else {
                        lista_bombo_ritardo[attachStat.Key]=false;
                    }
                }
            }
        }
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

    public void resetta(){
        lista_bombo_attivi.Clear();
        lista_bombo_GO.Clear();
        lista_bombo_destinazione.Clear();
        lista_bombo_origine.Clear();
        lista_bombo_mid_destinazione.Clear();
        lista_bombo_rotazione.Clear();
        lista_bombo_ritardo.Clear();
    }

    public void aggiungi(int num, int liv, float xar, float yar){
        float xor=-50f+Random.Range(0,10f);
        float yor=-5f+Random.Range(0,10f);
        Vector3 destinazione=new Vector3(xar,yar,-1);
        GameObject go_temp;

        go_temp=Instantiate(bombo_pf);
        go_temp.transform.SetParent(gameObject.transform);
        go_temp.transform.localPosition = new Vector3(xor, yor, -10f);
        go_temp.SetActive(true);
        lista_bombo_attivi.Add(num,0);
        lista_bombo_GO.Add(num,go_temp);
        lista_bombo_origine.Add(num,go_temp.transform.localPosition);
        lista_bombo_destinazione.Add(num,destinazione);

        Vector3 vin=go_temp.transform.localPosition+(destinazione-go_temp.transform.localPosition)/2 +Vector3.up *t;
        lista_bombo_mid_destinazione.Add(num,vin);

        lista_bombo_ritardo.Add(num,false);

        StartCoroutine(bombo_ritardo(num));

        lista_bombo_rotazione.Add(num,Random.Range(0,20));
        if (Random.Range(0,2)==1){lista_bombo_rotazione[num]*=-1;}
    }

    private IEnumerator bombo_ritardo(int num){
        yield return new WaitForSeconds(num*0.5f);
        lista_bombo_ritardo[num]=true;
    }
    
    public void avvia(){
        bool_attivo=true;
    }
}
