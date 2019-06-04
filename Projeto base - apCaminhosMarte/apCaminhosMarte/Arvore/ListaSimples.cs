using System;
using System.Windows.Forms;
//using System.Windows.Forms;

public class ListaSimples<Dado> where Dado : IComparable<Dado>
{
    private NoLista<Dado> primeiro, ultimo; // ou sem private 
    private int quantos;

    public ListaSimples()
	{
        primeiro = ultimo = null;
        quantos = 0;
	}

    public NoLista<Dado> Primeiro { get => primeiro; }
    public NoLista<Dado> Ultimo { get => ultimo; }
    public int Quantos { get => quantos; }

    public bool EstaVazia
    {
        get => primeiro == null;
    }

    private void InserirInicio(NoLista<Dado> no) // ou void apenas
    {
        if (no != null)
        {
            if (EstaVazia)
                ultimo = no;
            no.Prox = primeiro;
            primeiro = no;
            quantos++;
        }
    }

    private void InserirInicio(Dado informacao)
    {
        if(informacao != null)
        {
            NoLista<Dado> novoNo = new NoLista<Dado>(informacao, null);
            InserirInicio(novoNo);
        }
    }

    private void InserirFim(NoLista<Dado> no)
    {
        if (no != null)
        {
            if (EstaVazia)
                primeiro = no;
            else
                ultimo.Prox = no;
            no.Prox = null;
            ultimo = no;
            quantos++;
        }
    }

    public void InserirFim(Dado informacao)
    {
        if (informacao != null)
        {
            NoLista<Dado> novoNo = new NoLista<Dado>(informacao, null);
            InserirFim(novoNo);
        }
    }

    public bool ExisteDado(Dado procurado, ref NoLista<Dado> atual, ref NoLista<Dado> anterior)
    {
        if (EstaVazia)
            return false;        
        
        for(atual = primeiro; atual != ultimo; anterior = atual, atual = atual.Prox)
            if(atual.Info.Equals(procurado))  
                return true;
       
        return false;
    }

    public bool RemoverNo(Dado dados)
    {
        if (EstaVazia)
            return false;

        NoLista<Dado> atual = null, anterior = null;

        if (ExisteDado(dados, ref atual, ref anterior))
        {
            RemoverNo(atual, anterior);
            return true;
        }

        return false;
    }

    public void RemoverNo(NoLista<Dado> atual, NoLista<Dado> anterior)
    {
        if(anterior == null)
        {
            primeiro = atual.Prox;
            if (primeiro == null)
                ultimo = null;
        }
        else
        {
            anterior.Prox = atual.Prox;
            if (atual.Prox == null)
                ultimo = anterior;
        }
        quantos--;
    }

    // métodos merdas de vagabundos

    public void Listar(ListBox onde)
    {
        onde.Items.Clear();

        NoLista<Dado> atual = primeiro;
        for (int i = 0; i < quantos; i++)
        {
            onde.Items.Add(atual.Info);
            atual = atual.Prox;
        }
        /*
         * ou do jeito vagabundo
         * NoLista<Dado> atual = null;
         * for(atual = primeiro; atual != null; atual = atual.Prox)
         * {
         *      onde.Items.Add(atual.Info);
         * }
         */
    }

    public Dado[] ToArray()
    {
        NoLista<Dado> atual = primeiro;
        Dado[] ret = new Dado[quantos];
        
        for(int i = 0; i <= quantos; i++, atual = atual.Prox)
        {
            ret[i] = atual.Info;
        }

        return ret;
    }
}
