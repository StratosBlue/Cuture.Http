using Cuture.Http.Test.Server;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cuture.Http.Test
{
    [TestClass]
    public class TextRequestTest : TextResultRequestTest
    {
        #region ����

        public override IHttpTurboRequest GetRequest() => TestServer.TestHost.ToHttpRequest();

        public override int GetRequestCount() => 10_000;

        public override string GetTargetResult() => Resource.Index;

        #endregion ����
    }
}