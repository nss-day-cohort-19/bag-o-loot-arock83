using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;

namespace BagOLoot
{
    public class ToyRegister
    {
        
        private string _connectionString = $"Data Source={Environment.GetEnvironmentVariable("BAGOLOOT_DB")}";
        private SqliteConnection _connection;
        
        public ToyRegister()
        {
            _connection = new SqliteConnection(_connectionString);
        }
        public int AddToyToChild (string name, int childID)
        {
            int _lastId = 0; // Will store the id of the last inserted record
            using (_connection)
            {
                _connection.Open ();
                SqliteCommand dbcmd = _connection.CreateCommand ();

                // Insert the new child
                dbcmd.CommandText = $"insert into toy values (null, '{name}', {childID})";
                Console.WriteLine(dbcmd.CommandText);
                dbcmd.ExecuteNonQuery ();

                // Get the id of the new row
                dbcmd.CommandText = $"select last_insert_rowid()";
                using (SqliteDataReader dr = dbcmd.ExecuteReader()) 
                {
                    if (dr.Read()) {
                        _lastId = dr.GetInt32(0);
                    } else {
                        throw new Exception("Unable to insert value");
                    }
                }

                // clean up
                dbcmd.Dispose ();
                _connection.Close ();
            }
            return _lastId;
        }

        public List<string> GetChildsToys(int childID)
        {
           List<string> toyList = new List<string>(){};// Will hold list of all children
            using (_connection)
            {
                _connection.Open();
                SqliteCommand dbcmd = _connection.CreateCommand();

                // Insert the new child
                dbcmd.CommandText = $"select t.name from toy t, child c where c.id = t.childID";
                using (SqliteDataReader dr = dbcmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                       toyList.Add(dr[0].ToString()); 
                    }
                }

                // clean up
                dbcmd.Dispose ();
                _connection.Close ();
            }
            return toyList;
        }

        public bool RemoveToy(int toyID)
        {
            bool success = false;
            using (_connection)
            {
                _connection.Open();
                SqliteCommand dbcmd = _connection.CreateCommand();
                dbcmd.CommandText = $"delete from toy where toy.id = {toyID}";
                dbcmd.ExecuteNonQuery();
                
                dbcmd.CommandText = $"select toy.id from toy where toy.id = {toyID}";
                using(SqliteDataReader reader = dbcmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                    success = false;
                    }
                    else
                    {
                    success = true;
                    }
                }
                dbcmd.Dispose();
                _connection.Close();
            }
            return success;
        }
        public Dictionary <int, string> GetAllToysForChild(int childID)
        {
            Dictionary<int,string> allToysForChild = new Dictionary<int, string>();
            using (_connection)
            {
                _connection.Open();
                SqliteCommand dbcmd = _connection.CreateCommand();
                dbcmd.CommandText = $"select toy.id, toy.name from toy where toy.childID = {childID}";
                using(SqliteDataReader dr = dbcmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        allToysForChild.Add(dr.GetInt32(0), dr[1].ToString());
                    }
                }

                _connection.Dispose();
                _connection.Close();
            }

            return allToysForChild;
        }
    }
}