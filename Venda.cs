using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using NetOffice.ExcelApi;
public class Venda{
    //public bool parcelado {get; set;}
    public String pagamento {get; set;}
    public int parcelas {get; set;}
    public Pessoa cliente {get; set;}
    public Carro carro {get; set;}
    public double valorVenda {get; set;}
    public double valorParcela {get; set;}

    public List<int> listarCarrosDisponiveis(){
        String arquivo = Directory.GetCurrentDirectory() + "\\Carros.xlsx";
        List<int> codigos = new List<int>();
        Application ex = new Application();
        if (File.Exists(arquivo)){
            ex.Workbooks.Open(arquivo);
            int count = 0, linha = 1, campo = 1;
            while(ex.Cells[1, campo].Value != null){
                Console.Write(ex.Cells[1, campo].Value.ToString() + " | ");
                campo++;
            }
            Console.WriteLine();
            while(ex.Cells[linha, 1].Value != null){
                if(ex.Cells[linha, 13].Value.ToString().Equals("Sim")){
                    codigos.Add(Int16.Parse(ex.Cells[linha, 1].Value.ToString()));
                    campo = 1;
                    count++;
                    while(ex.Cells[linha, campo].Value != null){
                        Console.Write(ex.Cells[linha, campo].Value.ToString() + " | ");
                        campo++;
                    }
                    Console.WriteLine();
                }
                linha++;
            }
            Console.WriteLine("\n" + count + " Carros disponíveis." + "\n\n");
            ex.ActiveWorkbook.Close();
            ex.Quit();
            ex.Dispose();
            return codigos;
        } else {
            Console.WriteLine("O arquivo " + arquivo + " não foi encontrado.\n\n");
            return null;
        }
    }

    public void listarCarrosVendidosDia(){
        String arquivo = Directory.GetCurrentDirectory() + "\\Carros.xlsx";
        Application ex = new Application();
        if (File.Exists(arquivo)){
            ex.Workbooks.Open(arquivo);
            int count = 0, linha = 1, campo = 1;
            while(ex.Cells[1, campo].Value != null){
                Console.Write(ex.Cells[1, campo].Value.ToString() + " | ");
                campo++;
            }
            Console.WriteLine();
            while(ex.Cells[linha, 1].Value != null){
                DateTime data = DateTime.Parse(ex.Cells[linha, 15].Value.ToString());
                data.ToShortDateString();
                if(ex.Cells[linha, 15].Value.ToString().Equals("Sim")){
                    codigos.Add(Int16.Parse(ex.Cells[linha, 1].Value.ToString()));
                    campo = 1;
                    count++;
                    while(ex.Cells[linha, campo].Value != null){
                        Console.Write(ex.Cells[linha, campo].Value.ToString() + " | ");
                        campo++;
                    }
                    Console.WriteLine();
                }
                linha++;
            }
            Console.WriteLine("\n" + count + " Carros disponíveis." + "\n\n");
            ex.ActiveWorkbook.Close();
            ex.Quit();
            ex.Dispose();
        } else {
            Console.WriteLine("O arquivo " + arquivo + " não foi encontrado.\n\n");
        }
    }

    public void salvar(String arquivo){
        Application ex = new Application();
        int ultimaLinha = new Cadastro().getUltimaLinha(arquivo);
        ex.Workbooks.Open(arquivo);
        ex.Cells[ultimaLinha, 1].Value = this.carro.codigo;
        ex.Cells[ultimaLinha, 2].Value = this.cliente.documento;
        ex.Cells[ultimaLinha, 3].Value = this.pagamento;
        ex.Cells[ultimaLinha, 4].Value = this.parcelas;
        ex.Cells[ultimaLinha, 5].Value = this.valorParcela;
        ex.Cells[ultimaLinha, 6].Value = this.carro.valor;
        ex.Cells[ultimaLinha, 7].Value = this.valorVenda;
        ex.Cells[ultimaLinha, 8].Value = DateTime.Now;
        ex.ActiveWorkbook.Save();
        ex.ActiveWorkbook.Close();
        ex.Quit();
        ex.Dispose();
        this.carro.vender(this.carro.codigo);
    }
}