using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AVA.ForBrain.ApplicationCore.Entities
{
    [Table("Usuarios", Schema = "dbo")]
    public class TypeAcessoEntity
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IdUsuario")]
        public long IdUser { get; set; }
        public string TypeAcesso { get; set; }
        public int Ativo { get; set; }
    }
}
