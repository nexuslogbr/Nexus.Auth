namespace Nexus.Auth.Repository.Utils
{
    public class GenericCommandResultDetail
    {
        public GenericCommandResultDetail() { }

        public GenericCommandResultDetail(
            List<string> messages,
            List<string> warnings,
            List<string> errors)
        {
            Messages = messages;
            Warnings = warnings;
            Errors = errors;
        }

        public List<string> Messages { get; set; }
        public List<string> Warnings { get; set; }
        public List<string> Errors { get; set; }
    }
}
