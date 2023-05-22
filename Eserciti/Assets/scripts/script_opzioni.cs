using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_opzioni : MonoBehaviour
{
    public info_comuni info_comuni;
    public TMPro.TextMeshProUGUI Titolo_Setting;
    public TMPro.TextMeshProUGUI testo_opzioni_volume_musica;
    public TMPro.TextMeshProUGUI testo_opzioni_volume_sfx;
    public TMPro.TextMeshProUGUI testo_opzioni_lingue;

    void Start(){
        cambia_lingua_opzioni(PlayerPrefs.GetString("lingua"));
    }

    public void cambia_lingua(string lingua){
        info_comuni.setta_lingua(lingua);
        cambia_lingua_opzioni(lingua);
    }

    public void cambia_lingua_opzioni(string lingua){
        switch (lingua){
            case "italiano":{
                Titolo_Setting.SetText("Opzioni");
                testo_opzioni_volume_musica.SetText("Volume musica");
                testo_opzioni_volume_sfx.SetText("Volume effetti");
                testo_opzioni_lingue.SetText("Lingua");
                break;
            }
            default:{
                Titolo_Setting.SetText("Settings");
                testo_opzioni_volume_musica.SetText("Music Volume");
                testo_opzioni_volume_sfx.SetText("SFX Volume");
                testo_opzioni_lingue.SetText("Language");
                break;
            }
        }
    }
}
