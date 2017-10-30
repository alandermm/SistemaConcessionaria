using System;
public class Menu{
    public void mostrarMenuPrincipal(){
        int opt;
        do {
            Console.WriteLine("Escola uma das opções abaixo\n"
                    + "1 - Cadastrar Cliente\n"
                    + "2 - Cadastrar Carro\n"
                    + "3 - Vender Carro\n"
                    + "4 - Listar Carros Vendidos no dia\n"
                    + "0 - Sair\n");
            Console.Write("Opção: ");
            opt = 0;            
            do{
                opt = Int16.Parse(Console.ReadLine());
            } while (opt < 0 || opt > 6);
            switch(opt){
                case 0: Environment.Exit(0); break;
                case 1: 
                        string tipoDoc = mostrarMenuTipoCliente();
                        Pessoa pessoa = iniciarPessoa(tipoDoc);
                        Cadastro<Pessoa> cadastroCliente = new Cadastro<Pessoa>();
                        string arquivo = tipoDoc == "CPF" ? "PessoasFisicas.xlsx" : "PessoasJuridicas.xlsx";
                        cadastroCliente.salvar(AppDomain.CurrentDomain.BaseDirectory.ToString() + arquivo, pessoa);
                        break;

                case 2: Carro carro = new Carro();
                        carro.iniciarDados();
                        Cadastro<Carro> cadastroCarro = new Cadastro<Carro>();
                        cadastroCarro.salvar(AppDomain.CurrentDomain.BaseDirectory.ToString(), carro);
                        break;
                /*case 3: venderCarro(); break;
                case 4: listarCarroVendidoDia(); break;*/
            }
        } while(opt != 0);
    }
    private string mostrarMenuTipoCliente(){
        string tipoDoc;
        Console.WriteLine("Escolha o tipo do cliente:\n"
                    + "1 - Pessoa Física\n"
                    + "2 - Pessoa Jurídica\n");
        do{
            Console.Write("Opção: ");
            tipoDoc = Console.ReadLine();
        } while( tipoDoc != "1" && tipoDoc != "2");
        if (tipoDoc == "1"){
            return "CPF";
        } else {
            return "CNPJ";
        }
    }

    private Pessoa iniciarPessoa(String tipoDoc){
        Pessoa pessoa = new Pessoa();
        Console.Write("Nome do Cliente: ");
        pessoa.setNome(Console.ReadLine());
        Console.Write("Email do cliente: ");
        pessoa.setEmail(Console.ReadLine());
        Console.Write(tipoDoc + " do cliente: ");
        if(tipoDoc == "CPF")
            pessoa.setDocumento(new Validacao().pedirCPF());
        else 
            pessoa.setDocumento(new Validacao().pedirCNPJ());
        pessoa.setEndereco(iniciarEnredeco());
        return pessoa;
    }

    private Endereco iniciarEnredeco(){
        Endereco endereco = new Endereco();
        Console.Write("Rua: ");
        endereco.setRua(Console.ReadLine());
        Console.Write("Número: ");
        endereco.setNumero(Int16.Parse(Console.ReadLine()));
        Console.Write("Bairro: ");
        endereco.setBairro(Console.ReadLine());
        return endereco;
    }

    private Carro iniciarCarro(){
        Carro carro = new Carro();
        Console.Write("Marca do carro: ");
        carro.marca = Console.ReadLine();
        Console.Write("Modelo do carro: ");
        carro.modelo = Console.ReadLine();
        Console.Write("Cor do carro: ");
        carro.cor = Console.ReadLine();
        Console.Write("O carro é 0Km? (s, n) : ");
        if(Console.ReadLine().Substring(0,1).ToUpper() == "S"){
            carro.novo = true;
            carro.kilometragem = 0;
        } else {
            carro.novo = false;
            Console.Write("Kilometragem: ");
            carro.kilometragem = Int32.Parse(Console.ReadLine());
            Console.Write("placa: ");
            carro.placa = Console.ReadLine();
        }
        carro.disponivel = true;
        return carro;
    }
}