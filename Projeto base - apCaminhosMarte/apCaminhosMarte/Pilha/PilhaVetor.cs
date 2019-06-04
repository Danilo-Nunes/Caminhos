using System;

public class PilhaVetor<Dado> : IStack<Dado> where Dado : IComparable<Dado>
{
  Dado[] P;
  int topo;

  int posicoes;

  const int MAXIMO = 500;

  public PilhaVetor(int posic)
  {
    posicoes = posic;
    P = new Dado[posicoes];
  }

  public PilhaVetor() : this(MAXIMO)
  {
  }

  public void Empilhar(Dado elemento)
  {
    if (Tamanho() == posicoes)
      throw new PilhaCheiaException("Overflow - pilha cheia");

    topo++;
    P[topo] = elemento;
  }

  public Dado Desempilhar()
  {
    if (EstaVazia())
      throw new PilhaVaziaException("Underflow - pilha vazia");

    Dado elemento = P[topo];
    P[topo] = default(Dado);
    topo--;
    return elemento;
  }

  public Dado OTopo()
  {
    if (EstaVazia())
      throw new PilhaVaziaException("Underflow - pilha vazia");

    Dado elemento = P[topo];
    return elemento;
  }

  public bool EstaVazia()
  {
    return topo < 0;
  }

  public int Tamanho()
  {
    return topo+1;
  }
}
