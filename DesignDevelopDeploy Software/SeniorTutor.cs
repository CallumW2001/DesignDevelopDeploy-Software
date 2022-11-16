using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignDevelopDeploy_Software
{
    internal class SeniorTutor : Users
    {
        public void CreateAccount(string username, string password)
        {
            StreamWriter writetext = File.AppendText("Login Details.txt");
            writetext.WriteLine(username + "=" + password + "=SENIORTUTOR");
            writetext.Close();
        }

        public void ReturnStatus()
        {
            foreach (string line in File.ReadLines("Student Status.txt"))
            {
                Console.WriteLine(line);
            }
        }
    }
}
