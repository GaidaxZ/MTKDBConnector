using System;
using System.Collections.Generic;
using Npgsql;

namespace MatakDBConnector
{
    public class OrganizationController
    {
        public List<Organization> getAllOrganizations(out string errorMessage)
        {
            List<Organization> allOrganizations = new List<Organization>();
            errorMessage = null;
            
            try
            {
                DbConnector database = new DbConnector();
                database.Connect();
                
                var command = new NpgsqlCommand();
                command.Connection = database.Connection;
                command.CommandText = "SELECT * FROM organization";

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Organization organization = new Organization();

                    organization.OrgId = reader.GetInt32(0);
                    organization.Name = reader.GetString(1);
                    organization.MainUserId = reader.GetInt32(2);
                    organization.CountryId = reader.GetInt32(3);
                    organization.AddressId = reader.GetInt32(4);
                    organization.FaxId = reader.GetInt32(5);
                    organization.PhoneId = reader.GetInt32(8);
                    
                    allOrganizations.Add(organization);
                }
                database.Disconnect();
                return allOrganizations;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                errorMessage = e.ToString();
                throw;
            }
        }
    }
}