using System.ComponentModel;

namespace Nexus.Auth.Domain.Entities
{
    public class EntityBase
    {
        public int Id { get; set; }
        private DateTime _registerDate = DateTime.Now;

        public DateTime RegisterDate 
        {
            get { return _registerDate; }
            set { _registerDate = value; }
        }

        public DateTime ChangeDate { get; set;}

        [DefaultValue(false)]
        public bool Blocked { get; set; }
    }
}
