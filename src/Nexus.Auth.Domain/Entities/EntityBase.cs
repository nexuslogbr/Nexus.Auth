using System.ComponentModel;

namespace Nexus.Auth.Domain.Entities
{
    public class EntityBase
    {
        public int Id { get; set; }

        private DateTime _registerDate = DateTime.Now;
        private DateTime _changeDate = DateTime.Now;

        public DateTime RegisterDate 
        {
            get { return _registerDate; }
            set { _registerDate = value; }
        }

        public DateTime ChangeDate
        {
            get { return _changeDate; }
            set { _changeDate = value; }
        }

        [DefaultValue(false)]
        public bool Blocked { get; set; }
    }
}
