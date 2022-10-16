using System;
using System.IO;

namespace Logger_CSharp
{
    public class Logger
    {
        private String msg { get; set; }
        public Boolean enableSaveLogs { get; set; }
        private Boolean isEnable { get; set; } = true;
        public String logsDirectory { get; set; } = AppDomain.CurrentDomain.BaseDirectory;
        public ConsoleColor errorColor { get; set; } = ConsoleColor.Red;
        public ConsoleColor infoColor { get; set; } = ConsoleColor.Blue;
        public ConsoleColor debugColor { get; set; } = ConsoleColor.Green;
        public String errorPrefix { get; set; } = "[ERROR] ";
        public String infoPrefix { get; set; } = "[INFO] ";
        public String debugPrefix { get; set; } = "[DEBUG]";
        public String logFileBaseName { get; set; } = "logs_";
        public Action highSeverityAction { get; set; }
        public Action mediumSeverityAction { get; set; }
        public Action lowSeverityAction { get; set; }
        private Boolean b = false;
        private string timestamp;

        public Logger()
        {

        }

        public void Enable()
        {
            isEnable = true;
        }

        public void Disable()
        {
            isEnable = false;
        }

        public void log(string message)
        {
            if (!isEnable)
            {
                return;
            }

            Console.WriteLine(message);
            saveLogs(message);
        }

        public void log(ConsoleColor consoleColor, string message)
        {
            if (!isEnable)
            {
                return;
            }

            Console.ForegroundColor = consoleColor;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
            saveLogs(message);
        }

        public void error(string message)
        {
            if (!isEnable)
            {
                return;
            }

            Console.ForegroundColor = errorColor;
            Console.WriteLine(errorPrefix + message);
            Console.ForegroundColor = ConsoleColor.White;
            saveLogs(errorPrefix + message);
        }

        public void error(string message, int severity)
        {
            if (!isEnable)
            {
                return;
            }

            Console.ForegroundColor = errorColor;
            Console.WriteLine(errorPrefix + message);
            Console.ForegroundColor = ConsoleColor.White;

            switch (severity)
            {
                case HIGH_SEVERITY:
                    if (highSeverityAction != null)
                        highSeverityAction.Invoke();
                    break;
                case MEDIUM_SEVERITY:
                    if (mediumSeverityAction != null)
                        mediumSeverityAction.Invoke();
                    break;
                case LOW_SEVERITY:
                    if (lowSeverityAction != null)
                        lowSeverityAction.Invoke();
                    break;
            }

            saveLogs(errorPrefix + message);
        }

        public void info(string message)
        {
            if (!isEnable)
            {
                return;
            }

            Console.ForegroundColor = infoColor;
            Console.WriteLine(infoPrefix + message);
            Console.ForegroundColor = ConsoleColor.White;
            saveLogs(infoPrefix + message);
        }

        public void debug(string message)
        {
            if (!isEnable)
            {
                return;
            }

            Console.ForegroundColor = debugColor;
            Console.WriteLine(debugPrefix + message);
            Console.ForegroundColor = ConsoleColor.White;
            saveLogs(debugPrefix + message);
        }

        public String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }


        private void saveLogs(String message)
        {
            if (!isEnable)
            {
                return;
            }

            if (!b)
            {
                b = true;
                DateTime date = DateTime.Now;
                timestamp = GetTimestamp(date);
            }

            if (!enableSaveLogs)
            {
                return;
            }

            try
            {
                StreamWriter streamWriter = new StreamWriter(logsDirectory, append: true);
                streamWriter.Write("[" + DateTime.Now.ToString("HH:mm:ss") + "] " + message + "\n");
                streamWriter.Flush();
                streamWriter.Close();
            }
            catch (UnauthorizedAccessException uae)
            {
                logsDirectory = AppDomain.CurrentDomain.BaseDirectory;
            }
            catch (IOException ioe)
            {

            }
        }

        public const int HIGH_SEVERITY = 3, MEDIUM_SEVERITY = 2, LOW_SEVERITY = 1;

    }
}

