using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System;
using UnityEngine.SceneManagement;

public class info_comuni : MonoBehaviour
{
    //blocco GO traduzioni mainmenu, game, upgrade
    public TMPro.TextMeshProUGUI testo_mm_footer_diritti;
    public TMPro.TextMeshProUGUI bottone_mm_continue_game;
    public TMPro.TextMeshProUGUI bottone_mm_new_game;
    public TMPro.TextMeshProUGUI bottone_mm_quit;
    public TMPro.TextMeshProUGUI testo_mm_choose;
    public TMPro.TextMeshProUGUI titolo_upgrade_perenni;

    public TMPro.TextMeshProUGUI testo_stage;
    public TMPro.TextMeshProUGUI testo_game_pause;
    public TMPro.TextMeshProUGUI bottone_game_continue;
    public TMPro.TextMeshProUGUI bottone_game_mainmenu;
    public TMPro.TextMeshProUGUI bottone_game_opzioni;
    public TMPro.TextMeshProUGUI testo_game_opz_principale;
    public TMPro.TextMeshProUGUI bottone_trad_yes;
    public TMPro.TextMeshProUGUI bottone_trad_no;
    public TMPro.TextMeshProUGUI testo_game_youwin;
    public TMPro.TextMeshProUGUI bottone_game_nextstage;
    public TMPro.TextMeshProUGUI testo_game_youlose;
    public TMPro.TextMeshProUGUI bottone_game_restart;

    public TMPro.TextMeshProUGUI testo_upgrade_army;
    public TMPro.TextMeshProUGUI testo_upgrade_gold;
    public TMPro.TextMeshProUGUI testo_upgrade_improveyourarmy;
    public TMPro.TextMeshProUGUI testo_upgrade_lev1;
    public TMPro.TextMeshProUGUI testo_upgrade_lev2;
    public TMPro.TextMeshProUGUI testo_upgrade_lev3;
    public TMPro.TextMeshProUGUI testo_upgrade_upgrades;
    public TMPro.TextMeshProUGUI testo_upgrade_reward;
    public TMPro.TextMeshProUGUI testo_upgrade_choose1;
    public TMPro.TextMeshProUGUI testo_upgrade_choose2;
    public TMPro.TextMeshProUGUI testo_upgrade_choose3;
    public TMPro.TextMeshProUGUI testo_upgrade_puppetsupgrades;
    public TMPro.TextMeshProUGUI testo_upgrade_armyupgrades;
    public TMPro.TextMeshProUGUI testo_upgrade_tyrantupgrades;

    public TMPro.TextMeshProUGUI testo_upgrade_d_nome_abilita_distancedamage;
    public TMPro.TextMeshProUGUI testo_upgrade_d_nome_abilita_food;
    public TMPro.TextMeshProUGUI testo_upgrade_d_nome_abilita_health;
    public TMPro.TextMeshProUGUI testo_upgrade_d_nome_abilita_herocooldown;
    public TMPro.TextMeshProUGUI testo_upgrade_d_nome_abilita_herodamage;
    public TMPro.TextMeshProUGUI testo_upgrade_d_nome_abilita_herofury;
    public TMPro.TextMeshProUGUI testo_upgrade_d_nome_abilita_meleedamage;
    public TMPro.TextMeshProUGUI testo_upgrade_d_nome_abilita_randomrace;
    public TMPro.TextMeshProUGUI testo_upgrade_d_nome_abilita_rndheroability;
    public TMPro.TextMeshProUGUI testo_upgrade_d_nome_abilita_rndul1;
    public TMPro.TextMeshProUGUI testo_upgrade_d_nome_abilita_rndul2;
    public TMPro.TextMeshProUGUI testo_upgrade_d_nome_abilita_rndul3;
    public TMPro.TextMeshProUGUI testo_upgrade_d_nome_abilita_skillscooldown;
    public TMPro.TextMeshProUGUI testo_upgrade_d_nome_abilita_spelldamage;
    public TMPro.TextMeshProUGUI testo_upgrade_d_nome_abilita_upgnut;

    public upgrade upgrade;
    //fine blocco

    public Dictionary<string, Dictionary<string, string>> lista_descrizione_eroe_mainmenu = new Dictionary<string, Dictionary<string, string>>();

    public Dictionary<string, int> lista_costi_sblocco_eroe = new Dictionary<string, int>();

    public Dictionary<string, Dictionary<string, string>> lista_powerhero_descrizione = new Dictionary<string, Dictionary<string, string>>();

    public Dictionary<string, string> lista_premio_nome = new Dictionary<string, string>();
    public Dictionary<string, string> lista_premio_descrizione = new Dictionary<string, string>();

    public Dictionary<string, string> lista_abilita_id = new Dictionary<string, string>();
    public Dictionary<string, Dictionary<string, string>> lista_abilita_nome = new Dictionary<string, Dictionary<string, string>>();
    public Dictionary<string, Dictionary<string, string>> lista_abilita_descrizione = new Dictionary<string, Dictionary<string, string>>();
    public Dictionary<string, Dictionary<int, int>> lista_abilita_cooldown = new Dictionary<string, Dictionary<int, int>>();
    public Dictionary<string, Dictionary<int, int>> lista_abilita_cooldown_partenza = new Dictionary<string, Dictionary<int, int>>();
    public Dictionary<string, bool> lista_bool_abilita_classe = new Dictionary<string, bool>();

    public Dictionary<string, string> lista_razze_totale = new Dictionary<string, string>();
    public Dictionary<string, Dictionary<string, string>> lista_razza_pupi_nome = new Dictionary<string, Dictionary<string, string>>();
    public Dictionary<string, Dictionary<string, string>> lista_razza_pupi_nome_singolare = new Dictionary<string, Dictionary<string, string>>();
    public Dictionary<string, Dictionary<string, string>> lista_pupi_descrizione = new Dictionary<string, Dictionary<string, string>>();
    public Dictionary<string, Dictionary<string, string>> lista_classi_nome = new Dictionary<string, Dictionary<string, string>>();
    public Dictionary<string, int> lista_costo_unita_razza = new Dictionary<string, int>();
    // Start is called before the first frame update

    public Dictionary<string, string> lista_upgrade_perenni_id = new Dictionary<string, string>();
    public Dictionary<string, Dictionary<string, string>> lista_upgrade_perenni_nome = new Dictionary<string, Dictionary<string, string>>();
    public Dictionary<string, int> lista_upgrade_perenni_max_level = new Dictionary<string, int>();
    public Dictionary<string, Dictionary<int, int>> lista_upgrade_perenni_costi = new Dictionary<string, Dictionary<int, int>>();
    public Dictionary<string, Dictionary<string,Dictionary<int, string>>> lista_upgrade_perenni_descrizione = new Dictionary<string, Dictionary<string,Dictionary<int, string>>>();

    public Dictionary<string, float> lista_incremento_potere_eroe = new Dictionary<string, float>();
    public Dictionary<string, float> lista_decremento_potere_eroe = new Dictionary<string, float>();

    const int Iterations = 1000;    //encryption...non toccare

