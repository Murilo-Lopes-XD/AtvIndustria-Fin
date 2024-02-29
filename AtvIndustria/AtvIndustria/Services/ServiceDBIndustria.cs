using AtvIndustria.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtvIndustria.Services
{

    internal class ServiceDBIndustria
    {
        SQLiteConnection conn;
        public string StatusMessage { get; set; }
        public ServiceDBIndustria(string dbPath)
        {
            if (dbPath == "") dbPath = App.DbPath;
            conn = new SQLiteConnection(dbPath);
            conn.CreateTable<ModelIndustria>();
        }
        public void Inserir(ModelIndustria notas)
        {
            try
            {
                if (string.IsNullOrEmpty(notas.Nome))
                    throw new Exception("Nome não informado!");
                if (string.IsNullOrEmpty(notas.Turno))
                    throw new Exception("Turno não informado!");
                if (string.IsNullOrEmpty(notas.Setor))
                    throw new Exception("Setor não informado!");
                int result = conn.Insert(notas);
                if (result != 0)
                {
                    this.StatusMessage = string.Format("{0} registro(s) adicionado(s): [Nome = {1}", result, notas.Nome);
                }
                else
                {
                    this.StatusMessage = string.Format("Nenhum registro adicionado! Por favor, informe o nome, setor e turno do funcionário!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ModelIndustria> Listar()
        {
            List<ModelIndustria> lista = new List<ModelIndustria>();
            try
            {
                lista = conn.Table<ModelIndustria>().ToList();
                this.StatusMessage = "Listagem dos Funcionários:";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return lista;
        }

        public void Alterar(ModelIndustria nota)
        {
            try
            {
                if (string.IsNullOrEmpty(nota.Nome))
                    throw new Exception("Nome não informado!");
                if (string.IsNullOrEmpty(nota.Setor))
                    throw new Exception("Setor não informado!");
                if (string.IsNullOrEmpty(nota.Turno))
                    throw new Exception("Turno não informado!");
                if (nota.Id <= 0)
                    throw new Exception("ID não informado!");
                int result = conn.Update(nota);
                StatusMessage = string.Format("{0} registro(s) alterado(s)", result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Excluir(int id)
        {
            try
            {
                int result = conn.Table<ModelIndustria>().Delete(r => r.Id == id);
                StatusMessage = string.Format("{0} registro deletado!", result);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro: {0}", ex.Message));
            }
        }

        public List<ModelIndustria> Localizar(string nome)
        {
            List<ModelIndustria> lista = new List<ModelIndustria>();
            try
            {
                var resp = from p in conn.Table<ModelIndustria>() where p.Nome.ToLower().Contains(nome.ToLower()) select p;
                lista = resp.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro: {0}", ex.Message));
            }
            return lista;
        }
    }
}
