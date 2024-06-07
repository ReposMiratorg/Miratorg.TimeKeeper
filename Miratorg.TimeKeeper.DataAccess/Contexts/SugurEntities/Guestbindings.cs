using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class Guestbindings
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int? CardId { get; set; }
        public int? EmpId { get; set; }
        public DateTime? Datecre { get; set; }
        public DateTime? Datefin { get; set; }
        public DateTime? Dateexp { get; set; }
        public string Doctype { get; set; }
        public string Docname { get; set; }
        public string Name { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Series { get; set; }
        public string Number { get; set; }
        public string Whoissued { get; set; }
        public DateTime? Whenissued { get; set; }
        public string Address { get; set; }
        public string Comment { get; set; }
        public string Lprnumber { get; set; }
        public int? Cometo { get; set; }
        public int? ApplId { get; set; }
    }
}
