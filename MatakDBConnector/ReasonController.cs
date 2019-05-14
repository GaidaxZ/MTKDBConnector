using System;
using System.Collections.Generic;

namespace MatakDBConnector
{
    public class ReasonController : Reason
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
                    allReasons.Add(ReasonMaker(Reader));
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