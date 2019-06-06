using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _18207_18203_Projeto3ED
{
    public partial class Form1 : Form
    {
        const  int proporcaoAltura = 42;
        const  int proporcaoLargura = 15;
        ListaOrdenada<Cidade> cidades;
        ListaOrdenada<Caminho> caminhos;
        public Form1()
        {
            InitializeComponent();

            cidades = new ListaOrdenada<Cidade>();
            caminhos = new ListaOrdenada<Caminho>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {            
            StreamReader arquivoCaminhos = new StreamReader("D:\Informática 2018\GitHub\Caminhos\CaminhosEntreCidadesMarte.txt");
        }

        private void btnLerArquivoCidades_Click(object sender, EventArgs e)
        {
            int inicioIdCidade = 0;
            int tamanhoIdCidade = 3;
            int inicioNomeCidade = tamanhoIdCidade;
            int tamanhoNomeCidade = 15;
            int inicioCoordenadaX = tamanhoIdCidade + inicioIdCidade;
            int tamanhoCoordenadaX = 5;
            int inicioCoordenadaY = inicioCoordenadaX + tamanhoCoordenadaX;
            int tamanhoCoordenadaY = 5;

            DialogResult resultado = dlgAbrir.ShowDialog(); // exibir a caixa de diálogo para o usuário escolher o arquivo a ser lido
            if (resultado == DialogResult.OK) // se o usuário selecionar e abrir o arquivo
            {
                StreamReader arquivoCidades = new StreamReader(dlgAbrir.FileName, System.Text.Encoding.UTF7); // instancia a classe Streamreader e tem como
                while (!arquivoCidades.EndOfStream)
                {
                    string linha = arquivoCidades.ReadLine();

                    int idCidade = int.Parse(linha.Substring(inicioIdCidade, tamanhoIdCidade));
                    string nomeCidade = linha.Substring(inicioNomeCidade, tamanhoNomeCidade);
                    int coordenadaX = int.Parse(linha.Substring(inicioCoordenadaX, tamanhoCoordenadaX));
                    int coordenadaY = int.Parse(linha.Substring(inicioCoordenadaY, tamanhoCoordenadaY));

                    Cidade cidade = new Cidade(idCidade, nomeCidade, coordenadaX, coordenadaY);
                    cidades.InserirOrdenado(cidade);
                }
                arquivoCidades.Close();
            }
        }
    }
}