    void Awake(){
        lista_descrizione_eroe_mainmenu.Add("inglese",new Dictionary<string, string>());
        lista_descrizione_eroe_mainmenu["inglese"].Add("regina_formica_nera","Incursion! They always seem small and innocent, but in reality...");
        lista_descrizione_eroe_mainmenu["inglese"].Add("re_mosca","A dozen are troublesome. A dozen dozens become uncontrollable.");
        lista_descrizione_eroe_mainmenu["inglese"].Add("regina_ape","To sacrifice is to save little. To sacrifice all is to save the queen.");
        lista_descrizione_eroe_mainmenu["inglese"].Add("regina_ragno","Don't you want to relax? Let their spider webs do the job.");
        lista_descrizione_eroe_mainmenu["inglese"].Add("re_cavalletta","They hop quickly and sometimes uncontrollably.");
        lista_descrizione_eroe_mainmenu["inglese"].Add("re_scarabeo","They are not just simple and noble insects. Try putting a finger between their pincers.");

        lista_descrizione_eroe_mainmenu.Add("italiano",new Dictionary<string, string>());
        lista_descrizione_eroe_mainmenu["italiano"].Add("regina_formica_nera","Sembrano piccole ed innocenti, ma in realta...");
        lista_descrizione_eroe_mainmenu["italiano"].Add("re_mosca","Una dozzina sono un problema. Una dozzina di dozzine, diventano incontrollabili.");
        lista_descrizione_eroe_mainmenu["italiano"].Add("regina_ape","Sacrificare poco per salvare poco. Sacrificare tutto per salvare la regina.");
        lista_descrizione_eroe_mainmenu["italiano"].Add("regina_ragno","Non vuoi calmarti? Lascia fare ai ragni il lavoro.");
        lista_descrizione_eroe_mainmenu["italiano"].Add("re_cavalletta","Saltellano e si muovono in maniera rapido, veloce e confusionaria.");
        lista_descrizione_eroe_mainmenu["italiano"].Add("re_scarabeo","Pensi siano solo nobili insetti? Prova a mettere qualcosa tra le loro chele.");

        lista_descrizione_eroe_mainmenu.Add("spagnolo",new Dictionary<string, string>());
        lista_descrizione_eroe_mainmenu["spagnolo"].Add("regina_formica_nera","Siempre parecen pequeñas e inocentes, pero en realidad...");
        lista_descrizione_eroe_mainmenu["spagnolo"].Add("re_mosca","Una docena son un problema. Una docena de docenas se vuelve incontrolable.");
        lista_descrizione_eroe_mainmenu["spagnolo"].Add("regina_ape","Sacrificar poco es salvar poco. Sacrificarlo todo es salvar a la reina.");
        lista_descrizione_eroe_mainmenu["spagnolo"].Add("regina_ragno","¿No quieres relajarte? Deja que sus telarañas hagan el trabajo.");
        lista_descrizione_eroe_mainmenu["spagnolo"].Add("re_cavalletta","Saltan rápidamente y a veces de manera incontrolable");
        lista_descrizione_eroe_mainmenu["spagnolo"].Add("re_scarabeo","No son simplemente simples e nobles insectos. Intenta poner un dedo entre sus pinzas.");


        lista_powerhero_descrizione.Add("inglese",new Dictionary<string, string>());
        lista_powerhero_descrizione["inglese"].Add("regina_formica_nera","Invoke the Queen Ant to destroy the enemy puppets");
        lista_powerhero_descrizione["inglese"].Add("re_mosca","Invoke the King Mos, he going around and damage all enemies puppets in the arena");
        lista_powerhero_descrizione["inglese"].Add("regina_ape","Invoke the Queen Abe, she going around and heal all your puppets in the arena");
        lista_powerhero_descrizione["inglese"].Add("regina_ragno","Invoke the Spider Queen Weeb, she throws webs balls that reduce the speed of enemy puppets");
        lista_powerhero_descrizione["inglese"].Add("re_cavalletta","Invoke the King Grass, he throws balls of earth to damage enemy puppets");
        lista_powerhero_descrizione["inglese"].Add("re_scarabeo","Invoke the King Big to destroy the enemy puppets");

        lista_powerhero_descrizione.Add("italiano",new Dictionary<string, string>());
        lista_powerhero_descrizione["italiano"].Add("regina_formica_nera","Invoca la Regina formica per distruggere i pupetti nemici");
        lista_powerhero_descrizione["italiano"].Add("re_mosca","Invoca il Re Mosca che, volando, infliggerà danni a tutti i pupetti nemici");
        lista_powerhero_descrizione["italiano"].Add("regina_ape","Invoca l'Ape Regina che, volando, curera tutti i tuoi pupetti");
        lista_powerhero_descrizione["italiano"].Add("regina_ragno","Invoca la Regina Ragno che lancerà palle di ragnatele che bloccheranno tutti i pupetti colpiti");
        lista_powerhero_descrizione["italiano"].Add("re_cavalletta","Invoca il Re Cavalletta che lancerà palle esplosive che danneggeranno i pupetti nemici colpiti");
        lista_powerhero_descrizione["italiano"].Add("re_scarabeo","Invoca il Re scarabeo per distruggere i pupetti nemici");

        lista_powerhero_descrizione.Add("spagnolo",new Dictionary<string, string>());
        lista_powerhero_descrizione["spagnolo"].Add("regina_formica_nera","Invoca a la Reina Hormiga para destruir las unidades enemigas");
        lista_powerhero_descrizione["spagnolo"].Add("re_mosca","Invoca al Rey Mos que él se mueve alrededor y daña a todas las unidades enemigas en la arena");
        lista_powerhero_descrizione["spagnolo"].Add("regina_ape","Invoca a la Reina Abeja que se mueve alrededor y cura a todas tus unidades en la arena");
        lista_powerhero_descrizione["spagnolo"].Add("regina_ragno","Invoca a la Reina Araña Weeb que lanza bolas de telaraña que reducen la velocidad de las unidades enemigas");
        lista_powerhero_descrizione["spagnolo"].Add("re_cavalletta","Invoca al Rey Hierba que lanza bolas de tierra para dañar las unidades enemigas");
        lista_powerhero_descrizione["spagnolo"].Add("re_scarabeo","Invoca al Rey Big para destruir las unidades enemigas");

        lista_upgrade_perenni_nome.Add("inglese",new Dictionary<string, string>());
        lista_upgrade_perenni_nome["inglese"]["costi_pupi"]="Cost Army";
        lista_upgrade_perenni_nome["inglese"]["costi_guadagno"]="Improve earn";
        lista_upgrade_perenni_nome["inglese"]["costi_abilita"]="Cost skills";
        lista_upgrade_perenni_nome["inglese"]["melee_velocita_attacco"]="Melee attack speed";
        lista_upgrade_perenni_nome["inglese"]["melee_colpiti"]="Melee More hits";
        lista_upgrade_perenni_nome["inglese"]["melee_ignora_attacco"]="Ignore attacks";
        lista_upgrade_perenni_nome["inglese"]["melee_dono_zanzare"]="Lifesteal";
        lista_upgrade_perenni_nome["inglese"]["proiettili_ignora_armatura"]="Ignore Armor";
        lista_upgrade_perenni_nome["inglese"]["proiettili_head_shot"]="Head Shot";
        lista_upgrade_perenni_nome["inglese"]["proiettili_distanza"]="Max Distance";
        lista_upgrade_perenni_nome["inglese"]["magia_veleno"]="Poison Spells";
        lista_upgrade_perenni_nome["inglese"]["magia_blocco"]="Blocking Spells";

        lista_upgrade_perenni_nome.Add("italiano",new Dictionary<string, string>());
        lista_upgrade_perenni_nome["italiano"]["costi_pupi"]="Costo pupetti";
        lista_upgrade_perenni_nome["italiano"]["costi_guadagno"]="Guadagno";
        lista_upgrade_perenni_nome["italiano"]["costi_abilita"]="Costo abilità";
        lista_upgrade_perenni_nome["italiano"]["melee_velocita_attacco"]="Melee: Velocità attacco";
        lista_upgrade_perenni_nome["italiano"]["melee_colpiti"]="Melee: Numero colpi";
        lista_upgrade_perenni_nome["italiano"]["melee_ignora_attacco"]="Melee: Ignora attacco";
        lista_upgrade_perenni_nome["italiano"]["melee_dono_zanzare"]="Melee: Dono zanzare";
        lista_upgrade_perenni_nome["italiano"]["proiettili_ignora_armatura"]="Frecce: Ignora armatura";
        lista_upgrade_perenni_nome["italiano"]["proiettili_head_shot"]="Frecce: Head Shot";
        lista_upgrade_perenni_nome["italiano"]["proiettili_distanza"]="Frecce: Massima distanza";
        lista_upgrade_perenni_nome["italiano"]["magia_veleno"]="Magie velenose";
        lista_upgrade_perenni_nome["italiano"]["magia_blocco"]="Magie bloccanti";

        lista_upgrade_perenni_nome.Add("spagnolo",new Dictionary<string, string>());
        lista_upgrade_perenni_nome["spagnolo"]["costi_pupi"]="Costo del ejército";
        lista_upgrade_perenni_nome["spagnolo"]["costi_guadagno"]="Mejorar las ganancias";
        lista_upgrade_perenni_nome["spagnolo"]["costi_abilita"]="Costo de habilidades";
        lista_upgrade_perenni_nome["spagnolo"]["melee_velocita_attacco"]="Velocidad cuerpo a cuerpo";
        lista_upgrade_perenni_nome["spagnolo"]["melee_colpiti"]="Más golpes cuerpo a cuerpo";
        lista_upgrade_perenni_nome["spagnolo"]["melee_ignora_attacco"]="Ignorar ataques cuerpo a cuerpo";
        lista_upgrade_perenni_nome["spagnolo"]["melee_dono_zanzare"]="Regalo de los mosquitos";
        lista_upgrade_perenni_nome["spagnolo"]["proiettili_ignora_armatura"]="Ignorar armadura";
        lista_upgrade_perenni_nome["spagnolo"]["proiettili_head_shot"]="Head Shot";
        lista_upgrade_perenni_nome["spagnolo"]["proiettili_distanza"]="Distancia máxima";
        lista_upgrade_perenni_nome["spagnolo"]["magia_veleno"]="Hechizos venenosos";
        lista_upgrade_perenni_nome["spagnolo"]["magia_blocco"]="Hechizos de bloqueo";

        lista_abilita_id.Add("evoca_formiche","");
        lista_abilita_id.Add("mosche_fastidiose","");
        lista_abilita_id.Add("miele","");
        lista_abilita_id.Add("ragnatele","");
        lista_abilita_id.Add("velocita","");
        lista_abilita_id.Add("armatura","");
        lista_abilita_id.Add("zombie","");
        lista_abilita_id.Add("resurrezione","");
        lista_abilita_id.Add("insetto_esplosivo","");
        lista_abilita_id.Add("insetto_esplosivo_velenoso","");
        lista_abilita_id.Add("bombo","");
        lista_abilita_id.Add("balestra","");

        lista_abilita_nome.Add("inglese",new Dictionary<string, string>());
        lista_abilita_nome["inglese"].Add("evoca_formiche","Summon Warrior Ants");
        lista_abilita_nome["inglese"].Add("mosche_fastidiose","Swarm of Flies");
        lista_abilita_nome["inglese"].Add("miele","Honey");
        lista_abilita_nome["inglese"].Add("ragnatele","Spiderweb");
        lista_abilita_nome["inglese"].Add("velocita","Speed");
        lista_abilita_nome["inglese"].Add("armatura","Armour");
        lista_abilita_nome["inglese"].Add("zombie","Zombie");
        lista_abilita_nome["inglese"].Add("resurrezione","Resurrection");
        lista_abilita_nome["inglese"].Add("insetto_esplosivo","Explosive Bug");
        lista_abilita_nome["inglese"].Add("insetto_esplosivo_velenoso","Explosive Poison Bug");
        lista_abilita_nome["inglese"].Add("bombo","Bombo catapult");
        lista_abilita_nome["inglese"].Add("balestra","Balistic spikes");

        lista_abilita_nome.Add("italiano",new Dictionary<string, string>());
        lista_abilita_nome["italiano"].Add("evoca_formiche","Evoca Formiche Guerriere");
        lista_abilita_nome["italiano"].Add("mosche_fastidiose","Mosche fastidiose");
        lista_abilita_nome["italiano"].Add("miele","Miele");
        lista_abilita_nome["italiano"].Add("ragnatele","Ragnatele");
        lista_abilita_nome["italiano"].Add("velocita","Velocità");
        lista_abilita_nome["italiano"].Add("armatura","Armatura");
        lista_abilita_nome["italiano"].Add("zombie","Zombie");
        lista_abilita_nome["italiano"].Add("resurrezione","Resurrezione");
        lista_abilita_nome["italiano"].Add("insetto_esplosivo","Insetto Esplosivo");
        lista_abilita_nome["italiano"].Add("insetto_esplosivo_velenoso","Insetto tossico");
        lista_abilita_nome["italiano"].Add("bombo","Catapulta di Bombo");
        lista_abilita_nome["italiano"].Add("balestra","Balestra di Aculei");

        lista_abilita_nome.Add("spagnolo",new Dictionary<string, string>());
        lista_abilita_nome["spagnolo"].Add("evoca_formiche","Invocar Hormigas Guerreras");
        lista_abilita_nome["spagnolo"].Add("mosche_fastidiose","Enjambre de Moscas");
        lista_abilita_nome["spagnolo"].Add("miele","Miel");
        lista_abilita_nome["spagnolo"].Add("ragnatele","Telaraña");
        lista_abilita_nome["spagnolo"].Add("velocita","Velocidad");
        lista_abilita_nome["spagnolo"].Add("armatura","Armadura");
        lista_abilita_nome["spagnolo"].Add("zombie","Zombi");
        lista_abilita_nome["spagnolo"].Add("resurrezione","Resurrección");
        lista_abilita_nome["spagnolo"].Add("insetto_esplosivo","Bicho Explosivo");
        lista_abilita_nome["spagnolo"].Add("insetto_esplosivo_velenoso","Bicho Venenoso Explosivo");
        lista_abilita_nome["spagnolo"].Add("bombo","Catapulta de Bombos");
        lista_abilita_nome["spagnolo"].Add("balestra","Púas balísticas");

        lista_abilita_descrizione.Add("inglese",new Dictionary<string, string>());
        lista_abilita_descrizione["inglese"].Add("evoca_formiche","Summon 2 warrior ants for each level. Cooldown is really long and you can use only few times every match.");
        lista_abilita_descrizione["inglese"].Add("mosche_fastidiose","Create a swarm that moves randomly and damages everything it encounters. Flies and mosquitoes are immune.");
        lista_abilita_descrizione["inglese"].Add("ragnatele","Create a spiderweb in the area. All enemy hitten can't move or permorf attacks until spiderweb end. Spiders are immune.");
        lista_abilita_descrizione["inglese"].Add("velocita","Increase the speed of all friendly hitten unities in the area for 3 seconds + 1 for each level. It doesn't work on webbed targets.");
        lista_abilita_descrizione["inglese"].Add("armatura","Increase the armour of all friendly hitten unities in the area for 3 seconds + 1 for each level.");
        lista_abilita_descrizione["inglese"].Add("zombie","Summon random zombie insects. At the first level, it summons 5. At the second level, it summons 8. At the third level, it summons 10.");
        lista_abilita_descrizione["inglese"].Add("resurrezione","Revive a fallen ally puppet. The level of the summoned puppet is equal to the level of the ability. It has no effect if there are no fallen puppets.");
        lista_abilita_descrizione["inglese"].Add("insetto_esplosivo","Summon an explosive insect per level that will run randomly on the map and then explode, damaging all nearby insects.");
        lista_abilita_descrizione["inglese"].Add("insetto_esplosivo_velenoso","Summon an explosive poison insect per level that will run randomly on the map and then explode, damaging all nearby insects.");
        lista_abilita_descrizione["inglese"].Add("bombo","Cluster catapult that launches 5 bombs per level which explode on impact, damaging any insect in a large area hit");
        lista_abilita_descrizione["inglese"].Add("balestra","Launches 5 spikes per level one after the other that explode on impact damaging any insects in the wide area hit.");
        lista_abilita_descrizione["inglese"].Add("miele","Regenerate all friendly unities hitten in the area.");

        lista_abilita_descrizione.Add("italiano",new Dictionary<string, string>());
        lista_abilita_descrizione["italiano"].Add("evoca_formiche","Evoca 2 formiche guerriere per livello.");
        lista_abilita_descrizione["italiano"].Add("mosche_fastidiose","Crea uno nugolo di moscerini che si muove in maniera random danneggiando tutto ciò che tocca. Mosche e Zanzare ne sono immuni.");
        lista_abilita_descrizione["italiano"].Add("ragnatele","Crea una ragnatela nell'arena. Tutti i pupetti nemici colpiti, non si potranno muovere nè attaccare. I ragni ne sono immuni.");
        lista_abilita_descrizione["italiano"].Add("velocita","Aumenta la velocità dei tuoi pupetti colpiti per 3 secondi + 1 per ogni livello. Non funziona su bersaglio bloccati da ragnatele.");
        lista_abilita_descrizione["italiano"].Add("armatura","Aumenta l'armatura di tutti i tuoi pupetti colpiti nell'area per 3 secondi + 1 per livello.");
        lista_abilita_descrizione["italiano"].Add("zombie","Evoca degli insetti zombie casuali alleati. Al primo livello ne evoca 5, al secondo 8 ed al terzo 10.");
        lista_abilita_descrizione["italiano"].Add("resurrezione","Rievoca un pupetto alleato morto. Il livello del pupetto deve essere uguale al livello dell'abilitò. Se non sono morti pupetti, l'abilità non avrà effetto.");
        lista_abilita_descrizione["italiano"].Add("insetto_esplosivo","Evoca un insetto esplosivo per livello che correrà randomicamente sulla mappa per poi esplodere, danneggiando tutti i pupetti colpiti vicino.");
        lista_abilita_descrizione["italiano"].Add("insetto_esplosivo_velenoso","Evoca un insetto esplosivo per livello che correrà randomicamente sulla mappa per poi esplodere, avvelenando tutti i pupetti colpiti vicino.");
        lista_abilita_descrizione["italiano"].Add("bombo","Attiva una catapulta che lancia 5 bombe per livello che esploderanno all'impatto, danneggiando tutti i pupetti nell'area colpita.");
        lista_abilita_descrizione["italiano"].Add("balestra","Lancia 5 aculei per livello uno di seguito all'altro che esploderanno all'impatto, danneggiando tutti i pupetti nell'area colpita.");
        lista_abilita_descrizione["italiano"].Add("miele","Rigenera tutti i pupetti alleati colpiti nell'area.");

        lista_abilita_descrizione.Add("spagnolo",new Dictionary<string, string>());
        lista_abilita_descrizione["spagnolo"].Add("evoca_formiche","Invoca 2 hormigas guerreras por cada nivel. El tiempo de reutilización es muy largo y solo puedes usarlas algunas veces en cada partida.");
        lista_abilita_descrizione["spagnolo"].Add("mosche_fastidiose","Crea un enjambre que se mueve al azar y daña todo lo que encuentra. Las moscas y los mosquitos son inmunes.");
        lista_abilita_descrizione["spagnolo"].Add("ragnatele","Crea una telaraña en el área. Todos los enemigos golpeados no pueden moverse o realizar ataques hasta que la telaraña termine. Las arañas son inmunes.");
        lista_abilita_descrizione["spagnolo"].Add("velocita","Aumenta la velocidad de todas las unidades aliadas golpeadas en el área durante 3 segundos + 1 por cada nivel. No funciona en objetivos enredados en telarañas.");
        lista_abilita_descrizione["spagnolo"].Add("armatura","Aumenta la armadura de todas las unidades aliadas golpeadas en el área durante 3 segundos + 1 por cada nivel.");
        lista_abilita_descrizione["spagnolo"].Add("zombie","Invoca insectos zombis al azar. En el primer nivel, invoca 5. En el segundo nivel, invoca 8. En el tercer nivel, invoca 10.");
        lista_abilita_descrizione["spagnolo"].Add("resurrezione","Revive una unidad aliada caída. El nivel de la unidad invocada es igual al nivel de la habilidad. No tiene efecto si no hay unidades caídas.");
        lista_abilita_descrizione["spagnolo"].Add("insetto_esplosivo","Invoca un insecto explosivo por nivel que correrá al azar por el mapa y luego explotará, dañando a todos los insectos cercanos.");
        lista_abilita_descrizione["spagnolo"].Add("insetto_esplosivo_velenoso","Invoca un insecto explosivo venenoso por nivel que correrá al azar por el mapa y luego explotará, dañando a todos los insectos cercanos.");
        lista_abilita_descrizione["spagnolo"].Add("bombo","Catapulta de racimo que lanza 5 bombas por nivel las cuales explotan al impactar, dañando a cualquier insecto en un área amplia alcanzada.");
        lista_abilita_descrizione["spagnolo"].Add("balestra","Lanza 5 púas por nivel una tras otra que explotan al impactar, dañando a cualquier insecto en el área amplia alcanzada.");
        lista_abilita_descrizione["spagnolo"].Add("miele","Regenera todas las unidades aliadas golpeadas en el área.");

        lista_premio_nome.Add("end_hero_regina_formica_nera","Black Ant");
        lista_premio_nome.Add("end_hero_re_mosca","King Moss");
        lista_premio_nome.Add("end_hero_regina_ape","Queen Abe");
        lista_premio_nome.Add("end_hero_regina_ragno","Queen Web");
        lista_premio_nome.Add("end_hero_re_cavalletta","King Gross");
        lista_premio_nome.Add("end_hero_re_scarabeo","King Big");
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
        lista_premio_descrizione.Add("end_hero_re_mosca","Reach stage 40 with King Moss");
        lista_premio_descrizione.Add("end_hero_regina_ape","Reach stage 40 with Queen Abe");
        lista_premio_descrizione.Add("end_hero_regina_ragno","Reach stage 40 with Queen Web");
        lista_premio_descrizione.Add("end_hero_re_cavalletta","Reach stage 40 with King Gross");
        lista_premio_descrizione.Add("end_hero_re_scarabeo","Reach stage 40 with King Big");
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
        lista_costi_sblocco_eroe.Add("regina_formica_nera",0);
        lista_costi_sblocco_eroe.Add("re_mosca",20);
        lista_costi_sblocco_eroe.Add("regina_ape",25);
        lista_costi_sblocco_eroe.Add("regina_ragno",30);
        lista_costi_sblocco_eroe.Add("re_cavalletta",35);
        lista_costi_sblocco_eroe.Add("re_scarabeo",40);

        //non tradurre nulla
        lista_incremento_potere_eroe.Add("regina_formica_nera",1f);
        lista_incremento_potere_eroe.Add("re_mosca",1f);
        lista_incremento_potere_eroe.Add("regina_ape",1f);
        lista_incremento_potere_eroe.Add("regina_ragno",1.5f);
        lista_incremento_potere_eroe.Add("re_cavalletta",2f);
        lista_incremento_potere_eroe.Add("re_scarabeo",1f);

        //10f corrisponde grossomodo a 17 secondi
        //15f corrisponde grossomodo a 9 secondi
        lista_decremento_potere_eroe.Add("regina_formica_nera",10f);
        lista_decremento_potere_eroe.Add("re_mosca",10f);
        lista_decremento_potere_eroe.Add("regina_ape",10f);
        lista_decremento_potere_eroe.Add("regina_ragno",10f);
        lista_decremento_potere_eroe.Add("re_cavalletta",10f);
        lista_decremento_potere_eroe.Add("re_scarabeo",10f);

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

        //bisogna lasciarli
        lista_upgrade_perenni_id.Add("costi_pupi","");
        lista_upgrade_perenni_id.Add("costi_guadagno","");
        lista_upgrade_perenni_id.Add("costi_abilita","");
        lista_upgrade_perenni_id.Add("melee_velocita_attacco","");
        lista_upgrade_perenni_id.Add("melee_colpiti","");
        lista_upgrade_perenni_id.Add("melee_ignora_attacco","");
        lista_upgrade_perenni_id.Add("melee_dono_zanzare","");
        lista_upgrade_perenni_id.Add("proiettili_ignora_armatura","");
        lista_upgrade_perenni_id.Add("proiettili_head_shot","");
        lista_upgrade_perenni_id.Add("proiettili_distanza","");
        lista_upgrade_perenni_id.Add("magia_veleno","Poison Spells");
        lista_upgrade_perenni_id.Add("magia_blocco","Blocking Spells");

        lista_upgrade_perenni_descrizione.Add("inglese",new Dictionary<string, Dictionary<int, string>>());
        lista_upgrade_perenni_descrizione.Add("italiano",new Dictionary<string, Dictionary<int, string>>());
        lista_upgrade_perenni_descrizione.Add("spagnolo",new Dictionary<string, Dictionary<int, string>>());

        //settiamo genericamente che ogni upgrade perenne ha un costo di 10 per livello che si vuole ottenere
        foreach(KeyValuePair<string,string> attachStat in lista_upgrade_perenni_id){
            lista_upgrade_perenni_costi.Add(attachStat.Key,new Dictionary<int, int>());
            lista_upgrade_perenni_descrizione["inglese"].Add(attachStat.Key,new Dictionary<int, string>());
            lista_upgrade_perenni_descrizione["italiano"].Add(attachStat.Key,new Dictionary<int, string>());
            lista_upgrade_perenni_descrizione["spagnolo"].Add(attachStat.Key,new Dictionary<int, string>());
            for (int i=1;i<=lista_upgrade_perenni_max_level[attachStat.Key];i++){
                lista_upgrade_perenni_costi[attachStat.Key].Add(i,(10*i));
                lista_upgrade_perenni_descrizione["inglese"][attachStat.Key].Add(i,"Descrizione "+i);
                lista_upgrade_perenni_descrizione["italiano"][attachStat.Key].Add(i,"Descrizione "+i);
                lista_upgrade_perenni_descrizione["spagnolo"][attachStat.Key].Add(i,"Descrizione "+i);
            }
        }
        
        //UPGRADE PERENNI - traduerree la parte a destra di questi blocchi
        lista_upgrade_perenni_descrizione["inglese"]["costi_pupi"][1]="The puppets cost 5 less";
        lista_upgrade_perenni_descrizione["inglese"]["costi_pupi"][2]="The puppets cost 10 less";
        lista_upgrade_perenni_descrizione["inglese"]["costi_pupi"][3]="The puppets cost 15 less";

        lista_upgrade_perenni_descrizione["inglese"]["costi_guadagno"][1]="You earn +10 at the end of the stage";
        lista_upgrade_perenni_descrizione["inglese"]["costi_guadagno"][2]="You earn +20 at the end of the stage";
        lista_upgrade_perenni_descrizione["inglese"]["costi_guadagno"][3]="You earn +30 at the end of the stage";
        lista_upgrade_perenni_descrizione["inglese"]["costi_guadagno"][4]="You earn +40 at the end of the stage";
        lista_upgrade_perenni_descrizione["inglese"]["costi_guadagno"][5]="You earn +50 at the end of the stage";

        lista_upgrade_perenni_descrizione["inglese"]["costi_abilita"][1]="Reduces the cost of abilities by 5";
        lista_upgrade_perenni_descrizione["inglese"]["costi_abilita"][2]="Reduces the cost of abilities by 10";
        lista_upgrade_perenni_descrizione["inglese"]["costi_abilita"][3]="Reduces the cost of abilities by 15";
        lista_upgrade_perenni_descrizione["inglese"]["costi_abilita"][4]="Reduces the cost of abilities by 20";

        lista_upgrade_perenni_descrizione["inglese"]["melee_velocita_attacco"][1]="Increases melee attack speed by 5%";
        lista_upgrade_perenni_descrizione["inglese"]["melee_velocita_attacco"][2]="Increases melee attack speed by 10%";
        lista_upgrade_perenni_descrizione["inglese"]["melee_velocita_attacco"][3]="Increases melee attack speed by 15%";
        lista_upgrade_perenni_descrizione["inglese"]["melee_velocita_attacco"][4]="Increases melee attack speed by 20%";

        lista_upgrade_perenni_descrizione["inglese"]["melee_colpiti"][1]="Melee attacks can hit 2 additional puppets at the same time";
        lista_upgrade_perenni_descrizione["inglese"]["melee_colpiti"][2]="Melee attacks can hit 3 additional puppets at the same time";

        lista_upgrade_perenni_descrizione["inglese"]["melee_ignora_attacco"][1]="Melee puppets will ignore the first hit they receive";
        lista_upgrade_perenni_descrizione["inglese"]["melee_ignora_attacco"][2]="Melee puppets will ignore the first and second hits they receive";
        lista_upgrade_perenni_descrizione["inglese"]["melee_ignora_attacco"][3]="Melee puppets will ignore the first, second, and third hits they receive";

        lista_upgrade_perenni_descrizione["inglese"]["melee_dono_zanzare"][1]="Melee puppets gain 5% of their health as vitality when hit enemies";
        lista_upgrade_perenni_descrizione["inglese"]["melee_dono_zanzare"][2]="Melee puppets gain 8% of their health as vitality when hit enemies";
        lista_upgrade_perenni_descrizione["inglese"]["melee_dono_zanzare"][3]="Melee puppets gain 10% of their health as vitality when hit enemies";

        lista_upgrade_perenni_descrizione["inglese"]["proiettili_ignora_armatura"][1]="The arrows ignore the target's armor by 25%";
        lista_upgrade_perenni_descrizione["inglese"]["proiettili_ignora_armatura"][2]="The arrows ignore the target's armor by 50%";
        lista_upgrade_perenni_descrizione["inglese"]["proiettili_ignora_armatura"][3]="The arrows ignore the target's armor by 75%";
        lista_upgrade_perenni_descrizione["inglese"]["proiettili_ignora_armatura"][4]="The arrows ignore the target's armor by 100%";

        lista_upgrade_perenni_descrizione["inglese"]["proiettili_head_shot"][1]="The arrows have a 1% chance of killing the enemy with one shot";
        lista_upgrade_perenni_descrizione["inglese"]["proiettili_head_shot"][2]="The arrows have a 2% chance of killing the enemy with one shot";
        lista_upgrade_perenni_descrizione["inglese"]["proiettili_head_shot"][3]="The arrows have a 3% chance of killing the enemy with one shot";
        lista_upgrade_perenni_descrizione["inglese"]["proiettili_head_shot"][4]="The arrows have a 4% chance of killing the enemy with one shot";
        lista_upgrade_perenni_descrizione["inglese"]["proiettili_head_shot"][5]="The arrows have a 5% chance of killing the enemy with one shot";

        lista_upgrade_perenni_descrizione["inglese"]["proiettili_distanza"][1]="Increases the maximum attack range by +1";
        lista_upgrade_perenni_descrizione["inglese"]["proiettili_distanza"][2]="Increases the maximum attack range by +2";
        lista_upgrade_perenni_descrizione["inglese"]["proiettili_distanza"][3]="Increases the maximum attack range by +3";

        lista_upgrade_perenni_descrizione["inglese"]["magia_veleno"][1]="The casted spells poison the target. The higher the level, the greater the poison";
        lista_upgrade_perenni_descrizione["inglese"]["magia_veleno"][2]="The casted spells poison the target. The higher the level, the greater the poison";
        lista_upgrade_perenni_descrizione["inglese"]["magia_veleno"][3]="The casted spells poison the target. The higher the level, the greater the poison";

        lista_upgrade_perenni_descrizione["inglese"]["magia_blocco"][1]="The casted spells slow down the target. The higher the level, the longer the slowing time";
        lista_upgrade_perenni_descrizione["inglese"]["magia_blocco"][2]="The casted spells slow down the target. The higher the level, the longer the slowing time";
        lista_upgrade_perenni_descrizione["inglese"]["magia_blocco"][3]="The casted spells slow down the target. The higher the level, the longer the slowing time";


        lista_upgrade_perenni_descrizione["italiano"]["costi_pupi"][1]="I pupetti costano 5 monete in meno";
        lista_upgrade_perenni_descrizione["italiano"]["costi_pupi"][2]="I pupetti costano 10 monete in meno";
        lista_upgrade_perenni_descrizione["italiano"]["costi_pupi"][3]="I pupetti costano 15 monete in meno";

        lista_upgrade_perenni_descrizione["italiano"]["costi_guadagno"][1]="Guadagno 10 monete in più alla fine di ogni stage";
        lista_upgrade_perenni_descrizione["italiano"]["costi_guadagno"][2]="Guadagno 20 monete in più alla fine di ogni stage";
        lista_upgrade_perenni_descrizione["italiano"]["costi_guadagno"][3]="Guadagno 30 monete in più alla fine di ogni stage";
        lista_upgrade_perenni_descrizione["italiano"]["costi_guadagno"][4]="Guadagno 40 monete in più alla fine di ogni stage";
        lista_upgrade_perenni_descrizione["italiano"]["costi_guadagno"][5]="Guadagno 50 monete in più alla fine di ogni stage";

        lista_upgrade_perenni_descrizione["italiano"]["costi_abilita"][1]="Riduci il costo delle abilità di 5";
        lista_upgrade_perenni_descrizione["italiano"]["costi_abilita"][2]="Riduci il costo delle abilità di 10";
        lista_upgrade_perenni_descrizione["italiano"]["costi_abilita"][3]="Riduci il costo delle abilità di 15";
        lista_upgrade_perenni_descrizione["italiano"]["costi_abilita"][4]="Riduci il costo delle abilità di 20";

        lista_upgrade_perenni_descrizione["italiano"]["melee_velocita_attacco"][1]="Aumenta la velocità d'attacco corpo a corpo del 5%";
        lista_upgrade_perenni_descrizione["italiano"]["melee_velocita_attacco"][2]="Aumenta la velocità d'attacco corpo a corpo del 10%";
        lista_upgrade_perenni_descrizione["italiano"]["melee_velocita_attacco"][3]="Aumenta la velocità d'attacco corpo a corpo del 15%";
        lista_upgrade_perenni_descrizione["italiano"]["melee_velocita_attacco"][4]="Aumenta la velocità d'attacco corpo a corpo del 20%";

        lista_upgrade_perenni_descrizione["italiano"]["melee_colpiti"][1]="Gli attacchi corpo a corpo colpiranno 2 pupetti nemici nello stesso momento.";
        lista_upgrade_perenni_descrizione["italiano"]["melee_colpiti"][2]="Gli attacchi corpo a corpo colpiranno 3 pupetti nemici nello stesso momento.";

        lista_upgrade_perenni_descrizione["italiano"]["melee_ignora_attacco"][1]="I pupetti guerrieri, ignoreranno il primo colpo ricevuto";
        lista_upgrade_perenni_descrizione["italiano"]["melee_ignora_attacco"][2]="I pupetti guerrieri, ignoreranno il primo ed il secondo colpo ricevuti";
        lista_upgrade_perenni_descrizione["italiano"]["melee_ignora_attacco"][3]="I pupetti guerrieri, ignoreranno il primo, il secondo ed il terzo colpo ricevuti";

        lista_upgrade_perenni_descrizione["italiano"]["melee_dono_zanzare"][1]="I pupetti guerrieri guadagno il 5% della loro vitalità quando colpiscono.";
        lista_upgrade_perenni_descrizione["italiano"]["melee_dono_zanzare"][2]="I pupetti guerrieri guadagno il 8% della loro vitalità quando colpiscono.";
        lista_upgrade_perenni_descrizione["italiano"]["melee_dono_zanzare"][3]="I pupetti guerrieri guadagno il 10% della loro vitalità quando colpiscono.";

        lista_upgrade_perenni_descrizione["italiano"]["proiettili_ignora_armatura"][1]="Le frecce ignorano l'armatura del bersaglio del 25%";
        lista_upgrade_perenni_descrizione["italiano"]["proiettili_ignora_armatura"][2]="Le frecce ignorano l'armatura del bersaglio del 50%";
        lista_upgrade_perenni_descrizione["italiano"]["proiettili_ignora_armatura"][3]="Le frecce ignorano l'armatura del bersaglio del 75%";
        lista_upgrade_perenni_descrizione["italiano"]["proiettili_ignora_armatura"][4]="Le frecce ignorano l'armatura del bersaglio del 100%";

        lista_upgrade_perenni_descrizione["italiano"]["proiettili_head_shot"][1]="Le frecce hanno l'1% di possibilità di uccidere il nemico";
        lista_upgrade_perenni_descrizione["italiano"]["proiettili_head_shot"][2]="Le frecce hanno il 2% di possibilità di uccidere il nemico";
        lista_upgrade_perenni_descrizione["italiano"]["proiettili_head_shot"][3]="Le frecce hanno il 3% di possibilità di uccidere il nemico";
        lista_upgrade_perenni_descrizione["italiano"]["proiettili_head_shot"][4]="Le frecce hanno il 4% di possibilità di uccidere il nemico";
        lista_upgrade_perenni_descrizione["italiano"]["proiettili_head_shot"][5]="Le frecce hanno il 5% di possibilità di uccidere il nemico";

        lista_upgrade_perenni_descrizione["italiano"]["proiettili_distanza"][1]="Aumenta la gittata massima di +1";
        lista_upgrade_perenni_descrizione["italiano"]["proiettili_distanza"][2]="Aumenta la gittata massima di +2";
        lista_upgrade_perenni_descrizione["italiano"]["proiettili_distanza"][3]="Aumenta la gittata massima di +3";

        lista_upgrade_perenni_descrizione["italiano"]["magia_veleno"][1]="Le magie lanciate, avvelenano il bersaglio. Più è alto il livello, più sarà alto il livello di avvelenamento";
        lista_upgrade_perenni_descrizione["italiano"]["magia_veleno"][2]="Le magie lanciate, avvelenano il bersaglio. Più è alto il livello, più sarà alto il livello di avvelenamento";
        lista_upgrade_perenni_descrizione["italiano"]["magia_veleno"][3]="Le magie lanciate, avvelenano il bersaglio. Più è alto il livello, più sarà alto il livello di avvelenamento";

        lista_upgrade_perenni_descrizione["italiano"]["magia_blocco"][1]="Le amgie lanciate, rallentano il bersaglio. Più è alto il livello, più tempo saranno rallentate.";
        lista_upgrade_perenni_descrizione["italiano"]["magia_blocco"][2]="Le amgie lanciate, rallentano il bersaglio. Più è alto il livello, più tempo saranno rallentate.";
        lista_upgrade_perenni_descrizione["italiano"]["magia_blocco"][3]="Le amgie lanciate, rallentano il bersaglio. Più è alto il livello, più tempo saranno rallentate.";




        lista_upgrade_perenni_descrizione["spagnolo"]["costi_pupi"][1]="Las unidades cuestan 5 menos";
        lista_upgrade_perenni_descrizione["spagnolo"]["costi_pupi"][2]="Las unidades cuestan 10 menos";
        lista_upgrade_perenni_descrizione["spagnolo"]["costi_pupi"][3]="Las unidades cuestan 15 menos";

        lista_upgrade_perenni_descrizione["spagnolo"]["costi_guadagno"][1]="Obtienes +10 monedas al finalizar el nivel";
        lista_upgrade_perenni_descrizione["spagnolo"]["costi_guadagno"][2]="Obtienes +20 monedas al finalizar el nivel";
        lista_upgrade_perenni_descrizione["spagnolo"]["costi_guadagno"][3]="Obtienes +30 monedas al finalizar el nivel";
        lista_upgrade_perenni_descrizione["spagnolo"]["costi_guadagno"][4]="Obtienes +40 monedas al finalizar el nivel";
        lista_upgrade_perenni_descrizione["spagnolo"]["costi_guadagno"][5]="Obtienes +50 monedas al finalizar el nivel";

        lista_upgrade_perenni_descrizione["spagnolo"]["costi_abilita"][1]="Reduce el costo de las habilidades en 5";
        lista_upgrade_perenni_descrizione["spagnolo"]["costi_abilita"][2]="Reduce el costo de las habilidades en 10";
        lista_upgrade_perenni_descrizione["spagnolo"]["costi_abilita"][3]="Reduce el costo de las habilidades en 15";
        lista_upgrade_perenni_descrizione["spagnolo"]["costi_abilita"][4]="Reduce el costo de las habilidades en 20";

        lista_upgrade_perenni_descrizione["spagnolo"]["melee_velocita_attacco"][1]="Aumenta la velocidad de ataque cuerpo a cuerpo en un 5%";
        lista_upgrade_perenni_descrizione["spagnolo"]["melee_velocita_attacco"][2]="Aumenta la velocidad de ataque cuerpo a cuerpo en un 10%";
        lista_upgrade_perenni_descrizione["spagnolo"]["melee_velocita_attacco"][3]="Aumenta la velocidad de ataque cuerpo a cuerpo en un 15%";
        lista_upgrade_perenni_descrizione["spagnolo"]["melee_velocita_attacco"][4]="Aumenta la velocidad de ataque cuerpo a cuerpo en un 20%";

        lista_upgrade_perenni_descrizione["spagnolo"]["melee_colpiti"][1]="Los ataques cuerpo a cuerpo pueden golpear a 2 unidades adicionales al mismo tiempo";
        lista_upgrade_perenni_descrizione["spagnolo"]["melee_colpiti"][2]="Los ataques cuerpo a cuerpo pueden golpear a 3 unidades adicionales al mismo tiempo";

        lista_upgrade_perenni_descrizione["spagnolo"]["melee_ignora_attacco"][1]="Las unidades cuerpo a cuerpo ignorarán el primer golpe que reciben";
        lista_upgrade_perenni_descrizione["spagnolo"]["melee_ignora_attacco"][2]="Las unidades cuerpo a cuerpo ignorarán el primer y el segundo golpe que reciben";
        lista_upgrade_perenni_descrizione["spagnolo"]["melee_ignora_attacco"][3]="Las unidades cuerpo a cuerpo ignorarán el primer, il segundo y el tercero golpe que reciben";

        lista_upgrade_perenni_descrizione["spagnolo"]["melee_dono_zanzare"][1]="Las unidades cuerpo a cuerpo ganan un 5% de su salud como vitalidad cuando golpean enemigos";
        lista_upgrade_perenni_descrizione["spagnolo"]["melee_dono_zanzare"][2]="Las unidades cuerpo a cuerpo ganan un 8% de su salud como vitalidad cuando golpean enemigos";
        lista_upgrade_perenni_descrizione["spagnolo"]["melee_dono_zanzare"][3]="Las unidades cuerpo a cuerpo ganan un 10% de su salud como vitalidad cuando golpean enemigos";

        lista_upgrade_perenni_descrizione["spagnolo"]["proiettili_ignora_armatura"][1]="Las flechas ignoran un 25% de la armadura del objetivo";
        lista_upgrade_perenni_descrizione["spagnolo"]["proiettili_ignora_armatura"][2]="Las flechas ignoran un 50% de la armadura del objetivo";
        lista_upgrade_perenni_descrizione["spagnolo"]["proiettili_ignora_armatura"][3]="Las flechas ignoran un 75% de la armadura del objetivo";
        lista_upgrade_perenni_descrizione["spagnolo"]["proiettili_ignora_armatura"][4]="Las flechas ignoran un 100% de la armadura del objetivo";

        lista_upgrade_perenni_descrizione["spagnolo"]["proiettili_head_shot"][1]="Las flechas tienen un 1% de probabilidad de matar al enemigo con un solo lanzo";
        lista_upgrade_perenni_descrizione["spagnolo"]["proiettili_head_shot"][2]="Las flechas tienen un 2% de probabilidad de matar al enemigo con un solo lanzo";
        lista_upgrade_perenni_descrizione["spagnolo"]["proiettili_head_shot"][3]="Las flechas tienen un 3% de probabilidad de matar al enemigo con un solo lanzo";
        lista_upgrade_perenni_descrizione["spagnolo"]["proiettili_head_shot"][4]="Las flechas tienen un 4% de probabilidad de matar al enemigo con un solo lanzo";
        lista_upgrade_perenni_descrizione["spagnolo"]["proiettili_head_shot"][5]="Las flechas tienen un 5% de probabilidad de matar al enemigo con un solo lanzo";

        lista_upgrade_perenni_descrizione["spagnolo"]["proiettili_distanza"][1]="Aumenta el rango máximo de ataque a distancia de +1";
        lista_upgrade_perenni_descrizione["spagnolo"]["proiettili_distanza"][2]="Aumenta el rango máximo de ataque a distancia de +2";
        lista_upgrade_perenni_descrizione["spagnolo"]["proiettili_distanza"][3]="Aumenta el rango máximo de ataque a distancia de +3";

        lista_upgrade_perenni_descrizione["spagnolo"]["magia_veleno"][1]="Los hechizos lanzados envenenan al objetivo. A mayor nivel, mayor es el veneno";
        lista_upgrade_perenni_descrizione["spagnolo"]["magia_veleno"][2]="Los hechizos lanzados envenenan al objetivo. A mayor nivel, mayor es el veneno";
        lista_upgrade_perenni_descrizione["spagnolo"]["magia_veleno"][3]="Los hechizos lanzados envenenan al objetivo. A mayor nivel, mayor es el veneno";

        lista_upgrade_perenni_descrizione["spagnolo"]["magia_blocco"][1]="Los hechizos lanzados ralentizan al objetivo. A mayor nivel, mayor es el tiempo de ralentización";
        lista_upgrade_perenni_descrizione["spagnolo"]["magia_blocco"][2]="Los hechizos lanzados ralentizan al objetivo. A mayor nivel, mayor es el tiempo de ralentización";
        lista_upgrade_perenni_descrizione["spagnolo"]["magia_blocco"][3]="Los hechizos lanzados ralentizan al objetivo. A mayor nivel, mayor es el tiempo de ralentización";

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

        lista_upgrade_perenni_costi["magia_blocco"][1]=20;
        lista_upgrade_perenni_costi["magia_blocco"][2]=50;
        lista_upgrade_perenni_costi["magia_blocco"][3]=100;

        lista_classi_nome.Add("inglese",new Dictionary<string, string>());
        lista_classi_nome["inglese"].Add("warrior","Warrior");
        lista_classi_nome["inglese"].Add("arcer","Archer");
        lista_classi_nome["inglese"].Add("wizard","Wizard");

        lista_classi_nome.Add("italiano",new Dictionary<string, string>());
        lista_classi_nome["italiano"].Add("warrior","Guerriero");
        lista_classi_nome["italiano"].Add("arcer","Arciere");
        lista_classi_nome["italiano"].Add("wizard","Mago");

        lista_classi_nome.Add("spagnolo",new Dictionary<string, string>());
        lista_classi_nome["spagnolo"].Add("warrior","Guerrero");
        lista_classi_nome["spagnolo"].Add("arcer","Arquero");
        lista_classi_nome["spagnolo"].Add("wizard","Mago");
        
        //praticamente essendo di tre livelli diversi, tanto vale fare direttamente così; Come fossero tre razze diverse a seconda del livello.
        //Domani faremo la stessa cosa per le cavalcature
        lista_pupi_descrizione.Add("inglese",new Dictionary<string, string>());
        lista_pupi_descrizione.Add("italiano",new Dictionary<string, string>());
        lista_pupi_descrizione.Add("spagnolo",new Dictionary<string, string>());

        lista_razza_pupi_nome.Add("inglese",new Dictionary<string, string>());
        lista_razza_pupi_nome.Add("italiano",new Dictionary<string, string>());
        lista_razza_pupi_nome.Add("spagnolo",new Dictionary<string, string>());

        lista_razza_pupi_nome_singolare.Add("inglese",new Dictionary<string, string>());
        lista_razza_pupi_nome_singolare.Add("italiano",new Dictionary<string, string>());
        lista_razza_pupi_nome_singolare.Add("spagnolo",new Dictionary<string, string>());

        for (int i=1;i<=3;i++){
            //NB: Il singolare a destra è importante per definire la tipologia del pupo negli upgrade
            lista_razze_totale.Add("formiche_"+i,"formica");
            lista_razze_totale.Add("mosche_"+i,"mosca");
            lista_razze_totale.Add("api_"+i,"ape");
            lista_razze_totale.Add("ragnetti_"+i,"ragnetto");
            lista_razze_totale.Add("cavallette_"+i,"cavalletta");
            lista_razze_totale.Add("scarabei_"+i,"scarabeo");

            //i costi dei vari pupetti
            lista_costo_unita_razza.Add("formiche_"+i,30*i);
            lista_costo_unita_razza.Add("mosche_"+i,30*i);
            lista_costo_unita_razza.Add("api_"+i,30*i);
            lista_costo_unita_razza.Add("ragnetti_"+i,30*i);
            lista_costo_unita_razza.Add("cavallette_"+i,30*i);
            lista_costo_unita_razza.Add("scarabei_"+i,30*i);

            lista_pupi_descrizione["inglese"].Add("formiche_"+i,"Most commons soldiers");
            lista_pupi_descrizione["inglese"].Add("mosche_"+i,"Cheap cost but frails");
            lista_pupi_descrizione["inglese"].Add("api_"+i,"When they die, they launch their stinger at some enemy");
            lista_pupi_descrizione["inglese"].Add("ragnetti_"+i,"Every time they hit, they slow down the target. They are immune to this effect from other spiders");
            lista_pupi_descrizione["inglese"].Add("cavallette_"+i,"The grasshopers are fast. They have 10% of probability miss a melee attack or a distance attack");
            lista_pupi_descrizione["inglese"].Add("scarabei_"+i,"Beetles are the most resilient race you can aspire to");

            lista_pupi_descrizione["italiano"].Add("formiche_"+i,"Soldati più comuni");
            lista_pupi_descrizione["italiano"].Add("mosche_"+i,"Unità economiche, ma fragili");
            lista_pupi_descrizione["italiano"].Add("api_"+i,"Quando muoiono, lanciano un pungiglione contro un nemico");
            lista_pupi_descrizione["italiano"].Add("ragnetti_"+i,"Rallentano i nemici che colpiscono");
            lista_pupi_descrizione["italiano"].Add("cavallette_"+i,"Unità veloci ed inoltre hanno il 10% di possibilità di schivare un attacco corpo a corpo o a distanza");
            lista_pupi_descrizione["italiano"].Add("scarabei_"+i,"Gli scarabei sono le unità più resistenti ai danni");

            lista_pupi_descrizione["spagnolo"].Add("formiche_"+i,"Soldados más comunes");
            lista_pupi_descrizione["spagnolo"].Add("mosche_"+i,"Bajo costo pero frágile");
            lista_pupi_descrizione["spagnolo"].Add("api_"+i,"Cuando mueren, lanzan su aguijón hacia algún enemigo");
            lista_pupi_descrizione["spagnolo"].Add("ragnetti_"+i,"Cada vez que golpean, ralentizan al objetivo. Las arañas son inmunes");
            lista_pupi_descrizione["spagnolo"].Add("cavallette_"+i,"Los saltamontes son rápidos. Tienen un 10% de probabilidad de fallar un ataque cuerpo a cuerpo o a distancia");
            lista_pupi_descrizione["spagnolo"].Add("scarabei_"+i,"Los escarabajos son la raza más resistente a la que puedes aspirar");

            //NB: I Nomi e le descrizioni, saranno sempre al plurale
            lista_razza_pupi_nome["inglese"].Add("formiche_"+i,"Ants");     
            lista_razza_pupi_nome["inglese"].Add("mosche_"+i,"Flies");      
            lista_razza_pupi_nome["inglese"].Add("api_"+i,"Bees");          
            lista_razza_pupi_nome["inglese"].Add("ragnetti_"+i,"Spiders");  
            lista_razza_pupi_nome["inglese"].Add("cavallette_"+i,"Grasshoppers");  
            lista_razza_pupi_nome["inglese"].Add("scarabei_"+i,"Beetles");  

            //NB: I Nomi e le descrizioni, saranno sempre al plurale
            lista_razza_pupi_nome["italiano"].Add("formiche_"+i,"Formiche");     
            lista_razza_pupi_nome["italiano"].Add("mosche_"+i,"Mosche");      
            lista_razza_pupi_nome["italiano"].Add("api_"+i,"Api");          
            lista_razza_pupi_nome["italiano"].Add("ragnetti_"+i,"Ragni");  
            lista_razza_pupi_nome["italiano"].Add("cavallette_"+i,"Cavallette");  
            lista_razza_pupi_nome["italiano"].Add("scarabei_"+i,"Scarabei");  

            //NB: I Nomi e le descrizioni, saranno sempre al plurale
            lista_razza_pupi_nome["spagnolo"].Add("formiche_"+i,"Hormigas");     
            lista_razza_pupi_nome["spagnolo"].Add("mosche_"+i,"Moscas");      
            lista_razza_pupi_nome["spagnolo"].Add("api_"+i,"Abejas");          
            lista_razza_pupi_nome["spagnolo"].Add("ragnetti_"+i,"Arañas");  
            lista_razza_pupi_nome["spagnolo"].Add("cavallette_"+i,"Saltamontes");  
            lista_razza_pupi_nome["spagnolo"].Add("scarabei_"+i,"Escarabajos");  

            lista_razza_pupi_nome_singolare["inglese"].Add("formiche_"+i,"Ant");     
            lista_razza_pupi_nome_singolare["inglese"].Add("mosche_"+i,"Fly");      
            lista_razza_pupi_nome_singolare["inglese"].Add("api_"+i,"Bee");          
            lista_razza_pupi_nome_singolare["inglese"].Add("ragnetti_"+i,"Spider");  
            lista_razza_pupi_nome_singolare["inglese"].Add("cavallette_"+i,"Grasshopper");  
            lista_razza_pupi_nome_singolare["inglese"].Add("scarabei_"+i,"Beetle");  

            lista_razza_pupi_nome_singolare["italiano"].Add("formiche_"+i,"Formica");     
            lista_razza_pupi_nome_singolare["italiano"].Add("mosche_"+i,"Mosca");      
            lista_razza_pupi_nome_singolare["italiano"].Add("api_"+i,"Ape");          
            lista_razza_pupi_nome_singolare["italiano"].Add("ragnetti_"+i,"Ragno");  
            lista_razza_pupi_nome_singolare["italiano"].Add("cavallette_"+i,"Cavalletta");  
            lista_razza_pupi_nome_singolare["italiano"].Add("scarabei_"+i,"Scarabeo");  

            lista_razza_pupi_nome_singolare["spagnolo"].Add("formiche_"+i,"Hormiga");     
            lista_razza_pupi_nome_singolare["spagnolo"].Add("mosche_"+i,"Mosca");      
            lista_razza_pupi_nome_singolare["spagnolo"].Add("api_"+i,"Abeja");          
            lista_razza_pupi_nome_singolare["spagnolo"].Add("ragnetti_"+i,"Araña");  
            lista_razza_pupi_nome_singolare["spagnolo"].Add("cavallette_"+i,"Saltamonte");  
            lista_razza_pupi_nome_singolare["spagnolo"].Add("scarabei_"+i,"Escarabajo");  
        }

        //traduci nei blocchi le frasi lunghe (sempre a destra)
        //La lista di tutte le abilita!
        lista_bool_abilita_classe.Add("evoca_formiche",true);
        lista_abilita_cooldown.Add("evoca_formiche",new Dictionary<int, int>());
        lista_abilita_cooldown["evoca_formiche"].Add(1,15);
        lista_abilita_cooldown["evoca_formiche"].Add(2,18);
        lista_abilita_cooldown["evoca_formiche"].Add(3,20);

        lista_bool_abilita_classe.Add("mosche_fastidiose",true);
        lista_abilita_cooldown.Add("mosche_fastidiose",new Dictionary<int, int>());
        lista_abilita_cooldown["mosche_fastidiose"].Add(1,15);
        lista_abilita_cooldown["mosche_fastidiose"].Add(2,18);
        lista_abilita_cooldown["mosche_fastidiose"].Add(3,20);

        lista_bool_abilita_classe.Add("miele",true);
        lista_abilita_cooldown.Add("miele",new Dictionary<int, int>());
        lista_abilita_cooldown["miele"].Add(1,15);
        lista_abilita_cooldown["miele"].Add(2,18);
        lista_abilita_cooldown["miele"].Add(3,20);

        lista_bool_abilita_classe.Add("ragnatele",true);
        lista_abilita_cooldown.Add("ragnatele",new Dictionary<int, int>());
        lista_abilita_cooldown["ragnatele"].Add(1,15);
        lista_abilita_cooldown["ragnatele"].Add(2,18);
        lista_abilita_cooldown["ragnatele"].Add(3,20);

        lista_bool_abilita_classe.Add("velocita",true);
        lista_abilita_cooldown.Add("velocita",new Dictionary<int, int>());
        lista_abilita_cooldown["velocita"].Add(1,20);
        lista_abilita_cooldown["velocita"].Add(2,20);
        lista_abilita_cooldown["velocita"].Add(3,20);

        lista_bool_abilita_classe.Add("armatura",true);
        lista_abilita_cooldown.Add("armatura",new Dictionary<int, int>());
        lista_abilita_cooldown["armatura"].Add(1,20);
        lista_abilita_cooldown["armatura"].Add(2,20);
        lista_abilita_cooldown["armatura"].Add(3,20);

        lista_bool_abilita_classe.Add("zombie",false);
        lista_abilita_cooldown.Add("zombie",new Dictionary<int, int>());
        lista_abilita_cooldown["zombie"].Add(1,30);
        lista_abilita_cooldown["zombie"].Add(2,30);
        lista_abilita_cooldown["zombie"].Add(3,30);

        lista_bool_abilita_classe.Add("resurrezione",false);
        lista_abilita_cooldown.Add("resurrezione",new Dictionary<int, int>());
        lista_abilita_cooldown["resurrezione"].Add(1,20);
        lista_abilita_cooldown["resurrezione"].Add(2,30);
        lista_abilita_cooldown["resurrezione"].Add(3,40);

        lista_bool_abilita_classe.Add("insetto_esplosivo",false);
        lista_abilita_cooldown.Add("insetto_esplosivo",new Dictionary<int, int>());
        lista_abilita_cooldown["insetto_esplosivo"].Add(1,20);
        lista_abilita_cooldown["insetto_esplosivo"].Add(2,30);
        lista_abilita_cooldown["insetto_esplosivo"].Add(3,40);

        lista_bool_abilita_classe.Add("insetto_esplosivo_velenoso",false);
        lista_abilita_cooldown.Add("insetto_esplosivo_velenoso",new Dictionary<int, int>());
        lista_abilita_cooldown["insetto_esplosivo_velenoso"].Add(1,20);
        lista_abilita_cooldown["insetto_esplosivo_velenoso"].Add(2,30);
        lista_abilita_cooldown["insetto_esplosivo_velenoso"].Add(3,40);
        lista_bool_abilita_classe.Add("bombo",false);
        lista_abilita_cooldown.Add("bombo",new Dictionary<int, int>());
        lista_abilita_cooldown["bombo"].Add(1,20);
        lista_abilita_cooldown["bombo"].Add(2,30);
        lista_abilita_cooldown["bombo"].Add(3,40);

        lista_bool_abilita_classe.Add("balestra",false);
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

        if (!PlayerPrefs.HasKey("lingua")){PlayerPrefs.SetString("lingua","inglese");}
        setta_lingua(PlayerPrefs.GetString("lingua"));
    }

