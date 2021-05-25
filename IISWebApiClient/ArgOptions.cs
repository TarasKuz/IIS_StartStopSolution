using CommandLine;

namespace IISStartStopClient
{
    public class ArgOptions
    {
        [Option('a', "action", Required = true, HelpText = "Values: start|stop ")]
        public string Action { get; set; }

        [Option('n', "name", Required = true, HelpText = "Name of a web site")]
        public string WebSiteName { get; set; }
    }
}