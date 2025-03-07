﻿// RA: 18203 - Danilo de Oliveira Nunes
// RA: 18207 - João Victor Javitti Alves
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


    public class MatrizEsparsa<Dado> where Dado : IComparable<Dado>
    {
        // atributos nescessários para a criação de uma lista ligada cruzada
        int linhas, colunas;
        Celula<Dado> cabeca;

        // retorno quando a célula não está na matriz
        public readonly Dado padrao = default(Dado);

        // propriedade que retorna valor representativo do número de linhas da matriz e
        // permite alterá-lo se for maior que zero, mas somente internamente, na classe
        public int Linhas
        {
            get => linhas;
            private set
            {
                if (value > 0)
                    this.linhas = value;
                else
                    throw new Exception("O número de linhas deve ser maior que 0.");
            }
        }

        // propriedade que retorna valor representativo do número de colunas da matriz e
        // permite alterá-lo se for maior que zero, mas somente internamente, na classe
        public int Colunas
        {
            get => colunas;
            private set
            {
                if (value > 0)
                    this.colunas = value;
                else
                    throw new Exception("O número de colunas deve ser maior que 0.");
            }
        }

    public Celula<Dado> Cabeca { get => cabeca;}

    // retorna variável booleana de verdadeiro caso a matriz não esteja alocada na memória e falso para o contrário
    public bool Desalocada()
        {
            return this.cabeca == null; // se a cabeca for nula a mtriz esta desalocada
        }

        // matriz construida no modelo MxN, linhas por colunas, inicializando os atributos com as propriedades e a celula principal nula
        public MatrizEsparsa(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;

            cabeca = new Celula<Dado>(padrao, -1, -1);

            // celula que gerará outras para formar linhas e colunas da matriz
            Celula<Dado> geradora = cabeca;

            // cria colunas da matriz deixando os ponteiros abaixo das celulas valendo uma celula com este ponteiro -1
            for (int i = 0; i < this.colunas; i++)
            {
                Celula<Dado> novaCelulaColuna = new Celula<Dado>(padrao, -1, i);

                // avancamos a geradora para criar as outras celulas
                geradora.Abaixo = geradora; 
                geradora.Direita = novaCelulaColuna;
                geradora = geradora.Direita;
            }
            // coloca os últimos que não seriam acessados e faz com que o último aponte para o primeiro, tornando a lista circular
            geradora.Abaixo = geradora;
            geradora.Direita = cabeca; // mesmo valor da geradora no começo

            // prepara para gerar as linhas, reiniciando os atributos
            geradora = cabeca;

            for (int i = 0; i < this.Linhas; i++)
            {
                Celula<Dado> novaCelulaLinha = new Celula<Dado>(padrao, i, -1);

                // verifica se é a cabeça para não perder a lista das colunas
                if (geradora != cabeca)
                    geradora.Direita = geradora;

                geradora.Abaixo = novaCelulaLinha;
                geradora = geradora.Abaixo; // mesmo valor da geradora no começo
            }
            // coloca os últimos que não seriam acessados e faz com que o último aponte para o primeiro, tornando a lista circular
            geradora.Direita = geradora;
            geradora.Abaixo = cabeca;
        }
        public void ApagarMatriz()
        {
            cabeca = null; // deixa a cabeca nula o que faz com que seja inascessivel, eliminando assim todos os elementos da matriz que logo são deletados pelo garbage collector da linguagem
            linhas = 0;
            colunas = 0;
        }

        public void ExibirDataGridview(DataGridView dgv)
        {
            if (dgv == null)
                throw new Exception("gridView utilizado é nulo."); // ArgumentException

            // limpa o gridView para poder reutilizar gridViews
            dgv.Columns.Clear();
            dgv.Rows.Clear();

            // cria o cabeçalho das colunas
            for (int i = 0; i < this.Colunas; i++)
                dgv.Columns.Add(i.ToString(), i.ToString());

            // cria vetor de strings que armazenará as linhas da matriz para ser utilizado pelo gridView
            string[] linhaMatriz = new string[this.Colunas];
            Celula<Dado> celLinha = cabeca; // inicia celula que será usada para percorrer as linhas da matriz



            // percorre as linhas e colunas da matriz e insere os valores das celulas no gridView
            for (int j = 0; j < this.Linhas; j++)
            {
                celLinha = celLinha.Abaixo; // avancamos a linha
                Celula<Dado> celColuna = celLinha; // inicia celula que será usada para percorrer as colunas da linha da matriz 

                for (int i = 0; i < colunas; i++) // coloca os valores no vetor
                {
                    if (celColuna.Direita.Coluna != -1 && celColuna.Direita.Coluna == i)
                    {
                        // avanca a coluna e escreve a linha na matriz
                        celColuna = celColuna.Direita;
                        linhaMatriz[i] = celColuna.Valor.ToString();
                    }
                    else linhaMatriz[i] = "0"; // se nao o valor da celula seria 0
                }
                dgv.Rows.Add(linhaMatriz); // adiciona o vetor ao gridView
                dgv.Rows[j].HeaderCell.Value = j.ToString(); // adiciona cabeçalho da linha
            }
            dgv.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders);
            dgv.AutoResizeColumns();
        }

        public override string ToString()
        {
            string ret = "{";

            Celula<Dado> celLinha = cabeca.Abaixo;

            while (celLinha != cabeca) // enquanto nao terminarmos de percorrer as linhas
            {
                Celula<Dado> celColuna = celLinha.Direita; // inicia celula que usaremos para percorrer as colunas da linha da matriz

                while (celColuna != celLinha) // enquanto estivermos percorrendo elementos com valor diferente da celula cabeca
                {
                    ret += celColuna.ToString() + (celColuna.Direita != celLinha ? ", " : " "); // se a celula da direita for diferente da cabeca, será posto uma virgula, caso o contrário, nada, apenas espaço

                    celColuna = celColuna.Direita; // avançamos a coluna
                }

                celLinha = celLinha.Abaixo; // atualiza a linha para um abaixo
            }

            return ret + "}";
        }

        public void InserirElemento(Dado elemento, int linha, int coluna)
        {
            // condições que verificam a validade dos parâmetros passados
            if (linha < 0 || linha > linhas)
                throw new Exception("Linha fora dos limites da matriz."); // ArgumentOutOfRangeException

            if (coluna < 0 || coluna > colunas)
                throw new Exception("coluna fora dos limites da matriz.");

            if (elemento == null)
                throw new Exception("O valor a ser guardado na matriz não pode ser nulo."); // ArgumentException

            // declaração das Celulas a serem usadas para encontrar a poição requerida para a inserção
            Celula<Dado> celLinha = cabeca;
            Celula<Dado> celColuna = cabeca;

            // percorrem a lista das colunas e a das linhas alterando o valor das celulas para as respectivas requeridas nos parâmetro
            for (int i = 0; i <= coluna; i++)
                celColuna = celColuna.Direita;

            for (int j = 0; j <= linha; j++)
                celLinha = celLinha.Abaixo;

            // criamos duas Celulas que armazenarão o atual e o anterior da linha onde percorreremos na coluna
            Celula<Dado> anterior = celLinha;
            Celula<Dado> atual = celLinha.Direita;

            while (atual.Coluna < coluna && atual.Coluna != -1) // percorre as colunas até achar a posição desejada ou até que seja igual a cabeca
            { 
                // atualiza anterior e atual na linha
                anterior = atual;
                atual = atual.Direita;
            }

            if (atual.Valor == null || atual.Coluna > coluna) // caso seja uma célula vazia e a coluna maior que a desejada
            {
                Celula<Dado> nova = new Celula<Dado>(elemento, linha, coluna); // cria célula a ser inserida na matriz

                // insere a celula na linha entre o anterior e o atual
                anterior.Direita = nova;
                nova.Direita = atual;

                // incia variaveis de anterior e atual para a insercao na coluna
                Celula<Dado> colunaAnt = celColuna;
                Celula<Dado> colunaAt = celColuna.Abaixo;

                while (colunaAt.Abaixo != celColuna && colunaAt.Linha != linha) // percorre a coluna até achar a posião desejada e depois insere ela no ponteiro da célula anterior 
                { 
                    // atualiza o atual e anterior na coluna
                    colunaAnt = colunaAt;
                    colunaAt = colunaAt.Abaixo;
                }

                if (colunaAt.Linha > linha) // se a linha da colunaAt for maior que a desejada inserimos antes, entre a colunaAt e colunaAnt
                {
                    // inserimos entre as duas
                    colunaAnt.Abaixo = nova;
                    nova.Abaixo = colunaAt;                    
                }
                else // se for menor
                {
                    // inserimos depois da atual e fazemos com que aponte para o inicio da coluna
                    colunaAt.Abaixo = nova;
                    nova.Abaixo = celColuna;                    
                }
            }
            else // se for a mesma posicao
                atual.Valor = elemento; // caso não seja, apenas alteramos o valor guardado pela célula
        }

        public Dado ValorDe(int linha, int coluna)
        {
            // condições que verificam a validade dos parâmetros passados
            if (linha < 0 || linha > this.linhas)
                throw new Exception("Linha fora dos limites da matriz."); // ArgumentOutOfRangeException

            if (coluna < 0 || coluna > this.colunas)
                throw new Exception("coluna fora dos limites da matriz.");

            Celula<Dado> celLinha = cabeca; // iniciamos celula da linha com a cabeca

            for(int i = 0; i <= linha; i++) // percorre a linha até achar a posição desejada
                celLinha = celLinha.Abaixo;

            Celula<Dado> celColuna = celLinha.Direita; // inicia a célula da coluna, onde percorreremos 

            while (celColuna.Coluna < coluna && celColuna != celLinha) // percorre a coluna até achar a posição desejada 
                celColuna = celColuna.Direita;

            return celColuna.Valor; // retorna o valor armazenado pela célula, caso seja nenhum, retornará 0(celula coluna = celula linha)
        }

        public Celula<Dado> DadoDe(int linha, int coluna)
        {
            // condições que verificam a validade dos parâmetros passados
            if (linha < 0 || linha > this.linhas)
                throw new Exception("Linha fora dos limites da matriz."); // ArgumentOutOfRangeException

            if (coluna < 0 || coluna > this.colunas)
                throw new Exception("coluna fora dos limites da matriz.");

            Celula<Dado> celLinha = cabeca; // iniciamos celula da linha com a cabeca

            for (int i = 0; i <= linha; i++) // percorre a linha até achar a posição desejada
                celLinha = celLinha.Abaixo;

            Celula<Dado> celColuna = celLinha.Direita; // inicia a célula da coluna, onde percorreremos 

            while (celColuna.Coluna < coluna && celColuna != celLinha) // percorre a coluna até achar a posição desejada 
                celColuna = celColuna.Direita;

            return celColuna; // retorna o valor armazenado pela célula, caso seja nenhum, retornará 0(celula coluna = celula linha)
        }


    public Celula<Dado> BuscaColuna(int col)
    {
        Celula<Dado> celColuna = cabeca;

        for (int i = 0; i <= col; i++)
            celColuna = celColuna.Direita;

        celColuna = celColuna.Abaixo;

        return celColuna;
    }

    public bool RemoverEm(int linha, int coluna)
        {
            // condições que verificam a validade dos parâmetros passados
            if (linha < 0 || linha > this.linhas)
                throw new Exception("Linha fora dos limites da matriz."); // ArgumentOutOfRangeException

            if (coluna < 0 || coluna > this.colunas)
                throw new Exception("coluna fora dos limites da matriz.");

            Celula<Dado> celLinha = cabeca;
            Celula<Dado> celColuna = cabeca;

            // percorre as linhas e depois as colunas até achar as desejadas
            for (int i = 0; i <= linha; i++)
                celLinha = celLinha.Abaixo;

            for (int i = 0; i <= coluna; i++)
                celColuna = celColuna.Direita;

            Celula<Dado> anteriorLinha = celLinha;
            Celula<Dado> atualLinha = celLinha.Direita; // celula a ser removida da matriz

            while (atualLinha.Coluna != coluna && atualLinha.Coluna != -1) // percorre a coluna até achar a desaja ou até que ela seja igual a cabeca
            {
                // atualiza o atual e o anterior da linha
                anteriorLinha = atualLinha;
                atualLinha = atualLinha.Direita;
            }

            if (atualLinha.Valor.Equals(0)) // se o valor armazenado pela célula for 0, ou seja, célula vazia
                return false; // não há uma célula utilizada para ser removida

            anteriorLinha.Direita = atualLinha.Direita; // elimina o ponteiro do elemento anterior a ele na linha que se liga ao valor

            Celula<Dado> atualColuna = celColuna; // celula que usarem,os para percorre a coluna

            while (atualColuna.Abaixo != atualLinha) // percorre até achar o anterior ao elemento 
                atualColuna = atualColuna.Abaixo;

            atualColuna.Abaixo = atualLinha.Abaixo; // elimina o ponteiro do elemento anterior a ele na coluna que se liga ao valor

            return true;
        }        
    }
