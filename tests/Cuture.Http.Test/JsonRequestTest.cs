﻿using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Cuture.Http.Test.Server;
using Cuture.Http.Test.Server.Entity;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

namespace Cuture.Http.Test
{
    [TestClass]
    public class JsonRequestTest : WebServerHostTestBase
    {
        #region 方法

        [TestMethod]
        public async Task ContentTypeChangeTestAsync()
        {
            var (user, json) = NewUserWithJson();

            var contentType = "text/json; charset=utf-8; test=true";
            await ParallelRequestAsync(10_000,
                                       () => GetRequest().WithContent(new JsonContent(json, contentType)).TryGetAsStringAsync(),
                                       result =>
                                       {
                                           Assert.AreEqual(contentType, result.ResponseMessage.Headers.GetValues("R-Content-Type").First().ToString());
                                           Assert.AreEqual(json, result.Data);
                                       });
        }

        [TestMethod]
        public async Task FromJsonTestAsync()
        {
            var (user, json) = NewUserWithJson();

            await ParallelRequestAsync(10_000,
                                       () => GetRequest().WithJsonContent(json).TryGetAsStringAsync(),
                                       result => Assert.AreEqual(json, result.Data));
        }

        [TestMethod]
        public async Task FromObjectTestAsync()
        {
            var (user, json) = NewUserWithJson();

            await ParallelRequestAsync(10_000,
                                       () => GetRequest().WithJsonContent(user).TryGetAsStringAsync(),
                                       result => Assert.AreEqual(json, result.Data));
        }

        public IHttpTurboRequest GetRequest() => $"{TestServer.TestHost}/api/user/update".ToHttpRequest().UsePost();

        private (UserInfo user, string json) NewUserWithJson()
        {
            var user = new UserInfo()
            {
                Age = 10,
                Name = "TestUser",
            };

            var json = JsonConvert.SerializeObject(user);

            Debug.WriteLine($"New UserInfo: {json}");

            return (user, json);
        }

        #endregion 方法
    }
}