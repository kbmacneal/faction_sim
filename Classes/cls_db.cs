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

        public Assets.Asset get_asset(int id)
        {
            var con = GetConnection();

            con.Open();

            List<Dictionary<string, string>> rtn = new List<Dictionary<string, string>>();

            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM public.assets where public.assets.id = @id;";
                cmd.Parameters.AddWithValue("id",NpgsqlDbType.Integer,id);
                // cmd.Parameters.AddWithValue("id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Dictionary<string, string> temp = new Dictionary<string, string>();
                        for (int i = 0; i < reader.GetColumnSchema().Count(); i++)
                        {
                            temp.TryAdd<string, string>(reader.GetColumnSchema()[i].ColumnName, reader.GetValue(i).ToString());
                        }

                        rtn.Add(temp);
                    }
                }
                con.Close();
                return JsonConvert.DeserializeObject<Assets.Asset>(JsonConvert.SerializeObject(rtn[0]));

            }
        }

        public Factions.Faction get_faction(int id)
        {
            var con = GetConnection();

            con.Open();

            List<Dictionary<string, string>> rtn = new List<Dictionary<string, string>>();

            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM public.factions where public.factions.id = @id;";
                cmd.Parameters.AddWithValue("id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Dictionary<string, string> temp = new Dictionary<string, string>();
                        for (int i = 0; i < reader.GetColumnSchema().Count(); i++)
                        {
                            temp.TryAdd<string, string>(reader.GetColumnSchema()[i].ColumnName, reader.GetValue(i).ToString());
                        }

                        rtn.Add(temp);
                    }
                }
                con.Close();

                var deserialized = JsonConvert.DeserializeObject<Factions.Faction>(JsonConvert.SerializeObject(rtn[0]));
                
                return deserialized;

            }
        }

        public List<Factions.Faction> get_faction()
        {
            var con = GetConnection();

            con.Open();

            List<Dictionary<string, string>> rtn = new List<Dictionary<string, string>>();

            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM public.factions;";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Dictionary<string, string> temp = new Dictionary<string, string>();
                        for (int i = 0; i < reader.GetColumnSchema().Count(); i++)
                        {
                            temp.TryAdd<string, string>(reader.GetColumnSchema()[i].ColumnName, reader.GetValue(i).ToString());
                        }

                        rtn.Add(temp);
                    }
                }
                con.Close();

                return JsonConvert.DeserializeObject<List<Factions.Faction>>(JsonConvert.SerializeObject(rtn[0])).ToList();
            }
        }

        private NpgsqlConnection GetConnection()
        {
            var connstring = String.Concat("Host=localhost;Port=5432;Username=", this.obj.username, ";Password=", this.obj.password, ";Database=sim_data");

            return new NpgsqlConnection(connstring);
        }
    }

}
