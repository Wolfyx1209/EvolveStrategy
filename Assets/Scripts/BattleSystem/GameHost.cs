using EventBusSystem;
using System.Collections.Generic;
using TileSystem;
using UnityEngine;

public class GameHost : MonoBehaviour, IAcktorDiedHandler
{
    private Dictionary<PlayersList, GameAcktor> acktiveAcktors = new();
    private List<BattleBot> bots = new();

    private void Start()
    {
        TerrainTilemap tilemap = FindObjectOfType<TerrainTilemap>();
        acktiveAcktors.Add(PlayersList.Player, new Player(tilemap));
        acktiveAcktors.Add(PlayersList.None, new NoneAcktor(tilemap));
        List<TerrainCell> cells = tilemap.GetAllCells();
        foreach (TerrainCell cell in cells)
        {
            if (!acktiveAcktors.ContainsKey(cell.startOwner))
            {
                acktiveAcktors.Add(cell.startOwner, new BattleBot(cell.startOwner, tilemap));
                bots.Add(acktiveAcktors[cell.startOwner] as BattleBot);
            }
            cell.owner = acktiveAcktors[cell.startOwner];
            cell.FillCell();
        }
        Bank bank = Bank.instance;
        foreach(KeyValuePair<PlayersList, GameAcktor> pair in acktiveAcktors) 
        {
            bank.OpenAnAccount(pair.Key, 0);
        }
        foreach(BattleBot bot in bots) 
        {
            bot.StartBot();
        }
    }

    public void AcktorDie(GameAcktor acktor)
    {
        if(acktor.acktorName == PlayersList.Player) 
        {
            Debug.Log("You proebal");
        }
        else if(acktor.acktorName != PlayersList.None) 
        {
            BattleBot bot = acktor as BattleBot;
            bots.Remove(bot);
            bot.StopBot();
            if(bots.Count == 0) 
            {
                Debug.Log("Yra, pobeda!");
            }
        }
    }
}
