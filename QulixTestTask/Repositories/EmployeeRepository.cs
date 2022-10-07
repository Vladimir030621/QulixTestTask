using Microsoft.Extensions.Configuration;
using QulixTestTask.Helpers;
using QulixTestTask.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace QulixTestTask.Repositories
{
    public interface IEmployeeRepository
    {
        public List<Employee> GetEmployees();
        Employee GetEmployeeById(int employeeId);
        void CreateEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(int employeeId);
    }


    public class EmployeeRepository : IEmployeeRepository
    {
        private string connection = string.Empty;
        public EmployeeRepository()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).
                AddJsonFile("appsettings.json").Build();

            connection = builder.GetSection("ConnectionStrings:DefaultConnection").Value;
        }

        public List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            using(var conn = new SqlConnection(connection))
            {
                using (var cmd = new SqlCommand("employeemanager_getemployees", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();

                    IDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        employees.Add(new Employee()
                        {
                            Id = int.Parse(reader["Id"].ToString()),
                            FirstName = reader["firstname"].ToString(),
                            SecondName = reader["secondname"].ToString(),
                            StartDate = DateTime.Parse(reader["startdate"].ToString()),
                            PositionTypeId = int.Parse(reader["positiontypeid"].ToString()),
                            CompanyId = string.IsNullOrEmpty(reader["companyid"].ToString()) ? 0 : int.Parse(reader["companyid"].ToString()),
                            CompanyName = reader["companyname"]?.ToString() ?? ""
                        });
                    }
                }
            }

            return employees;
        }

        public Employee GetEmployeeById(int employeeId)
        {
            Employee employee = new Employee();
            using (var conn = new SqlConnection(connection))
            {
                using (var cmd = new SqlCommand("employeemanager_getemployeebyid", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    cmd.Parameters.Add("@EmployeeId", SqlDbType.VarChar).Value = employeeId;

                    IDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        employee.Id = int.Parse(reader["Id"].ToString());
                        employee.FirstName = reader["firstname"].ToString();
                        employee.SecondName = reader["secondname"].ToString();
                        employee.StartDate = DateTime.Parse(reader["startdate"].ToString());
                        employee.PositionTypeId = int.Parse(reader["positiontypeid"].ToString());
                        employee.CompanyId = int.Parse(reader["companyid"].ToString());
                        employee.CompanyName = reader["companyname"].ToString();
                    }
                }
            }

            return employee;
        }

        public void CreateEmployee(Employee employee)
        {
            using (var conn = new SqlConnection(connection))
            {
                using (var cmd = new SqlCommand("employeemanager_createemployee", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    cmd.Parameters.Add("@Firstname", SqlDbType.VarChar).Value = employee.FirstName;
                    cmd.Parameters.Add("@Secondname", SqlDbType.VarChar).Value = employee.SecondName;
                    cmd.Parameters.Add("@Startdate", SqlDbType.DateTime).Value = employee.StartDate;
                    cmd.Parameters.Add("@Positiontypeid", SqlDbType.Int).Value = employee.PositionTypeId;
                    cmd.Parameters.Add("@Companyid", SqlDbType.Int).Value = employee.CompanyId;

                    IDataReader reader = cmd.ExecuteReader();
                }
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            using (var conn = new SqlConnection(connection))
            {
                using (var cmd = new SqlCommand("employeemanager_updateemployee", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    cmd.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = employee.Id;
                    cmd.Parameters.Add("@Firstname", SqlDbType.VarChar).Value = employee.FirstName;
                    cmd.Parameters.Add("@Secondname", SqlDbType.VarChar).Value = employee.SecondName;
                    cmd.Parameters.Add("@Startdate", SqlDbType.DateTime).Value = employee.StartDate;
                    cmd.Parameters.Add("@Positiontypeid", SqlDbType.Int).Value = employee.PositionTypeId;
                    cmd.Parameters.Add("@Companyid", SqlDbType.Int).Value = employee.CompanyId;

                    IDataReader reader = cmd.ExecuteReader();
                }
            }
        }

        public void DeleteEmployee(int employeeId)
        {
            using (var conn = new SqlConnection(connection))
            {
                using (var cmd = new SqlCommand("employeemanager_deleteemployee", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    cmd.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = employeeId;

                    IDataReader reader = cmd.ExecuteReader();
                }
            }
        }
    }
}
