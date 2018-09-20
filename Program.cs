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
            Console.WriteLine("ID of the attacking faction:");
            int attacking_id = 8;

            Console.WriteLine("CSV of the attacking assets:");
            string[] attacking_ass = "1,1".Split(",");

            Console.WriteLine("ID of the defending faction:");
            int defending_id = 9;

            Console.WriteLine("CSV of the defending assets:");
            string[] defending_ass = "75".Split(",");

            Console.WriteLine("Number of iterations:");
            int iterations = 5;

            // Console.WriteLine("ID of the attacking faction:");
            // int attacking_id = Convert.ToInt32(Console.ReadLine());

            // Console.WriteLine("CSV of the attacking assets:");
            // string[] attacking_ass = Console.ReadLine().Split(",");

            // Console.WriteLine("ID of the defending faction:");
            // int defending_id = Convert.ToInt32(Console.ReadLine());

            // Console.WriteLine("CSV of the defending assets:");
            // string[] defending_ass = Console.ReadLine().Split(",");

            // Console.WriteLine("Number of iterations:");
            // int iterations = Convert.ToInt32(Console.ReadLine());


            List<List<round>> results = new List<List<round>>();



            for (int i = 0; i < iterations; i++)
            {
                var stacks = initialize_stacks(attacking_id, defending_id, attacking_ass, defending_ass);
                var result = run_sim(stacks);
                results.Add(result);
            }

            if (System.IO.File.Exists("results.json"))
            {
                System.IO.File.Delete("results.json");
            }

            System.IO.File.WriteAllText("results.json", Newtonsoft.Json.JsonConvert.SerializeObject(results));
        }

        private static Dictionary<Classes.Factions.Faction, List<Int32>> initialize_stacks(int attacking_id, int defending_id, string[] attacking_ass, string[] defending_ass)
        {
            Dictionary<Classes.Factions.Faction, List<Int32>> rtner = new Dictionary<Classes.Factions.Faction, List<Int32>>();

            List<Classes.Factions.Faction> combatants = initialize_factions(attacking_id, defending_id);

            List<Int32> attacking_assets = get_ids(attacking_ass);

            List<Int32> defending_assets = get_ids(defending_ass);

            rtner.Add(combatants[0], attacking_assets);

            rtner.Add(combatants[1], defending_assets);

            return rtner;
        }

        private static List<round> run_sim(Dictionary<Classes.Factions.Faction, List<Int32>> members)
        {
            List<round> results = new List<round>();

            List<Classes.Assets.Asset> attackers = initialize_assets(members.First().Value.ToArray());
            List<Classes.Assets.Asset> defenders = initialize_assets(members.Last().Value.ToArray());

            Faction attacking_faction = members.First().Key;
            Faction defending_faction = members.First().Key;



            foreach (var atk in attackers)
            {
                List<Asset> eligible_defenders = defenders.Where(e => e.Hp > 0).ToList();

                if (eligible_defenders.Count == 0)
                {
                    round result = run_round(atk, null, attacking_faction, defending_faction);

                    results.Add(result);
                }
                else
                {
                    Asset rand_defender = eligible_defenders.ToArray()[rand.Next(eligible_defenders.Count())];
                    round result = run_round(atk, rand_defender, attacking_faction, defending_faction);

                    results.Add(result);
                }


            }


            return results;
        }

        private static round run_round(Asset attacker, Asset defender, Faction atk_faction, Faction def_faction)
        {
            round rnd = new round();

            attacker.AttackedAlready = true;

            rnd.attacking_asset = attacker;

            rnd.defending_asset = defender;

            if (defender == null)
            {
                string[] vs_roll = attacker.AttackStats.Split("v");

                long atk_mod = (long)helpers.GetPropValue(atk_faction, short_to_long[vs_roll[0]]);

                string atk_roll = "1d10+" + atk_mod.ToString();

                int atk_result = roller.Roll(atk_roll).Sum();
                rnd.atk_roll = atk_result;

                rnd.atk_success = true;

                rnd.damage = roller.Roll(attacker.AttackDice).Sum();

                rnd.counter_damage = 0;

                return rnd;
            }
            else
            {
                string[] vs_roll = attacker.AttackStats.Split("v");

                long atk_mod = (long)helpers.GetPropValue(atk_faction, short_to_long[vs_roll[0]]);

                long def_mod = (long)helpers.GetPropValue(def_faction, short_to_long[vs_roll[1]]);

                string atk_roll = "1d10+" + atk_mod.ToString();

                string def_roll = "1d10+" + def_mod.ToString();

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

        private static List<int> get_ids(string[] assets)
        {
            List<int> rtner = new List<int>();

            assets.ToList().ForEach(e => rtner.Add(Convert.ToInt32(e)));


            return rtner;
        }

        private static List<Classes.Assets.Asset> initialize_assets(int[] ids)
        {
            List<Classes.Assets.Asset> rtner = new List<Classes.Assets.Asset>();

            List<Classes.Assets.Asset> master_list = Classes.Assets.Asset.FromJson(System.IO.File.ReadAllText("assets.json")).ToList();

            foreach (int id in ids)
            {
                Asset asset = new Asset();
                asset = master_list.First(f => f.Id == id);
                rtner.Add(asset);
            }

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
