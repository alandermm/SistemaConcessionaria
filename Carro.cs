using System;
public class Carro{
    public string marca {get; set;}
    public string modelo {get; set;}
    public string cor {get; set;}
    public int kilometragem {get; set;}
    public string placa {get; set;}
    //public string chassi {get; set;}
    public OpcionaisCarro opcionais;

    public void iniciarDados(){
        Console.Write("Marca do carro: ");
        this.marca = Console.ReadLine();
        Console.Write("Modelo do carro: ");
        this.modelo = Console.ReadLine();
        Console.Write("Cor do carro: ");
        this.cor = Console.ReadLine();
        Console.Write("Kilometragem: ");
        this.kilometragem = Int32.Parse(Console.ReadLine());
        Console.Write("placa: ");
        this.placa = Console.ReadLine();
    }
}