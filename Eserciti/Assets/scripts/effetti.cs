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
    public GameObject particle_sangue_1;
    public GameObject particle_sangue_2;

    public GameObject eff_hit_melee;
    public GameObject eff_hit_magic_sfera;
    public GameObject eff_hit_critico;
    public GameObject eff_cura;
    public GameObject eff_resurrezione;

    public GameObject ps_eroe_formica;
    public GameObject ps_eroe_mosca;

    public GameObject eff_esplosione;
    public GameObject eff_esplosione_piccola;
    public GameObject eff_esplosione_velenosa;

    public void eff_evocazione_eroe(float xar, float yar, string id_hero){
        print ("attivo l'effetto per id_hero: "+id_hero);
        switch (id_hero){
            case "re_scarabeo":
            case "regina_formica_nera":{
                GameObject go_temp;
                go_temp=Instantiate(ps_eroe_formica);
                go_temp.transform.SetParent(mappa.transform);
                go_temp.transform.localPosition = new Vector3(xar, yar, 1f);
                go_temp.GetComponent<ParticleSystem>().Play();
                break;
            }
        }
    }

    public void effetto_esplosione(float xar, float yar){
        GameObject go_temp;
        go_temp=Instantiate(eff_esplosione);
        go_temp.transform.SetParent(mappa.transform);
        go_temp.transform.localPosition = new Vector3(xar, yar, 1f);
        go_temp.SetActive(true);
    }

    public void effetto_esplosione_piccola(float xar, float yar){
        GameObject go_temp;
        go_temp=Instantiate(eff_esplosione_piccola);
        go_temp.transform.SetParent(mappa.transform);
        go_temp.transform.localPosition = new Vector3(xar, yar, 1f);
        go_temp.SetActive(true);
    }

    public void effetto_esplosione_velenosa(float xar, float yar){
        GameObject go_temp;
        go_temp=Instantiate(eff_esplosione_velenosa);
        go_temp.transform.SetParent(mappa.transform);
        go_temp.transform.localPosition = new Vector3(xar, yar, 1f);
        go_temp.SetActive(true);
    }

    //in verità per ora lo usiamo direttamente sui pupi...
    public void effetto_cura(float xar, float yar){
        GameObject go_temp;
        go_temp=Instantiate(eff_cura);
        go_temp.transform.SetParent(mappa.transform);
        go_temp.transform.localPosition = new Vector3(xar, yar, 1f);
        go_temp.SetActive(true);
    }

    public void effetto_resurrezione(float xar, float yar){
        GameObject go_temp;
        go_temp=Instantiate(eff_resurrezione);
        go_temp.transform.SetParent(mappa.transform);
        go_temp.transform.localPosition = new Vector3(xar, yar, 1f);
        go_temp.SetActive(true);
    }

    public void effetto_hit_critico(float xar, float yar){
        GameObject go_temp;
        go_temp=Instantiate(eff_hit_critico);
        go_temp.transform.SetParent(mappa.transform);
        go_temp.transform.localPosition = new Vector3(xar, yar, 1f);
        go_temp.SetActive(true);
    }

    public void effetto_hit_magic_sfera(float xar, float yar){
        GameObject go_temp;
        go_temp=Instantiate(eff_hit_magic_sfera);
        go_temp.transform.SetParent(mappa.transform);
        go_temp.transform.localPosition = new Vector3(xar, yar, 1f);
        go_temp.SetActive(true);
    }

    public void effetto_hit_melee(float xar, float yar){
        GameObject go_temp;
        go_temp=Instantiate(eff_hit_melee);
        go_temp.transform.SetParent(mappa.transform);
        go_temp.transform.localPosition = new Vector3(xar, yar, 1f);
        go_temp.SetActive(true);
    }

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

    public void eff_sangue(float xar, float yar){
        GameObject go_temp;
        go_temp=Instantiate(particle_sangue_1);
        switch (Random.Range(1,3)){
            case 2:{go_temp=Instantiate(particle_sangue_2);break;}
        }
        go_temp.transform.SetParent(mappa.transform);
        go_temp.transform.localPosition = new Vector3(xar, yar, 1f);
        go_temp.GetComponent<ParticleSystem>().Play();
    }
}
