using System;

namespace SistemaConcessionaria
{
    class Program
    {
        static void Main(string[] args)
        {
            Pessoa p1 = new Pessoa();
            p1.iniciarDados();
            Cadastro<Pessoa> cadastro = new Cadastro<Pessoa>();
            cadastro.salvar(@"c:\Users\alander\CodeXP\SistemaConcessionaria\pessoas.xlsx", p1);

            /*Carro c1 = new Carro();
            c1.iniciarDados();
            Cadastro<Carro> cadastro = new Cadastro<Carro>();
            cadastro.salvar(@"c:\Users\alander\CodeXP\SistemaConcessionaria\carros.xlsx", c1);*/
        }
    }
}