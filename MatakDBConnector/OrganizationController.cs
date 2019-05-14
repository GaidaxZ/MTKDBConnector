using System;
using System.Collections.Generic;
using Npgsql;

namespace MatakDBConnector
{
    public class OrganizationController : Organization
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
                    allOrganizations.Add(OrganizationMaker(Reader));
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
    }
}