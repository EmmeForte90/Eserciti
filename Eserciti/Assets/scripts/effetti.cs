using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effetti : MonoBehaviour
{
    public GameObject mappa;
    public GameObject particle_summon_brown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void eff_evocazione_brown(float xar, float yar){
        GameObject go_temp;
        go_temp=Instantiate(particle_summon_brown);
        go_temp.transform.SetParent(mappa.transform);
        go_temp.transform.localPosition = new Vector3(xar, yar, 1f);
        go_temp.GetComponent<ParticleSystem>().Play();
    }
}
