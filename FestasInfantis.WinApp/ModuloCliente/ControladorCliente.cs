﻿using FestaAniversario.Infra.Dados.Arquivo.ModuloCliente;
using FestaInfantil.Dominio.ModuloCliente;
using FestasInfantis.WinApp.Compartilhado;

namespace FestasInfantis.WinApp.ModuloCliente
{
    public class ControladorCliente : ControladorBase
    {
        private IRepositorioCliente repositorioCliente;
        private ListagemClienteControl listagemCliente;

        public ControladorCliente(IRepositorioCliente repositorioCliente)
        {
            this.repositorioCliente = repositorioCliente;
        }

        public override string ToolTipInserir { get { return "Inserir um novo Cliente"; } }

        public override string ToolTipEditar { get { return "Editar Cliente existente"; } }

        public override string ToolTipExcluir { get { return "Excluir Cliente existente"; } }

        private Cliente ObterClienteSelecionado()
        {
            int id = listagemCliente.ObterIdSelecionado();

            return repositorioCliente.SelecionarPorId(id);
        }
        public override void Inserir()
        {
            TelaCliente telaCliente = new TelaCliente();

            if(telaCliente.ShowDialog() == DialogResult.OK)
            {
                Cliente cliente = telaCliente.Cliente;

                repositorioCliente.Inserir(cliente);
                CarregarClientes();
            }
        }

        public override void Editar()
        {
            Cliente cliente = ObterClienteSelecionado();

            if (cliente == null)
            {
                MessageBox.Show($"Selecione um cliente primeiro!",
                    "Edição de Clientes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);

                return;
            }

            int id = 0;
            TelaCliente telaCliente = new TelaCliente();
            telaCliente.Cliente = cliente;
            id = cliente.id;

            DialogResult opcaoEscolhida = telaCliente.ShowDialog();

            if (opcaoEscolhida == DialogResult.OK)
            {
                telaCliente.Cliente.id = id;
                repositorioCliente.Editar(id, telaCliente.Cliente);

                CarregarClientes();
            }
            else
            {
                MessageBox.Show("Cancelado!");
                return;
            }
        }


        public override void Excluir()
        {
            Cliente cliente = ObterClienteSelecionado();

            if (cliente == null)
            {
                MessageBox.Show($"Selecione um cliente primeiro!",
                    "Exclusão de Clientes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);

                return;
            }

            DialogResult opcaoEscolhida = MessageBox.Show($"Deseja excluir o cliente {cliente.nome}?", "Exclusão de Clientes",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (opcaoEscolhida == DialogResult.OK)
            {
                repositorioCliente.Excluir(cliente);

                CarregarClientes();
            }
        }
        public override UserControl ObterListagem()
        {
            if (listagemCliente == null)
                listagemCliente = new ListagemClienteControl();

            CarregarClientes();
            return listagemCliente;
        }

        public override string ObterTipoCadastro()
        {
            return "Cadastro de Cliente";
        }

        public void CarregarClientes()
        {
            List<Cliente> clientes = repositorioCliente.SelecionarTodos();

            listagemCliente.AtualizarRegistros(clientes);
        }

        public override void RealizarPagamento()
        {
            throw new NotImplementedException();
        }
    }
}
