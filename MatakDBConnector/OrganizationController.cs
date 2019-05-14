using System;
using System.Collections.Generic;
using Npgsql;

namespace MatakDBConnector
{
    public class OrganizationController : DbConnector
    {
        public List<Organization> getAllOrganizations(out string errorMessage)
        {
            List<Organization> allOrganizations = new List<Organization>();
            errorMessage = null;

            try
            {
                Connect();
                
                Command.CommandText = "SELECT * FROM organization";
                Reader = Command.ExecuteReader();

                while (Reader.Read())
                {
                    Organization organization = new Organization();
                    allOrganizations.Add(organization.OrganizationMaker(Reader));
                }

                return allOrganizations;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                errorMessage = e.ToString();
                throw;
            }
            finally
            {
                Disconnect();
            }
        }
        
        public Organization getOrganizationById(int org_id, out string errorMessage)
        {
            List<Organization> allOrganizations = new List<Organization>();
            errorMessage = null;

            try
            {
                Connect();
                
                Command.CommandText = "SELECT * FROM organization WHERE org_id = (@p)";
                Command.Parameters.AddWithValue("p", org_id);
                Reader = Command.ExecuteReader();

                while (Reader.Read())
                {
                    Organization organization = new Organization();
                    organization.OrganizationMaker(Reader);
                    return organization;
                }

                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                errorMessage = e.ToString();
                throw;
            }
            finally
            {
                Disconnect();
            }
        }
    }
}