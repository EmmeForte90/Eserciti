using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
AUDIO MANCANTI
Fare un audio per ogni eroe o per lo meno quando l'eroe entra in campo
esplosione generica
audio di quando l'eroe và via
audio generico per l'evocazione dei pupi
audio zip/schiva quando le mosche, zanzare zippano o quando le cavallette schivano
? audio del fumo (per il gas)
    audio attacco warrior, archer, wizard
    audio hit
audio morte personaggio
audio freccia parte
*/

public class f_audio : MonoBehaviour
{
    //blocco dedicata all'audio
    public Dictionary<string, float> audio_singolo = new Dictionary<string, float>();
    public Dictionary<string, AudioSource> lista_audioData = new Dictionary<string, AudioSource>();
    public GameObject audio_default;
    private GameObject audio_temp;
    public GameObject GO_Suoni;

    // Start is called before the first frame update
    void Awake()
    {
        audio_singolo.Add("nicola_morte",1);
        audio_singolo.Add("nicola_acquista_upgrade",1);
        audio_singolo.Add("hit_carne_3",1);
        audio_singolo.Add("hit_carne_2",1);
        audio_singolo.Add("hit_carne_1",1);
        audio_singolo.Add("click_ok_2",1);
        audio_singolo.Add("resurrezione",1);
        audio_singolo.Add("denaro_3",1);
        audio_singolo.Add("denaro_2",1);
        audio_singolo.Add("denaro_1",1);
        audio_singolo.Add("colpo_3",0.2f);
        audio_singolo.Add("colpo_2",0.2f);
        audio_singolo.Add("colpo_1",0.2f);
        audio_singolo.Add("click_ok",1);
        audio_singolo.Add("click_generico_t",1);
        audio_singolo.Add("lancio_magia_nuvola_bianca",1);


        foreach(KeyValuePair<string,float> attachStat in audio_singolo){
            audio_temp=Instantiate(audio_default);
            audio_temp.GetComponent<AudioSource>().clip = Resources.Load("audio/"+attachStat.Key) as AudioClip;
            audio_temp.GetComponent<AudioSource>().volume = attachStat.Value;

            if (
                //(attachStat.Key=="music_main_menu")||
                //(attachStat.Key=="music_negozio_2")||
                //(attachStat.Key=="music_game_1")||
                (attachStat.Key=="lancio_magia_nuvola_bianca")
            ){audio_temp.GetComponent<AudioSource>().loop=true;}

            audio_temp.name="audio_"+attachStat.Key;
            audio_temp.transform.SetParent(GO_Suoni.transform);

            lista_audioData.Add(attachStat.Key,audio_temp.GetComponent<AudioSource>());
        }
    }

    public void stop_audio(string id_audio){
        try{
            lista_audioData[id_audio].Stop();
        } catch {}
    }

    public void play_audio(string id_audio){
        //lista_audioData[id_audio].Play(0);
        lista_audioData[id_audio].PlayOneShot(lista_audioData[id_audio].clip);
    }

    /*
    public void audio_f_play(string id_audio, float ritardo){
        if (id_audio.Contains("music_")){lista_audioData[id_audio].Play(0);return;}
        if (opzioni_booleane_gameobject["audio_audio_effects"].GetComponent<Toggle>().isOn){
            if (id_audio.Contains("lancio_magia_nuvola")){lista_audioData[id_audio].Play(0);}
            else {
                #if UNITY_ANDROID && !UNITY_EDITOR
                    android_FileID = lista_android_file_id[id_audio];
                    android_SoundID = AndroidNativeAudio.play(android_FileID);
                #else
                    //lista_audioData[id_audio].Play(0);
                    lista_audioData[id_audio].PlayOneShot(lista_audioData[id_audio].clip);
                #endif
            }
        }
    }
    */
}
