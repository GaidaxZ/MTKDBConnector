using System;
using System.Collections.Generic;
using Npgsql;

namespace MatakDBConnector
{
    public class OrganizationModel : Organization
    {
        public List<Organization> getAllOrganizations(out string errorMessage)
        {
            List<Organization> allOrganizations = new List<Organization>();
            errorMessage = null;

            try
            {
                DbConnector.Connect();
                
                DbConnector.Command.CommandText = "SELECT * FROM organization";
                DbConnector.Reader = DbConnector.Command.ExecuteReader();

                while (DbConnector.Reader.Read())
                {
                    Organization organization = new Organization();
                    allOrganizations.Add(organization.OrganizationMaker(DbConnector.Reader));
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
                DbConnector.Disconnect();
            }
        }
        
        public Organization getOrganizationById(int orgid, out string errorMessage)
        {
            errorMessage = null;

            try
            {
                DbConnector.Connect();
                
                DbConnector.Command.CommandText = "SELECT * FROM organization WHERE org_id = (@p)";
                DbConnector.Command.Parameters.AddWithValue("p", orgid);
                DbConnector.Reader = DbConnector.Command.ExecuteReader();

                while (DbConnector.Reader.Read())
                {
                    return OrganizationMaker(DbConnector.Reader);;
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
                DbConnector.Disconnect();
            }
        }
    }
}