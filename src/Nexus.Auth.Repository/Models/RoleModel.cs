﻿namespace Nexus.Auth.Repository.Models
{
    public class RoleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Blocked { get; set; }
        public DateTime ChangeDate { get; set; }
        public bool Mobile { get; set; }
        public IList<MenuModel> Menus { get; set; }
    }
}
