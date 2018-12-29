﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Audit.Core;
using Audit.Mvc;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Audit.Integration.AspNetCore
{
    public class MvcTests
    {
        private readonly int _port;
        public MvcTests(int port)
        {
            _port = port;
        }

        public async Task Test_Mvc_HappyPath_Async()
        {
            var insertEvs = new List<AuditAction>();
            var replaceEvs = new List<AuditAction>();
            Audit.Core.Configuration.Setup()
                .UseDynamicAsyncProvider(_ => _.OnInsert(async ev =>
                    {
                        await Task.Delay(1);
                        insertEvs.Add(JsonConvert.DeserializeObject<AuditAction>(JsonConvert.SerializeObject(ev.GetMvcAuditAction())));
                        return Guid.NewGuid();
                    })
                    .OnReplace(async (id, ev) =>
                    {
                        await Task.Delay(1);
                        replaceEvs.Add(JsonConvert.DeserializeObject<AuditAction>(JsonConvert.SerializeObject(ev.GetMvcAuditAction())));
                    }))
                .WithCreationPolicy(EventCreationPolicy.InsertOnStartReplaceOnEnd);

            var c = new HttpClient();
            var s = await c.GetStringAsync($"http://localhost:{_port}/test/mytitle");
            Assert.IsTrue(s.Contains("<h2>mytitle</h2>"));
            Assert.AreEqual(1, insertEvs.Count);
            Assert.AreEqual(1, replaceEvs.Count);
            Assert.AreEqual(null, insertEvs[0].Model);
            Assert.AreEqual(@"{""Title"":""mytitle""}", replaceEvs[0].Model.ToString().Replace(" ", "").Replace("\r", "").Replace("\n", ""));
        }

        public async Task Test_Mvc_Exception_Async()
        {
            var insertEvs = new List<AuditAction>();
            var replaceEvs = new List<AuditAction>();
            Audit.Core.Configuration.Setup()
                .UseDynamicAsyncProvider(_ => _.OnInsert(async ev =>
                    {
                        await Task.Delay(1);
                        insertEvs.Add(JsonConvert.DeserializeObject<AuditAction>(JsonConvert.SerializeObject(ev.GetMvcAuditAction())));
                        return Guid.NewGuid();
                    })
                    .OnReplace(async (id, ev) =>
                    {
                        await Task.Delay(1);
                        replaceEvs.Add(JsonConvert.DeserializeObject<AuditAction>(JsonConvert.SerializeObject(ev.GetMvcAuditAction())));
                    }))
                .WithCreationPolicy(EventCreationPolicy.InsertOnStartReplaceOnEnd);

            var c = new HttpClient();
            string s = null;
            try
            {
                s = await c.GetStringAsync($"http://localhost:{_port}/test/666");
            }
            catch
            {
            }
            finally
            {
            }
            Assert.AreEqual(null, s);
            Assert.AreEqual(1, insertEvs.Count);
            Assert.AreEqual(1, replaceEvs.Count);
            Assert.IsTrue(replaceEvs[0].Exception.Contains("THIS IS A TEST EXCEPTION"), "returned exception: " + replaceEvs[0].Exception);
        }
    }
}