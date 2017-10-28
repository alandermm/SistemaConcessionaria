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
            cadastro.CriarExcel(@"C:\Users\01317235614\Desktop\Orientacao\SistemaConcessionaria\pessoas.xlsx", p1);

        }
    }
}
