using System;
public class Carro{
    public string marca {get; set;}
    public string modelo {get; set;}
    public string cor {get; set;}
    public int kilometragem {get; set;}
    public string placa {get; set;}
    public bool novo {get; set;}
    public bool disponivel {get; set;}
    //public string chassi {get; set;}
    public OpcionaisCarro opcionais;
    public void iniciarDados(){
        Console.Write("Marca do carro: ");
        this.marca = Console.ReadLine();
        Console.Write("Modelo do carro: ");
        this.modelo = Console.ReadLine();
        Console.Write("Cor do carro: ");
        this.cor = Console.ReadLine();
        Console.Write("O carro é 0Km? (s, n) : ");
        if(Console.ReadLine().Substring(0,1).ToUpper() == "S"){
            this.novo = true;
            this.kilometragem = 0;
        } else {
            this.novo = false;
            Console.Write("Kilometragem: ");
            this.kilometragem = Int32.Parse(Console.ReadLine());
            Console.Write("placa: ");
            this.placa = Console.ReadLine();
        }
        this.disponivel = true;    
    }
}