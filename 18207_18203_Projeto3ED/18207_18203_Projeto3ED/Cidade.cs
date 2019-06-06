using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _18207_18203_Projeto3ED
{
    class Cidade:IComparable<Cidade>
    {
        int idCidade, coordenadaX, coordenadaY;
        string nomeCidade;

        public int IdCidade
        {
            get => idCidade;
            set
            {
                if(value < 0) // int nunca é null
                    throw new ArgumentOutOfRangeException("O id da Cidade deve ser maior que 0!");

                idCidade = value;
            }
        }
        public string NomeCidade
        {
            get => nomeCidade;
            set
            {
                if (value == "" || value == null)
                    throw new ArgumentOutOfRangeException("O nome da Cidade deve ser diferente de cadeia vazia!");

                nomeCidade = value;
            }
        }
        public int CoordenadaX
        {
            get => coordenadaX;
            set
            {
                if (value < 0) // int nunca é null
                    throw new ArgumentOutOfRangeException("A coordenada X deve ser maior que 0!");

                coordenadaX = value;
            }
        }
        public int CoordenadaY
        {
            get => coordenadaY;
            set
            {
                if (value < 0) // int nunca é null
                    throw new ArgumentOutOfRangeException("A coordenada Y deve ser maior que 0!");

                coordenadaY = value;
            }
        }        

        public Cidade(int idCidade, string nomeCidade, int coordenadaX, int coordenadaY)
        {
            IdCidade = idCidade;
            NomeCidade = nomeCidade;
            CoordenadaX = coordenadaX;
            CoordenadaY = coordenadaY;            
        }

        public Cidade()
        {

        }

        public int CompareTo(Cidade other)
        {
            return IdCidade.CompareTo(other.IdCidade);
        }
    }
}
