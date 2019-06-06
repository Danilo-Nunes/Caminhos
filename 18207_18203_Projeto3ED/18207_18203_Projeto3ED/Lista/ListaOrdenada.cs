using System;
using System.Windows.Forms;
//using System.Windows.Forms;

public class ListaOrdenada<Dado> where Dado : IComparable<Dado>
{
    private NoLista<Dado> primeiro, ultimo; // ou sem private 
    private int quantos;

    public ListaOrdenada()
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
            //quantos++;
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
            //quantos++;
        }
    }

    private void InserirFim(Dado informacao)
    {
        if (informacao != null)
        {
            NoLista<Dado> novoNo = new NoLista<Dado>(informacao, null);
            InserirFim(novoNo);
        }
    }

    private void InserirMeio(NoLista<Dado> no, NoLista<Dado> atual, NoLista<Dado> anterior)
    {
        if (no != null)
        {
            anterior.Prox = no;
            no.Prox = atual;
            if (anterior == ultimo)
                ultimo = no;
            //quantos++;
        }
    }
    
    public void InserirOrdenado(Dado informacao)
    {
        NoLista<Dado> atual = null, anterior = null;

        if(!ExisteDado(informacao, ref atual, ref anterior))
        {
            if (anterior == null)
                InserirInicio(informacao);
            else
            if (anterior == ultimo)
                InserirFim(informacao); // ou só null mesmo
            else
            {
                NoLista<Dado> no = new NoLista<Dado>(informacao, null);
                InserirMeio(no, atual, anterior);
            }
            quantos++;
        }
    }

    public bool ExisteDado(Dado procurado, ref NoLista<Dado> atual, ref NoLista<Dado> anterior)
    {
        atual = primeiro;

        if (EstaVazia)
            return false;

        if (procurado.CompareTo(atual.Info) < 0)
        {
            atual = null;
            //anterior = null; já é nulo
            return false;
        }

        if (procurado.CompareTo(ultimo.Info) > 0)
        {
            atual = null;
            anterior = ultimo;
            return false;
        }
       
        while(procurado.CompareTo(atual.Info) > 0 && atual != null)
        {
            if(procurado.CompareTo(atual.Info) == 0)
                return true;
            anterior = atual;
            atual = atual.Prox;            
        }
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

    private void RemoverNo(NoLista<Dado> atual, NoLista<Dado> anterior)
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
}
