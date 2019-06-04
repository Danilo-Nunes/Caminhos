using System;

class FilaLista<Dado> : IQueue<Dado> where Dado : IComparable<Dado>, IGravarEmArquivo
{
  ListaSimples<Dado> lista; // lista a ser usada para representar a fila

  public FilaLista() // instancia a lista a ser usada
  {
    lista = new ListaSimples<Dado>();
  }

  public void Enfileirar(Dado elemento) // inclui objeto “elemento”
  {
    lista.InserirFim(elemento);
  }

  public Dado Retirar() // devolve objeto do início e o
  {                     // retira da fila
    if (!EstaVazia())
    {
      NoLista<Dado> first = lista.Primeiro;
      Dado elemento = first.Info;
      lista.RemoverNo(first, null);

      return elemento;
    }
    throw new FilaVaziaException("Fila vazia");
  }

  public Dado Primeiro() // devolve objeto do início sem retirá-lo da fila
  {                     
    if (EstaVazia())
      throw new FilaVaziaException("Fila vazia");

    Dado o = lista.Primeiro.Info; // acessa o 1o objeto genérico
    return o;
  }

  public Dado Ultimo() // devolve objeto do fim sem retirá-lo da fila
  {                  
    if (EstaVazia())
      throw new FilaVaziaException("Fila vazia");

    Dado o = lista.Ultimo.Info; // acessa o 1o objeto genérico
    return o;
  }

  public int Tamanho() // devolve número de elementos da fila
  {
    return lista.Quantos;  // tamanho da lista ligada usada
  }

  public bool EstaVazia() // retorna método interno da lista que indica se esta ou não vazia
  {
    return lista.EstaVazia;
  }

  public Dado[] ToArray() // retorna método interno da lista que a converte para vetor
  {
    return lista.ToArray();
  }
}