    public void setta_lingua(string lingua){
        print ("lingua: "+lingua);
        PlayerPrefs.SetString("lingua",lingua);

        if (SceneManager.GetActiveScene().name=="upgrade"){
            upgrade.cambia_lingua();
        }

        switch (lingua){
            case "italiano":{
                switch (SceneManager.GetActiveScene().name){
                    case "mainmenu":{
                        testo_mm_footer_diritti.SetText("Tutti i diritti riservati. Creato da Eros Cappello e Mauro \"Emme\" Forte");
                        bottone_mm_continue_game.SetText("Continua");
                        bottone_mm_new_game.SetText("Nuova partita");
                        bottone_mm_quit.SetText("Esci");
                        testo_mm_choose.SetText("Scegli il tuo conquistatore");
                        titolo_upgrade_perenni.SetText("Ricompense Infinite");
                        break;
                    }
                    case "game":{
                        testo_stage.SetText("Livello");
                        testo_game_pause.SetText("Pausa");
                        bottone_game_continue.SetText("Continua");
                        bottone_game_mainmenu.SetText("Menu principale");
                        bottone_game_opzioni.SetText("Opzioni");

                        testo_game_opz_principale.SetText("I tuoi progressi verranno salvati automaticamente DOPO la battaglia.\n\nSe ritorni ora al menu principale, perderei gli upgrade accumulati durante la battaglia.\n\nSei sicuro?");
                        bottone_trad_yes.SetText("Si");
                        bottone_trad_no.SetText("No");

                        testo_game_youwin.SetText("Hai Vinto");
                        bottone_game_nextstage.SetText("Prossimo livello");

                        testo_game_youlose.SetText("Hai Perso");
                        bottone_game_restart.SetText("Ricomincia");
                        break;
                    }
                    case "upgrade":{
                        testo_upgrade_army.SetText("ESERCITO");
                        testo_upgrade_gold.SetText("Monete");
                        testo_upgrade_improveyourarmy.SetText("Migliora il tuo esercito");
                        testo_upgrade_lev1.SetText("Liv 1");
                        testo_upgrade_lev2.SetText("Liv 2");
                        testo_upgrade_lev3.SetText("Liv 3");
                        testo_upgrade_upgrades.SetText("POTENZIAMENTI");
                        testo_upgrade_reward.SetText("Scegli la Ricompensa");
                        testo_upgrade_choose1.SetText("SCEGLI");
                        testo_upgrade_choose2.SetText("SCEGLI");
                        testo_upgrade_choose3.SetText("SCEGLI");
                        testo_upgrade_puppetsupgrades.SetText("Potenziamenti Pupetti");
                        testo_upgrade_armyupgrades.SetText("Potenziamento Esercito");
                        testo_upgrade_tyrantupgrades.SetText("Potenziamento Tiranno");

                        testo_upgrade_d_nome_abilita_distancedamage.SetText("Danno Distanza");
                        testo_upgrade_d_nome_abilita_food.SetText("Cibo");
                        testo_upgrade_d_nome_abilita_health.SetText("Salute");
                        testo_upgrade_d_nome_abilita_herocooldown.SetText("Cooldown Eroe");
                        testo_upgrade_d_nome_abilita_herodamage.SetText("Danno Eroe");
                        testo_upgrade_d_nome_abilita_herofury.SetText("Furia Eroe");
                        testo_upgrade_d_nome_abilita_meleedamage.SetText("Danno\ncorpo a corpo");
                        testo_upgrade_d_nome_abilita_randomrace.SetText("Razza Random");
                        testo_upgrade_d_nome_abilita_rndheroability.SetText("Abilità Eroe Random");
                        testo_upgrade_d_nome_abilita_rndul1.SetText("Unità Random livello 1");
                        testo_upgrade_d_nome_abilita_rndul2.SetText("Unità Random livello 2");
                        testo_upgrade_d_nome_abilita_rndul3.SetText("Unità Random livello 3");
                        testo_upgrade_d_nome_abilita_skillscooldown.SetText("Cooldown Abilità");
                        testo_upgrade_d_nome_abilita_spelldamage.SetText("Danno Magie");
                        testo_upgrade_d_nome_abilita_upgnut.SetText("Sblocca prossimo livello unità");

                        testo_game_pause.SetText("Pausa");
                        bottone_game_continue.SetText("Continua");
                        bottone_game_mainmenu.SetText("Menu principale");
                        bottone_game_opzioni.SetText("Opzioni");

                        testo_game_opz_principale.SetText("I tuoi progressi sono stati salvati.\n\nVuoi tornare al menu principale?");
                        bottone_trad_yes.SetText("Si");
                        bottone_trad_no.SetText("No");
                        break;
                    }
                }
                break;
            }
            case "spagnolo":{
                switch (SceneManager.GetActiveScene().name){
                    case "mainmenu":{
                        testo_mm_footer_diritti.SetText("Todos los derechos reservados. Creado por Eros Cappello y Mauro \"Emme\" Forte");
                        bottone_mm_continue_game.SetText("Continuar partida");
                        bottone_mm_new_game.SetText("Nueva partida");
                        bottone_mm_quit.SetText("Salir");
                        testo_mm_choose.SetText("Elige tu conquistador");
                        titolo_upgrade_perenni.SetText("Recompensas infinitas");
                        break;
                    }
                    case "game":{
                        testo_stage.SetText("Nivel");
                        testo_game_pause.SetText("Pausa");
                        bottone_game_continue.SetText("Continuar");
                        bottone_game_mainmenu.SetText("Menú principal");
                        bottone_game_opzioni.SetText("Opciones");

                        testo_game_opz_principale.SetText("Tu progreso se guardará automáticamente DESPUÉS de la batalla.\n\nSi regresas al menú principal durante la batalla, podrías perder tus mejoras.\n\n¿Estás seguro?");
                        bottone_trad_yes.SetText("Si");
                        bottone_trad_no.SetText("No");
                        testo_game_youwin.SetText("Has ganado");
                        bottone_game_nextstage.SetText("Siguiente Nivel");

                        testo_game_youlose.SetText("Has perdido");
                        bottone_game_restart.SetText("Reiniciar");
                        break;
                    }
                    case "upgrade":{
                        testo_upgrade_army.SetText("EJÉRCITO");
                        testo_upgrade_gold.SetText("Dinero");
                        testo_upgrade_improveyourarmy.SetText("Mejora tu ejército.");
                        testo_upgrade_lev1.SetText("Niv 1");
                        testo_upgrade_lev2.SetText("Niv 2");
                        testo_upgrade_lev3.SetText("Niv 3");
                        testo_upgrade_upgrades.SetText("POTENCIAMIENTOS");
                        testo_upgrade_reward.SetText("Elige tu recompensa");
                        testo_upgrade_choose1.SetText("ELIGE");
                        testo_upgrade_choose2.SetText("ELIGE");
                        testo_upgrade_choose3.SetText("ELIGE");
                        testo_upgrade_puppetsupgrades.SetText("Potenciamiento Unidades");
                        testo_upgrade_armyupgrades.SetText("Potenciamiento Ejército");
                        testo_upgrade_tyrantupgrades.SetText("Potenciamiento Tirano");

                        testo_upgrade_d_nome_abilita_distancedamage.SetText("Daño a distancia");
                        testo_upgrade_d_nome_abilita_food.SetText("Alimento");
                        testo_upgrade_d_nome_abilita_health.SetText("Salud");
                        testo_upgrade_d_nome_abilita_herocooldown.SetText("Cooldown del héroe");
                        testo_upgrade_d_nome_abilita_herodamage.SetText("Daño del héroe");
                        testo_upgrade_d_nome_abilita_herofury.SetText("Furia del héroe");
                        testo_upgrade_d_nome_abilita_meleedamage.SetText("Daño cuerpo a cuerpo");
                        testo_upgrade_d_nome_abilita_randomrace.SetText("Raza aleatoria");
                        testo_upgrade_d_nome_abilita_rndheroability.SetText("Habilidad aleatoria del héroe");
                        testo_upgrade_d_nome_abilita_rndul1.SetText("Unidad Aleatoria de Nivel 1");
                        testo_upgrade_d_nome_abilita_rndul2.SetText("Unidad Aleatoria de Nivel 2");
                        testo_upgrade_d_nome_abilita_rndul3.SetText("Unidad Aleatoria de Nivel 3");
                        testo_upgrade_d_nome_abilita_skillscooldown.SetText("Cooldown de habilidades");
                        testo_upgrade_d_nome_abilita_spelldamage.SetText("Daño de hechizos");
                        testo_upgrade_d_nome_abilita_upgnut.SetText("Mejorar el siguiente nivel de unidad.");


                        testo_game_pause.SetText("Pausa");
                        bottone_game_continue.SetText("Continuar");
                        bottone_game_mainmenu.SetText("Menú principal");
                        bottone_game_opzioni.SetText("Opciones");

                        bottone_trad_yes.SetText("Si");
                        bottone_trad_no.SetText("No");
                        testo_game_opz_principale.SetText("Tu progreso se guardará automáticamente después de la batalla.\n\nSi regresas al menú principal durante la batalla, es posible que pierdas tus mejoras.\n\n¿Estás seguro?");
                        break;
                    }
                }
                break;
            }
            default:{
                switch (SceneManager.GetActiveScene().name){
                    case "mainmenu":{
                        testo_mm_footer_diritti.SetText("All Rights reserved. Created by Eros Cappello and Mauro \"Emme\" Forte");
                        bottone_mm_continue_game.SetText("Continue Game");
                        bottone_mm_new_game.SetText("New Game");
                        bottone_mm_quit.SetText("Quit");
                        testo_mm_choose.SetText("Choose your Conquerer");
                        titolo_upgrade_perenni.SetText("Infinity Rewards");
                        break;
                    }
                    case "game":{
                        testo_stage.SetText("Stage");
                        testo_game_pause.SetText("Pause");
                        bottone_game_continue.SetText("Continue");
                        bottone_game_mainmenu.SetText("Main Menu");
                        bottone_game_opzioni.SetText("Options");

                        testo_game_opz_principale.SetText("Your progress will be automatically saved AFTER the battle.\n\nIf you will come back on the main menu during battle you may lost your upgrade.\n\nAre you sure?");
                        bottone_trad_yes.SetText("Yes");
                        bottone_trad_no.SetText("No");
                        testo_game_youwin.SetText("You Win");
                        bottone_game_nextstage.SetText("Next Stage");

                        testo_game_youlose.SetText("You Lose");
                        bottone_game_restart.SetText("Restart");
                        break;
                    }
                    case "upgrade":{
                        testo_upgrade_army.SetText("ARMY");
                        testo_upgrade_gold.SetText("Gold");
                        testo_upgrade_improveyourarmy.SetText("Improve your army");
                        testo_upgrade_lev1.SetText("Lev 1");
                        testo_upgrade_lev2.SetText("Lev 2");
                        testo_upgrade_lev3.SetText("Lev 3");
                        testo_upgrade_upgrades.SetText("UPGRADES");
                        testo_upgrade_reward.SetText("Choose Your Reward");
                        testo_upgrade_choose1.SetText("CHOOSE");
                        testo_upgrade_choose2.SetText("CHOOSE");
                        testo_upgrade_choose3.SetText("CHOOSE");
                        testo_upgrade_puppetsupgrades.SetText("Puppets upgrades");
                        testo_upgrade_armyupgrades.SetText("Army upgrades");
                        testo_upgrade_tyrantupgrades.SetText("Tyrant upgrades");

                        testo_upgrade_d_nome_abilita_distancedamage.SetText("Distance Damage");
                        testo_upgrade_d_nome_abilita_food.SetText("Food");
                        testo_upgrade_d_nome_abilita_health.SetText("Health");
                        testo_upgrade_d_nome_abilita_herocooldown.SetText("Hero Cooldown");
                        testo_upgrade_d_nome_abilita_herodamage.SetText("Hero damage");
                        testo_upgrade_d_nome_abilita_herofury.SetText("Hero Fury");
                        testo_upgrade_d_nome_abilita_meleedamage.SetText("Melee Damage");
                        testo_upgrade_d_nome_abilita_randomrace.SetText("Random Race");
                        testo_upgrade_d_nome_abilita_rndheroability.SetText("Random Hero Ability");
                        testo_upgrade_d_nome_abilita_rndul1.SetText("Random Unit Level 1");
                        testo_upgrade_d_nome_abilita_rndul2.SetText("Random Unit Level 2");
                        testo_upgrade_d_nome_abilita_rndul3.SetText("Random Unit Level 3");
                        testo_upgrade_d_nome_abilita_skillscooldown.SetText("Skills Cooldown");
                        testo_upgrade_d_nome_abilita_spelldamage.SetText("Spell Damage");
                        testo_upgrade_d_nome_abilita_upgnut.SetText("Upgrade next unity tier");


                        testo_game_pause.SetText("Pause");
                        bottone_game_continue.SetText("Continue");
                        bottone_game_mainmenu.SetText("Main Menu");
                        bottone_game_opzioni.SetText("Options");
                        bottone_trad_yes.SetText("Yes");
                        bottone_trad_no.SetText("No");
                        testo_game_opz_principale.SetText("Your progress will be automatically saved after the battle.\n\nIf you will come back on the main menu during battle you may lost your upgrade.\n\nAre you sure?");
                        break;
                    }
                }
                break;
            }
        }
    }

