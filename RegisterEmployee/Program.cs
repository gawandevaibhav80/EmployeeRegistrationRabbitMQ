

using System;
using RabbitMQ.Client;

namespace RabbitMQ.EmployeeRegistrationDemo
{
    class Program
    {
        private static ConnectionFactory _factory;
        private static IConnection _connection;
        private static IModel _model;

        private const string ExchangeName = "Registration_Processor";

        static void Main()
        {
            var emp1 = new Employee { Salary = 25000, EmpId = 1733, Name = "Vaibhav" };
            var emp2 = new Employee { Salary = 52000, EmpId = 1731, Name = "Moin" };
            var emp3 = new Employee { Salary = 20000, EmpId = 1572, Name = "Ashish" };
            
            CreateConnection();

            RegisterEmployee(emp1);
            RegisterEmployee(emp2);
            RegisterEmployee(emp2);

            Console.ReadLine();
        }

        private static void CreateConnection()
        {
            _factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest" };
            _connection = _factory.CreateConnection();
            _model = _connection.CreateModel();
            _model.ExchangeDeclare(ExchangeName, "fanout", false);
        }

        private static void RegisterEmployee(Employee emp)
        {           
            _model.BasicPublish(ExchangeName, "", null, emp.Serialize());
            Console.WriteLine("Employee Data Queued For Registration {0} - {1}", emp.EmpId, emp.Name);             
        }
    }
}
