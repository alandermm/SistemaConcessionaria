using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Classe Menu
/// </summary>
public class Menu{
    /// <summary>
    /// Método para mostrar o menu principal
    /// </summary>
    public void mostrarMenuPrincipal(){
        String path = Directory.GetCurrentDirectory() + "\\";
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
                        string arquivo;
                        arquivo = tipoDoc.Equals("CPF") ? path + "PessoasFisicas.xlsx" : path + "PessoasJuridicas.xlsx";
                        if (!File.Exists(arquivo)){
                            if(!File.Exists(arquivo) || new Cadastro().getUltimaLinha(arquivo) == 1){
                                String[] cabecalho = new String[]{"Documento", "Nome", "E-mail", "Rua", "Número", "Bairro", "Data Cadastro"};
                                new Cadastro().gerarCabecalho(arquivo, cabecalho);
                            }
                        }
                        pessoa.salvar(arquivo);
                        break;
                case 2: Carro carro = new Carro();
                        arquivo = path + "Carros.xlsx";
                        if (!File.Exists(arquivo)){
                            if(!File.Exists(arquivo) || new Cadastro().getUltimaLinha(arquivo) == 1){
                                String[] cabecalho = new String[]{"Código", "Marca", "Modelo", "Cor", "Novo", "Kilometragem", "Placa",
                                                                "Ar Condicionado", "Alarme", "Direção Hidráulica", "Trava Elétrica" ,
                                                                "Som", "Disponível", "Valor", "Data Cadastro"
                                };
                                new Cadastro().gerarCabecalho(arquivo, cabecalho);
                            }
                        }
                        carro.iniciarDados();
                        carro.salvar(arquivo);
                        break;
                case 3: Venda venda = new Venda();
                        arquivo = path + "Vendas.xlsx";
                        string arquivoCarro = path + "Carros.xlsx";
                        List<int> codigos = venda.listarCarrosDisponiveis();
                        int codigoCarro = mostrarMenuSelecionarCarro(codigos);
                        if(codigoCarro > 0){
                            venda.carro = new Carro().carregarCarro(codigoCarro);
                            venda.pagamento = mostrarMenuSelecionarCondicaoPagamento();
                            if(venda.pagamento.Equals("Parcelado")){
                                int parcelas = 0;
                                do{
                                    Console.Write("Digite a quantidade de parcelas (2 a 60): ");
                                    parcelas = Int16.Parse(Console.ReadLine());
                                }while(parcelas < 2 || parcelas > 60 );
                                venda.parcelas = parcelas;
                                venda.valorVenda = venda.carro.valor;
                                venda.valorParcela = venda.valorVenda / venda.parcelas;
                            } else {
                                venda.parcelas = 1;
                                venda.valorVenda = venda.carro.valor * 0.95;
                                venda.valorParcela = venda.valorVenda;
                            }
                            //Selecionar Cliente
                            tipoDoc = mostrarMenuTipoCliente();
                            string arquivoCliente = tipoDoc.Equals("CPF")? path + "PessoasFisicas.xlsx" : path + "PessoasJuridicas.xlsx";
                            string doc = tipoDoc.Equals("CPF") ? new Validacao().pedirCPF() : new Validacao().pedirCNPJ();
                            venda.cliente = new Pessoa().carregarPessoa(Int64.Parse(doc) , arquivoCliente);
                            if (!File.Exists(arquivo)){
                                if(!File.Exists(arquivo) || new Cadastro().getUltimaLinha(arquivo) == 1){
                                    String[] cabecalho = new String[]{"Código Carro", "Documento Cliente", "Pagamento", "Parcelas", "Valor Carro", "Valor Venda", "Data Venda"};
                                    new Cadastro().gerarCabecalho(arquivo, cabecalho);
                                }
                            }
                            venda.salvar(arquivo);
                        } else {
                            Console.WriteLine("No momento não existem carros disponíveis para venda!");
                        }
                        break;
                case 4: new Venda().listarCarrosVendidosDia(); break;
            }
        } while(opt != 0);
    }

    /// <summary>
    /// Método para mostrar o Menu Tipo do Cliente
    /// </summary>
    /// <returns>Retorna "CPF" para pessoas físicas ou "CNPJ" para pessoas jurídicas</returns>
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

    /// <summary>
    /// Método para mostrar o menu para selecionar o carro para venda
    /// </summary>
    /// <param name="resultado">Lista dos códigos dos carros disponíveis para venda</param>
    /// <returns>retorna inteiro com o código do carro</returns>
    private int mostrarMenuSelecionarCarro(List<int> resultado){
        int opt;
        if(resultado.Count != 0){
            do{
                Console.Write("Digite o código do carro: ");
                opt = Int16.Parse(Console.ReadLine());
            }while(!resultado.Contains(opt));
            return opt;
        }
        return 0;
    }

    /// <summary>
    /// Método para mostrar o menu para seleção da condição de pagamento
    /// </summary>
    /// <returns>Retorna "À Vista" ou "Parcelado"</returns>
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