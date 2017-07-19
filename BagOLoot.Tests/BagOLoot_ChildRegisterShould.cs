using System;
using System.Collections.Generic;
using Xunit;

namespace BagOLoot.Tests
{
    public class ChildRegisterShould
    {
        private readonly ChildRegister _register;

        public ChildRegisterShould()
        {
            _register = new ChildRegister();
        }

        [Theory]
        [InlineData("Sarah")]
        [InlineData("Jamal")]
        [InlineData("Kelly")]
        public void AddChildren(string child)
        {
            var result = _register.AddChild(child);
            Assert.True(result);
        }

        [Fact]
        public void ReturnListOfChildren()
        {
            var result = _register.GetChildren();
            Assert.IsType<List<string>>(result);
        }
        [Fact]
        public void GetAllChildrenWithToysShould()
        {
            List<int> kidsWithToys = _register.GetAllChildrenWithToys();
            Assert.IsType<List<int>> (kidsWithToys);
        }
        [Fact]
        public void SetIsDeliveredShould()
        {
            int childID = 1;
            bool success = _register.IsDelivered(childID);
            Assert.True(success);
        }


    }
}
