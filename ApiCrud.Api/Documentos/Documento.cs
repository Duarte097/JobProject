using ApiCrud.Models; 
using System;

namespace ApiCrud.Models 
{
    /*public enum Status
    {
        Ativo,
        Inativo
    }*/

    public class Documento
    {
        public int Id { get; init; }
        public string Nome { get; private set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; } 
        public DateTime DataUpload { get; set; }
        //public Status Status { get; private set; }
        public bool Ativo{ get; set; }
        public bool Inativo{ get; set; }
        public string Caminho { get; set; }
        public string Versao { get; set; }

        public Documento(int id, string nome, string descricao, DateTime dataUpload, string caminho, string versao, string categoria)
        {
            this.Nome = nome;
            this.Id = id;
            this.Descricao = descricao;
            this.DataUpload = dataUpload;
            this.Ativo = true;
            this.Inativo = false;
            this.Caminho = caminho;
            this.Versao = versao;
            this.Categoria = categoria; 
        }
    }
}

