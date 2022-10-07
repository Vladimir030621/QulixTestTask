using Microsoft.Extensions.Configuration;
using QulixTestTask.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace QulixTestTask.Repositories
{

    public interface ICompanyRepository
    {
        List<Company> GetCompanies();
        Company GetCompanyById(int companyId);
        void CreateCompany(Company company);
        void UpdateCompany(Company company);
        void DeleteCompany(int companyId);
    }
    public class CompanyRepository : ICompanyRepository
    {

        private string connection = string.Empty;
        public CompanyRepository()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).
                AddJsonFile("appsettings.json").Build();

            connection = builder.GetSection("ConnectionStrings:DefaultConnection").Value;
        }

        public List<Company> GetCompanies()
        {
            List<Company> companies = new List<Company>();
            using (var conn = new SqlConnection(connection))
            {
                using (var cmd = new SqlCommand("companymanager_getcompanies", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();

                    IDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        companies.Add(new Company()
                        {
                            Id = int.Parse(reader["Id"].ToString()),
                            CompanyName = reader["companyname"].ToString(),
                            Size = int.Parse(reader["size"].ToString()),
                            BusinessType = reader["businesstype"].ToString()
                        });
                    }
                }
            }

            return companies;
        }

        public Company GetCompanyById(int companyId)
        {
            Company company = new Company();
            using (var conn = new SqlConnection(connection))
            {
                using (var cmd = new SqlCommand("companymanager_getcompanybyid", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = companyId;

                    IDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        company.Id = int.Parse(reader["Id"].ToString());
                        company.CompanyName = reader["companyname"].ToString();
                        company.Size = int.Parse(reader["size"].ToString());
                        company.BusinessType = reader["businesstype"].ToString();
                    }
                }
            }

            return company;
        }

        public void CreateCompany(Company company)
        {
            using (var conn = new SqlConnection(connection))
            {
                using (var cmd = new SqlCommand("companymanager_createcompany", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    cmd.Parameters.Add("@Companyname", SqlDbType.VarChar).Value = company.CompanyName;
                    cmd.Parameters.Add("@Size", SqlDbType.Int).Value = company.Size;
                    cmd.Parameters.Add("@Businesstype", SqlDbType.VarChar).Value = company.BusinessType;

                    IDataReader reader = cmd.ExecuteReader();
                }
            }
        }


        public void UpdateCompany(Company company)
        {
            using (var conn = new SqlConnection(connection))
            {
                using (var cmd = new SqlCommand("companymanager_updatecompany", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = company.Id;
                    cmd.Parameters.Add("@Companyname", SqlDbType.VarChar).Value = company.CompanyName;
                    cmd.Parameters.Add("@Size", SqlDbType.Int).Value = company.Size;
                    cmd.Parameters.Add("@Businesstype", SqlDbType.VarChar).Value = company.BusinessType;

                    IDataReader reader = cmd.ExecuteReader();
                }
            }
        }

        public void DeleteCompany(int companyId)
        {
            using (var conn = new SqlConnection(connection))
            {
                using (var cmd = new SqlCommand("companymanager_deletecompany", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = companyId;

                    IDataReader reader = cmd.ExecuteReader();
                }
            }
        }
    }
}
