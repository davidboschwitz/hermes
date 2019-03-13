using System;
using System.Diagnostics;
using Hermes.Database;
using NUnit.Framework;
using SQLite;

namespace UnitTests
{
    public class TestItem : DatabaseItem
    {
        public string Body { get; set; }

        public TestItem() { }

        public TestItem(string body) : base(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), DateTime.Now, DateTime.Now, "Test", "TestItem")
        {
            Body = body;
        }

        public override string ToString()
        {
            return $"{base.ToString()} {Body}";
        }
    }
    public class TestItem2 : DatabaseItem
    {
        public int Body { get; set; }

        public TestItem2() { }

        public TestItem2(int body) : base(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), DateTime.Now, DateTime.Now, "Test", "TestItem")
        {
            Body = body;
        }

        public override string ToString()
        {
            return $"{base.ToString()} {Body}";
        }
    }

    [TestFixture]
    public class DatabaseTests
    {
        DatabaseController DatabaseController;

        [SetUp]
        public void SetUp()
        {
            Console.WriteLine("SetUp:start");
            DatabaseController = new DatabaseController();
            TestContext.WriteLine("SetUp:end");
        }

        [Test]
        public void TestMethod1()
        {
            TestContext.WriteLine("asdfasdf");
            DatabaseController.CreateTable<TestItem>();
            var db1 = new TestItem("bod1");
            var db2 = new TestItem2(10);
            DatabaseController.CreateTable<TestItem>();
            TestContext.WriteLine(DatabaseController.Insert(db1));
            TestContext.WriteLine(DatabaseController.Insert(db2));
            foreach (var i in DatabaseController.Table<TestItem>())
            {
                TestContext.WriteLine(i);
            }
            foreach (var i in DatabaseController.Table<TestItem2>())
            {
                TestContext.WriteLine(i);
            }
            Assert.Pass();
        }

        [Test]
        public void TestMethod2()
        {
            TestContext.WriteLine("asdfasdf");
            DatabaseController.CreateTable<TestItem>();
            DatabaseController.CreateTable<TestItem2>();
            var db1 = new TestItem("bod1");
            var db2 = new TestItem("bod2");
            DatabaseController.CreateTable<TestItem>();
            TestContext.WriteLine(DatabaseController.Insert(db1));
            TestContext.WriteLine(DatabaseController.Insert(db2));
            foreach (var i in DatabaseController.Table<TestItem>())
            {
                TestContext.WriteLine(i);
            }
            Assert.Pass();
        }
    }
}
