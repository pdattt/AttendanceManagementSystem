using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Common.Mailings
{
    public static class Mailing
    {
        public static string Subject = "[Warning] - There is a class in current time";

        public static class From
        {
            public const string EmailAddress = "";
            public const string Password = "";
        }

        public static class Body
        {
            public static string BreakLine = "<br>";

            public static string SayDear(string name)
            {
                return "<p> Dear " + name + ", </p>";
            }

            public static string ConfirmCheckIn(string eventName, string checkInTime)
            {
                string content = "";

                content += "<p>";
                content += "We confirm you have checked in successful for the event with name ";
                content += "<b>" + eventName + "</b>";
                content += " in ";
                content += "<b>" + checkInTime + "</b>";
                content += "</p>";

                return content;
            }

            public static string Alert(string className, string classStartTime, string classEndTime)
            {
                string content = "";

                content += "<p>";
                content += "However, you still have a class with name ";
                content += "<b>" + className + "</b>";
                content += " from ";
                content += "<b>" + classStartTime + "</b>";
                content += " to ";
                content += "<b>" + classEndTime + "</b>";
                content += "</p>";

                return content;
            }

            public static string SayBeSure = "<p> Be sure that you have been prepared everything to not missing the lesson </p>";
        }
    }
}