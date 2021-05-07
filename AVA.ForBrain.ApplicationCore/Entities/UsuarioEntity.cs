using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AVA.ForBrain.ApplicationCore.Entities
{
    [Table("Usuarios", Schema = "dbo")]
    public class UsuarioEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IdUsuario")]
        public long IdUser { get; set; }
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string PrimeiroAcesso { get; set; }
        public string ExigeADFS { get; set; }
        public string Observacao { get; set; }

        [ForeignKey("TypeAcesso")]
        public virtual TypeAcessoEntity TypeAcessoEntity { get; set; }
        public Nullable<DateTime> DataCriacao { get; set; }
        public Nullable<DateTime> DataUltimoAcesso { get; set; }
        public int Ativo { get; set; }
    }
}
