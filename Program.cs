//TODO
//multithreading
//defending asset % chance to be destroyed by attacker

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using faction_sim.Classes;
using faction_sim.Classes.Assets;
using faction_sim.Classes.Factions;

namespace faction_sim {
    public class result {
        public Classes.Assets.Asset asset { get; set; }
        public int iterations { get; set; }
        public int avg_damage { get; set; }
        public string chance_of_death { get; set; }
        public string hit_chance { get; set; }
        public int avg_counter_damage_taken { get; set; }
        public int avg_damage_per_swing { get; set; }
        public int total_damage { get; set; }
        public int total_deaths { get; set; }
        public int total_successes { get; set; }
        public int average_faction_atk_damage { get; set; }
        public int average_faction_def_damage { get; set; }
    }

    public class round {
        public Asset attacking_asset { get; set; }
        public Asset defending_asset { get; set; }
        public bool atk_success { get; set; }
        public int atk_roll { get; set; }
        public int def_roll { get; set; }
        public int damage { get; set; }
        public int counter_damage { get; set; }

    }
    class Program {
        public static Random rand = new Random ();
        static void Main (string[] args) {
            // int attacking_id = 12;
            // string[] attacking_ass = "23,21,74,37".Split (",");
            // int defending_id = 8;
            // string[] defending_ass = "47,23,46,21,15".Split (",");
            // int iterations = 10000;

            Console.WriteLine ("ID of the attacking faction:");
            int attacking_id = Convert.ToInt32 (Console.ReadLine ());

            Console.WriteLine ("CSV of the attacking assets:");
            string[] attacking_ass = Console.ReadLine ().Split (",");

            Console.WriteLine ("ID of the defending faction:");
            int defending_id = Convert.ToInt32 (Console.ReadLine ());

            Console.WriteLine ("CSV of the defending assets:");
            string[] defending_ass = Console.ReadLine ().Split (",");

            Console.WriteLine ("Number of iterations:");
            int iterations = Convert.ToInt32 (Console.ReadLine ());

            List<List<round>> results = new List<List<round>> ();

            for (int i = 0; i < iterations; i++) {
                var stacks = initialize_stacks (attacking_id, defending_id, attacking_ass, defending_ass);
                var result = run_sim (stacks);
                results.Add (result);
            }

            if (System.IO.File.Exists ("results.json")) {
                System.IO.File.Delete ("results.json");
            }

            System.IO.File.WriteAllText ("results.json", Newtonsoft.Json.JsonConvert.SerializeObject (results));

            var stats = get_results (results, get_ids (attacking_ass).ToArray (), iterations);

            System.IO.File.WriteAllText ("stats.json", Newtonsoft.Json.JsonConvert.SerializeObject (stats));
        }

        private static void get_defense_results (List<List<round>> results, int[] defenders, int interactions) {

        }

        private static List<result> get_results (List<List<round>> results, int[] assets, int iterations) {
            List<result> rtner = new List<result> ();

            foreach (var item in assets) {
                result result = new result ();
                Asset asset = get_asset (item);
                result.asset = asset;

                int total_damage = 0;
                int total_successes = 0;
                int total_deaths = 0;
                int total_counter = 0;
                int atk_faction_damage = 0;
                int def_faction_damage = 0;

                foreach (var round in results) {
                    if (round.Where (e => e.attacking_asset.Name == asset.Name).Count () > 0) {
                        total_damage += round.Where (e => e.attacking_asset.Name == asset.Name).Select (e => e.damage).Sum ();
                        total_successes += round.Where (e => e.attacking_asset.Name == asset.Name).Where (e => e.atk_success).Count ();
                        total_deaths += round.Where (e => e.attacking_asset.Name == asset.Name).Where (e => e.attacking_asset.Hp <= 0).Count ();
                        total_counter += round.Where (e => e.attacking_asset.Name == asset.Name).Select (e => e.counter_damage).Sum ();
                        atk_faction_damage += round.Select (e => e.damage).Sum ();
                        def_faction_damage += round.Select (e => e.counter_damage).Sum ();

                    }
                }

                if (total_successes != 0) {
                    result.avg_damage = total_damage / total_successes;
                } else {
                    result.avg_damage = 0;
                }

                result.total_damage = total_damage;
                result.total_deaths = total_deaths;
                result.total_successes = total_successes;
                double doub_death = (double) total_deaths / (double) iterations;
                result.chance_of_death = string.Format ("{0:N6}", doub_death);
                double doub_hit = (double) total_successes / (double) iterations;
                result.hit_chance = string.Format ("{0:N6}", doub_hit);
                result.iterations = iterations;
                result.avg_counter_damage_taken = total_counter / (iterations - total_successes);
                result.avg_damage_per_swing = total_damage / iterations;
                result.average_faction_atk_damage = atk_faction_damage / iterations;
                result.average_faction_def_damage = def_faction_damage / iterations;

                rtner.Add (result);

            }

            return rtner;

        }

