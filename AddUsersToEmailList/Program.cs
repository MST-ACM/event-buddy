using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AddUsersToEmailList
{
    class Program
    {
        public enum Groups
        {
            Events,
            Careers,
            Game,
            Hack,
            Security,
            Comp,
            Web,
            ACMW,
            Data
        }

        public struct Student
        {
            public string FirstName;
            public string LastName;
            public string Email;
            public List<Groups> InterestedGroups;
        }

        static void Main(string[] args)
        {
            string file = "";
            while (file == "")
            {
                try
                {
                    Console.WriteLine("Enter path to csv: ");
                    string path = Console.ReadLine();
                    file = File.ReadAllText(path);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Couldn't read file: " + ex.Message);
                }
            }
            List<string> lines = file.Split('\n').ToList();
            List<Student> studentsToAdd = new List<Student>();
            for (int i = 0; i < lines.Count; i++)
            {
                try
                {
                    Student s = new Student();
                    string[] splitLine = lines[i].Split(',');
                    s.FirstName = splitLine[1].Split(' ')[0];
                    s.LastName = splitLine[1].Split(' ')[1];
                    s.Email = splitLine[2];
                    s.InterestedGroups = new List<Groups>();
                    if (lines[i].Contains("Events"))
                    {
                        s.InterestedGroups.Add(Groups.Events);
                    }
                    if (lines[i].Contains("Career"))
                    {
                        s.InterestedGroups.Add(Groups.Careers);
                    }
                    if (lines[i].Contains("Game"))
                    {
                        s.InterestedGroups.Add(Groups.Game);
                    }
                    if (lines[i].Contains("ACM-W"))
                    {
                        s.InterestedGroups.Add(Groups.ACMW);
                    }
                    if (lines[i].Contains("Web"))
                    {
                        s.InterestedGroups.Add(Groups.Web);
                    }
                    if (lines[i].Contains("Security"))
                    {
                        s.InterestedGroups.Add(Groups.Security);
                    }
                    if (lines[i].Contains("Comp"))
                    {
                        s.InterestedGroups.Add(Groups.Comp);
                    }
                    if (lines[i].Contains("Hack"))
                    {
                        s.InterestedGroups.Add(Groups.Hack);
                    }
                    if (lines[i].Contains("Data"))
                    {
                        s.InterestedGroups.Add(Groups.Data);
                    }
                    studentsToAdd.Add(s);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Student on line " + i + " failed to parse. " + ex.Message);
                }
            }
            /*      The whole verify with your phone makes this non-automateable but leave here in case that changes.
            
            Console.WriteLine("Enter the username (email) of an authorized group adder (You only get one chance): ");
            string username = Console.ReadLine();
            Console.WriteLine("Enter the password of the authorized group adder (You only get once chance): ");
            string password = Console.ReadLine();
            
             */
            string chromeOrFirefox = "";
            IWebDriver wd;
            while (chromeOrFirefox != "c" && chromeOrFirefox != "f")
            {
                Console.WriteLine("Type c for chrome or f for firefox:");
                chromeOrFirefox = Console.ReadLine();
            }
            if (chromeOrFirefox == "c")
            {
                wd = new ChromeDriver();
            }
            else
            {
                wd = new FirefoxDriver();
            }
            IJavaScriptExecutor js = wd as IJavaScriptExecutor;
            wd.Url = "https://accounts.google.com/signin/v2/identifier?continue=https%3A%2F%2Fgroups.google.com%2Fd%2Foverview&service=groups2&sacu=1&rip=1&flowName=GlifWebSignIn&flowEntry=ServiceLogin";
            wd.Navigate();
            /* The whole verify with your phone makes this non-automateable but leave here in case that changes. 
            
            Thread.Sleep(2000);
            js.ExecuteScript("document.getElementsByTagName('input')[0].value='"+username+"'");
            Thread.Sleep(1000);
            wd.FindElement(By.TagName("input")).SendKeys(Keys.Enter);
            Thread.Sleep(5000);
            js.ExecuteScript("document.getElementsByTagName('input')[2].value='" + password + "'");
            Thread.Sleep(1000);
            (js.ExecuteScript("return document.getElementsByTagName('input')[2]") as IWebElement).SendKeys(Keys.Enter);

            */
            Console.WriteLine("Login to Google as an authorized user then press any key to continue. ");
            Console.ReadKey();

            /* ACM GENERAL */
            wd.Url = "https://groups.google.com/a/mst.edu/forum/#!managemembers/acm-grp/invite";
            wd.Navigate();
            Thread.Sleep(3000);
            string compsToAdd = "";
            for (int i = 0; i < studentsToAdd.Count; i++)
            {
                compsToAdd += studentsToAdd[i].Email + ",";
            }
            compsToAdd = compsToAdd.Substring(0, compsToAdd.Length - 1); // Get rid of last comma
            js.ExecuteScript("document.getElementsByTagName('textarea')[0].value ='" + compsToAdd + "'");
            Thread.Sleep(1000);
            js.ExecuteScript("document.getElementsByTagName('textarea')[1].value='Welcome to ACM General Peeps!'");
            Thread.Sleep(1000);
            js.ExecuteScript("document.getElementsByTagName('input')[4].click()");
            Console.WriteLine("Do the I'm not a robot thing if it's there -- Then press any key to continue");
            Console.ReadKey();

            /* SIG COMP */
            wd.Url = "https://groups.google.com/forum/#!managemembers/sig-comp/invite";
            wd.Navigate();
            Thread.Sleep(3000);
            compsToAdd = "";
            for(int i=0; i<studentsToAdd.Count; i++)
            {
                if(studentsToAdd[i].InterestedGroups.Contains(Groups.Comp))
                {
                    compsToAdd += studentsToAdd[i].Email + ",";
                }
            }
            if (compsToAdd.Length > 0)
            {
                compsToAdd = compsToAdd.Substring(0, compsToAdd.Length - 1); // Get rid of last comma
                js.ExecuteScript("document.getElementsByTagName('textarea')[0].value ='" + compsToAdd + "'");
                Thread.Sleep(1000);
                js.ExecuteScript("document.getElementsByTagName('textarea')[1].value='Welcome to Sig-Comp Peeps!'");
                Thread.Sleep(1000);
                js.ExecuteScript("document.getElementsByTagName('input')[4].click()");
                Console.WriteLine("Do the I'm not a robot thing if it's there -- Then press any key to continue");
                Console.ReadKey();
            }

            /* SIGSEC */
            wd.Url = "https://groups.google.com/a/mst.edu/forum/#!managemembers/sigsec-grp/invite";
            wd.Navigate();
            Thread.Sleep(3000);
            compsToAdd = "";
            for (int i = 0; i < studentsToAdd.Count; i++)
            {
                if (studentsToAdd[i].InterestedGroups.Contains(Groups.Security))
                {
                    compsToAdd += studentsToAdd[i].Email + ",";
                }
            }
            if (compsToAdd.Length > 0)
            {
                compsToAdd = compsToAdd.Substring(0, compsToAdd.Length - 1); // Get rid of last comma
                js.ExecuteScript("document.getElementsByTagName('textarea')[0].value ='" + compsToAdd + "'");
                Thread.Sleep(1000);
                js.ExecuteScript("document.getElementsByTagName('textarea')[1].value='Welcome to Sig-Security Peeps!'");
                Thread.Sleep(1000);
                js.ExecuteScript("document.getElementsByTagName('input')[4].click()");
                Console.WriteLine("Do the I'm not a robot thing if it's there -- Then press any key to continue");
                Console.ReadKey();
            }

            /* Events*/
            wd.Url = "https://groups.google.com/a/mst.edu/forum/#!managemembers/acm-events-grp/invite";
            wd.Navigate();
            Thread.Sleep(3000);
            compsToAdd = "";
            for (int i = 0; i < studentsToAdd.Count; i++)
            {
                if (studentsToAdd[i].InterestedGroups.Contains(Groups.Events))
                {
                    compsToAdd += studentsToAdd[i].Email + ",";
                }
            }
            if (compsToAdd.Length > 0)
            {
                compsToAdd = compsToAdd.Substring(0, compsToAdd.Length - 1); // Get rid of last comma
                js.ExecuteScript("document.getElementsByTagName('textarea')[0].value ='" + compsToAdd + "'");
                Thread.Sleep(1000);
                js.ExecuteScript("document.getElementsByTagName('textarea')[1].value='Welcome to ACM Events Peeps!'");
                Thread.Sleep(1000);
                js.ExecuteScript("document.getElementsByTagName('input')[4].click()");
                Console.WriteLine("Do the I'm not a robot thing if it's there -- Then press any key to continue");
                Console.ReadKey();
            }

            /* Careers */
            wd.Url = "https://groups.google.com/a/mst.edu/forum/#!managemembers/acm-employment-grp/invite";
            wd.Navigate();
            Thread.Sleep(3000);
            compsToAdd = "";
            for (int i = 0; i < studentsToAdd.Count; i++)
            {
                if (studentsToAdd[i].InterestedGroups.Contains(Groups.Careers))
                {
                    compsToAdd += studentsToAdd[i].Email + ",";
                }
            }
            if (compsToAdd.Length > 0)
            {
                compsToAdd = compsToAdd.Substring(0, compsToAdd.Length - 1); // Get rid of last comma
                js.ExecuteScript("document.getElementsByTagName('textarea')[0].value ='" + compsToAdd + "'");
                Thread.Sleep(1000);
                js.ExecuteScript("document.getElementsByTagName('textarea')[1].value='Welcome to ACM Careers Peeps!'");
                Thread.Sleep(1000);
                js.ExecuteScript("document.getElementsByTagName('input')[4].click()");
                Console.WriteLine("Do the I'm not a robot thing if it's there -- Then press any key to continue");
                Console.ReadKey();
            }

            /* SIG WEB */
            wd.Url = "https://groups.google.com/a/mst.edu/forum/#!managemembers/sig.com-general-grp/invite";
            wd.Navigate();
            Thread.Sleep(3000);
            compsToAdd = "";
            for (int i = 0; i < studentsToAdd.Count; i++)
            {
                if (studentsToAdd[i].InterestedGroups.Contains(Groups.Web))
                {
                    compsToAdd += studentsToAdd[i].Email + ",";
                }
            }
            if (compsToAdd.Length > 0)
            {
                compsToAdd = compsToAdd.Substring(0, compsToAdd.Length - 1); // Get rid of last comma
                js.ExecuteScript("document.getElementsByTagName('textarea')[0].value ='" + compsToAdd + "'");
                Thread.Sleep(1000);
                js.ExecuteScript("document.getElementsByTagName('textarea')[1].value='Welcome to Sig-Web Peeps!'");
                Thread.Sleep(1000);
                js.ExecuteScript("document.getElementsByTagName('input')[4].click()");
                Console.WriteLine("Do the I'm not a robot thing if it's there -- Then press any key to continue");
                Console.ReadKey();
            }

            /* SIG Game */
            wd.Url = "https://groups.google.com/a/mst.edu/forum/#!managemembers/sig-game-devs-grp/invite";
            wd.Navigate();
            Thread.Sleep(3000);
            compsToAdd = "";
            for (int i = 0; i < studentsToAdd.Count; i++)
            {
                if (studentsToAdd[i].InterestedGroups.Contains(Groups.Game))
                {
                    compsToAdd += studentsToAdd[i].Email + ",";
                }
            }
            if (compsToAdd.Length > 0)
            {
                compsToAdd = compsToAdd.Substring(0, compsToAdd.Length - 1); // Get rid of last comma
                js.ExecuteScript("document.getElementsByTagName('textarea')[0].value ='" + compsToAdd + "'");
                Thread.Sleep(1000);
                js.ExecuteScript("document.getElementsByTagName('textarea')[1].value='Welcome to Sig-Game Peeps!'");
                Thread.Sleep(1000);
                js.ExecuteScript("document.getElementsByTagName('input')[4].click()");
                Console.WriteLine("Do the I'm not a robot thing if it's there -- Then press any key to continue");
                Console.ReadKey();
            }

            /* SIG Data */
            using (StreamWriter sw = new StreamWriter("ToAddSigData.csv"))
            {
                sw.AutoFlush = true;
                for (int i = 0; i < studentsToAdd.Count; i++)
                {
                    if (studentsToAdd[i].InterestedGroups.Contains(Groups.Data))
                    {
                        sw.WriteLine(studentsToAdd[i].FirstName+","+studentsToAdd[i].LastName+","+studentsToAdd[i].Email);
                    }
                }
            }

            /* ACMW */
            using (StreamWriter sw = new StreamWriter("ToAddACMW.csv"))
            {
                sw.AutoFlush = true;
                for (int i = 0; i < studentsToAdd.Count; i++)
                {
                    if (studentsToAdd[i].InterestedGroups.Contains(Groups.ACMW))
                    {
                        sw.WriteLine(studentsToAdd[i].FirstName + "," + studentsToAdd[i].LastName + "," + studentsToAdd[i].Email);
                    }
                }
            }

            /* HACK */
            using (StreamWriter sw = new StreamWriter("ToAddSigHack.csv"))
            {
                sw.AutoFlush = true;
                for (int i = 0; i < studentsToAdd.Count; i++)
                {
                    if (studentsToAdd[i].InterestedGroups.Contains(Groups.Hack))
                    {
                        sw.WriteLine(studentsToAdd[i].FirstName + "," + studentsToAdd[i].LastName + "," + studentsToAdd[i].Email);
                    }
                }
            }

            wd.Dispose();
        }
    }
}
