using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvernoteClone_Demo.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [SQLite.MaxLength(50)]
        public string Name { get; set; }
        [SQLite.MaxLength(50)]
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
