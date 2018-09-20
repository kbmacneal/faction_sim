using System;
using faction_sim.Classes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace faction_sim
{

    public class results
    {
        Classes.Assets.Asset asset{get;set;}
        Classes.Assets.Asset most_damage{get;set;}
        Classes.Assets.Asset most_threat{get;set;}
    }
    class Program
    {
        public static Random rand = new Random();
        static void Main(string[] args)
        {
            Dictionary<Classes.Factions.Faction, List<Classes.Assets.Asset>> members = new Dictionary<Classes.Factions.Faction, List<Classes.Assets.Asset>>();

            List<Classes.Factions.Faction> combatants = initialize_factions(Convert.ToInt32(args[0]), Convert.ToInt32(args[1]));

            List<Classes.Assets.Asset> attacking_assets = initialize_assets(get_ids("1.txt").ToArray());

            List<Classes.Assets.Asset> defending_assets = initialize_assets(get_ids("2.txt").ToArray());

            members.Add(combatants[0],attacking_assets);

            members.Add(combatants[1],defending_assets);
        }

        private static List<results> run_sim (Dictionary<Classes.Factions.Faction, List<Classes.Assets.Asset>> members)
        {
            List<results> results = new List<results>();


            return results;
        }

        private static List<int> get_ids (string path)
        {
            List<int> rtner = new List<int>();

            string[] content = System.IO.File.ReadAllLines(path);

            content.ToList().ForEach(e=>rtner.Add(Convert.ToInt32(e)));


            return rtner;
        }

        private static List<Classes.Assets.Asset> initialize_assets (int[] ids)
        {
            List<Classes.Assets.Asset> rtner = new List<Classes.Assets.Asset>();

            List<Classes.Assets.Asset> master_list = Classes.Assets.Asset.FromJson(System.IO.File.ReadAllText("assets.json")).ToList();

            ids.ToList().ForEach(e=> rtner.Add(master_list.First(f=>f.Id == e)));

            return rtner;
        }


        private static List<Classes.Factions.Faction> initialize_factions (int faction_atk, int faction_defend)
        {
            List<Classes.Factions.Faction> rtner = new List<Classes.Factions.Faction>();

            List<Classes.Factions.Faction> master_list = Classes.Factions.Faction.FromJson(System.IO.File.ReadAllText("factions.json")).ToList();

            rtner.Add(master_list.First(e=>e.Id == faction_atk));
            rtner.Add(master_list.First(e=>e.Id == faction_defend));

            return rtner;
        }

    }
}
