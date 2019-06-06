using System;
/*
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

não são nescessários, e se forem escritos, ocupam mais espaço devido aos imports
*/

public class NoLista<X> where X : IComparable<X> // X poderia se chamar Dado, e ele deverá implementar o compareTo
{ 
    // quando não tem nada na frente em c#,é private
    NoLista<X> prox;
    X info;

    public NoLista(X info, NoLista<X> prox)
    {
        // usamos a propriedade para que ela ja verfique, evitando códigos a mais
        Info = info;
        Prox = prox;
    }

    public X Info
    {
        get { return info; }
        set
        {
            if (value != null)
                info = value; // cuidado para n por o mesmo nome da propriedade, senão dá loop, o c# n reconhece isso
        }
    }

    public NoLista<X> Prox
    {
        get => prox;
        set => prox = value;
    }
}