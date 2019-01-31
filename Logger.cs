using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MockServer.Utilities
{
    public class Logger
    {
        private const string LOG_FILE_NAME = "log.txt";

        public static void Log(string message, Exception e = null)
        {
            string originalLogMessage = "";
            try
            {
                string folderPath = "APP_NAME";// put your app name here

                var pathToLogFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), folderPath);

                if(!Directory.Exists(pathToLogFile))
                {
                    Directory.CreateDirectory(pathToLogFile);
                }
                var fileName = Path.Combine(pathToLogFile, LOG_FILE_NAME);

                originalLogMessage = GetLogMessage(message, e);
                File.AppendAllText(fileName, originalLogMessage);
            }
            catch (Exception logException)
            {
                // Unable to log to file
                Console.WriteLine("Unable to log to file");
                Console.WriteLine(GetLogMessage(message, e));

                File.WriteAllText("local_output.txt", GetLogMessage("Unable to write to desired file", logException));
                File.WriteAllText("local_output.txt", originalLogMessage);
            }
        }

        private static string GetLogMessage(string message, Exception e = null)
        {
            string exceptionMessage = GetExceptionString(e);
            string logMessageDate = DateTime.Now.ToLongDateString();
            string logMessage = logMessageDate + " " + message + " " + exceptionMessage +  Environment.NewLine;

            return logMessage;
        }

        private static string GetExceptionString(Exception error)
        {
            if(error == null)
            {
                return "";
            }
            string errorString = "EXCEPTION: ";
            Exception currentError = error;
            while (currentError != null)
            {
                errorString += currentError.Message + " ";
                currentError = currentError.InnerException;
            }

            return errorString;
        }
    }
}
