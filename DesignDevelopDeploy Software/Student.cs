using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignDevelopDeploy_Software
{
    internal class Student : Users
    {

        private string name;

        public Student(string _Name)
        {
            name = _Name;
        }
        public string Name
        {
            get {  return name; }
            set { name = value; }
        }

        public void CreateAccount(string username, string password, string name)
        {
            StreamWriter writetext = File.AppendText("Login Details.txt");
            writetext.WriteLine(username + "=" + password + "=STUDENT=" + name);
            writetext.Close();
        }
        public void ReportStatus(string status)
        {
            StreamWriter writetext = File.AppendText("Student Status.txt");
            writetext.WriteLine(status + " @ " + DateTime.Now);
            writetext.Close();
        }
        public void BookMeeting(string time, DateOnly dt)
        {
            string[] linesplit = { };
            bool psfound = false;
            StreamWriter writetext = File.AppendText("Meeting Times.txt");
            foreach (string line in File.ReadLines("PS Students.txt"))
            {
                linesplit = line.Split(":");
                if (linesplit[1] == name)
                {
                    psfound = true;
                    break;                  
                }
            }
            if(psfound == true)
            {
                writetext.WriteLine(name + " booked a meeting for: " + time + " on " + dt + " with " + linesplit[0]);
            }
            else
            {
                Console.WriteLine("Student is not assigned to a Personal Supervisor, nobody to book a meeting with.");
            }

           
            writetext.Close();
        }
    }
}
