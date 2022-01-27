/// < summary >
/// API test to execute test cases propose on the test assesment
/// API https://reqres.in 
// </summary> 
using ApiTest.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;



namespace AppGate.APi.Test
{
    [TestClass]
    public class Test : AbstracTest
    {

        [TestMethod]
        [DeploymentItem(@"DataTest\CreateUser.csv")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "CreateUser.csv", "CreateUser#csv", DataAccessMethod.Sequential)]
        public async Task API_VerifyCreateUSerAsync()
        {
            //Arrange
            var userData = TestContext.DataRow["UserToCreate"].ToString();
            var jtokenExpected = JToken.Parse(TestContext.DataRow["ExpectedData"].ToString());
            //Act
            var response = await ExecuteRequest("https://reqres.in/api/users/", "POST", userData).ConfigureAwait(false);

            //Assert
            Assert.AreEqual(TestContext.DataRow["ExpectedStatusCode"].ToString(), ((int)response.StatusCode).ToString(), $"Status Code is not the Expected, Actual:{(int)response.StatusCode}, Expected: {TestContext.DataRow["ExpectedStatusCode"]}");

            var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var jtokenBody = JToken.Parse(responseBody);

            Assert.AreEqual(jtokenBody["name"].ToString(), jtokenExpected["name"].ToString(), "name attribute is not the expected");
            Assert.AreEqual(jtokenBody["salary"].ToString(), jtokenExpected["salary"].ToString(), "name attribute is not the expected");

        }

        [TestMethod]
        [DeploymentItem(@"DataTest\DeleteUser.csv")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "DeleteUser.csv", "DeleteUser#csv", DataAccessMethod.Sequential)]
        public async Task API_DeleteUserAsync()
        {
            //Arrange
            var inputUrlParameters = TestContext.DataRow["UserId"].ToString();
            //Act
            var response = await ExecuteRequest("https://reqres.in/api/users/", "DELETE", inputParameters: inputUrlParameters).ConfigureAwait(false);

            //Assert
            Assert.AreEqual(TestContext.DataRow["ExpectedStatusCode"].ToString(), ((int)response.StatusCode).ToString(), $"Status Code is not the Expected, Actual:{(int)response.StatusCode}, Expected: {TestContext.DataRow["ExpectedStatusCode"]}");
        }



    }
}
