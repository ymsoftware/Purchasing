using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace YM.Purchasing.Tests
{
    [TestClass]
    public class GroupCollectionTests
    {
        [TestMethod]
        public void Add_items_must_simply_increment()
        {
            var col = GetCollection();
            object result = col.Invoke("AddItem", "1");

            string[] items = (string[])col.GetProperty("Items");
            Assert.IsTrue(items.Length == 1);
            Assert.IsTrue(items[0] == "1");

            col.Invoke("AddItem", "2");
            items = (string[])col.GetProperty("Items");
            Assert.IsTrue(items.Length == 2);
            Assert.IsTrue(items[1] == "2");

            col.Invoke("AddItem", "3");
            items = (string[])col.GetProperty("Items");
            Assert.IsTrue(items.Length == 3);
            Assert.IsTrue(items[2] == "3");

            string[] groups = (string[])col.GetProperty("Groups");
            Assert.IsTrue(groups.Length == 0);
        }

        [TestMethod]
        public void Remove_items_must_simply_decrement()
        {
            var col = GetCollection();
            object result = col.Invoke("AddItem", "1");

            string[] items = (string[])col.GetProperty("Items");
            Assert.IsTrue(items.Length == 1);
            Assert.IsTrue(items[0] == "1");

            col.Invoke("AddItem", "2");
            items = (string[])col.GetProperty("Items");
            Assert.IsTrue(items.Length == 2);
            Assert.IsTrue(items[1] == "2");

            col.Invoke("AddItem", "3");
            items = (string[])col.GetProperty("Items");
            Assert.IsTrue(items.Length == 3);
            Assert.IsTrue(items[2] == "3");

            col.Invoke("RemoveItem", 1);
            items = (string[])col.GetProperty("Items");
            Assert.IsTrue(items.Length == 2);
            Assert.IsTrue(items[1] == "3");

            string[] groups = (string[])col.GetProperty("Groups");
            Assert.IsTrue(groups.Length == 0);
        }

        [TestMethod]
        public void Add_groups_must_simply_increment()
        {
            var col = GetCollection();
            object result = col.Invoke("AddGroup", "1");

            string[] groups = (string[])col.GetProperty("Groups");
            Assert.IsTrue(groups.Length == 1);
            Assert.IsTrue(groups[0] == "1");

            col.Invoke("AddGroup", "2");
            groups = (string[])col.GetProperty("Groups");
            Assert.IsTrue(groups.Length == 2);
            Assert.IsTrue(groups[1] == "2");

            string[] items = (string[])col.GetProperty("Items");
            Assert.IsTrue(items.Length == 0);
        }

        [TestMethod]
        public void Add_items_to_groups_must_rearrange()
        {
            var col = GetCollection();

            col.Invoke("AddGroup", "1");
            col.Invoke("AddGroup", "2");
            string[] groups = (string[])col.GetProperty("Groups");
            Assert.IsTrue(groups.Length == 2);
            Assert.IsTrue(groups[0] == "1");
            Assert.IsTrue(groups[1] == "2");

            col.Invoke("AddItem", "1", 0);
            col.Invoke("AddItem", "2", 1);
            col.Invoke("AddItem", "3", 0);

            string[] items = (string[])col.GetProperty("Items");
            Assert.IsTrue(items.Length == 3);
            Assert.IsTrue(items[0] == "1");
            Assert.IsTrue(items[1] == "3");
            Assert.IsTrue(items[2] == "2");

            col = GetCollection();

            col.Invoke("AddItem", "1");

            col.Invoke("AddGroup", "1");
            col.Invoke("AddGroup", "2");
            groups = (string[])col.GetProperty("Groups");
            Assert.IsTrue(groups.Length == 2);
            Assert.IsTrue(groups[0] == "1");
            Assert.IsTrue(groups[1] == "2");
            
            col.Invoke("AddItem", "2", 1);
            col.Invoke("AddItem", "3", 0);

            items = (string[])col.GetProperty("Items");
            Assert.IsTrue(items.Length == 3);
            Assert.IsTrue(items[0] == "3");
            Assert.IsTrue(items[1] == "2");
            Assert.IsTrue(items[2] == "1");
        }

        [TestMethod]
        public void Moving_items_must_rearrange()
        {
            var col = GetCollection();

            col.Invoke("AddItem", "1"); //no group
            col.Invoke("AddItem", "2"); //no group

            col.Invoke("AddGroup", "1");
            col.Invoke("AddGroup", "2");
            var groups = (string[])col.GetProperty("Groups");
            Assert.IsTrue(groups.Length == 2);
            Assert.IsTrue(groups[0] == "1");
            Assert.IsTrue(groups[1] == "2");

            col.Invoke("AddItem", "3", 0); //group 1
            col.Invoke("AddItem", "4", 0); //group 1
            col.Invoke("AddItem", "5", 1); //group 2
            col.Invoke("AddItem", "6", 1); //group 2

            var items = (string[])col.GetProperty("Items");
            Assert.IsTrue(items.Length == 6);
            Assert.IsTrue(items[0] == "3");
            Assert.IsTrue(items[1] == "4");
            Assert.IsTrue(items[2] == "5");
            Assert.IsTrue(items[3] == "6");
            Assert.IsTrue(items[4] == "1");
            Assert.IsTrue(items[5] == "2");

            col.Invoke("MoveItem", 4, 0); //item 1 to group 1
            col.Invoke("MoveItem", 5, 1); //item 2 to group 2
            items = (string[])col.GetProperty("Items");
            Assert.IsTrue(items[2] == "1");
            Assert.IsTrue(items[5] == "2");

            col.Invoke("MoveItem", 0, 0, 1); //item 3 to group 2
            col.Invoke("MoveItem", 0, 0, 1); //item 4 to group 2
            items = (string[])col.GetProperty("Items");
            Assert.IsTrue(items[0] == "1");
            Assert.IsTrue(items[3] == "2");
            Assert.IsTrue(items[4] == "3");
            Assert.IsTrue(items[5] == "4");

            col.Invoke("RemoveItem", 0, 0); //remove item 1
            items = (string[])col.GetProperty("Items");
            Assert.IsTrue(items.Length == 5);
            Assert.IsTrue(items[0] == "5");
            Assert.IsTrue(items[2] == "2");
            Assert.IsTrue(items[3] == "3");
            Assert.IsTrue(items[4] == "4");

            col.Invoke("RemoveGroup", 0, 0); //remove group 1; item 3 will move to "no group"
            items = (string[])col.GetProperty("Items");
            Assert.IsTrue(items.Length == 5);
            Assert.IsTrue(items[0] == "5");
            Assert.IsTrue(items[1] == "6");
            Assert.IsTrue(items[2] == "2");
            Assert.IsTrue(items[3] == "3");
            Assert.IsTrue(items[4] == "4");
        }
        
        private PrivateObject GetCollection()
        {
            var asm = typeof(IRequisition).Assembly;
            var type = asm.GetType("YM.Purchasing.Common.GroupCollection`2");
            var genType = type.MakeGenericType(new Type[2] { typeof(string), typeof(string) });
            var obj = Activator.CreateInstance(genType, true);
            return new PrivateObject(obj);
        }
    }
}
