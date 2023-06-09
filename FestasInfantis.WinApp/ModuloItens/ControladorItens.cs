﻿using FestaAniversario.Infra.Dados.Arquivo.ModuloItens;
using FestaInfantil.Dominio.ModuloTema;
using FestasInfantis.WinApp.Compartilhado;
using FestasInfantis.WinApp.ModuloTema;

namespace FestasInfantis.WinApp.ModuloItens
{
    internal class ControladorItens : ControladorBase
    {
        private IRepositorioItens repositorioItem;
        ListagemItensControl listagemItens = new ListagemItensControl();

        public ControladorItens(IRepositorioItens repositorioItens)
        {
            repositorioItem = repositorioItens;
        }

        public override string ToolTipInserir => "Inserir Itens";

        public override string ToolTipEditar => "Editar Itens";

        public override string ToolTipExcluir => "Excluir Itens";

        public override void Editar()
        {
            Itens item = ObterItemSelecionado();

            if (item == null)
            {
                MessageBox.Show("Selecione um item primeiro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            TelaItens telaItens = new TelaItens();
            int id = item.id;
            telaItens.itens = item;

            if (telaItens.ShowDialog() == DialogResult.OK)
            {
                telaItens.itens.id = id;
                repositorioItem.Editar(id, telaItens.itens);
                CarregarItens();
            }
        }


        public override void Excluir()
        {
            Itens item = ObterItemSelecionado();
            if (item == null)
            {
                MessageBox.Show("Selecione um item primeiro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult verificarExclusao = MessageBox.Show($"Deseja excluir o tema {item.nomeDoItem}?", "Exclusão de Item",
              MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (verificarExclusao == DialogResult.OK)
            {
                repositorioItem.Excluir(item);
                CarregarItens();
            }
        }

        public override void Inserir()
        {
            TelaItens telaItens = new TelaItens();

            if (telaItens.ShowDialog() == DialogResult.OK)
            {
                Itens item = telaItens.itens;

                repositorioItem.Inserir(item);

                CarregarItens();
            }
        }
        public void CarregarItens()
        {
            List<Itens> item = repositorioItem.SelecionarTodos();

            listagemItens.AtualizarRegistros(item);
        }
        public Itens ObterItemSelecionado()
        {
            int id = listagemItens.ObterIdSelecionado();

            return repositorioItem.SelecionarPorId(id);
        }

        public override UserControl ObterListagem()
        {
            if (listagemItens == null)
                listagemItens = new ListagemItensControl();

            CarregarItens();
            return listagemItens;
        }

        public override string ObterTipoCadastro()
        {
            return "Cadastro de Itens";
        }

        public override void RealizarPagamento()
        {
            throw new NotImplementedException();
        }
    }
}