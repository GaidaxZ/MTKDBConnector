using System;
using System.Collections.Generic;
using Npgsql;

namespace MatakDBConnector
{
    public class VehicleModel : Vehicle
    {
        public List<Vehicle> getAllVehicles(out string errorMessage)
        {
            List<Vehicle> allVehicles = new List<Vehicle>();
            errorMessage = null;

            try
            {
                DbConnector.Connect();

                DbConnector.Command.CommandText = "SELECT * FROM vehicle";
                DbConnector.Reader = DbConnector.Command.ExecuteReader();

                while (DbConnector.Reader.Read())
                {
                    Vehicle vehicle = new Vehicle();
                    allVehicles.Add(vehicle.VehicleMaker(DbConnector.Reader));
                }

                return allVehicles;
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