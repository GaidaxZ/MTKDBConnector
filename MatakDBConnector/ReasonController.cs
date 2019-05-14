using System;
using System.Collections.Generic;

namespace MatakDBConnector
{
    public class ReasonController : DbConnector
    {
        public List<Reason> GetAllReasons(out string errorMessage)
        {
            List<Reason> allReasons = new List<Reason>();
            errorMessage = null;

            try
            {
                Connect();

                Command.CommandText = "SELECT * FROM reason";
                Reader = Command.ExecuteReader();

                while (Reader.Read())
                {
                    Reason reason = new Reason();
                    allReasons.Add(reason.ReasonMaker(Reader));
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
                Disconnect();
            }
        }
    }
}