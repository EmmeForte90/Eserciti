using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainmenu : MonoBehaviour
{
    public GameObject sch_sel_personaggio;
    public GameObject sch_mainmenu;
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1280, 720, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void new_game(){
        sch_mainmenu.SetActive(false);
        sch_sel_personaggio.SetActive(true);
    }
}
