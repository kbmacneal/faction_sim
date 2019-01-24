//TODO

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using faction_sim.Classes;
using faction_sim.Classes.Assets;
using faction_sim.Classes.Factions;
using Newtonsoft.Json;

namespace faction_sim
{
    public class result
    {
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
        public string chance_of_kill { get; set; }
        public string attacker_average_faction_damage { get; set; }
        public string hit_chance_less_death_chance { get; set; }
        public string average_total_stack_damage { get; set; }
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
        public int defender_hp_at_start_of_round { get; set; }
        public int direct_faction_damage { get; set; }
    }

    public class run_options
    {
        public int attacking_id { get; set; }
        public List<int> attacking_assets { get; set; }
        public int defending_id { get; set; }
        public List<int> defending_assets { get; set; }
        public int iterations { get; set; }
        public List<long> atk_reroll_ids { get; set; } = new List<long>();
        public List<long> def_reroll_ids { get; set; } = new List<long>();
        public List<long> atk_extra_dice { get; set; } = new List<long>();
        public List<long> def_extra_dice { get; set; } = new List<long>();
        public List<long> attacking_faction_ids { get; set; } = new List<long>();
        public List<long> defending_faction_ids { get; set; } = new List<long>();
        public bool attacker_pmax { get; set; } = false;
        public bool defender_pmax { get; set; } = false;
        public bool apply_passthrough_damage { get; set; } = false;
        public List<int> atk_asset_hp { get; set; } = new List<int>();
        public List<int> def_asset_hp { get; set; } = new List<int>();
        public int def_faction_hp { get; set; } = 0;

        public run_options()
        {
            this.attacking_assets = new List<int>();
            this.defending_assets = new List<int>();
        }

        public static void apply_runoptions(ref List<Asset> atk_assets, ref List<Asset> def_assets, ref Faction atk_faction, ref Faction def_faction, run_options opt)
        {
            if (opt.atk_reroll_ids.Count() > 0)
            {
                atk_assets.First(e => e.AttackerReroll == false && opt.atk_reroll_ids.Contains(e.Id)).AttackerReroll = true;
            }

            if (opt.def_reroll_ids.Count() > 0)
            {
                def_assets.First(e => e.DefenderReroll == false && opt.def_reroll_ids.Contains(e.Id)).DefenderReroll = true;
            }

            if (opt.atk_extra_dice.Count() > 0)
            {
                atk_assets.First(e => e.AttackerExtraDice == false && opt.atk_extra_dice.Contains(e.Id)).AttackerExtraDice = true;
            }

            if (opt.def_extra_dice.Count() > 0)
            {
                def_assets.First(e => e.DefenderExtraDice == false && opt.def_extra_dice.Contains(e.Id)).DefenderExtraDice = true;
            }

            if (atk_faction != null)
            {
                atk_faction.PMax = opt.attacker_pmax;
            }
            if (def_faction != null)
            {
                def_faction.PMax = opt.defender_pmax;
            }


        }
    }

    class Program
    {
        public static Random rand = new Random();
        public static run_options _runoptions = new run_options();
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                //     List<Asset> assets = Asset.FromJson(System.IO.File.ReadAllText("assets.json")).ToList();
                // List<Faction> factions = Faction.FromJson(System.IO.File.ReadAllText("factions.json")).ToList();

                // assets.ForEach(e=> Classes.Assets.Asset.InsertAsset(e));
                // factions.ForEach(e=> Classes.Factions.Faction.InsertFaction(e));
                Console.WriteLine("-f for a file input, -i for an interactive input. after input file specify full output location");
                return;
            }

            List<List<round>> results = new List<List<round>>();

            _runoptions = Newtonsoft.Json.JsonConvert.DeserializeObject<run_options>(System.IO.File.ReadAllText(args[1]));

            // _runoptions = Newtonsoft.Json.JsonConvert.DeserializeObject<run_options>(System.IO.File.ReadAllText("options.json"));

            string out_location = args[3];
            // string out_location = "options_results.json";

            int[] atk_assets = _runoptions.attacking_assets.Select(e => e).ToArray();

            int[] def_assets = _runoptions.defending_assets.Select(e => e).ToArray();

            List<Asset> attacking_assets = initialize_assets(_runoptions.atk_asset_hp, atk_assets);

            List<Asset> defending_assets = initialize_assets(_runoptions.def_asset_hp, def_assets);

            Faction attacking_faction = null;

            if (_runoptions.attacking_id != 0)
            {
                db_connection con = new db_connection();
                attacking_faction = con.get_faction(_runoptions.attacking_id);
            }

