using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BagOLoot.Tests
{
    public class ToyRegisterShould
    {
        private readonly ToyRegister _register;

        public ToyRegisterShould()
        {
            _register = new ToyRegister();
        }
        [Fact]
        public void AddToyToChildShould()
        {
            string toyName = "Firetruck";
            int childID = 1;
            int toyID = _register.AddToyToChild(toyName, childID);
            List<string> toys = _register.GetChildsToys(childID);


            Assert.Contains(toyName, toys);
        }
        [Fact]
        public void RemoveToyFromChildShould()
        {
            string toy = "Firetruck";
            int childID = 1;
            Dictionary<int, string> toys = _register.GetAllToysForChild(childID);
            int toyID = toys.FirstOrDefault(x => x.Value == toy).Key;            
            bool success = _register.RemoveToy(toyID);
            Assert.True(success);

        }
        [Fact]
        public void GetAllToysForChildShould()
        {
            int childID = 1;
            Dictionary<int, string> toys=_register.GetAllToysForChild(childID);
            Assert.IsType<Dictionary<int, string>>(toys);

        }
        

       
    }
}