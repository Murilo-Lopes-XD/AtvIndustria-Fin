using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtvIndustria.Models
{
    [Table("Funcionarios")]
     public class ModelIndustria
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull]

        public String Nome { get; set; }
        [NotNull]

        public String Setor { get; set; }
        [NotNull]

        public String Turno { get; set; }

        public ModelIndustria()
        {
            this.Id = 0;
            this.Nome = "";
            this.Setor = "";
            this.Turno = "";
        }
    }
}
