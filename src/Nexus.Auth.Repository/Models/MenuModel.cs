using System.ComponentModel;

namespace Nexus.Auth.Repository.Models
{
    public class MenuModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DefaultValue(false)]
        public bool Mobile { get; set; }
        public string RegisterDate { get; set; }
        public string ChangeDate { get; set; }
    }
}