        private static Dictionary<Classes.Factions.Faction, List<Int32>> initialize_stacks (int attacking_id, int defending_id, string[] attacking_ass, string[] defending_ass) {
            Dictionary<Classes.Factions.Faction, List<Int32>> rtner = new Dictionary<Classes.Factions.Faction, List<Int32>> ();

            List<Classes.Factions.Faction> combatants = initialize_factions (attacking_id, defending_id);

            List<Int32> attacking_assets = get_ids (attacking_ass);

            List<Int32> defending_assets = get_ids (defending_ass);

            rtner.Add (combatants[0], attacking_assets);

            rtner.Add (combatants[1], defending_assets);

            return rtner;
        }

        private static List<round> run_sim (Dictionary<Classes.Factions.Faction, List<Int32>> members) {
            List<round> results = new List<round> ();

            List<Classes.Assets.Asset> defenders = initialize_assets (members.Last ().Value.ToArray ());

            Faction attacking_faction = members.First ().Key;
            Faction defending_faction = members.Last ().Key;

            

            foreach (var id in members.First ().Value.ToArray ()) {
                var atk = get_asset (id);
                List<Asset> eligible_defenders = defenders.Where (e => e.Hp > 0).ToList ();

                if (atk.AttackStats == "None") continue;

                if (eligible_defenders.Count == 0) {
                    round result = run_round (atk, null, attacking_faction, defending_faction);

                    results.Add (result);
                } else {
                    Asset rand_defender = eligible_defenders.ToArray () [rand.Next (eligible_defenders.Count ())];
                    round result = run_round (atk, rand_defender, attacking_faction, defending_faction);

                    results.Add (result);
                }

            }

            return results;
        }