            Faction defending_faction = null;
            if (_runoptions.defending_id != 0)
            {
                db_connection con = new db_connection();
                defending_faction = con.get_faction(_runoptions.defending_id);

                if (_runoptions.def_faction_hp > 0) defending_faction.hp = _runoptions.def_faction_hp;
            }

            run_options.apply_runoptions(ref attacking_assets, ref defending_assets, ref attacking_faction, ref defending_faction, _runoptions);

            using (var progress = new ProgressBar())
            {
                for (int i = 0; i < _runoptions.iterations; i++)
                {
                    progress.Report((double)i / _runoptions.iterations);
                    attacking_assets.ForEach(e => e.resetHP());
                    defending_assets.ForEach(e => e.resetHP());
                    var result = run_sim(attacking_faction, defending_faction, attacking_assets, defending_assets);
                    results.Add(result);
                }
            }



            var stats = get_results(results, attacking_assets, _runoptions.iterations);

            System.IO.File.WriteAllText(out_location, Newtonsoft.Json.JsonConvert.SerializeObject(stats, Formatting.Indented));
            return;

        }

        private static bool reroll_same_or_other(int raw_a, int raw_b)
        {
            bool reroll_same = false;

            int value_a = 10 - raw_a;
            int value_b = raw_b - 1;

            if (value_a > value_b) reroll_same = true;

            return reroll_same;
            //Value A = 10 minus the unmodified Houses Minor roll
            //Value B = unmodified Other Faction roll minus 1

            // If value A > value B, use the Book of Secrets to reroll our die.
            // If value A <= value B, use the Book of Screts to reroll the other faction's die.
        }

        private static List<result> get_results(List<List<round>> results, List<Asset> assets, int iterations)
        {
            // System.IO.File.WriteAllText("results.json", JsonConvert.SerializeObject(results));
            List<result> rtner = new List<result>();

            int total_attacker_damage = 0;

            if (_runoptions.atk_asset_hp.Count > 0)
            {
                for (int i = 0; i < _runoptions.atk_asset_hp.Count; i++)
                {
                    if (_runoptions.atk_asset_hp[i] != 0) assets[i].hp = _runoptions.atk_asset_hp[i];
                }
            }

            foreach (var asset in assets)
            {

                List<round> result_set = new List<round>();

                // results.ForEach (e => e.Where (f => f.attacking_asset.instance_discriminator == asset.instance_discriminator).ToList ().ForEach (g => result_set.Add (g)));

                foreach (var round_list in results)
                {
                    foreach (var instance in round_list)
                    {
                        if (instance.attacking_asset.instance_discriminator == asset.instance_discriminator)
                        {
                            result_set.Add(instance);
                        }
                    }
                }

                // System.IO.File.WriteAllText("result_set.json",JsonConvert.SerializeObject(result_set));

                int total_damage = result_set.Select(e => e.damage).Sum();
                int total_successes = result_set.Where(e => e.atk_success).Count();
                int total_deaths = result_set.Where(e => e.attacking_asset.hp == 0).Count();
                int total_counter = result_set.Select(e => e.counter_damage).Sum();
                int atk_faction_damage = result_set.Select(e => e.damage).Sum();
                total_attacker_damage += atk_faction_damage;

                int total_kills = result_set.Where(e => e.defending_asset != null).Where(e => e.damage >= e.defender_hp_at_start_of_round).Count();
                int def_faction_damage = result_set.Select(e => e.counter_damage).Sum();

                int round_damage = 0;

                results.ForEach(e => e.Where(f => f.attacking_asset.instance_discriminator == asset.instance_discriminator).ToList().ForEach(g => round_damage += g.damage));

                // if (asset.Name == "Treachery")
                // {
                //     continue;
                // }
                result result = new result();
                result.asset = asset;

                if (total_successes != 0)
                {
                    result.avg_damage = total_damage / total_successes;
                }
                else
                {
                    result.avg_damage = 0;
                }

                if (total_successes != 0)
                {
                    // result.attacker_average_faction_damage = string.Format("{0:N6}", result_set.Select(e=>e.direct_faction_damage).Average());
                }
                else
                {
                    result.avg_damage = 0;
                }

                result.total_damage = total_damage;
                result.total_deaths = total_deaths;
                result.total_successes = total_successes;
                double doub_death = (double)total_deaths / (double)iterations;
                result.chance_of_death = string.Format("{0:N6}", doub_death);
                double doub_hit = (double)total_successes / (double)iterations;

                result.hit_chance_less_death_chance = string.Format("{0:N6}", ((double)total_successes / (double)iterations) - ((double)total_deaths / (double)iterations));
                result.hit_chance = string.Format("{0:N6}", doub_hit);
                result.iterations = iterations;
                if (iterations == total_successes)
                {
                    result.avg_counter_damage_taken = 0;
                }
                else
                {
                    result.avg_counter_damage_taken = total_counter / (iterations - total_successes);
                }

                result.avg_damage_per_swing = total_damage / iterations;
                result.average_faction_atk_damage = atk_faction_damage / iterations;
                result.average_faction_def_damage = def_faction_damage / iterations;

                if (total_successes > 0)
                {
                    double doub_kill = (double)total_kills / (double)total_successes;
                    result.chance_of_kill = string.Format("{0:N6}", doub_kill);
                }
                else
                {
                    result.chance_of_kill = ".000000";
                }
                rtner.Add(result);

            }

            double avg_dir = results.Select(e => e.Select(f => f.direct_faction_damage).Average()).Average();

            rtner.ForEach(e => e.attacker_average_faction_damage = string.Format("{0:N6}", avg_dir));

            double average_faction_dmg = (double)rtner.Select(e => e.total_damage).Sum() / (double)iterations;

            rtner.ForEach(e => e.average_total_stack_damage = string.Format("{0:N6}", average_faction_dmg));

            return rtner;

        }

        private static List<round> run_sim(Faction attacking_faction, Faction defending_faction, List<Asset> attackers, List<Asset> defenders)
        {
            List<round> results = new List<round>();

            if (attacking_faction == null)
            {
                for (int i = 0; i < _runoptions.attacking_faction_ids.Count(); i++)
                {
                    db_connection con = new db_connection();
                    attackers[i].owner = con.get_faction(Convert.ToInt32(_runoptions.attacking_faction_ids[i]));
                }
            }
            else
            {

                foreach (var attacker in attackers)
                {
                    attacker.owner = attacking_faction;
                }
            }

            if (defending_faction == null)
            {
                for (int i = 0; i < _runoptions.defending_faction_ids.Count(); i++)
                {
                    db_connection con = new db_connection();
                    defenders[i].owner = con.get_faction(Convert.ToInt32(_runoptions.defending_faction_ids[i]));
                }
            }
            else
            {
                foreach (var defender in defenders)
                {
                    defender.owner = defending_faction;
                }
            }

            foreach (Asset attacker in attackers)
            {
                var atk = attacker;
                List<Asset> eligible_defenders = defenders.Where(e => e.hp > 0).ToList();

                if (atk.AttackStats == "None" || attacker.hp == 0) continue;

                if (eligible_defenders.Count == 0)
                {
                    round result = run_round(atk, null, attacker.owner, defenders.Select(e => e.owner).First());

                    results.Add(result);
                }
                else
                {
                    Asset rand_defender = eligible_defenders.ToArray()[rand.Next(eligible_defenders.Count())];

                    round result = run_round(atk, rand_defender, attacker.owner, rand_defender.owner);

                    results.Add(result);
                }

            }

            return results;
        }

        private static round run_round(Asset attacker, Asset defender, Faction atk_faction, Faction def_faction)
        {
            round rnd = new round();

            rnd.attacking_asset = attacker;



            if (defender == null)
            {
                rnd.defending_asset = null;
                string[] vs_roll = attacker.AttackStats.Split("v");

                long atk_mod = (long)helpers.GetPropValue(atk_faction, short_to_long[vs_roll[0]]);

                long def_mod = (long)helpers.GetPropValue(def_faction, short_to_long[vs_roll[1]]);

                int atk_result = 0;
                int def_result = 0;

                string atk_roll = calculate_diceroll(atk_faction, short_to_long[vs_roll[0]]) + "+" + atk_mod.ToString();

                string def_roll = calculate_diceroll(def_faction, short_to_long[vs_roll[1]]) + "+" + def_mod.ToString();

                if (attacker.AttackerExtraDice)
                {
                    atk_roll = add_dice(atk_roll);
                    atk_result = roller.RollKeeps(atk_roll).Sum();
                    rnd.atk_roll = atk_result;
                }
                else
                {
                    if (atk_faction.PMax)
                    {
                        atk_result = roller.RollKeeps(atk_roll).Sum();
                        rnd.atk_roll = atk_result;
                    }
                    else
                    {
                        atk_result = roller.Roll(atk_roll).Sum();
                        rnd.atk_roll = atk_result;
                    }

                }


                if (def_faction.PMax)
                {
                    def_result = roller.RollKeeps(def_roll).Sum();
                    rnd.def_roll = def_result;
                }
                else
                {
                    def_result = roller.Roll(def_roll).Sum();
                    rnd.def_roll = def_result;
                }

                if (atk_result >= def_result)
                {
                    atk_result = roller.Roll(calculate_diceroll(atk_faction, short_to_long[vs_roll[0]]) + "+" + atk_mod.ToString()).Sum();
                    rnd.atk_roll = atk_result;

                }

                resolve_attack(ref atk_result, ref def_result, ref attacker, ref defender, ref rnd);

                return rnd;
            }

            else
            {
                rnd.defending_asset = defender;
                rnd.defender_hp_at_start_of_round = (int)defender.hp;
                string[] vs_roll = attacker.AttackStats.Split("v");

                long atk_mod = (long)helpers.GetPropValue(atk_faction, short_to_long[vs_roll[0]]);

                long def_mod = (long)helpers.GetPropValue(def_faction, short_to_long[vs_roll[1]]);

                int atk_result = 0;
                int def_result = 0;

                string atk_roll = calculate_diceroll(atk_faction, short_to_long[vs_roll[0]]) + "+" + atk_mod.ToString();

                string def_roll = calculate_diceroll(def_faction, short_to_long[vs_roll[1]]) + "+" + def_mod.ToString();

                if (attacker.AttackerExtraDice)
                {
                    atk_roll = add_dice(atk_roll);
                    atk_result = roller.RollKeeps(atk_roll).Sum();
                    rnd.atk_roll = atk_result;
                }
                else
                {
                    if (atk_faction.PMax)
                    {
                        atk_result = roller.RollKeeps(atk_roll).Sum();
                        rnd.atk_roll = atk_result;
                    }
                    else
                    {
                        atk_result = roller.Roll(atk_roll).Sum();
                        rnd.atk_roll = atk_result;
                    }

                }

                if (defender.DefenderExtraDice)
                {
                    def_roll = add_dice(def_roll);
                    def_result = roller.RollKeeps(def_roll).Sum();
                    rnd.def_roll = def_result;
                }
                else
                {
                    if (def_faction.PMax)
                    {
                        def_result = roller.RollKeeps(def_roll).Sum();
                        rnd.def_roll = def_result;
                    }
                    else
                    {
                        def_result = roller.Roll(def_roll).Sum();
                        rnd.def_roll = def_result;
                    }

                }

                if (atk_result < def_result && attacker.AttackerReroll)
                {
                    if (reroll_same_or_other(atk_result, def_result))
                    {
                        atk_result = roller.Roll(calculate_diceroll(atk_faction, short_to_long[vs_roll[0]]) + "+" + atk_mod.ToString()).Sum();
                        rnd.atk_roll = atk_result;
                    }
                    else
                    {
                        def_result = roller.Roll(calculate_diceroll(def_faction, short_to_long[vs_roll[1]]) + "+" + def_mod.ToString()).Sum();
                    }
                }

                if (atk_result >= def_result && defender.DefenderReroll)
                {

                    if (reroll_same_or_other(def_result, atk_result))
                    {
                        def_result = roller.Roll(calculate_diceroll(def_faction, short_to_long[vs_roll[1]]) + "+" + def_mod.ToString()).Sum();
                        rnd.def_roll = def_result;
                    }
                    else
                    {
                        atk_result = roller.Roll(calculate_diceroll(atk_faction, short_to_long[vs_roll[0]]) + "+" + atk_mod.ToString()).Sum();
                        rnd.atk_roll = atk_result;
                    }
                }

                resolve_attack(ref atk_result, ref def_result, ref attacker, ref defender, ref rnd);

                return rnd;
            }



        }

        public static void apply_damage(ref Asset def_asset, Faction def_faction, int damage, ref round rnd)
        {
            if (_runoptions.apply_passthrough_damage)
            {
                bool will_kill_asset = damage >= def_asset.hp;
                bool will_kill_faction = damage + rnd.direct_faction_damage >= def_faction.hp;

                if (will_kill_asset && !will_kill_faction)
                {
                    rnd.direct_faction_damage += damage;
                }
                else
                {
                    def_asset.hp -= damage;
                    if (def_asset.hp <= 0) def_asset.hp = 0;
                }
            }
            else
            {
                def_asset.hp -= damage;
                if (def_asset.hp <= 0) def_asset.hp = 0;
            }

        }

        public static void resolve_attack(ref int atk_result, ref int def_result, ref Asset attacker, ref Asset defender, ref round rnd)
        {
            if (defender == null)
            {
                if (atk_result >= def_result)
                {
                    //if there is no defender and the attack is successful, all damage is applied directly to faction hp
                    rnd.atk_success = true;
                    rnd.damage = roller.Roll(attacker.AttackDice).Sum();
                    rnd.counter_damage = 0;
                    rnd.attacking_asset = attacker;
                    rnd.defending_asset = null;

                    rnd.direct_faction_damage += rnd.damage;

                }

                if (def_result > atk_result)
                {
                    //no counter, attack fizzles
                    rnd.atk_success = false;
                    rnd.attacking_asset = attacker;
                    rnd.defending_asset = defender;
                    rnd.counter_damage = 0;
                    rnd.damage = 0;

                }
            }
            else
            {
                //we have a valid defender
                if (atk_result >= def_result)
                {
                    rnd.atk_success = true;
                    rnd.damage = roller.Roll(attacker.AttackDice).Sum();
                    rnd.counter_damage = 0;

                    if (rnd.defending_asset != null)
                    {
                        apply_damage(ref defender, defender.owner, rnd.damage, ref rnd);
                        // rnd.defending_asset.hp -= rnd.damage;
                        // if (rnd.defending_asset.hp < 0) rnd.defending_asset.hp = 0;
                    }
                    rnd.attacking_asset = attacker;
                    rnd.defending_asset = defender;
                }
                if (def_result > atk_result)
                {
                    rnd.attacking_asset = attacker;
                    rnd.defending_asset = defender;
                    rnd.atk_success = false;
                    rnd.damage = 0;
                    if (defender.Counterattack == "None")
                    {
                        rnd.counter_damage = 0;
                    }
                    else
                    {
                        rnd.counter_damage = roller.Roll(defender.Counterattack).Sum();
                        if (rnd.attacking_asset != null)
                        {
                            rnd.attacking_asset.hp -= rnd.counter_damage;
                            if (rnd.attacking_asset.hp < 0) rnd.attacking_asset.hp = 0;
                        }

                    }


                }

                if (def_result == atk_result)
                {
                    rnd.attacking_asset = attacker;
                    rnd.defending_asset = defender;
                    rnd.atk_success = true;
                    rnd.damage = 0;
                    if (defender.Counterattack == "None")
                    {
                        rnd.counter_damage = 0;
                    }
                    else
                    {
                        rnd.counter_damage = roller.Roll(defender.Counterattack).Sum();
                        if (rnd.attacking_asset != null)
                        {
                            rnd.attacking_asset.hp -= rnd.counter_damage;
                            if (rnd.attacking_asset.hp < 0) rnd.attacking_asset.hp = 0;
                        }
                    }

                    rnd.damage = roller.Roll(attacker.AttackDice).Sum();
                    if (rnd.defending_asset != null)
                    {
                        rnd.defending_asset.hp -= rnd.damage;
                        if (rnd.defending_asset.hp < 0) rnd.defending_asset.hp = 0;
                    }

                }
            }
        }

        private static List<Classes.Assets.Asset> initialize_assets(List<int> hps, int[] ids)
        {
            List<Classes.Assets.Asset> rtner = new List<Classes.Assets.Asset>();

            if (hps.Count > 0)
            {
                for (int i = 0; i < hps.Count; i++)
                {
                    db_connection con = new db_connection();
                    Asset asset = new Asset();
                    asset = con.get_asset(ids[i]);
                    asset.max_hp = asset.hp;
                    if (ids[i] != 0) asset.hp = hps[i];
                    rtner.Add(asset);
                }
            }
            else
            {
                foreach (int id in ids)
                {
                    db_connection con = new db_connection();
                    Asset asset = new Asset();
                    asset = con.get_asset(id);
                    asset.max_hp = asset.hp;
                    rtner.Add(asset);
                }
            }



            return rtner;
        }

        private static string calculate_diceroll(Faction faction, string roll_stat)
        {
            int num_dice = 1;

            if (faction.PMax && roll_stat == "Cunning")
            {
                num_dice++;
            }

            return num_dice.ToString() + "d10";
        }

        private static string add_dice(string diceroll)
        {
            string[] roll = diceroll.Split("d");

            if (roll.Length == 2)
            {
                int num_dice = Convert.ToInt32(roll[0]) + 1;

                return string.Concat(num_dice, "d", roll[1]);
            }
            else
            {
                return string.Concat(2, diceroll);
            }
        }

        private static Dictionary<string, string> short_to_long = new Dictionary<string, string> { { "C", "Cunning" },
            { "W", "Wealth" },
            { "F", "Force" }
        };

    }
}