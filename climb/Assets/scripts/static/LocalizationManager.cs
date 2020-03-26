using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LocalizationManager {

	public static Dictionary<string, string> localizedDictionary = new Dictionary<string, string> ();

	public static void Init (string lang) {
		localizedDictionary.Clear ();

		switch (lang) {
		case "en":
			#region
			localizedDictionary.Add ("apply", "Apply");
			localizedDictionary.Add ("as_tag", "Autosave Icon");
			localizedDictionary.Add ("back", "Back");
			localizedDictionary.Add ("best_times", "Best Times");
			localizedDictionary.Add ("body", "Body");
			localizedDictionary.Add ("cancel", "Cancel");
			localizedDictionary.Add ("change", "Change");
			localizedDictionary.Add ("as_warning", "Climb automatically saves when you touch respawn points.");
			localizedDictionary.Add ("controls", "Bind Controls");
			localizedDictionary.Add ("coins", "Coins");
			localizedDictionary.Add ("confirm", "Are you sure?");
			localizedDictionary.Add ("continue", "Continue");
			localizedDictionary.Add ("default", "Default");
			localizedDictionary.Add ("delete", "Delete");
			localizedDictionary.Add ("empty", "-- EMPTY --");
			localizedDictionary.Add ("file1", "File 1 -");
			localizedDictionary.Add ("file2", "File 2 -");
			localizedDictionary.Add ("file3", "File 3 -");
			localizedDictionary.Add ("name_prompt", "Enter your name!");
			localizedDictionary.Add ("grnt_falls", "Granite\nFalls");
			localizedDictionary.Add ("home", "Home");
			localizedDictionary.Add ("hud_opac", "HUD Opacity");
			localizedDictionary.Add ("level", "Level");
			localizedDictionary.Add ("lvl_select", "Level Select");
			localizedDictionary.Add ("load_game", "Load Game");
			localizedDictionary.Add ("mtn_base", "Mountain\nBase");
			localizedDictionary.Add ("music_vol", "Music Volume");
			localizedDictionary.Add ("new_game", "New Game");
			localizedDictionary.Add ("new_record", "New Record!");
			localizedDictionary.Add ("no", "No");
			localizedDictionary.Add ("options", "Options");
			localizedDictionary.Add ("pause", "Pause");
			localizedDictionary.Add ("player", "Player");
			localizedDictionary.Add ("button_prompt", "Press pause to continue...");
			localizedDictionary.Add ("purchase", "Purchase");
			localizedDictionary.Add ("purchased", "Purchased");
			localizedDictionary.Add ("quit", "Quit");
			localizedDictionary.Add ("quit_title", "Quit to Title");
			localizedDictionary.Add ("restart", "Restart");
			localizedDictionary.Add ("resume", "Resume");
			localizedDictionary.Add ("file_sel_prompt", "Select a File");
			localizedDictionary.Add ("sfx_vol", "SFX Volume");
			localizedDictionary.Add ("shmns_cave", "Shaman's\nCave");
			localizedDictionary.Add ("speedrun", "Speedrun");
			localizedDictionary.Add ("speedrun_unlock", "You've unlocked Speedrun mode! It can be accessed from the Level Select Menu");
			localizedDictionary.Add ("sun_ridge", "Sunset\nRidge");
			localizedDictionary.Add ("time", "Time:");
			localizedDictionary.Add ("trim", "Trim");
			localizedDictionary.Add ("which_level", "Which level?");
			localizedDictionary.Add ("yes", "Yes");
			localizedDictionary.Add ("zbra_bridge", "Zebralope\nBridge");
			localizedDictionary.Add ("wardrobe", "Wardrobe");
			localizedDictionary.Add ("bdrm_painting", "Bedroom Painting");
			localizedDictionary.Add ("bdrm_table", "Bedroom End Table");
			localizedDictionary.Add ("study_end_table", "Study End Table");
			localizedDictionary.Add ("bed", "Bed");
			localizedDictionary.Add ("bdrm_mirror", "Bedroom Mirror");
			localizedDictionary.Add ("floor_rugs", "Floor Rugs");
			localizedDictionary.Add ("study_books", "Study Bookcase");
			localizedDictionary.Add ("lvngrm_plant", "Living Room Houseplant");
			localizedDictionary.Add ("study_plant", "Study Houseplant");
			localizedDictionary.Add ("study_chair", "Study Armchair");
			localizedDictionary.Add ("study_side_table", "Study Side Table");
			localizedDictionary.Add ("lvngrm_painting", "Living Room Painting");
			localizedDictionary.Add ("lvngrm_chair", "Living Room Chair");
			localizedDictionary.Add ("lvngrm_table", "Living Room Table");
			localizedDictionary.Add ("display", "Display Case");
			localizedDictionary.Add ("select_prompt", "Jump");
			localizedDictionary.Add ("cancel_prompt", "Run");
			localizedDictionary.Add ("pause_prompt", "Pause");
			localizedDictionary.Add ("up_key_prompt", "Up");
			localizedDictionary.Add ("dn_key_prompt", "Down");
			localizedDictionary.Add ("lf_key_prompt", "Left");
			localizedDictionary.Add ("rt_key_prompt", "Right");
			localizedDictionary.Add ("norm_poncho", "Normal Poncho");
			localizedDictionary.Add ("norm_poncho_desc", "Vic's traditional garb. It shields him from the sun & keeps him cool.");
			localizedDictionary.Add ("spdy_poncho", "Speedy Poncho");
			localizedDictionary.Add ("spdy_poncho_desc", "This poncho is designed to let air flow through the seams. It enables Vic to run faster.");
			localizedDictionary.Add ("lght_poncho", "Lightweight Poncho");
			localizedDictionary.Add ("lght_poncho_desc", "This poncho is crafted with the smoothest, lightest material. It boosts Vic's jump height.");
			localizedDictionary.Add ("wool_poncho", "Woolen Poncho");
			localizedDictionary.Add ("wool_poncho_desc", "This poncho is very dry (& slightly itchy!) It stops Vic from sliding on slippery surfaces.");
			localizedDictionary.Add ("nght_poncho", "Nighttime Poncho");
			localizedDictionary.Add ("nght_poncho_desc", "This poncho is imbued with the nature of nocturnal predators. It allows Vic to see in the dark.");
			localizedDictionary.Add ("agle_poncho", "Agile Poncho");
			localizedDictionary.Add ("agle_poncho_desc", "This poncho is woven with a physics-defying fabric. It lets Vic jump a second time in the air.");
			localizedDictionary.Add ("mlti_poncho", "Multicolor Poncho");
			localizedDictionary.Add ("mlti_poncho_desc", "You cannot grasp the true color of this poncho! Vic can change the colors of this attire.");
			localizedDictionary.Add ("credits", "-- CREDITS --\n" +
				"Climb. was made possible by\n\n" +
				"Luke Frank for inspiring me to complete this project\n" +
				"Nick Bassett of Dessert Media for composing a beautiful soundtrack\n" +
				"Crystal Marquis for providing some environmental art and fantastic ideas\n" +
				"Allison Wilson for providing some wall art and poncho designs\n\n" +
				"My mom, dad, and little sister for being there for me, always\n" +
				"Countless friends and loved ones who spent their valuable free-time playtesting my game & inciting me to continue on this project even through tough times\n" +
				"You, for purchasing, playing, and supporting Climb.\n" +
				"Everything else was made by myself, Josh Eslinger\n\n" +
				"Thank you for playing my game!");
			localizedDictionary.Add ("level_select_hint", "This button lets you travel to previous levels and to your house!");
			localizedDictionary.Add ("costume_unlock_hint", "You've unlocked a new costume!");
			localizedDictionary.Add ("blueprint_hint", "Use this to furnish your house!");
			#endregion
			break;

		case "pt":
			#region
			localizedDictionary.Add ("apply", "Confirmar");
			localizedDictionary.Add ("as_tag", "Ícone de Gravação Automática");
			localizedDictionary.Add ("back", "Voltar");
			localizedDictionary.Add ("best_times", "Os Melhores Tempos");
			localizedDictionary.Add ("body", "Corpo");
			localizedDictionary.Add ("cancel", "Cancelar");
			localizedDictionary.Add ("change", "Mudar");
			localizedDictionary.Add ("as_warning", "Climb grava automaticamente quando tocas nos pontos de respawn.");
			localizedDictionary.Add ("controls", "Controles");
			localizedDictionary.Add ("coins", "Moedas");
			localizedDictionary.Add ("confirm", "Tens a certeza?");
			localizedDictionary.Add ("continue", "Continuar");
			localizedDictionary.Add ("default", "Padrão");
			localizedDictionary.Add ("delete", "Apagar");
			localizedDictionary.Add ("empty", "-- VAZIO --");
			localizedDictionary.Add ("file1", "Ficheiro 1 -");
			localizedDictionary.Add ("file2", "Ficheiro 2 -");
			localizedDictionary.Add ("file3", "Ficheiro 3 -");
			localizedDictionary.Add ("name_prompt", "Escreva o seu nome!");
			localizedDictionary.Add ("grnt_falls", "Quedas de\nGranito");
			localizedDictionary.Add ("home", "Lar");
			localizedDictionary.Add ("hud_opac", "Opacidade HUD ");
			localizedDictionary.Add ("level", "Nível");
			localizedDictionary.Add ("lvl_select", "Seleção de Níveis");
			localizedDictionary.Add ("load_game", "Carregar o Jogo");
			localizedDictionary.Add ("mtn_base", "Base da\nMontanha");
			localizedDictionary.Add ("music_vol", "Volume da Música");
			localizedDictionary.Add ("new_game", "Novo Jogo");
			localizedDictionary.Add ("new_record", "Novo Recorde!");
			localizedDictionary.Add ("no", "Não");
			localizedDictionary.Add ("options", "Opções");
			localizedDictionary.Add ("pause", "Pausa");
			localizedDictionary.Add ("player", "Jogador");
			localizedDictionary.Add ("button_prompt", "Pressione qualquer butão...");
			localizedDictionary.Add ("purchase", "Comprar");
			localizedDictionary.Add ("purchased", "Comprado");
			localizedDictionary.Add ("quit", "Sair");
			localizedDictionary.Add ("quit_title", "Sair para o Ecrã Inicial");
			localizedDictionary.Add ("restart", "Recomeçar");
			localizedDictionary.Add ("resume", "Prosseguir");
			localizedDictionary.Add ("file_sel_prompt", "Selecione um ficheiro");
			localizedDictionary.Add ("sfx_vol", "Volume SFX");
			localizedDictionary.Add ("shmns_cave", "Caverna do\nXamã");
			localizedDictionary.Add ("speedrun", "Speedrun");
			localizedDictionary.Add ("speedrun_unlock", "Desbloqueaste o modo Speedrun! Este pode ser acedido no Menu Selecionador de Nível");
			localizedDictionary.Add ("sun_ridge", "Cume do\nPor do Sol");
			localizedDictionary.Add ("time", "Tempo:");
			localizedDictionary.Add ("trim", "Arranjar");
			localizedDictionary.Add ("which_level", "Qual Nível?");
			localizedDictionary.Add ("yes", "Sim");
			localizedDictionary.Add ("zbra_bridge", "Ponte\nZebralope");
			localizedDictionary.Add ("wardrobe", "Armário");
			localizedDictionary.Add ("bdrm_painting", "Quadro de Quarto");
			localizedDictionary.Add ("bdrm_table", "Cabeceira de Quarto");
			localizedDictionary.Add ("study_end_table", "Cabeceira de Escritório");
			localizedDictionary.Add ("bed", "Cama");
			localizedDictionary.Add ("bdrm_mirror", "Espelho de Quarto");
			localizedDictionary.Add ("floor_rugs", "Tapete");
			localizedDictionary.Add ("study_books", "Estante");
			localizedDictionary.Add ("lvngrm_plant", "Planta da Sala de Estar");
			localizedDictionary.Add ("study_plant", "Planta de Escritório");
			localizedDictionary.Add ("study_chair", "Poltrona de Escritório");
			localizedDictionary.Add ("study_side_table", "Mesa de Café de Escritório");
			localizedDictionary.Add ("lvngrm_painting", "Quadro da Sala de Estar");
			localizedDictionary.Add ("lvngrm_chair", "Cadeira da Sala de Estar");
			localizedDictionary.Add ("lvngrm_table", "Mesa da Sala de Estar");
			localizedDictionary.Add ("display", "Vitrine");
			localizedDictionary.Add ("select_prompt", "Saltar");
			localizedDictionary.Add ("cancel_prompt", "Correr");
			localizedDictionary.Add ("pause_prompt", "Pausa");
			localizedDictionary.Add ("up_key_prompt", "Cima");
			localizedDictionary.Add ("dn_key_prompt", "Baixo");
			localizedDictionary.Add ("lf_key_prompt", "Esquerda");
			localizedDictionary.Add ("rt_key_prompt", "Direita");
			localizedDictionary.Add ("norm_poncho", "Poncho Normal");
			localizedDictionary.Add ("norm_poncho_desc", "O traje tradicional de Vic. Protege-o do sol e mantém-no fresco.");
			localizedDictionary.Add ("spdy_poncho", "Poncho Rápido");
			localizedDictionary.Add ("spdy_poncho_desc", "Este poncho é projetado para deixar o ar fluir através das costuras. Isso permite que Vic corra mais rápido.");
			localizedDictionary.Add ("lght_poncho", "Poncho Leve");
			localizedDictionary.Add ("lght_poncho_desc", "Este poncho é trabalhado com o material mais suave e leve. Isso faz Vic pular mais alto.");
			localizedDictionary.Add ("wool_poncho", "Poncho De Lã");
			localizedDictionary.Add ("wool_poncho_desc", "Este poncho é muito seco (e ligeiramente áspero!) Ele impede que Vic deslize sobre superfícies escorregadias.");
			localizedDictionary.Add ("nght_poncho", "Poncho Noturno");
			localizedDictionary.Add ("nght_poncho_desc", "Este poncho é imbuído da natureza dos predadores noturnos. Isso permite que Vic veja no escuro.");
			localizedDictionary.Add ("agle_poncho", "Poncho Ágil");
			localizedDictionary.Add ("agle_poncho_desc", "Este poncho é tecido com um tecido que desafia a física. Isso permite que Vic salte uma segunda vez no ar.");
			localizedDictionary.Add ("mlti_poncho", "Poncho Multicolor");
			localizedDictionary.Add ("mlti_poncho_desc", "Você não pode entender a verdadeira cor deste poncho! Vic pode mudar as cores deste traje.");
			localizedDictionary.Add ("credits", "-- CRÉDITOS --\n" +
				"Climb. foi tornando possível por\n\n" +
				"Luke Frank por me inspirar a concluir este projeto\n" +
				"Nick Bassett, da Dessert Media, por compor uma bela trilha sonora\n" +
				"Crystal Marquis por fornecer arte ambiental e ideias fantásticas\n" +
				"Allison Wilson por fornecer alguns arte de parede e desenhos de poncho\n\n" +
				"Minha mãe, pai, e irmãzinha por estarem lá por mim, sempre\n" +
				"Inúmeros amigos e entes queridos que passaram o seu valioso tempo livre a testar o meu jogo e a incitar-me a continuar neste projeto mesmo em tempos difíceis\n" +
				"Você, porque comprou, jogou e apoiou Climb.\n" +
				"Tudo o resto foi feito por mim, Josh Eslinger\n\n" +
				"Obrigado por jogar o meu jogo!");
			localizedDictionary.Add ("level_select_hint", "Este butão permite-te viajar para níveis anteriores e para a tua casa!");
			localizedDictionary.Add ("costume_unlock_hint", "Desbloqueaste um novo traje!");
			localizedDictionary.Add ("blueprint_hint", "Usa isto para mobiliar a tua casa!");
			#endregion
			break;
		}
	}

}
