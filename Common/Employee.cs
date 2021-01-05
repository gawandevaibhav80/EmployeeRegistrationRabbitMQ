using System;

namespace RabbitMQ.EmployeeRegistrationDemo
{
    [Serializable] 
    public class Employee
    {
        public int Salary;
        public int EmpId;        
        public string Name;
    }
}
