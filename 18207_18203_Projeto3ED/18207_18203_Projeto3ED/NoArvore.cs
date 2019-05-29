using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arvore
{
    class NoArvore<Tipo> : IComparable<NoArvore<Tipo>>
        where Tipo : IComparable<Tipo>
    {
        public bool marcadoParaExclusaoFutura;
        public Tipo dados;
        public NoArvore<Tipo> esq;
        public NoArvore<Tipo> dir;
        public int altura;
        public NoArvore(Tipo dados)
        {
            this.dados = dados;
            this.esq = null;
            this.dir = null;
            altura = 0;
        }
        public NoArvore(Tipo dados, NoArvore<Tipo> esquerdo, NoArvore<Tipo> direito,
                int altura)
        {
            this.dados = dados;
            this.esq = esquerdo;
            this.dir = direito;
            this.altura = altura;
        }
        public int CompareTo(NoArvore<Tipo> o)
        {
            return dados.CompareTo(o.dados);
        }
        public bool Equals(NoArvore<Tipo> o)
        {
            return this.dados.Equals(o.dados);
        }
    }
}
