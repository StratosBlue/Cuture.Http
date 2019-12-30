using System.Linq;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cuture.Http.Test
{
    public abstract class TextResultRequestTest : WebServerHostTestBase
    {
        #region ����

        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <returns></returns>
        public abstract IHttpTurboRequest GetRequest();

        /// <summary>
        /// ��ȡ�����ܴ���
        /// </summary>
        /// <returns></returns>
        public abstract int GetRequestCount();

        /// <summary>
        /// ��ȡĿ��������
        /// </summary>
        /// <returns></returns>
        public abstract string GetTargetResult();

        [TestMethod]
        public async Task ParallelRequestTestAsync()
        {
            HttpDefaultSetting.DefaultConnectionLimit = 200;

            var count = GetRequestCount();
            var target = GetTargetResult();
            var all = Enumerable.Range(0, count);

            var tasks = all.Select(m => GetRequest().TryGetAsStringAsync()).ToArray();

            await Task.WhenAll(tasks);

            var fails = tasks.Where(m => !target.Equals(m.Result.Data)).ToArray();
            Assert.AreEqual(0, fails.Length);
        }

        #endregion ����
    }
}