using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _18207_18203_Projeto3ED
{
    public class Caminho : IComparable<Caminho>
    {
        int codOrigem, codDestino, distancia, tempo, custo;

        public int CodOrigem
        {
            get => codOrigem;
            set
            {
                if (codOrigem >= 0)
                    codOrigem = value;
                else
                    throw new IndexOutOfRangeException("O codigo da origem deve ser maior ou igual a 0!");
            }
        }
        public int CodDestino
        {
            get => codDestino;
            set
            {
                if (codDestino >= 0)
                    codDestino = value;
                else
                    throw new IndexOutOfRangeException("O codigo do destino deve ser maior ou igual a 0!");
            }
        }
        public int Distancia
        {
            get => distancia;
            set
            {
                if (distancia > 0)
                    distancia = value;
                else
                    throw new IndexOutOfRangeException("A distancia deve ser maior que 0!");
            }
        }
        public int Tempo
        {
            get => tempo;
            set
            {
                if (tempo > 0)
                    tempo = value;
                else
                    throw new IndexOutOfRangeException("O tempo deve ser maior que 0!");
            }
        }
        public int Custo
        {
            get => custo;
            set
            {
                if (custo >= 0)
                    custo = value;
                else
                    throw new IndexOutOfRangeException("O custo deve ser maior ou igual a 0!");
            }
        }

        public Caminho(int codOrigem, int codDestino, int distancia, int tempo, int custo)
        {
            CodOrigem = codOrigem;
            CodDestino = codDestino;
            Distancia = distancia;
            Tempo = tempo;
            Custo = custo;
        }

        public Caminho()
        {

        }

        public int CompareTo(Caminho other)
        {
            return this.distancia.CompareTo(other.distancia);
        }
    }
}
