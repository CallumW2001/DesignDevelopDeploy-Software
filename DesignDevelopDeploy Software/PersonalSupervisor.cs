using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DesignDevelopDeploy_Software
{
    internal class PersonalSupervisor : Users
    {
        string name;
        List<string> students = new List<string>();



        public PersonalSupervisor(string name)
        {
            this.name = name;

            string[] lines = File.ReadAllLines("PS Students.txt");

            foreach (string line in lines)
            {

                int x = 0;
                string[] linesplit = line.Split(":");
                x++;
                if (linesplit[0] == name)
                {
                    students.Add(linesplit[1]);
                }
            }

        }

        public List<string> Students
        {
            get { return students; }            
        }
        

        public void CreateAccount(string username, string password)
        {
            StreamWriter writetext = File.AppendText("Login Details.txt");
            writetext.WriteLine(username + "=" + password + "=PERSONALSUPERVISOR=" + name);
            writetext.Close();
        }

        public void AddStudent(string login)
        {
            bool exists = false;
            foreach (string line in File.ReadLines("Login Details.txt"))
            {
                string[] linesplit = line.Split("=");
                if (linesplit[0] == login && linesplit[2] == "STUDENT")
                {
                    Console.WriteLine("Student added to Personal Supervisor");
                    exists = true;
                    Console.WriteLine(linesplit[3]);
                    students.Add(linesplit[3]);
                    StreamWriter writetext = File.AppendText("PS Students.txt");
                    writetext.WriteLine(name + ":" + linesplit[3]);

                    writetext.Close();
                    break;
                }
            }
            if(exists == false)
            {
                Console.WriteLine("No student with this name");
            }
        }      

        public void ReadStatus()
        {
            foreach (string line in File.ReadLines("Student Status.txt"))
            {
                string[] linesplit = line.Split(":");
                for (int i = 0; i < students.Count(); i++)
                {
                    if (linesplit[0] == students[i])
                    {
                        Console.WriteLine(line);
                        break;
                        
                    }
                }
            }
        }

        public void BookMeeting(string student, string time, DateOnly date)
        {
            DateTime dt = DateTime.Now;
            bool studentExists = false;

            for (int i = 0; i < students.Count(); i++)
            {
                if (student == students[i])
                {
                    studentExists = true;
                }
            }
            if(studentExists == true)
            {
                StreamWriter writetext = File.AppendText("Meeting Times.txt");
                writetext.WriteLine(name + " booked a meeting with " + student + " for: " + time + " on " + date );
                writetext.Close();
                Console.WriteLine("Meeting created with " + student + " for: " + time + " on " + date);
            }
            else
            {
                Console.WriteLine("Student doesn't exist or is not assigned to " + name);
            }
            
        }

        public void ViewMeeting()
        {
            foreach (string line in File.ReadLines("Meeting Times.txt"))
            {
                string[] linesplit = line.Split(" ");
                for (int i = 0; i < students.Count(); i++)
                {
                    if (linesplit[0] == students[i] || linesplit[0] == name)
                    {
                        Console.WriteLine(line);
                        break;

                    }
                }
            }
        }
    }
}
