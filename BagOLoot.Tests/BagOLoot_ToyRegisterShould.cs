using System;
using System.Collections.Generic;
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
            List<int> toys = _register.GetChildsToys(childID);


            Assert.Contains(toyID, toys);
        }
        [Fact]
        public void RemoveToyFromChildShould()
        {
            int toyID = 9;
            int childID = 1;
            List<int> toys = _register.GetChildsToys(childID);            
            _register.RemoveToy(toyID);
            Assert.DoesNotContain(toyID, toys);

        }
        [Fact]
        public void GetAllToysForChildShould()
        {
            int childID = 1;
            List<int> toys=_register.GetAllToysForChild(childID);
            Assert.IsType<List<int>>(toys);

        }
        

       
    }
}