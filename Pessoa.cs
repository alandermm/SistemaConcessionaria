using System;
public class Pessoa{
    private String nome {get; set;}
    private String email {get; set;}
    private String cpf {get; set;}
    private DateTime dataCadastro {get; set;}
    private Endereco endereco {get; set;}
    //private String[] dados;

    public void iniciarDados(){
        Console.Write("Nome do Cliente: ");
        this.nome = Console.ReadLine();
        Console.Write("Email do cliente: ");
        this.email = Console.ReadLine();
        Console.Write("CPF do cliente: ");
        this.cpf = Console.ReadLine();
        this.endereco = new Endereco();
        Console.Write("Rua: ");
        this.endereco.rua = Console.ReadLine();
        Console.Write("Número: ");
        this.endereco.numero = Int16.Parse(Console.ReadLine());
        Console.Write("Bairro: ");
        this.endereco.bairro = Console.ReadLine();
    }


}