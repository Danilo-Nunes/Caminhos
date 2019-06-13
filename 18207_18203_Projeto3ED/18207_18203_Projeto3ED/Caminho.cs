using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _18207_18203_Projeto3ED
{
    class Caminho: IComparable<Caminho>
    {
        ListaSimples<Cidade> cidadesVisitadas;
        int distancia, tempo, custo;

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
        internal ListaSimples<Cidade> CidadesVisitadas { get => cidadesVisitadas;}

        public Caminho()
        {
            cidadesVisitadas = new ListaSimples<Cidade>();
        }

        public Caminho(Caminho cam)
        {
            this.cidadesVisitadas = cam.cidadesVisitadas;
            this.distancia = cam.distancia;
            this.tempo = cam.tempo;
            this.custo = cam.custo;
        }

        public int CompareTo(Caminho other)
        {
            return distancia.CompareTo(other.distancia);
        }

        public override string ToString()
        {
            return cidadesVisitadas.ToString() + "Tempo: " + tempo + " Distância: " + distancia + " Custo: " + custo;
        }
    }
}
