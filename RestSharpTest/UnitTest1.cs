using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;

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
        [TestMethod]
        public void givenEmployee_ShouldReturnEmployee()
        {
            RestRequest request = new RestRequest("/Employee",Method.Post);
            JObject jObjectBody = new JObject();
            jObjectBody.Add("name"," Umesh Kumar");
            jObjectBody.Add("Salary"," 60000");
            request.AddParameter("application/json", jObjectBody, ParameterType.RequestBody);
            RestResponse response = client.Execute(request);

            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
            Employee responseEmployee = JsonConvert.DeserializeObject<Employee>(response.Content);
            Assert.AreEqual("Umesh Kumar", responseEmployee.Name);
            Assert.AreEqual("60000",responseEmployee.Salary);
        }
    }
}