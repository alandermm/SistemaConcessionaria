using System;
public class Pessoa{
    public string nome {get; set;}
    public string email {get; set;}
    public string cpf {get; set;}
    public Endereco endereco {get; set;}
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