        private static round run_round (Asset attacker, Asset defender, Faction atk_faction, Faction def_faction) {
            round rnd = new round ();

            rnd.attacking_asset = attacker;

            Asset private_defender = defender;

            rnd.defending_asset = private_defender;

            Console.WriteLine(attacker.Name + " is attacking " + defender.Name);

            if (defender == null) {
                string[] vs_roll = attacker.AttackStats.Split ("v");

                long atk_mod = (long) helpers.GetPropValue (atk_faction, short_to_long[vs_roll[0]]);

                string atk_roll = "1d10+" + atk_mod.ToString ();

                int atk_result = roller.Roll (atk_roll).Sum ();
                rnd.atk_roll = atk_result;

                rnd.atk_success = true;

                rnd.damage = roller.Roll (attacker.AttackDice).Sum ();

                rnd.counter_damage = 0;

                return rnd;
            } else {
                string[] vs_roll = attacker.AttackStats.Split ("v");

                // if(vs_roll[0]=="None")continue;

                long atk_mod = (long) helpers.GetPropValue (atk_faction, short_to_long[vs_roll[0]]);

                long def_mod = (long) helpers.GetPropValue (def_faction, short_to_long[vs_roll[1]]);

                int atk_result = 0;
                int def_result = 0;

                string atk_roll = calculate_diceroll (atk_faction, short_to_long[vs_roll[0]]) + "+" + atk_mod.ToString ();
                string def_roll = calculate_diceroll (def_faction, short_to_long[vs_roll[1]]) + "+" + def_mod.ToString ();

                if (atk_faction.AlwaysRerollAtk && short_to_long[vs_roll[0]] == atk_faction.AttackerRerollStat) {
                    atk_result = roller.RollKeeps (atk_roll).Sum ();
                    rnd.atk_roll = atk_result;
                } else {
                    atk_result = roller.Roll (atk_roll).Sum ();
                    rnd.atk_roll = atk_result;
                }

                if (def_faction.AlwaysRerollDef && short_to_long[vs_roll[1]] == def_faction.DefenderRerollStat) {
                    def_result = roller.RollKeeps (def_roll).Sum ();
                    rnd.def_roll = def_result;
                } else {
                    def_result = roller.Roll (def_roll).Sum ();
                    rnd.def_roll = def_result;
                }

                if (atk_result >= def_result) {
                    rnd.atk_success = true;
                    
                    rnd.damage = roller.Roll (attacker.AttackDice).Sum ();
                    if(rnd.damage >= defender.Hp)
                    {
                        rnd.damage = Convert.ToInt32(defender.Hp);
                        defender.Hp = 0;
                    }
                    else
                    {
                        defender.Hp = defender.Hp - rnd.damage;
                    }

                    
                    rnd.counter_damage = 0;
                }

                if (def_result >= atk_result) {
                    rnd.atk_success = false;
                    rnd.damage = 0;
                    if (defender.Counterattack == "None") {
                        rnd.counter_damage = 0;
                    } else {
                        rnd.counter_damage = roller.Roll (defender.Counterattack).Sum ();
                        attacker.Hp = attacker.Hp - rnd.counter_damage;
                    }
                }

                if (def_result == atk_result) {
                    rnd.atk_success = true;
                }

                return rnd;
            }

        }

        private static List<int> get_ids (string[] assets) {
            List<int> rtner = new List<int> ();

            assets.ToList ().ForEach (e => rtner.Add (Convert.ToInt32 (e)));

            return rtner;
        }

        private static Asset get_asset (int id) {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Asset>> (System.IO.File.ReadAllText ("assets.json")).First (x => x.Id == id);
        }

        private static List<Classes.Assets.Asset> initialize_assets (int[] ids) {
            List<Classes.Assets.Asset> rtner = new List<Classes.Assets.Asset> ();

            List<Classes.Assets.Asset> master_list = Classes.Assets.Asset.FromJson (System.IO.File.ReadAllText ("assets.json")).ToList ();

            foreach (int id in ids) {
                Asset asset = new Asset ();
                asset = master_list.First (f => f.Id == id);
                rtner.Add (asset);
            }

            return rtner;
        }

        private static List<Classes.Factions.Faction> initialize_factions (int faction_atk, int faction_defend) {
            List<Classes.Factions.Faction> rtner = new List<Classes.Factions.Faction> ();

            List<Classes.Factions.Faction> master_list = Classes.Factions.Faction.FromJson (System.IO.File.ReadAllText ("factions.json")).ToList ();

            rtner.Add (master_list.First (e => e.Id == faction_atk));
            rtner.Add (master_list.First (e => e.Id == faction_defend));

            return rtner;
        }

        private static string calculate_diceroll (Faction faction, string roll_stat) {
            int num_dice = 1;

            if (faction.PMax && roll_stat == "Cunning") num_dice++;

            return num_dice.ToString () + "d10";
        }

        private static Dictionary<string, string> short_to_long = new Dictionary<string, string> { { "C", "Cunning" },
            { "W", "Wealth" },
            { "F", "Force" }
        };

    }
}