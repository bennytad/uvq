using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace UV_Quant
{
    /// <summary>
    /// This class is used to encapsulate commands passed to UV Quant
    /// via a file
    /// </summary>
    class Command
    {
        private string file_path = null;
        private string current_command = null;

        /// <summary>
        /// This is the full path of the command file
        /// </summary>
        public string FileName
        {
            get { return file_path; }
            set { file_path = value; }
        }

        /// <summary>
        /// this is the current command in the command file
        /// </summary>
        public string CurrentCommand
        {
            get { return current_command; }
            set { current_command = value; }
        }

        /// <summary>
        ///  this initialized the command object with the file passed in
        /// </summary>
        /// <param name="command_file_name"></param>
        public Command(string command_file_name)
        {
            FileName = command_file_name;
            initializeCommand();
        }

        /// <summary>
        /// this parses the command file to the specific command in the file
        /// </summary>
        private void initializeCommand()
        {
            int number_of_trials = 0;
            begining:
            try
            {
                FileInfo command_file = new FileInfo(file_path);
                StreamReader input_file = File.OpenText(command_file.FullName);

                string line_read = input_file.ReadLine();
                if (line_read != null)
                {
                    line_read = line_read.Trim();
                    if (line_read != "")
                    {
                        current_command = line_read;
                    }
                    else
                    {
                        current_command = Commands.SIT_IDLE;
                    }
                }
                else
                {
                    current_command = Commands.SIT_IDLE;
                }

                input_file.Close();
            }
            catch (Exception ex)
            {
                number_of_trials += 1;
                if (number_of_trials <= 5)
                {
                    System.Threading.Thread.Sleep(10); //let the other process finish in 10 ms
                    goto begining;
                }
                else
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Once a command has been processed, the appropriate
        /// response should be sent back via this method
        /// </summary>
        /// <param name="response"></param>
        public void processedCommand(string response)
        {
            FileInfo command_file = new FileInfo(file_path);
            File.WriteAllText(command_file.FullName, response);
        }

        /// <summary>
        /// This class standardizes the commands used for UV Quant Oxygen analyzer
        /// </summary>
        public static class Commands
        {
            public static string SIT_IDLE = "0"; //00
            public static string COLLECT_DATA = "1"; //
            public static string COLLECT_LOW_O2 = "2";
            public static string COLLECT_HIGH_O2 = "3";
            public static string ERROR = "4";
        }
    }
}
