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
            int toyID = 4;
            return toyID;
        }

        public List<int> GetChildsToys(int childID)
        {
            return new List<int>() {4,6,7,8};
        }

        public void RemoveToy(int toyID)
        {
            int childID = 1;
            List<int> toys = GetChildsToys(childID);
            toys.Remove(toyID);

        }
        public List<int> GetAllToysForChild(int childID)
        {
            return new List<int>();
        }
    }
}