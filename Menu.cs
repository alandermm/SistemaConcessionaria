using System;
using System.Collections;
public class Menu{
    public void mostrarMenuPrincipal(){
        String path = AppDomain.CurrentDomain.BaseDirectory.ToString();
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
                        Pessoa pessoa = new Pessoa();
                        string tipoDoc = mostrarMenuTipoCliente();
                        pessoa.iniciarDados(tipoDoc);
                        Cadastro<Pessoa> cadastroCliente = new Cadastro<Pessoa>();
                        string arquivo;
                        arquivo = tipoDoc.Equals("CPF") ? "PessoasFisicas.xlsx" : "PessoasJuridicas.xlsx";
                        cadastroCliente.salvar(path + arquivo, pessoa);
                        break;

                case 2: Carro carro = new Carro();
                        carro.iniciarDados();
                        Cadastro<Carro> cadastroCarro = new Cadastro<Carro>();
                        cadastroCarro.salvar(path + "carros.xlsx", carro);
                        break;
                case 3: Venda venda = new Venda();
                        ArrayList resultado;
                        Cadastro<Venda> cadastroVenda = new Cadastro<Venda>();
                        resultado = cadastroVenda.buscar(path + "carros.xlsx", "disponivel".ToUpper(), "true");
                        int codigoCarro = mostrarMenuSelecionarCarro(resultado);
                        Carro carroVendido = new Carro();
                        carroVendido = cadastroVenda.carregarObjeto(codigoCarro, path + "carros.xlsx", carroVendido);
                        venda.pagamento = mostrarMenuSelecionarCondicaoPagamento();
                        if(venda.pagamento.Equals("Parcelado")){
                            int parcela = 0;
                            do{
                                Console.Write("Digite a quantidade de parcelas (2 a 60): ");
                                parcela = Int16.Parse(Console.ReadLine());
                            }while(parcela > 1 && parcela < 60 );
                            venda.parcelas = parcela;
                        } else {
                            venda.parcelas = 1;
                            
                        }

                        break;
                /*case 4: listarCarroVendidoDia(); break;*/
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
        } while( !tipoDoc.Equals("1") && !tipoDoc.Equals("2"));
        return tipoDoc.Equals("1") ? "CPF" : "CNPJ";
    }

    private int mostrarMenuSelecionarCarro(ArrayList resultado){
        int opt;
        do{
            Console.Write("Digite o código do carro: ");
            opt = Int16.Parse(Console.ReadLine());
        }while(!resultado.Contains(opt));
        return opt;
    }

    private String mostrarMenuSelecionarCondicaoPagamento(){
        int opt;
        do{
            Console.WriteLine("Escolha a forma de pagamento:\n"
                    + "1 - À Vista\n"
                    + "2 - Parcelado\n");
            opt = Int16.Parse(Console.ReadLine());
        }while(opt != 1 && opt != 2);

        
        return opt == 1 ? "À Vista" : "Parcelado";
    }
}