using System;
using System.IO;
using NetOffice.ExcelApi;
public class Carro{
    public int codigo {get; set;}
    public string marca {get; set;}
    public string modelo {get; set;}
    public string cor {get; set;}
    public int kilometragem {get; set;}
    public string placa {get; set;}
    public bool novo {get; set;}
    public bool disponivel {get; set;}
    //public string chassi {get; set;}
    public OpcionaisCarro opcionais;
    public double valor;
    public void iniciarDados(){
        string arquivo = Directory.GetCurrentDirectory() + "\\Carros.xlsx";
        this.codigo = (new Cadastro().getUltimaLinha(arquivo)) - 1;
        Console.Write("Marca do carro: ");
        this.marca = Console.ReadLine();
        Console.Write("Modelo do carro: ");
        this.modelo = Console.ReadLine();
        Console.Write("Cor do carro: ");
        this.cor = Console.ReadLine();
        Console.Write("O carro é 0Km? (s, n) : ");
        if(Console.ReadLine().Substring(0,1).ToUpper().Equals("S")){
            this.novo = true;
            this.kilometragem = 0;
        } else {
            this.novo = false;
            Console.Write("Kilometragem: ");
            this.kilometragem = Int32.Parse(Console.ReadLine());
            Console.Write("placa: ");
            this.placa = Console.ReadLine();
        }
        this.opcionais = new OpcionaisCarro();
        Console.Write("Tem ar condicionado? (s, n) :");
        this.opcionais.arCondicionado = Console.ReadLine().Substring(0,1).ToUpper().Equals("S") ? true : false;

        Console.Write("Tem direção hidráulica? (s, n) :");
        this.opcionais.direcaoHidraulica = Console.ReadLine().Substring(0,1).ToUpper().Equals("S") ? true : false;

        Console.Write("Tem alarme? (s, n) :");
        this.opcionais.alarme = Console.ReadLine().Substring(0,1).ToUpper().Equals("S") ? true : false;

        Console.Write("Tem trava elétrica? (s, n) :");
        this.opcionais.travaEletrica = Console.ReadLine().Substring(0,1).ToUpper().Equals("S") ? true : false;

        Console.Write("Tem som? (s, n) :");
        this.opcionais.som = Console.ReadLine().Substring(0,1).ToUpper().Equals("S") ? true : false;

        Console.Write("Digite o Valor do Carro: ");
        this.valor = double.Parse(Console.ReadLine());
        
        this.disponivel = true;    
    }

    public void salvar(String arquivo){
        Application ex = new Application();
        int ultimaLinha = new Cadastro().getUltimaLinha(arquivo);
        ex.Workbooks.Open(arquivo);
        ex.Cells[ultimaLinha, 1].Value = this.codigo;
        ex.Cells[ultimaLinha, 2].Value = this.marca;
        ex.Cells[ultimaLinha, 3].Value = this.modelo;
        ex.Cells[ultimaLinha, 4].Value = this.cor;
        ex.Cells[ultimaLinha, 5].Value = this.novo ? "Sim" : "Não";
        ex.Cells[ultimaLinha, 6].Value = this.kilometragem;
        ex.Cells[ultimaLinha, 7].Value = this.placa;
        ex.Cells[ultimaLinha, 8].Value = this.opcionais.arCondicionado ? "Sim" : "Não";
        ex.Cells[ultimaLinha, 9].Value = this.opcionais.direcaoHidraulica ? "Sim" : "Não";
        ex.Cells[ultimaLinha, 10].Value = this.opcionais.alarme ? "Sim" : "Não";
        ex.Cells[ultimaLinha, 11].Value = this.opcionais.travaEletrica ? "Sim" : "Não";
        ex.Cells[ultimaLinha, 12].Value = this.opcionais.som ? "Sim" : "Não";
        ex.Cells[ultimaLinha, 13].Value = this.disponivel ? "Sim" : "Não";
        ex.Cells[ultimaLinha, 14].Value = this.valor; 
        ex.Cells[ultimaLinha, 15].Value = DateTime.Now;
        ex.ActiveWorkbook.Save();
        ex.ActiveWorkbook.Close();
        ex.Quit();
        ex.Dispose();
    }

    public Carro carregarCarro(int codigoCarro){
        String arquivo = Directory.GetCurrentDirectory() + "\\Carros.xlsx";
        Application ex = new Application();
        ex.Workbooks.Open(arquivo);
        Carro carro = new Carro();
        int linha = codigoCarro + 1;
        /*while(!ex.Cells[linha, 1].Value.ToString().Contains(codigoCarro.ToString()) && ex.Cells[linha,1].Value != null ){
            linha++;
        }*/
        carro.codigo = Int16.Parse(ex.Cells[linha, 1].Value.ToString());
        carro.marca = ex.Cells[linha, 2].Value.ToString();
        carro.modelo = ex.Cells[linha, 3].Value.ToString();
        carro.cor = ex.Cells[linha, 4].Value.ToString();
        carro.novo = ex.Cells[linha, 5].Value.ToString().Equals("Sim")? true : false;
        carro.kilometragem = Int32.Parse(ex.Cells[linha, 6].Value.ToString());
        carro.placa = ex.Cells[linha, 7].Value.ToString();
        carro.opcionais = new OpcionaisCarro();
        carro.opcionais.arCondicionado = ex.Cells[linha, 8].Value.ToString().Equals("Sim")? true : false;
        carro.opcionais.direcaoHidraulica = ex.Cells[linha, 9].Value.ToString().Equals("Sim")? true : false;
        carro.opcionais.alarme = ex.Cells[linha, 10].Value.ToString().Equals("Sim")? true : false;
        carro.opcionais.travaEletrica = ex.Cells[linha, 11].Value.ToString().Equals("Sim")? true : false;
        carro.opcionais.som = ex.Cells[linha, 12].Value.ToString().Equals("Sim")? true : false;
        carro.disponivel = ex.Cells[linha, 13].Value.ToString().Equals("Sim")? true : false;
        carro.valor = double.Parse(ex.Cells[linha, 14].Value.ToString());
        ex.ActiveWorkbook.Close();
        ex.Quit();
        ex.Dispose();
        return carro;
    }

    public void vender(int codigo){
        Application ex = new Application();
        String arquivo = Directory.GetCurrentDirectory() + "\\Carros.xlsx";
        ex.Workbooks.Open(arquivo);
        ex.Cells[codigo + 1, 13].Value = "Não";
        ex.ActiveWorkbook.Save();
        ex.ActiveWorkbook.Close();
        ex.Quit();
        ex.Dispose();
    }
    
}