    //blocco relativo allencrypt
    public string Encrypt(string plainText, string password)
    {
        if (plainText == null)
        {
            throw new ArgumentNullException("plainText");
        }

        if (string.IsNullOrEmpty(password))
        {
            throw new ArgumentNullException("password");
        }

        // create instance of the DES crypto provider
        var des = new DESCryptoServiceProvider();

        // generate a random IV will be used a salt value for generating key
        des.GenerateIV();

        // use derive bytes to generate a key from the password and IV
        var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, des.IV, Iterations);

        // generate a key from the password provided
        byte[] key = rfc2898DeriveBytes.GetBytes(8);

        // encrypt the plainText
        using (var memoryStream = new MemoryStream())
        using (var cryptoStream = new CryptoStream(memoryStream, des.CreateEncryptor(key, des.IV), CryptoStreamMode.Write))
        {
            // write the salt first not encrypted
            memoryStream.Write(des.IV, 0, des.IV.Length);

            // convert the plain text string into a byte array
            byte[] bytes = Encoding.UTF8.GetBytes(plainText);

            // write the bytes into the crypto stream so that they are encrypted bytes
            cryptoStream.Write(bytes, 0, bytes.Length);
            cryptoStream.FlushFinalBlock();

            return Convert.ToBase64String(memoryStream.ToArray());
        }
    }

    public bool TryDecrypt(string cipherText, string password, out string plainText)
    {
        // its pointless trying to decrypt if the cipher text
        // or password has not been supplied
        if (string.IsNullOrEmpty(cipherText) || 
            string.IsNullOrEmpty(password))
        {
            plainText = "";
            return false;
        }

        try
        {   
            byte[] cipherBytes = Convert.FromBase64String(cipherText);

            using (var memoryStream = new MemoryStream(cipherBytes))
            {
                // create instance of the DES crypto provider
                var des = new DESCryptoServiceProvider();

                // get the IV
                byte[] iv = new byte[8];
                memoryStream.Read(iv, 0, iv.Length);

                // use derive bytes to generate key from password and IV
                var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, iv, Iterations);

                byte[] key = rfc2898DeriveBytes.GetBytes(8);

                using (var cryptoStream = new CryptoStream(memoryStream, des.CreateDecryptor(key, iv), CryptoStreamMode.Read))
                using (var streamReader = new StreamReader(cryptoStream))
                {
                    plainText = streamReader.ReadToEnd();
                    return true;
                }
            }
        }
        catch(Exception ex)
        {
            // TODO: log exception
            print(ex);

            plainText = "";
            return false;
        }
    }
    
    public string decripta(string testo, string password)
    {
        string decryptedValue="";
        TryDecrypt(testo, password, out decryptedValue);
        return decryptedValue;
    }
    //fine blocco
}
