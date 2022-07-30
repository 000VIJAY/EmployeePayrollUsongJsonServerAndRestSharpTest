using Newtonsoft.Json;
using RestSharp;

namespace RestSharpTest
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Salary { get; set; }
    }
    [TestClass]
    public class RestSharpTestCase
    {
        RestClient client;
        [TestInitialize]
        public void Setup()
        {
            client = new RestClient("http://localhost:3000");
        }
        public RestResponse getEmployeeList()
        {
            RestRequest request = new RestRequest("/Employee", Method.Get);
            RestResponse response = client.Get(request);
            return response;
        }
        [TestMethod]
        public void callingGetApi_ReturnEmployeeList()
        {
            RestResponse response = getEmployeeList();
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            List<Employee> employees = JsonConvert.DeserializeObject<List<Employee>>(response.Content);
            Assert.AreEqual(employees.Count, 11);
            foreach (Employee employee in employees)
            {
                Console.WriteLine("id: " + employee.Id + " Name: " + employee.Name + " Salary: " + employee.Salary);
            }
        }
    }
}