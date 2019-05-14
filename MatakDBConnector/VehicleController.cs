using System;
using System.Collections.Generic;
using Npgsql;

namespace MatakDBConnector
{
    public class VehicleController : DbConnector
    {
        public List<Vehicle> getAllVehicles(out string errorMessage)
        {
            List<Vehicle> allVehicles = new List<Vehicle>();
            errorMessage = null;

            try
            {
                Connect();

                Command.CommandText = "SELECT * FROM vehicle";
                Reader = Command.ExecuteReader();

                while (Reader.Read())
                {
                    Vehicle vehicle = new Vehicle();
                    allVehicles.Add(vehicle.VehicleMaker(Reader));
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
                Disconnect();
            }
        }
    }
}