using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;

namespace BagOLoot
{
    public class ChildRegister
    {
        private List<string> _children = new List<string>();
        private string _connectionString = $"Data Source={Environment.GetEnvironmentVariable("BAGOLOOT_DB")}";
        private SqliteConnection _connection;

        public ChildRegister()
        {
            _connection = new SqliteConnection(_connectionString);
        }

        public bool AddChild (string child) 
        {
            int _lastId = 0; // Will store the id of the last inserted record
            using (_connection)
            {
                _connection.Open ();
                SqliteCommand dbcmd = _connection.CreateCommand ();

                // Insert the new child
                dbcmd.CommandText = $"insert into child values (null, '{child}', 0)";
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
            return _lastId != 0;
        }

        public Dictionary<int, string> GetChildren()
        {
             
           Dictionary<int, string> childList = new Dictionary<int, string>();// Will hold list of all children
            using (_connection)
            {
                _connection.Open();
                SqliteCommand dbcmd = _connection.CreateCommand();

                // Insert the new child
                dbcmd.CommandText = $"select id, name from child";
                using (SqliteDataReader dr = dbcmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                       childList.Add(dr.GetInt32(0), dr[1].ToString()); 
                    }
                }

                // clean up
                dbcmd.Dispose ();
                _connection.Close ();
            }
            return childList;
        }

        public string GetChild (string name)
        {
            var child = _children.SingleOrDefault(c => c == name);

            // Inevitably, two children will have the same name. Then what?

            return child;
        }

        public Dictionary<int, string> GetAllChildrenWithToys()
        {
            Dictionary<int, string> childList = new Dictionary<int, string>();// Will hold list of all children
            using (_connection)
            {
                _connection.Open();
                SqliteCommand dbcmd = _connection.CreateCommand();

                // Insert the new child
                dbcmd.CommandText = $"select c.id, c.name from child c inner join toy t where c.id = t.childID";
                using (SqliteDataReader dr = dbcmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        if(childList.ContainsKey(dr.GetInt32(0)))
                        {

                        }
                        else
                        {
                            childList.Add(dr.GetInt32(0), dr[1].ToString());
                        }
                         
                    }
                }

                // clean up
                dbcmd.Dispose ();
                _connection.Close ();
            }
            return childList;
        }

        public bool IsDelivered(int childID)
        {
            bool success = false;
            using (_connection)
            {
                _connection.Open();
                SqliteCommand dbcmd = _connection.CreateCommand();
                dbcmd.CommandText = $"update child set delivered = 1 where id={childID}";
                dbcmd.ExecuteNonQuery();

                dbcmd.CommandText = $"select c.id from child c where c.id={childID} and c.delivered = 1";
                using(SqliteDataReader reader = dbcmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                    success = true;
                    }
                    else
                    {
                    success = false;
                    }
                }
                _connection.Dispose();
                _connection.Close();
            }
            return success;
        }
    }
}