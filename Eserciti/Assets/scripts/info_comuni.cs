using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class info_comuni : MonoBehaviour
{
    public Dictionary<string, string> lista_powerhero_descrizione = new Dictionary<string, string>();

    public Dictionary<string, string> lista_premio_nome = new Dictionary<string, string>();
    public Dictionary<string, string> lista_premio_descrizione = new Dictionary<string, string>();

    public Dictionary<string, string> lista_abilita_nome = new Dictionary<string, string>();
    public Dictionary<string, string> lista_abilita_descrizione = new Dictionary<string, string>();
    public Dictionary<string, Dictionary<int, int>> lista_abilita_cooldown = new Dictionary<string, Dictionary<int, int>>();
    public Dictionary<string, Dictionary<int, int>> lista_abilita_cooldown_partenza = new Dictionary<string, Dictionary<int, int>>();
    public Dictionary<string, bool> lista_bool_abilita_classe = new Dictionary<string, bool>();

    public Dictionary<string, string> lista_razze_totale = new Dictionary<string, string>();
    public Dictionary<string, string> lista_razza_pupi_nome = new Dictionary<string, string>();
    public Dictionary<string, string> lista_pupi_descrizione = new Dictionary<string, string>();
    public Dictionary<string, string> lista_classi_nome = new Dictionary<string, string>();
    public Dictionary<string, int> lista_costo_unita_razza = new Dictionary<string, int>();
    // Start is called before the first frame update

    public Dictionary<string, string> lista_upgrade_perenni_nome = new Dictionary<string, string>();
    public Dictionary<string, int> lista_upgrade_perenni_max_level = new Dictionary<string, int>();
    public Dictionary<string, Dictionary<int, int>> lista_upgrade_perenni_costi = new Dictionary<string, Dictionary<int, int>>();
    public Dictionary<string, Dictionary<int, string>> lista_upgrade_perenni_descrizione = new Dictionary<string, Dictionary<int, string>>();

    public Dictionary<string, float> lista_incremento_potere_eroe = new Dictionary<string, float>();
    public Dictionary<string, float> lista_decremento_potere_eroe = new Dictionary<string, float>();

    private string string_temp;
    void Awake(){
        lista_premio_nome.Add("end_hero_regina_formica_nera","Black Ant");
        lista_premio_nome.Add("end_hero_regina_re_mosca","King Moss");
        lista_premio_nome.Add("end_hero_regina_regina_ape","Queen Abe");
        lista_premio_nome.Add("end_hero_regina_regina_ragno","Queen Web");
        lista_premio_nome.Add("end_hero_regina_re_cavalletta","King Gross");
        lista_premio_nome.Add("end_hero_regina_re_scarabeo","King Big");
        lista_premio_nome.Add("reach_stage_10","Stage 10");
        lista_premio_nome.Add("reach_stage_20","Stage 20");
        lista_premio_nome.Add("reach_stage_30","Stage 30");
        lista_premio_nome.Add("gain_10000_gold","Big money");
        lista_premio_nome.Add("gain_100_gems","A Diamond is forever");
        lista_premio_nome.Add("gain_1000_gems","But many Diamonds are better");
        /*
        lista_premio_nome.Add("unlock_all_race_level_1","Titolo");
        lista_premio_nome.Add("unlock_all_race_level_2","Titolo");
        lista_premio_nome.Add("unlock_all_race_level_3","Titolo");
        */

        lista_premio_descrizione.Add("end_hero_regina_formica_nera","Reach stage 40 with Black Ant");
        lista_premio_descrizione.Add("end_hero_regina_re_mosca","Reach stage 40 with King Moss");
        lista_premio_descrizione.Add("end_hero_regina_regina_ape","Reach stage 40 with Queen Abe");
        lista_premio_descrizione.Add("end_hero_regina_regina_ragno","Reach stage 40 with Queen Web");
        lista_premio_descrizione.Add("end_hero_regina_re_cavalletta","Reach stage 40 with King Gross");
        lista_premio_descrizione.Add("end_hero_regina_re_scarabeo","Reach stage 40 with King Big");
        lista_premio_descrizione.Add("reach_stage_10","Reach stage 10");
        lista_premio_descrizione.Add("reach_stage_20","Reach stage 20");
        lista_premio_descrizione.Add("reach_stage_30","Reach stage 30");
        lista_premio_descrizione.Add("gain_10000_gold","Earn 10000 Coins");
        lista_premio_descrizione.Add("gain_100_gems","Earn 100 gems");
        lista_premio_descrizione.Add("gain_1000_gems","Earn 1000 gems");
        /*
        lista_premio_descrizione.Add("unlock_all_race_level_1","Unlock all kind of puppets level 1");
        lista_premio_descrizione.Add("unlock_all_race_level_2","Unlock all kind of puppets level 2");
        lista_premio_descrizione.Add("unlock_all_race_level_3","Unlock all kind of puppets level 3 ");
        */

        //non tradurre nulla
        lista_incremento_potere_eroe.Add("regina_formica_nera",0.01f);
        lista_incremento_potere_eroe.Add("re_mosca",0.01f);
        lista_incremento_potere_eroe.Add("regina_ape",0.01f);
        lista_incremento_potere_eroe.Add("regina_ragno",0.015f);
        lista_incremento_potere_eroe.Add("re_cavalletta",0.02f);
        lista_incremento_potere_eroe.Add("re_scarabeo",0.01f);

        //0.1f corrisponde grossomodo a 17 secondi
        //0.15 corrisponde grossomodo a 9 secondi
        lista_decremento_potere_eroe.Add("regina_formica_nera",0.1f);
        lista_decremento_potere_eroe.Add("re_mosca",0.1f);
        lista_decremento_potere_eroe.Add("regina_ape",0.1f);
        lista_decremento_potere_eroe.Add("regina_ragno",0.1f);
        lista_decremento_potere_eroe.Add("re_cavalletta",0.1f);
        lista_decremento_potere_eroe.Add("re_scarabeo",0.1f);

        //tradurre la parte destra di questo blocco
        lista_powerhero_descrizione.Add("regina_formica_nera","Invoke the Queen Ant to detroy the enemy puppets");
        lista_powerhero_descrizione.Add("re_mosca","Invoke the King Mos, he going around and damage all enemies puppets in the arena");
        lista_powerhero_descrizione.Add("regina_ape","Invoke the Queen Abe, she going around and heal all your puppets in the arena");
        lista_powerhero_descrizione.Add("regina_ragno","Invoke the Spider Queen Weeb, she throws webs balls that reduce the speed of enemy puppets");
        lista_powerhero_descrizione.Add("re_cavalletta","Invoke the King Grass, he throws balls of earth to damage enemy puppets.");
        lista_powerhero_descrizione.Add("re_scarabeo","Invoke the King Big to detroy the enemy puppets");

        //UPGRADE PERENNI traduerree la parte a destra di questo blocco
        lista_upgrade_perenni_nome.Add("costi_pupi","Cost Army");
        lista_upgrade_perenni_nome.Add("costi_guadagno","Improve earn");
        lista_upgrade_perenni_nome.Add("costi_abilita","Cost skills");
        lista_upgrade_perenni_nome.Add("melee_velocita_attacco","Melee attack speed");
        lista_upgrade_perenni_nome.Add("melee_colpiti","Melee More hits");
        lista_upgrade_perenni_nome.Add("melee_ignora_attacco","Ignore attacks");
        lista_upgrade_perenni_nome.Add("melee_dono_zanzare","Lifesteal");
        lista_upgrade_perenni_nome.Add("proiettili_ignora_armatura","Ignore Armor");
        lista_upgrade_perenni_nome.Add("proiettili_head_shot","Head Shot");
        lista_upgrade_perenni_nome.Add("proiettili_distanza","Max Distance");

        lista_upgrade_perenni_nome.Add("magia_veleno","Poison Spells");
        lista_upgrade_perenni_nome.Add("magia_blocco","Blocking Spells");

        lista_upgrade_perenni_max_level.Add("proiettili_ignora_armatura",4);
        lista_upgrade_perenni_max_level.Add("proiettili_head_shot",5);
        lista_upgrade_perenni_max_level.Add("proiettili_distanza",3);
        lista_upgrade_perenni_max_level.Add("costi_pupi",3);
        lista_upgrade_perenni_max_level.Add("costi_guadagno",5);
        lista_upgrade_perenni_max_level.Add("costi_abilita",4);
        lista_upgrade_perenni_max_level.Add("melee_velocita_attacco",4);
        lista_upgrade_perenni_max_level.Add("melee_colpiti",2);
        lista_upgrade_perenni_max_level.Add("melee_ignora_attacco",3);
        lista_upgrade_perenni_max_level.Add("melee_dono_zanzare",3);

        lista_upgrade_perenni_max_level.Add("magia_veleno",3);
        lista_upgrade_perenni_max_level.Add("magia_blocco",3);

        //settiamo genericamente che ogni upgrade perenne ha un costo di 10 per livello che si vuole ottenere
        foreach(KeyValuePair<string,string> attachStat in lista_upgrade_perenni_nome){
            lista_upgrade_perenni_costi.Add(attachStat.Key,new Dictionary<int, int>());
            lista_upgrade_perenni_descrizione.Add(attachStat.Key,new Dictionary<int, string>());
            for (int i=1;i<=lista_upgrade_perenni_max_level[attachStat.Key];i++){
                lista_upgrade_perenni_costi[attachStat.Key].Add(i,(10*i));
                lista_upgrade_perenni_descrizione[attachStat.Key].Add(i,"Descrizione "+i);
            }
        }
        //UPGRADE PERENNI - traduerree la parte a destra di questi blocchi
        lista_upgrade_perenni_descrizione["costi_pupi"][1]="The puppets cost 5 less";
        lista_upgrade_perenni_descrizione["costi_pupi"][2]="The puppets cost 10 less";
        lista_upgrade_perenni_descrizione["costi_pupi"][3]="The puppets cost 15 less";

        lista_upgrade_perenni_descrizione["costi_guadagno"][1]="You earn +10 at the end of the stage";
        lista_upgrade_perenni_descrizione["costi_guadagno"][2]="You earn +20 at the end of the stage";
        lista_upgrade_perenni_descrizione["costi_guadagno"][3]="You earn +30 at the end of the stage";
        lista_upgrade_perenni_descrizione["costi_guadagno"][4]="You earn +40 at the end of the stage";
        lista_upgrade_perenni_descrizione["costi_guadagno"][5]="You earn +50 at the end of the stage";

        lista_upgrade_perenni_descrizione["costi_abilita"][1]="Reduces the cost of abilities by 5";
        lista_upgrade_perenni_descrizione["costi_abilita"][2]="Reduces the cost of abilities by 10";
        lista_upgrade_perenni_descrizione["costi_abilita"][3]="Reduces the cost of abilities by 15";
        lista_upgrade_perenni_descrizione["costi_abilita"][4]="Reduces the cost of abilities by 20";

        lista_upgrade_perenni_descrizione["melee_velocita_attacco"][1]="Increases melee attack speed by 5%";
        lista_upgrade_perenni_descrizione["melee_velocita_attacco"][2]="Increases melee attack speed by 10%";
        lista_upgrade_perenni_descrizione["melee_velocita_attacco"][3]="Increases melee attack speed by 15%";
        lista_upgrade_perenni_descrizione["melee_velocita_attacco"][4]="Increases melee attack speed by 20%";

        lista_upgrade_perenni_descrizione["melee_colpiti"][1]="Melee attacks can hit 2 additional puppets at the same time";
        lista_upgrade_perenni_descrizione["melee_colpiti"][2]="Melee attacks can hit 3 additional puppets at the same time";

        lista_upgrade_perenni_descrizione["melee_ignora_attacco"][1]="Melee puppets will ignore the first hit they receive";
        lista_upgrade_perenni_descrizione["melee_ignora_attacco"][2]="Melee puppets will ignore the first and second hits they receive";
        lista_upgrade_perenni_descrizione["melee_ignora_attacco"][3]="Melee puppets will ignore the first, second, and third hits they receive";

        lista_upgrade_perenni_descrizione["melee_dono_zanzare"][1]="Melee puppets gain 5% of their health as vitality.";
        lista_upgrade_perenni_descrizione["melee_dono_zanzare"][2]="Melee puppets gain 8% of their health as vitality.";
        lista_upgrade_perenni_descrizione["melee_dono_zanzare"][3]="Melee puppets gain 10% of their health as vitality.";

        lista_upgrade_perenni_descrizione["proiettili_ignora_armatura"][1]="The arrows ignore the target's armor 25%";
        lista_upgrade_perenni_descrizione["proiettili_ignora_armatura"][2]="The arrows ignore the target's armor 50%";
        lista_upgrade_perenni_descrizione["proiettili_ignora_armatura"][3]="The arrows ignore the target's armor 75%";
        lista_upgrade_perenni_descrizione["proiettili_ignora_armatura"][4]="The arrows ignore the target's armor 100%";

        lista_upgrade_perenni_descrizione["proiettili_head_shot"][1]="The arrows have a 1% chance of killing the enemy with one shot";
        lista_upgrade_perenni_descrizione["proiettili_head_shot"][2]="The arrows have a 2% chance of killing the enemy with one shot";
        lista_upgrade_perenni_descrizione["proiettili_head_shot"][3]="The arrows have a 3% chance of killing the enemy with one shot";
        lista_upgrade_perenni_descrizione["proiettili_head_shot"][4]="The arrows have a 4% chance of killing the enemy with one shot";
        lista_upgrade_perenni_descrizione["proiettili_head_shot"][5]="The arrows have a 5% chance of killing the enemy with one shot";

        lista_upgrade_perenni_descrizione["proiettili_distanza"][1]="Increases the maximum attack range by +1";
        lista_upgrade_perenni_descrizione["proiettili_distanza"][2]="Increases the maximum attack range by +2";
        lista_upgrade_perenni_descrizione["proiettili_distanza"][3]="Increases the maximum attack range by +3";

        lista_upgrade_perenni_descrizione["magia_veleno"][1]="The casted spells poison the target. The higher the level, the greater the poison";
        lista_upgrade_perenni_descrizione["magia_veleno"][2]="The casted spells poison the target. The higher the level, the greater the poison";
        lista_upgrade_perenni_descrizione["magia_veleno"][3]="The casted spells poison the target. The higher the level, the greater the poison";

        lista_upgrade_perenni_descrizione["magia_blocco"][1]="The casted spells slow down the target. The higher the level, the longer the slowing time";
        lista_upgrade_perenni_descrizione["magia_blocco"][2]="The casted spells slow down the target. The higher the level, the longer the slowing time";
        lista_upgrade_perenni_descrizione["magia_blocco"][3]="The casted spells slow down the target. The higher the level, the longer the slowing time";

        lista_upgrade_perenni_costi["costi_pupi"][1]=30;
        lista_upgrade_perenni_costi["costi_pupi"][2]=70;
        lista_upgrade_perenni_costi["costi_pupi"][3]=150;

        lista_upgrade_perenni_costi["costi_guadagno"][1]=50;
        lista_upgrade_perenni_costi["costi_guadagno"][2]=120;
        lista_upgrade_perenni_costi["costi_guadagno"][3]=200;
        lista_upgrade_perenni_costi["costi_guadagno"][4]=300;
        lista_upgrade_perenni_costi["costi_guadagno"][5]=450;

        lista_upgrade_perenni_costi["costi_abilita"][1]=30;
        lista_upgrade_perenni_costi["costi_abilita"][2]=60;
        lista_upgrade_perenni_costi["costi_abilita"][3]=100;
        lista_upgrade_perenni_costi["costi_abilita"][4]=150;

        lista_upgrade_perenni_costi["melee_velocita_attacco"][1]=25;
        lista_upgrade_perenni_costi["melee_velocita_attacco"][2]=50;
        lista_upgrade_perenni_costi["melee_velocita_attacco"][3]=80;
        lista_upgrade_perenni_costi["melee_velocita_attacco"][4]=120;

        lista_upgrade_perenni_costi["melee_colpiti"][1]=50;
        lista_upgrade_perenni_costi["melee_colpiti"][2]=150;

        lista_upgrade_perenni_costi["melee_ignora_attacco"][1]=30;
        lista_upgrade_perenni_costi["melee_ignora_attacco"][2]=60;
        lista_upgrade_perenni_costi["melee_ignora_attacco"][3]=100;

        lista_upgrade_perenni_costi["melee_dono_zanzare"][1]=30;
        lista_upgrade_perenni_costi["melee_dono_zanzare"][2]=80;
        lista_upgrade_perenni_costi["melee_dono_zanzare"][3]=150;

        lista_upgrade_perenni_costi["proiettili_ignora_armatura"][1]=15;
        lista_upgrade_perenni_costi["proiettili_ignora_armatura"][2]=35;
        lista_upgrade_perenni_costi["proiettili_ignora_armatura"][3]=65;
        lista_upgrade_perenni_costi["proiettili_ignora_armatura"][4]=100;

        lista_upgrade_perenni_costi["proiettili_distanza"][1]=20;
        lista_upgrade_perenni_costi["proiettili_distanza"][2]=50;
        lista_upgrade_perenni_costi["proiettili_distanza"][3]=100;

        lista_upgrade_perenni_costi["magia_veleno"][1]=20;
        lista_upgrade_perenni_costi["magia_veleno"][2]=50;
        lista_upgrade_perenni_costi["magia_veleno"][3]=100;

        lista_classi_nome.Add("warrior","Warrior");
        lista_classi_nome.Add("arcer","Arcer");
        lista_classi_nome.Add("wizard","Wizard");
        
        //praticamente essendo di tre livelli diversi, tanto vale fare direttamente così; Come fossero tre razze diverse a seconda del livello.
        //Domani faremo la stessa cosa per le cavalcature
        for (int i=1;i<=3;i++){
            //NB: Il singolare a destra è importante per definire la tipologia del pupo negli upgrade
            lista_razze_totale.Add("formiche_"+i,"formica");
            lista_razze_totale.Add("mosche_"+i,"mosca");
            lista_razze_totale.Add("api_"+i,"ape");
            lista_razze_totale.Add("ragnetti_"+i,"ragnetto");
            lista_razze_totale.Add("cavallette_"+i,"cavalletta");
            lista_razze_totale.Add("scarabei_"+i,"scarabeo");

            //tradurre i seguenti blocchi (a destra)
            //Le descrizione
            lista_pupi_descrizione.Add("formiche_"+i,"Most commons soldiers");
            lista_pupi_descrizione.Add("mosche_"+i,"Cheap cost but frails");
            lista_pupi_descrizione.Add("api_"+i,"When they die, they launch their stinger at some enemy");
            lista_pupi_descrizione.Add("ragnetti_"+i,"Every time they hit, they slow down the target. They are immune to this effect from other spiders");
            lista_pupi_descrizione.Add("cavallette_"+i,"Le cavallette sono particolarmente veloci. Inoltre hanno il 10% di possibilità di schivare un attacco corpo a corpo o a distanza");
            lista_pupi_descrizione.Add("scarabei_"+i,"Beetles are the most resilient race you can aspire to");

            //NB: I Nomi e le descrizioni, saranno sempre al plurale
            lista_razza_pupi_nome.Add("formiche_"+i,"Ants");     
            lista_razza_pupi_nome.Add("mosche_"+i,"Flies");      
            lista_razza_pupi_nome.Add("api_"+i,"Bees");          
            lista_razza_pupi_nome.Add("ragnetti_"+i,"Spiders");  
            lista_razza_pupi_nome.Add("cavallette_"+i,"Grasshopper");  
            lista_razza_pupi_nome.Add("scarabei_"+i,"Beetle");  

            //i costi dei vari pupetti
            lista_costo_unita_razza.Add("formiche_"+i,30*i);
            lista_costo_unita_razza.Add("mosche_"+i,30*i);
            lista_costo_unita_razza.Add("api_"+i,30*i);
            lista_costo_unita_razza.Add("ragnetti_"+i,30*i);
            lista_costo_unita_razza.Add("cavallette_"+i,30*i);
            lista_costo_unita_razza.Add("scarabei_"+i,30*i);
        }

        //traduci nei blocchi le frasi lunghe (sempre a destra)
        //La lista di tutte le abilita!
        lista_abilita_nome.Add("evoca_formiche","Summon Warrior Ants");
        lista_bool_abilita_classe.Add("evoca_formiche",true);
        lista_abilita_descrizione.Add("evoca_formiche","Summon 2 warrior ants for each level. Cooldown is really long and you can use only few times every match.");
        lista_abilita_cooldown.Add("evoca_formiche",new Dictionary<int, int>());
        lista_abilita_cooldown["evoca_formiche"].Add(1,15);
        lista_abilita_cooldown["evoca_formiche"].Add(2,18);
        lista_abilita_cooldown["evoca_formiche"].Add(3,20);

        lista_abilita_nome.Add("mosche_fastidiose","Swarm of flies");
        lista_bool_abilita_classe.Add("mosche_fastidiose",true);
        lista_abilita_descrizione.Add("mosche_fastidiose","Create a swarm that moves randomly and damages everything it encounters. Flies and mosquitoes are immune.");
        lista_abilita_cooldown.Add("mosche_fastidiose",new Dictionary<int, int>());
        lista_abilita_cooldown["mosche_fastidiose"].Add(1,15);
        lista_abilita_cooldown["mosche_fastidiose"].Add(2,18);
        lista_abilita_cooldown["mosche_fastidiose"].Add(3,20);

        lista_abilita_nome.Add("miele","Honey");
        lista_bool_abilita_classe.Add("miele",true);
        lista_abilita_descrizione.Add("miele","Regenerate all friendly unities hitten in the area.");
        lista_abilita_cooldown.Add("miele",new Dictionary<int, int>());
        lista_abilita_cooldown["miele"].Add(1,15);
        lista_abilita_cooldown["miele"].Add(2,18);
        lista_abilita_cooldown["miele"].Add(3,20);

        lista_abilita_nome.Add("ragnatele","Spiderweb");
        lista_bool_abilita_classe.Add("ragnatele",true);
        lista_abilita_descrizione.Add("ragnatele","Create a spiderweb in the area. All enemy hitten can't move or permorf attacks until spiderweb end. Spiders are immune.");
        lista_abilita_cooldown.Add("ragnatele",new Dictionary<int, int>());
        lista_abilita_cooldown["ragnatele"].Add(1,15);
        lista_abilita_cooldown["ragnatele"].Add(2,18);
        lista_abilita_cooldown["ragnatele"].Add(3,20);

        lista_abilita_nome.Add("velocita","Speed");
        lista_bool_abilita_classe.Add("velocita",true);
        lista_abilita_descrizione.Add("velocita","Increase the speed of all friendly hitten unities in the area for 3 seconds + 1 for each level. It doesn't work on webbed targets.");
        lista_abilita_cooldown.Add("velocita",new Dictionary<int, int>());
        lista_abilita_cooldown["velocita"].Add(1,20);
        lista_abilita_cooldown["velocita"].Add(2,20);
        lista_abilita_cooldown["velocita"].Add(3,20);

        lista_abilita_nome.Add("armatura","Armour");
        lista_bool_abilita_classe.Add("armatura",true);
        lista_abilita_descrizione.Add("armatura","Increase the armour of all friendly hitten unities in the area for 3 seconds + 1 for each level.");
        lista_abilita_cooldown.Add("armatura",new Dictionary<int, int>());
        lista_abilita_cooldown["armatura"].Add(1,20);
        lista_abilita_cooldown["armatura"].Add(2,20);
        lista_abilita_cooldown["armatura"].Add(3,20);

        lista_abilita_nome.Add("zombie","Zombie");
        lista_bool_abilita_classe.Add("zombie",false);
        lista_abilita_descrizione.Add("zombie","Summon random zombie insects. At the first level, it summons 5. At the second level, it summons 8. At the third level, it summons 10.");
        lista_abilita_cooldown.Add("zombie",new Dictionary<int, int>());
        lista_abilita_cooldown["zombie"].Add(1,30);
        lista_abilita_cooldown["zombie"].Add(2,30);
        lista_abilita_cooldown["zombie"].Add(3,30);

        lista_abilita_nome.Add("resurrezione","Resurrezione");
        lista_bool_abilita_classe.Add("resurrezione",false);
        lista_abilita_descrizione.Add("resurrezione","Revive a fallen ally puppet. The level of the summoned puppet is equal to the level of the ability. It has no effect if there are no fallen puppets.");
        lista_abilita_cooldown.Add("resurrezione",new Dictionary<int, int>());
        lista_abilita_cooldown["resurrezione"].Add(1,20);
        lista_abilita_cooldown["resurrezione"].Add(2,30);
        lista_abilita_cooldown["resurrezione"].Add(3,40);

        lista_abilita_nome.Add("insetto_esplosivo","Insetto Esplosivo");
        lista_bool_abilita_classe.Add("insetto_esplosivo",false);
        lista_abilita_descrizione.Add("insetto_esplosivo","Summon an explosive insect per level that will run randomly on the map and then explode, damaging all nearby insects.");
        lista_abilita_cooldown.Add("insetto_esplosivo",new Dictionary<int, int>());
        lista_abilita_cooldown["insetto_esplosivo"].Add(1,20);
        lista_abilita_cooldown["insetto_esplosivo"].Add(2,30);
        lista_abilita_cooldown["insetto_esplosivo"].Add(3,40);

        lista_abilita_nome.Add("insetto_esplosivo_velenoso","Insetto Esplosivo");
        lista_bool_abilita_classe.Add("insetto_esplosivo_velenoso",false);
        lista_abilita_descrizione.Add("insetto_esplosivo_velenoso","Summon an explosive poison insect per level that will run randomly on the map and then explode, damaging all nearby insects.");
        lista_abilita_cooldown.Add("insetto_esplosivo_velenoso",new Dictionary<int, int>());
        lista_abilita_cooldown["insetto_esplosivo_velenoso"].Add(1,20);
        lista_abilita_cooldown["insetto_esplosivo_velenoso"].Add(2,30);
        lista_abilita_cooldown["insetto_esplosivo_velenoso"].Add(3,40);

        lista_abilita_nome.Add("bombo","Catapulta di Bombo");
        lista_bool_abilita_classe.Add("bombo",false);
        lista_abilita_descrizione.Add("bombo","Cluster catapult that launches 5 bombs per level which explode on impact, damaging any insect in a large area hit");
        lista_abilita_cooldown.Add("bombo",new Dictionary<int, int>());
        lista_abilita_cooldown["bombo"].Add(1,20);
        lista_abilita_cooldown["bombo"].Add(2,30);
        lista_abilita_cooldown["bombo"].Add(3,40);

        lista_abilita_nome.Add("balestra","Balestre di aculei");
        lista_bool_abilita_classe.Add("balestra",false);
        lista_abilita_descrizione.Add("balestra","Launches 5 spikes per level one after the other that explode on impact damaging any insects in the wide area hit.");
        lista_abilita_cooldown.Add("balestra",new Dictionary<int, int>());
        lista_abilita_cooldown["balestra"].Add(1,20);
        lista_abilita_cooldown["balestra"].Add(2,30);
        lista_abilita_cooldown["balestra"].Add(3,40);


        //settiamo genericamente che ogni abilità ha un cooldown di partenza uguale alla metà di un normale cooldown
        foreach(KeyValuePair<string,Dictionary<int,int>> attachStat in lista_abilita_cooldown){
            lista_abilita_cooldown_partenza.Add(attachStat.Key,new Dictionary<int, int>());
            for (int i=1;i<=3;i++){
                lista_abilita_cooldown_partenza[attachStat.Key].Add(i,attachStat.Value[i]/2);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
