using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _18207_18203_Projeto3ED
{
    public class CaminhoEntreCidades : IComparable<CaminhoEntreCidades>
    {
        int idCidadeOrigem, idCidadeDestino, distancia, tempo, custo;

        public int IdCidadeOrigem
        {
            get => idCidadeOrigem;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("O id da origem deve ser maior ou igual a 0!");

                idCidadeOrigem = value;
            }
        }
        public int IdCidadeDestino
        {
            get => idCidadeDestino;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("O id do destino deve ser maior ou igual a 0!");

                idCidadeDestino = value;
            }
        }
        public int Distancia
        {
            get => distancia;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("A distancia deve ser maior que 0!");

                distancia = value;                  
            }
        }
        public int Tempo
        {
            get => tempo;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("O tempo deve ser maior que 0!");

                tempo = value;
            }
        }
        public int Custo
        {
            get => custo;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("O custo deve ser maior ou igual a 0!");

                custo = value;
            }
        }

        public CaminhoEntreCidades(int idCidadeOrigem, int idCidadeDestino, int distancia, int tempo, int custo)
        {
            IdCidadeOrigem = idCidadeOrigem;
            IdCidadeDestino = idCidadeDestino;
            Distancia = distancia;
            Tempo = tempo;
            Custo = custo;
        }

        public CaminhoEntreCidades()
        {

        }

        public override string ToString()
        {
            return idCidadeOrigem + " - " + idCidadeDestino + " Distância = " + distancia + " Preco = " + custo;
        }

        public int CompareTo(CaminhoEntreCidades other)
        {
            return this.distancia.CompareTo(other.distancia);
        }
    }
}
