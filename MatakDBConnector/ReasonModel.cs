using System;
using System.Collections.Generic;

namespace MatakDBConnector
{
    public class ReasonModel : Reason
    {
        public List<Reason> GetAllReasons(out string errorMessage)
        {
            List<Reason> allReasons = new List<Reason>();
            errorMessage = null;

            try
            {
                DbConnector.Connect();

                DbConnector.Command.CommandText = "SELECT * FROM reason";
                DbConnector.Reader = DbConnector.Command.ExecuteReader();

                while (DbConnector.Reader.Read())
                {
                    Reason reason = new Reason();
                    allReasons.Add(reason.ReasonMaker(DbConnector.Reader));
                }

                return allReasons;
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