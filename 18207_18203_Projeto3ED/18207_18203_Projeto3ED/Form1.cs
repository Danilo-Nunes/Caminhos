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
    public partial class FrmCaminhos : Form // d&d beyond
    {
        decimal proporcaoAltura;
        decimal proporcaoLargura;
        
        Arvore<Cidade> cidades;
        MatrizEsparsa<CaminhoEntreCidades> caminhos;

        //string filePath; string para pegar os arquivos a partir da pasta raiz do projeto

        public FrmCaminhos()
        {
            InitializeComponent();

            cidades = new Arvore<Cidade>();

            /*proporcaoAltura = pbMapa.Height / 2048m;
            proporcaoLargura = pbMapa.Width / 4096m;*/
            proporcaoAltura = pbMapa.Height / pbMapa.Image.Height;
            proporcaoLargura = pbMapa.Width / pbMapa.Image.Width;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            filePath = Directory.GetParent(Directory.GetParent(filePath).FullName).FullName; // volta 2 pastas
            filePath = Directory.GetParent(Directory.GetParent(filePath).FullName).FullName; // volta mais 2

            LerArquivoCidades(filePath);
            LerArquivoCaminhos(filePath);
        }
       
        private void LerArquivoCidades(string filePath)
        {
            int inicioIdCidade = 0;
            int tamanhoIdCidade = 3;
            int inicioNomeCidade = tamanhoIdCidade;
            int tamanhoNomeCidade = 15;
            int inicioCoordenadaX = inicioNomeCidade + tamanhoNomeCidade;
            int tamanhoCoordenadaX = 5;
            int inicioCoordenadaY = inicioCoordenadaX + tamanhoCoordenadaX;
            int tamanhoCoordenadaY = 5;

            /*DialogResult resultado = dlgAbrir.ShowDialog();*/ // exibir a caixa de diálogo para o usuário escolher o arquivo a ser lido
                                                                //if (resultado == DialogResult.OK) // se o usuário selecionar e abrir o arquivo
                                                                   //{
            StreamReader arquivoCidades = new StreamReader(filePath + @"\CidadesMarte.txt", System.Text.Encoding.UTF7);

            while (!arquivoCidades.EndOfStream) // ou usa ReadAllLines e joga em um vetor
            {
                string linha = arquivoCidades.ReadLine();

                int idCidade = int.Parse(linha.Substring(inicioIdCidade, tamanhoIdCidade));
                string nomeCidade = linha.Substring(inicioNomeCidade, tamanhoNomeCidade);
                int coordenadaX = int.Parse(linha.Substring(inicioCoordenadaX, tamanhoCoordenadaX));
                int coordenadaY = int.Parse(linha.Substring(inicioCoordenadaY, tamanhoCoordenadaY));

                Cidade cidade = new Cidade(idCidade, nomeCidade, coordenadaX, coordenadaY);
                cidades.Incluir(cidade);
            }
            arquivoCidades.Close();

            Listar(cidades.Raiz);

            int qtasCidades = cidades.QuantosDados;
            caminhos = new MatrizEsparsa<CaminhoEntreCidades>(qtasCidades, qtasCidades);

            pbMapa.Invalidate();
        }

        private void Listar(NoArvore<Cidade> atual)
        {
            if(atual != null)
            {
                Listar(atual.Esq);

                string cidAt = atual.ToString();
                lisbOrigem.Items.Add(cidAt);
                lisbDestino.Items.Add(cidAt);

                Listar(atual.Dir);
            }
        }
       
        private void LerArquivoCaminhos(string filePath)
        {
            int inicioIdCidadeOrigem = 0;
            int tamanhoIdCidadeOrigem = 3;
            int inicioIdCidadeDestino = tamanhoIdCidadeOrigem;
            int tamanhoIdCidadeDestino = 3;
            int inicioDistancia = tamanhoIdCidadeDestino + inicioIdCidadeDestino;
            int tamanhoDistancia = 5;
            int inicioTempo = inicioDistancia + tamanhoDistancia;
            int tamanhoTempo = 4;
            int inicioCusto = inicioTempo + tamanhoTempo;
            int tamanhoCusto = 5;

            StreamReader arquivoCaminhos = new StreamReader(filePath + @"\CaminhosEntreCidadesMarte.txt", System.Text.Encoding.UTF7);

            while (!arquivoCaminhos.EndOfStream)
            {
                string linha = arquivoCaminhos.ReadLine();

                int idCidadeOrigem = int.Parse(linha.Substring(inicioIdCidadeOrigem, tamanhoIdCidadeOrigem));
                int idCidadeDestino = int.Parse(linha.Substring(inicioIdCidadeDestino, tamanhoIdCidadeDestino));
                int distancia = int.Parse(linha.Substring(inicioDistancia, tamanhoDistancia));
                int tempo = int.Parse(linha.Substring(inicioTempo, tamanhoTempo));
                int custo = int.Parse(linha.Substring(inicioCusto, tamanhoCusto));

                CaminhoEntreCidades caminho = new CaminhoEntreCidades(idCidadeOrigem, idCidadeDestino, distancia, tempo, custo);
                CaminhoEntreCidades caminhoInverso = new CaminhoEntreCidades(idCidadeDestino, idCidadeOrigem, distancia, tempo, custo);

                caminhos.InserirElemento(caminho, idCidadeOrigem, idCidadeDestino); //Guardamos o valor de dois caminhos porque pode-se ir e voltar pelo mesmo caminho
                caminhos.InserirElemento(caminhoInverso, idCidadeDestino, idCidadeOrigem);
            }
            arquivoCaminhos.Close();

            caminhos.ExibirDataGridview(dgvTestes);
            
        }

        private void pbMapa_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            DemarcarLocalizacao(cidades.Raiz, g);
            //DesenhaCaminhos(g, caminhos.DadoDe(0,8));
        }

        private void DemarcarLocalizacao(NoArvore<Cidade> atual, Graphics g)
        {
            if (atual.Info.IdCidade == 20)
                Console.WriteLine("Pare aqui!");

            int x = Convert.ToInt32(atual.Info.CoordenadaX * proporcaoLargura - 5); 
            int y = Convert.ToInt32(atual.Info.CoordenadaY * proporcaoAltura - 5);


            SolidBrush preenchimento = new SolidBrush(Color.Black);
            Rectangle retangulo = new Rectangle(x, y, 7, 7); // tamanho e local do circulo

            g.FillEllipse(preenchimento, retangulo); // 5 é o raio, portanto a altura e a largura devem ser o dobro
            g.DrawEllipse(new Pen(preenchimento), retangulo); // desenha borda

            y += 5;
            x -= 20;

            g.DrawString(atual.Info.NomeCidade, new Font("Cambria", 10, FontStyle.Bold), preenchimento, x, y);

            if(atual.Esq != null)
                DemarcarLocalizacao(atual.Esq, g);
            if(atual.Dir != null)
                DemarcarLocalizacao(atual.Dir, g);
        }

        private void btnProcurar_Click(object sender, EventArgs e)
        {
            int idOrigem = lisbOrigem.SelectedIndex;
            int idDestino = lisbDestino.SelectedIndex;



            if (idOrigem == idDestino) //Caso o usuário já esteja na cidade escolhida como destino
                MessageBox.Show("Você chegou a seu destino, '-'", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                Cidade cid = new Cidade(idOrigem, "", 0,0);
                NoArvore<Cidade> proc = new NoArvore<Cidade>();

                cidades.Existe(cid, ref proc); //Procura-se a Cidade de origem na Árvore para se ter acesso as devidas informações
                CriaCaminho(proc, idDestino);
            }
        }

        private void CriaCaminho(NoArvore<Cidade> origem, int procurado)
        {
            ListaSimples<Caminho> cam = new ListaSimples<Caminho>(); //Uma lista para armazenar todos os caminhos possíveis
            Caminho inicial = new Caminho(); //Um caminho inicial apenas para a chamada do método

            VaiPara(ref cam, inicial, origem, procurado);

            txtTeste.Text = cam.ToString();
            
        }

        private void VaiPara(ref ListaSimples<Caminho> cam, Caminho jaPercorrido, NoArvore<Cidade> origem, int procurado)
        {
           //Como salvamos o valor dos caminhos usando da coluna para marcar a cidade de destino, pensamos em criar um método da matriz esparsa para
           //nos retornar o primeiro membro dessa coluna, desta forma poderíamos ver todas as possibilidades de caminho que levam até a cidade escolhida

            Celula<CaminhoEntreCidades> atual = caminhos.BuscaColuna(procurado); 
            NoArvore<Cidade> cid = new NoArvore<Cidade>(); //A Cidade que nos levará até lá
            NoArvore<Cidade> cidadeAtual = new NoArvore<Cidade>(); //A Cidade que queremos ir
          
            while (atual.Valor != null) //Percorre-se até que não aja mais nenhum caminho que nos leve a cidade
            {
                cidades.Existe(new Cidade(procurado, "", 0, 0), ref cidadeAtual); //Apenas para ajustar o pârametro de referência          

                if (cidades.Existe(new Cidade(atual.Valor.IdCidadeOrigem, "", 0, 0), ref cid))//Idem
                {
                    if (atual.Valor.IdCidadeOrigem != origem.Info.IdCidade) //Caso atinjamos a cidade buscada pela cidade de origem podemos encerrar o
                                                                           //método visto que já conseguimos completar o caminho
                    {
                        if (!jaPercorrido.CidadesVisitadas.ExisteDesordenado(cid.Info)
                            && !jaPercorrido.CidadesVisitadas.ExisteDesordenado(cidadeAtual.Info)) //Para não passarmos por cidades repetidamente
                                                                                                    //E Evitar loops (ir e voltar infinitamente)
                        {
                            Caminho Aux = new Caminho(jaPercorrido); //Como ainda existem outros caminhos para se explorar, não podemos perder o valor
                                                                     //do jaPercorrido Atual que será usado quando este metodo descongelar, então é 
                                                                    //melhor que editemos e mandemos apenas um clone
                            Aux.CidadesVisitadas.InserirAntesDoInicio(cidadeAtual.Info); //A Cidade Atual é a cidade por qual devemos passar para chegar
                                                                                         //ao destino (ou o próprio destino)
                            Aux.Distancia += atual.Valor.Distancia;
                            Aux.Custo += atual.Valor.Custo;
                            Aux.Tempo += atual.Valor.Tempo;

                            VaiPara(ref cam, Aux, origem, cid.Info.IdCidade); //Chamamos o Método Novamente de Forma Recursiva
                        }
                    }
                    else
                    {
                        Caminho Aux = new Caminho(jaPercorrido); //Como Anteriormente dito, mesmo que o caminho esteja completo, novos caminhos podem
                                                                 //ser explorados, então não podemos perder os valores padrões do jaPercorrido

                        Aux.CidadesVisitadas.InserirAntesDoInicio(cidadeAtual.Info); //Inserimos a primeira cidade visitada
                        Aux.Distancia += atual.Valor.Distancia;
                        Aux.Custo += atual.Valor.Custo;
                        Aux.Tempo += atual.Valor.Tempo;
                        Aux.CidadesVisitadas.InserirAntesDoInicio(origem.Info); // E por fim a Origem

                        cam.InserirEmOrdem(Aux); //Este caminho está completo e deve ser guardado
                    }
                }
                else
                {
                    throw new Exception("Cidade Não Existe");
                }

                atual = atual.Abaixo; //Continuamos a procurar um novo caminho
            }
        }

        private void FrmCaminhos_Resize(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tpRotas)
                pbMapa.Invalidate();
            else
                pbArvore.Invalidate();
        }

        private void tabControl1_Enter(object sender, EventArgs e)
        {
            pbArvore.Invalidate();            
        }

        private void DesenhaArvore(bool primeiraVez, NoArvore<Cidade> atual,
                 int x, int y, double angulo, double incremento,
                 double comprimento, Graphics g)
        {
            int xf, yf;
            if (atual != null)
            {
                Pen caneta = new Pen(Color.Black);

                xf = (int)Math.Round(x + Math.Cos(angulo) * comprimento);
                yf = (int)Math.Round(y + Math.Sin(angulo) * comprimento);

                if (primeiraVez)
                    yf = 25;

                g.DrawLine(caneta, x, y, xf, yf);
                // sleep(100);
                DesenhaArvore(false, atual.Esq, xf, yf, Math.PI / 2 + incremento,
                                                 incremento * 0.60, comprimento * 0.8, g);
                DesenhaArvore(false, atual.Dir, xf, yf, Math.PI / 2 - incremento,
                                                  incremento * 0.60, comprimento * 0.8, g);
                // sleep(100);
                SolidBrush preenchimento = new SolidBrush(Color.Goldenrod); // ou royalBlue
                g.FillEllipse(preenchimento, xf - 15, yf - 15, 30, 30);
                g.DrawString(Convert.ToString(atual.Info.ToString()), new Font("Cambria", 10),
                              new SolidBrush(Color.Black), xf - 15, yf - 10);
            }
        }

        private void DesenhaCaminhos(Graphics g, Celula<CaminhoEntreCidades> cel)
        {
            NoArvore<Cidade> NoOrigem = new NoArvore<Cidade>();
            NoArvore<Cidade> NoDestino = new NoArvore<Cidade>();

            Cidade origem = new Cidade(cel.Linha, "", 0, 0);
            Cidade destino = new Cidade(cel.Coluna, "", 0, 0);

            cidades.Existe(origem, ref NoOrigem);
            cidades.Existe(destino, ref NoDestino);

            int x = Convert.ToInt32(NoOrigem.Info.CoordenadaX * proporcaoLargura - 5);
            int y = Convert.ToInt32(NoOrigem.Info.CoordenadaY * proporcaoAltura - 5);
            int xf = Convert.ToInt32(NoDestino.Info.CoordenadaX * proporcaoLargura);
            int yf = Convert.ToInt32(NoDestino.Info.CoordenadaY * proporcaoAltura - 5);

            Pen caneta = new Pen(Color.Blue);
            g.DrawLine(caneta, x, y, xf, yf);

            cel = cel.Direita;

            while(cel.Valor == null)
            {
                cel = cel.Abaixo;
                cel = cel.Direita;
            }

            DesenhaCaminhos(g, cel);
        }

        private void pbArvore_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            DesenhaArvore(true, cidades.Raiz, (int)pbMapa.Width / 2, 0, Math.PI / 2,
                                 Math.PI / 2.5, 300, g);
        }
    }
}

