public class Logger
{
    private const string LOG_FILE_NAME = "log.txt";

    public static void Log(string message, Exception e = null)
    {
        try
        {
            string folderPath = "APP_NAME";// put your app name here

            var fileName = Path.Combine(folderPath, LOG_FILE_NAME);

            File.AppendAllText(fileName, GetLogMessage(message, e));
        }
        catch (Exception logException)
        {
            // Unable to log to file
            Console.WriteLine("Unable to log to file");
            Console.WriteLine(GetLogMessage(message, e));
        }
    }

    private static string GetLogMessage(string message, Exception e)
    {
        string exceptionMessage = GetExceptionString(e);
        string logMessage = DateTime.Now.ToLongDateString();
        logMessage += " " + message + Environment.NewLine;

        return logMessage;
    }

    private static string GetExceptionString(Exception error)
    {
        string errorString = "";
        Exception currentError = error;
        while (currentError != null)
        {
            errorString += currentError.Message + " ";
            currentError = currentError.InnerException;
        }

        return errorString;
    }
}
