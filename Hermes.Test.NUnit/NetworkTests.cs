using Hermes.Capability.Chat;
using Hermes.Database;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Hermes.Test.NUnit
{
    public class NetworkTests
    {
        DatabaseController DatabaseController;

        [SetUp]
        public void Setup()
        {
            DatabaseController = new DatabaseController();
            DatabaseController.CreateTable<ChatMessage>();
        }

        [Test]
        public void Test1()
        {
            var x1 = new ChatMessage(Guid.NewGuid(), Guid.NewGuid(), "test1");
            DatabaseController.Insert(x1);
            var x2 = new ChatMessage(Guid.NewGuid(), Guid.NewGuid(), "test2");
            DatabaseController.Insert(x2);
            var t = typeof(ChatMessage);
            var map = DatabaseController.GetMapping(t);
            var list = DatabaseController.Query(map, "SELECT * FROM ChatMessage");
            foreach(var m in list)
            {
                TestContext.WriteLine(m);
                TestContext.WriteLine(m is ChatMessage);
            }
            foreach(var m in list)
            {
                TestContext.WriteLine(JsonConvert.SerializeObject(m));
                TestContext.WriteLine(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(m), t) is ChatMessage);
                TestContext.WriteLine(JsonConvert.SerializeObject(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(m))));
            }
            TestContext.WriteLine("asdfasdf");
            TestContext.WriteLine(JsonConvert.SerializeObject(list));
        }

        [Test]
        public void HashSetTest()
        {

        }

        [Test]
        public void MetadataTest()
        {
            var T = typeof(ChatMessage);
            var mapping = DatabaseController.GetMapping(T);
            var localTable = DatabaseController.Query(mapping, $"SELECT * FROM ChatMessage");
            var json = localTable
                .Select(r => new SyncMetadata(((DatabaseItem)r).MessageID, ((DatabaseItem)r).UpdatedTimestamp))
                .AsEnumerable<SyncMetadata>();
            var ser = JsonConvert.SerializeObject(json);
            TestContext.WriteLine(ser);
            var des = JsonConvert.DeserializeObject<List<SyncMetadata>>(ser);
            foreach(var d in des)
            {
                TestContext.WriteLine(d);
            }
        }
    }

    public class SyncMetadata
    {
        public SyncMetadata(Guid messageID, DateTime updatedTimestamp)
        {
            MessageID = messageID;
            UpdatedTimestamp = updatedTimestamp;
        }

        public Guid MessageID { get; set; }
        public DateTime UpdatedTimestamp { get; set; }

        public override string ToString()
        {
            return $"{MessageID}/{UpdatedTimestamp}";
        }
    }


    class ControllerModelCreator<ModelType> where ModelType : new()
    {
        public ControllerModelCreator()
        {

        }

        public ModelType Create(object o)
        {
            return new ModelType();
        }
    }
}
