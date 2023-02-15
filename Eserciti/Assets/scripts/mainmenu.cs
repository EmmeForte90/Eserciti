using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainmenu : MonoBehaviour
{
    public info_comuni info_comuni;
    public TMPro.TextMeshProUGUI txt_nome_eroe;
    public TMPro.TextMeshProUGUI txt_descrizione_eroe;
    public TMPro.TextMeshProUGUI txt_nome_abilita;
    public TMPro.TextMeshProUGUI txt_descrizione_abilita;
    public GameObject lista_eroi;
    public Dictionary<string, GameObject> lista_obj_eroi = new Dictionary<string, GameObject>();

    public GameObject sch_sel_personaggio;
    public GameObject sch_mainmenu;
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1920, 1080, true);

        foreach (Transform child in lista_eroi.transform) {
            lista_obj_eroi.Add(child.name,child.gameObject);
        }

        nascondi_eroi();
    }

    public void new_game(){
        sch_mainmenu.SetActive(false);
        sch_sel_personaggio.SetActive(true);
    }

    private void nascondi_eroi(){
        foreach(KeyValuePair<string,GameObject> attachStat in lista_obj_eroi){
            attachStat.Value.SetActive(false);
        }
    }

    public void click_eroe(string id_eroe){
        string nome="";
        string descrizione="";
        string id_abilita="";
        switch (id_eroe){
            case "formica_nera":{
                id_abilita="evoca_formiche";
                nome="Black Ant";
                descrizione="Sinonimo d'invasione! Sembrano sempre poche ed innocenti ed invece...";
                break;
            }
            case "formica_rossa":{
                id_abilita="evoca_formiche";
                nome="Red Ant";
                descrizione="Sembrano carine ed innocenti, ma se ti prendono, iniziano a mordere!";
                break;
            }
        }
        txt_nome_eroe.SetText(nome);
        txt_descrizione_eroe.SetText(descrizione);

        print (info_comuni.lista_abilita_nome[id_abilita]);

        txt_nome_abilita.SetText(info_comuni.lista_abilita_nome[id_abilita]);
        txt_descrizione_abilita.SetText(info_comuni.lista_abilita_descrizione[id_abilita]);

        nascondi_eroi();
        lista_obj_eroi[id_eroe].SetActive(true);
    }
}
