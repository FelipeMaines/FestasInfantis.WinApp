﻿using System.Text.Json.Serialization;
using System.Text.Json;
using FestaInfantil.Dominio.ModuloFesta;
using FestaInfantil.Dominio.ModuloTema;
using FestaInfantil.Dominio.ModuloCliente;

namespace FestaAniversario.Infra.Dados.Arquivo.Compartilhado
{
    public class ContextoDados
    {
        private const string NOME_ARQUIVO = "Compartilhado\\e-Agenda.json";

        public List<Cliente> clientes;
        public List<Festa> festas;
        public List<Tema> temas;

        public ContextoDados()
        {
            clientes = new List<Cliente>();
            festas = new List<Festa>();
            temas = new List<Tema>();
           
        }

        public ContextoDados(bool carregarDados) : this()
        {
            if (File.Exists(NOME_ARQUIVO) == false)
            {
                //NOME_ARQUIVO = File.Create(NOME_ARQUIVO).ToString();
            }

            CarregarArquivo();
        }

        public void GravarTarefasEmArquivo()
        {
            JsonSerializerOptions config = ObterConfiguracoes();

            string strJason = JsonSerializer.Serialize(this, config);

            File.WriteAllText(NOME_ARQUIVO, strJason);
        }

        public void CarregarArquivo()
        {
            JsonSerializerOptions config = ObterConfiguracoes();

            if (File.Exists(NOME_ARQUIVO))
            {
                string strJson = File.ReadAllText(NOME_ARQUIVO);

                if (strJson.Length > 0)
                {
                    ContextoDados ctx = JsonSerializer.Deserialize<ContextoDados>(strJson, config);

                    this.clientes = ctx.clientes;
                    this.festas = ctx.festas;
                    this.temas = ctx.temas;
                }
            }
        }

        private static JsonSerializerOptions ObterConfiguracoes()
        {
            JsonSerializerOptions opcoes = new JsonSerializerOptions();
            opcoes.IncludeFields = true;
            opcoes.WriteIndented = true;
            opcoes.ReferenceHandler = ReferenceHandler.Preserve;

            return opcoes;
        }
    }
}
