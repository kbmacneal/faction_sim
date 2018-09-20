using System;
using faction_sim.Classes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using faction_sim.Classes.Assets;
using faction_sim.Classes.Factions;

namespace faction_sim
{

    public class results
    {
        public Classes.Assets.Asset asset { get; set; }
        public Classes.Assets.Asset most_damage { get; set; }
        public Classes.Assets.Asset most_threat { get; set; }
    }

    public class round
    {
        public Asset attacking_asset { get; set; }
        public Asset defending_asset { get; set; }
        public bool atk_success { get; set; }
        public int atk_roll { get; set; }
        public int def_roll { get; set; }
        public int damage { get; set; }
        public int counter_damage { get; set; }
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

            members.Add(combatants[0], attacking_assets);

            members.Add(combatants[1], defending_assets);
        }

        private static List<results> run_sim(Dictionary<Classes.Factions.Faction, List<Classes.Assets.Asset>> members)
        {
            List<results> results = new List<results>();


            return results;
        }

        private static round run_round(ref Asset attacker, ref Asset defender, Faction atk_faction, Faction def_faction)
        {
            round rnd = new round();

            rnd.attacking_asset = attacker;

            rnd.defending_asset = defender;

            if (attacker.AttackStats == "None" | attacker.Hp == 0)
            {
                rnd.damage = 0;
                rnd.counter_damage = 0;
                rnd.atk_success = false;
                return rnd;
            }
            else
            {
                string[] vs_roll = attacker.AttackStats.Split("v");

                int atk_mod = (int)helpers.GetPropValue(atk_faction, short_to_long[vs_roll[0]]);

                int def_mod = (int)helpers.GetPropValue(def_faction, short_to_long[vs_roll[1]]);

                string atk_roll = "1d10" + atk_mod.ToString();

                string def_roll = "1d10" + def_mod.ToString();

                int atk_result = roller.Roll(atk_roll).Sum();
                rnd.atk_roll = atk_result;

                int def_result = roller.Roll(def_roll).Sum();
                rnd.def_roll = def_result;

                if (atk_result > def_result)
                {
                    rnd.atk_success = true;

                    rnd.damage = roller.Roll(attacker.AttackDice).Sum();
                    defender.Hp = defender.Hp - rnd.damage;
                    rnd.counter_damage = 0;
                }
                else
                {
                    rnd.atk_success = false;
                    rnd.damage = 0;
                    if (defender.Counterattack == "None")
                    {
                        rnd.counter_damage = 0;
                    }
                    else
                    {
                        rnd.counter_damage = roller.Roll(defender.Counterattack).Sum();
                        attacker.Hp = attacker.Hp - rnd.counter_damage;
                    }

                }

                return rnd;
            }


        }

        private static List<int> get_ids(string path)
        {
            List<int> rtner = new List<int>();

            string[] content = System.IO.File.ReadAllLines(path);

            content.ToList().ForEach(e => rtner.Add(Convert.ToInt32(e)));


            return rtner;
        }

        private static List<Classes.Assets.Asset> initialize_assets(int[] ids)
        {
            List<Classes.Assets.Asset> rtner = new List<Classes.Assets.Asset>();

            List<Classes.Assets.Asset> master_list = Classes.Assets.Asset.FromJson(System.IO.File.ReadAllText("assets.json")).ToList();

            ids.ToList().ForEach(e => rtner.Add(master_list.First(f => f.Id == e)));

            return rtner;
        }


        private static List<Classes.Factions.Faction> initialize_factions(int faction_atk, int faction_defend)
        {
            List<Classes.Factions.Faction> rtner = new List<Classes.Factions.Faction>();

            List<Classes.Factions.Faction> master_list = Classes.Factions.Faction.FromJson(System.IO.File.ReadAllText("factions.json")).ToList();

            rtner.Add(master_list.First(e => e.Id == faction_atk));
            rtner.Add(master_list.First(e => e.Id == faction_defend));

            return rtner;
        }

        private static Dictionary<string, string> short_to_long = new Dictionary<string, string>{
            {"C","Cunning"},
            {"W","Wealth"},
            {"F","Force"}
        };

    }
}
