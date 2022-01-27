/// < summary >
/// DataBase test to execute test cases propose on the test assesment
/// API https://reqres.in 
// </summary> 
using ApiTest.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGate.APi.Test
{
    [TestClass]
    public class DatabaseTest : AbstracTest
    {


        [TestMethod]
        [DeploymentItem(@"DataTest\CreateUser.csv")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "CreateUser.csv", "CreateUser#csv", DataAccessMethod.Sequential)]
        public async Task DB_VerifyCreateUSerAsync()
        {
            //Arrange
            var userData = TestContext.DataRow["UserToCreate"].ToString();
            var jtokenExpected = JToken.Parse(TestContext.DataRow["ExpectedData"].ToString());
            //Act
            string userId = await CreateUserAsync(userData).ConfigureAwait(false);
            //Assert
            string queryResult = DataManipulation.SelectUserByIDDatabase(userId);
            List<string> userCreate = queryResult.Split(';').ToList();

            Assert.AreEqual(userCreate[0], userId, "id attribute is not the expected");
            Assert.AreEqual(userCreate[1], jtokenExpected["name"].ToString(), "name attribute is not the expected");
            Assert.AreEqual(userCreate[2], jtokenExpected["salary"].ToString(), "salary attribute is not the expected");
        }



        [TestMethod]
        [DeploymentItem(@"DataTest\CreateUser.csv")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "CreateUser.csv", "CreateUser#csv", DataAccessMethod.Sequential)]
        public async Task DB_DeleteUserAsync()
        {
            //Arrange
            var userData = TestContext.DataRow["UserToCreate"].ToString();
            var jtokenExpected = JToken.Parse(TestContext.DataRow["ExpectedData"].ToString());
            //Act
            string userId = await CreateUserAsync(userData).ConfigureAwait(false);
            string queryResult = DataManipulation.SelectUserByIDDatabase(userId);
            List<string> userCreate = queryResult.Split(';').ToList();
            Assert.AreEqual(userCreate[0], userId, "id attribute is not the expected");
            DataManipulation.DeleteUserDatabaseById(userId);
            string deleteQueryResult = DataManipulation.SelectUserByIDDatabase(userId);

            //Assert
            Assert.IsTrue(String.IsNullOrEmpty(deleteQueryResult), "user was not deleted");
        }



        #region private Methods
        /// <summary>
        /// Create a User based on the API given on the  Test Assesment
        /// </summary>
        /// <param name="userData">User To create the user</param>
        /// <returns></returns>
        public async Task<string> CreateUserAsync(string userData)
        {
            var response = await ExecuteRequest("https://reqres.in/api/users/", "POST", userData).ConfigureAwait(false);
            var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var jtokenBody = JToken.Parse(responseBody);
            DataManipulation.CreateUserDatabase(jtokenBody["id"].ToString(), jtokenBody["name"].ToString(), jtokenBody["salary"].ToString());
            return jtokenBody["id"].ToString();
        }
        #endregion
    }
}
