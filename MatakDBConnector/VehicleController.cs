using System;
using System.Collections.Generic;
using Npgsql;

namespace MatakDBConnector
{
    public class VehicleController
    {
        public List<Vehicle> getAllVehicles(out string errorMessage)
        {
            List<Vehicle> allVehicles = new List<Vehicle>();
            errorMessage = null;
            
            try
            {
                DbConnector database = new DbConnector();
                database.Connect();
                
                var command = new NpgsqlCommand();
                command.Connection = database.Connection;
                command.CommandText = "SELECT * FROM vehicle";

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Vehicle vehicle = new Vehicle();

                    vehicle.VehicleId = reader.GetInt32(0);
                    vehicle.PlateNumber = reader.GetString(1);
                    vehicle.OrgId = reader.GetInt32(2);
                    vehicle.PhotoId = reader.GetInt32(3);
                    vehicle.TypeId = reader.GetInt32(4);
                    vehicle.Model = reader.GetString(5);
                    vehicle.Color = reader.GetString(6);
                    vehicle.Manufacturer = reader.GetString(7);
                    
                    allVehicles.Add(vehicle);
                }
                database.Disconnect();
                return allVehicles;
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