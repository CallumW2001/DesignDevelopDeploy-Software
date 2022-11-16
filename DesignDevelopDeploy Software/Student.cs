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
            writetext.WriteLine(status);
            writetext.Close();
        }
        public void BookMeeting(string time, DateOnly dt)
        {
            time = time;

            StreamWriter writetext = File.AppendText("Meeting Times.txt");
            writetext.WriteLine(name + " booked a meeting for: " + time + " on " + dt);
            writetext.Close();
        }
    }
}
