﻿using System.IO;
using System.Linq;
using CarDeliveryNetwork.Api.ClientProxy;
using CarDeliveryNetwork.Api.Data;
using Moq;
using NUnit.Framework;

namespace CdnLink.Tests
{
    [TestFixture]
    public class TestSend
    {
        [TestFixtureSetUp]
        public void Init()
        {
            var db = new CdnLinkDataContext(Settings.GetConnectionString());

            db.CdnSendVehicles.DeleteAllOnSubmit(db.CdnSendVehicles);
            db.SubmitChanges();

            db.CdnSends.DeleteAllOnSubmit(db.CdnSends);
            db.SubmitChanges();

            db.CdnSendLoads.DeleteAllOnSubmit(db.CdnSendLoads);
            db.SubmitChanges();
        }

        [Test]
        public void Send()
        {
            var connectionString = Settings.GetConnectionString();

            var db = new CdnLinkDataContext(connectionString);
            db.ExecuteCommand(File.ReadAllText("Db\\testdata_send.sql"));
            var sendCount = db.CdnSends.Count();
            var link = new CdnLink(connectionString, GetMockApi(), null);
            Assert.AreEqual(sendCount, link.Send());
            Assert.AreEqual(0, link.Send());

            foreach (var send in db.CdnSends)
            {
                Assert.AreEqual((int)CdnSend.SendStatus.Sent, send.Status);
            }
        }

        private ICdnApi GetMockApi()
        {
            var mock = new Mock<ICdnApi>();

            mock.Setup(s => s.CreateJob(It.IsAny<Job>()))
                .Returns((Job j) => j);

            return mock.Object;
        }
    }
}