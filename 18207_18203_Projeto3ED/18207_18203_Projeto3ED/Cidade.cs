using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _18207_18203_Projeto3ED
{
    class Cidade:IComparable<Cidade>
    {
        int codigo, x, y;
        string nome;

        public Cidade(int c, int coordenadaX, int coordenadaY, string n )
        {
            codigo = c;
            x = coordenadaX;
            y = coordenadaY;
            nome = n;
        }

        public Cidade()
        {

        }

        public int CompareTo(Cidade other)
        {
            return codigo.CompareTo(other.codigo);
        }
    }
}
