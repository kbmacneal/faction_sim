// Generated by https://quicktype.io

using System;
using System.Collections.Generic;
using System.Linq;
using Npgsql;
using Newtonsoft.Json;
using System.Dynamic;
using NpgsqlTypes;

namespace faction_sim.Classes
{
    public class db_connection
    {
        private class settings
        {
            public string username { get; set; }
            public string password { get; set; }
        }
        settings obj = JsonConvert.DeserializeObject<settings>(System.IO.File.ReadAllText("faction_sim.json"));

        private List<Assets.Asset> DbToAsset(NpgsqlDataReader reader)
        {
            List<Assets.Asset> rtn = new List<Assets.Asset>();

            while (reader.Read())
            {
                Assets.Asset temp = new Assets.Asset();
                for (int i = 0; i < reader.GetColumnSchema().Count(); i++)
                {
                    helpers.SetPropValue(temp,reader.GetColumnSchema()[i].ColumnName,reader.GetValue(i).ToString());
                }
                rtn.Add(temp);
            }

            return rtn;
        }

        private List<Factions.Faction> DbToFaction(NpgsqlDataReader reader)
        {
            List<Factions.Faction> rtn = new List<Factions.Faction>();

            while (reader.Read())
            {
                Factions.Faction temp = new Factions.Faction();
                for (int i = 0; i < reader.GetColumnSchema().Count(); i++)
                {
                    helpers.SetPropValue(temp,reader.GetColumnSchema()[i].ColumnName,reader.GetValue(i).ToString());
                }
                rtn.Add(temp);
            }

            return rtn;
        }

        public Assets.Asset get_asset(int id)
        {
            var con = GetConnection();

            con.Open();

            Assets.Asset rtn = new Assets.Asset();

            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM assets where assets.\"Id\" = @id;";
                cmd.Parameters.AddWithValue("id", NpgsqlDbType.Integer, id);
                // cmd.Parameters.AddWithValue("id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    rtn = DbToAsset(reader)[0];
                }
                con.Close();
                return rtn;

            }
        }

        public Factions.Faction get_faction(int id)
        {
            var con = GetConnection();

            con.Open();

            Factions.Faction rtn = new Factions.Faction();

            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM factions where factions.\"Id\" = @id;";
                cmd.Parameters.AddWithValue("id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    rtn = DbToFaction(reader)[0];
                }
                con.Close();

                return rtn;

            }
        }

        public List<Factions.Faction> get_faction()
        {
            var con = GetConnection();

            con.Open();

            List<Factions.Faction> rtn = new List<Factions.Faction>();

            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM public.factions;";

                using (var reader = cmd.ExecuteReader())
                {
                    rtn = DbToFaction(reader);    
                }
                con.Close();

                return rtn;
            }
        }

        private NpgsqlConnection GetConnection()
        {
            var connstring = String.Concat("Host=localhost;Port=5432;Username=", this.obj.username, ";Password=", this.obj.password, ";Database=sim_data");

            return new NpgsqlConnection(connstring);
        }
    }

}
