// See https://aka.ms/new-console-template for more information
using DesignDevelopDeploy_Software;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;


string username = "";
string password = "";
string fileName = "Student Status.txt";
string fileName2 = "Login Details.txt";
string fileName3 = "PS Students.txt";
string fileName4 = "Meeting Times.txt";
CreateFile(fileName);
CreateFile(fileName2);
CreateFile(fileName3);
CreateFile(fileName4);
bool Login = false;
bool studentlogin = false;
bool personalsupervisor = false;
bool seniortutor = false;
Student student = new Student("");
PersonalSupervisor ps = new PersonalSupervisor("");
SeniorTutor st = new SeniorTutor();
int option;
int option2;



while (true)
{



    while (true)
    {
        Console.WriteLine("1. Login");
        Console.WriteLine("2. Create Account");
        Console.WriteLine("3. Exit");
        try
        {
            option = Convert.ToInt32(Console.ReadLine());
            break;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    if (option == 2)
    {
        Console.WriteLine("Which type of account would you like to create?");
        Console.WriteLine("1. Student");
        Console.WriteLine("2. Personal Supervisor");
        Console.WriteLine("3. Senior Tutor");

        option2 = Convert.ToInt32(Console.ReadLine());


        Console.WriteLine("Enter Username");
        string newUsername = Console.ReadLine();
        Console.WriteLine("Enter Password");
        string newPassword = Console.ReadLine();



        switch (option2)
        {
            case 1:                  
                Console.WriteLine("Enter your name: ");            
                string name = Console.ReadLine();

                student = new Student(name);
                student.CreateAccount(newUsername, newPassword, name);
                break;

            case 2:
                Console.WriteLine("Enter your name: ");
                name = Console.ReadLine();
                ps = new PersonalSupervisor(name);
                ps.CreateAccount(newUsername, newPassword);               
                break;
            case 3:
                st.CreateAccount(newUsername, newPassword);
                break;

        }
        Console.WriteLine("Account successfully created.");

    }

    else if (option == 1)
    {
        while (Login == false)
        {

                Console.WriteLine("Enter Username");
                username = Console.ReadLine().ToUpper();
                Console.WriteLine("Enter Password");
                password = Console.ReadLine().ToUpper();


            string file = File.ReadAllText("Login Details.txt");
            string[] fileSplit1 = file.Split("\r\n");

            for (int i = 0; i < fileSplit1.Length; i++)
            {
                string[] fileSplit2 = fileSplit1[i].Split("=");

                for (int y = 0; y < fileSplit2.Length; y++)
                {
                    if (username == fileSplit2[y].ToUpper() && password == fileSplit2[y + 1].ToUpper())
                    {
                        if (fileSplit2[y + 2].ToUpper() == "STUDENT")
                        {
                            studentlogin = true;
                            personalsupervisor = false;
                            seniortutor = false;
                            string studentname = (fileSplit2[y + 3]);
                            student = new Student(studentname);
                        }
                        else if (fileSplit2[y + 2].ToUpper() == "SENIORTUTOR")
                        {
                            studentlogin = false;
                            personalsupervisor = false;
                            seniortutor = true;
                            st = new SeniorTutor();
                        }
                        else if (fileSplit2[y + 2].ToUpper() == "PERSONALSUPERVISOR")
                        {
                            studentlogin = false;
                            personalsupervisor = true;
                            seniortutor = false;
                            string psname = (fileSplit2[y + 3]);
                            ps = new PersonalSupervisor(psname);
                        }
                        Login = true;
                        Console.WriteLine("Login Accepted");
                        break;
                    }
                }
                if (Login == true)
                {                  
                    break;
                }
            }

        }
       
        Login = false;
    }

    else if (option == 3)
    {
        Console.WriteLine("Program Quit.");
        break;
    }


    while (true)
    {


        if (studentlogin == true)
        {
            int input;
            while (true)
            {
                Console.WriteLine("1. Book a meeting with personal supervisor");
                Console.WriteLine("2. Report status");
                Console.WriteLine("3. Logout");
                try
                {
                    input = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            if (input == 1)
            {
                Console.WriteLine("Enter Date to book meeting: (DD/MM/YY)");
                string line = Console.ReadLine();
                DateOnly dt;
                while (!DateOnly.TryParseExact(line, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dt))
                {
                    Console.WriteLine("Invalid date, please retry");
                    line = Console.ReadLine();
                }

                Console.WriteLine("Enter time to book meeting: (HH:MM)");
                string[] ar;
                string s = Console.ReadLine();
                ar = s.Split('-');
                DateTime dateTime = DateTime.Parse(ar[0]);

                student.BookMeeting(dateTime.ToString("HH:mm"), dt);
            }
            else if (input == 2)
            {
                Console.WriteLine("Write your status: ");
                string status = Console.ReadLine();

                // convert everything to TimeSpan
                TimeSpan start = new TimeSpan(07, 0, 0);
                TimeSpan end = new TimeSpan(18, 0, 0);
                TimeSpan now = DateTime.Now.TimeOfDay;
                // see if start comes before end

                if (now < start || now > end)
                {
                    Console.WriteLine("Can not report status at time: " + now + ". Must be between 07:00 & 18:00");
                }
                else
                {
                    student.ReportStatus(student.Name + ": " + status);
                    Console.WriteLine("Report successful.");
                }


            }
            else if(input == 3)
            {
                break;
            }
        }
        else if (personalsupervisor == true)
        {
            int input;
            while (true)
            {
                Console.WriteLine("1. Book a meeting with student");
                Console.WriteLine("2. View status of all their students");
                Console.WriteLine("3. Add students to monitor");
                Console.WriteLine("4. Remove students to monitor");
                Console.WriteLine("5. View meeting times of your students.");
                Console.WriteLine("6. Logout");
                try
                {
                    input = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            if (input == 1)
            {
                Console.WriteLine("Enter name of student to book a meeting with: ");
                string stu = Console.ReadLine();
                Console.WriteLine("Enter Date to book meeting: (DD/MM/YY)");

                string line = Console.ReadLine();
                DateOnly dt;
                while (!DateOnly.TryParseExact(line, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dt))
                {
                    Console.WriteLine("Invalid date, please retry");
                    line = Console.ReadLine();
                }

                Console.WriteLine("Enter time to book meeting: (HH:MM)");
                string[] ar;
                string s = Console.ReadLine();
                ar = s.Split('-');
                DateTime dateTime = DateTime.Parse(ar[0]);
                ps.BookMeeting(stu, dateTime.ToString("HH:mm"), dt);
            }
            else if (input == 2)
            {
                ps.ReadStatus();
            }
            else if (input == 3)
            {
                Console.WriteLine("Enter name of student to add to personal supervisor management: ");
                string addstu = Console.ReadLine();
                ps.AddStudent(addstu);
            }
            else if (input == 4)
            {
                Console.WriteLine("Enter student name to remove from PS: ");
                string stuname = Console.ReadLine();
                ps.DeleteStudent(stuname);
            }
            else if (input == 5)
            {
                ps.ViewMeeting();
            }
            else if(input == 6)
            {
                break;
            }
        }
        else if (seniortutor == true)
        {
            int input;
            while (true)
            {
                Console.WriteLine("1. View PS and student interactions");
                Console.WriteLine("2. View status of all students");
                Console.WriteLine("3. Logout");
                try
                {
                    input = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            if (input == 1)
            {
                st.ViewInteractions();
            }
            else if (input == 2)
            {
                st.ReturnStatus();
            }
            else if(input == 3)
            {
                break;
            }    
        }
    }


}


void CreateFile(string fileName)
{
    if (!File.Exists(fileName))
    {
        FileStream fs = File.Create(fileName);
        fs.Close();
    }    //Create new file
    
    

}   

