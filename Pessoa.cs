using System;
public class Pessoa{
    public string nome {get; set;}
    public string email {get; set;}
    public string documento {get; set;}
    public Endereco endereco {get; set;}
    //private String[] dados;
    public void iniciarDados(String tipoDoc){
        Console.Write("Nome do Cliente: ");
        this.nome = Console.ReadLine();
        Console.Write("Email do cliente: ");
        this.email = Console.ReadLine();
        Console.Write(tipoDoc + " do cliente: ");
        Validacao documento = new Validacao();
        if(tipoDoc == "CPF"){
            this.documento = documento.pedirCPF();
        } else {
            this.documento = documento.pedirCNPJ();
        }
        //this.documento = Console.ReadLine();
        this.endereco = new Endereco();
        Console.Write("Rua: ");
        this.endereco.rua = Console.ReadLine();
        Console.Write("Número: ");
        this.endereco.numero = Int16.Parse(Console.ReadLine());
        Console.Write("Bairro: ");
        this.endereco.bairro = Console.ReadLine();
    }
}