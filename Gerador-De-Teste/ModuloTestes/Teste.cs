﻿using GeradorDeTeste.ConsoleApp.Compartilhado;
using GeradorDeTeste.WinApp.Compartilhado;
using Gerador_De_Teste.ModuloDisciplinas;
using Gerador_De_Teste.ModuloQuestoes;
using Gerador_De_Teste.ModuloMateria;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerador_De_Teste.ModuloTestes
{
    public class Teste : EntidadeBase
    {
        public string Titulo { get; set; }
        public Disciplina Disciplina { get; set; }
        public int QuantidadeDeQuestoes { get; set; }
        public Materia Materia { get; set; }
        public bool Recuperacao { get; set; }
        public List<Questao> Questoes { get; set; }


        public Teste(string titulo, Disciplina disciplina, int quantidadeDeQuestoes, Materia materia, bool recuperacao, List<Questao> questoes)
        {
            Titulo = titulo;
            Disciplina = disciplina;
            QuantidadeDeQuestoes = quantidadeDeQuestoes;
            Materia = materia;
            Recuperacao = recuperacao;
            Questoes = questoes;

        }

        public override List<string> Validar()
        {
            List<string> erros = new List<string>();

            if (string.IsNullOrEmpty(Materia.Nome.Trim()))
                erros.Add("O campo \"matéria\" é obrigatório");
            if (string.IsNullOrEmpty((Titulo)))
                erros.Add("O campo \"título\" é obrigatório");
            if (Questoes.Count<=0)
                erros.Add("O campo \"questões\" é obrigatório");
            

            return erros;
        }

        public override void AtualizarRegistro(EntidadeBase novoRegistro)
        {
            Teste atualizado = (Teste)novoRegistro;

            Titulo = atualizado.Titulo;
            Disciplina = atualizado.Disciplina;
            QuantidadeDeQuestoes = atualizado.QuantidadeDeQuestoes;
            Materia = atualizado.Materia;
            Recuperacao = atualizado.Recuperacao;
            Questoes = atualizado.Questoes;
        }

        public override string ToString()
        {
            return Titulo.ToTitleCase();
        }
    }
}
