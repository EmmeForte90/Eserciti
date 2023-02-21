using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effetti : MonoBehaviour
{
    public GameObject mappa;
    public GameObject particle_summon_brown;
    public GameObject particle_miele;
    public GameObject particle_ragnatele_1;
    public GameObject particle_ragnatele_2;
    public GameObject particle_ragnatele_3;
    public GameObject particle_ragnatele_grande;
    public GameObject particle_velocita;
    public GameObject particle_armatura;

    public void eff_evocazione_brown(float xar, float yar){
        GameObject go_temp;
        go_temp=Instantiate(particle_summon_brown);
        go_temp.transform.SetParent(mappa.transform);
        go_temp.transform.localPosition = new Vector3(xar, yar, 1f);
        go_temp.GetComponent<ParticleSystem>().Play();
    }

    public void eff_miele(float xar, float yar){
        GameObject go_temp;
        go_temp=Instantiate(particle_miele);
        go_temp.transform.SetParent(mappa.transform);
        go_temp.transform.localPosition = new Vector3(xar, yar, 1f);
        go_temp.GetComponent<ParticleSystem>().Play();
    }

    public void eff_ragnatele(float xar, float yar){
        GameObject go_temp;
        go_temp=Instantiate(particle_ragnatele_1);
        go_temp.transform.SetParent(mappa.transform);
        go_temp.transform.localPosition = new Vector3(xar, yar, 1f);
        go_temp.GetComponent<ParticleSystem>().Play();

        GameObject go_temp_2;
        go_temp_2=Instantiate(particle_ragnatele_2);
        go_temp_2.transform.SetParent(mappa.transform);
        go_temp_2.transform.localPosition = new Vector3(xar, yar, 1f);
        go_temp_2.GetComponent<ParticleSystem>().Play();

        GameObject go_temp_3;
        go_temp_3=Instantiate(particle_ragnatele_2);
        go_temp_3.transform.SetParent(mappa.transform);
        go_temp_3.transform.localPosition = new Vector3(xar, yar, 1f);
        go_temp_3.GetComponent<ParticleSystem>().Play();

        GameObject go_temp_4;
        go_temp_4=Instantiate(particle_ragnatele_grande);
        go_temp_4.transform.SetParent(mappa.transform);
        go_temp_4.transform.localPosition = new Vector3(xar, yar, 1f);
        go_temp_4.GetComponent<ParticleSystem>().Play();
    }

    public void eff_velocita(float xar, float yar){
        GameObject go_temp;
        go_temp=Instantiate(particle_velocita);
        go_temp.transform.SetParent(mappa.transform);
        go_temp.transform.localPosition = new Vector3(xar, yar, 1f);
        go_temp.GetComponent<ParticleSystem>().Play();
    }

    public void eff_armatura(float xar, float yar){
        GameObject go_temp;
        go_temp=Instantiate(particle_armatura);
        go_temp.transform.SetParent(mappa.transform);
        go_temp.transform.localPosition = new Vector3(xar, yar, 1f);
        go_temp.GetComponent<ParticleSystem>().Play();
    }
}
