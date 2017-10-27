using System;
using System.Collections;
using NetOffice.ExcelApi;
public class Cadastro<T>{
    private DateTime dataCadastro {get; set;}

    static void CriarExcel(String arquivo, T registro){
        Application ex = new Application();
        ex.Workbooks.Add();
        var propriedades = registro.GetType().GetProperties();

        foreach(var propriedade in propriedades){
            ex.Cells[getUltimaLinha,1].Value = "Ford";
        }
        //
        ex.ActiveWorkbook.SaveAs(arquivo);
        ex.Quit();
    }

    static void LerExcel(){
        Application ex = new Application();
        ex.Workbooks.Open(@"C:\Users\01317235614\Desktop\Orientacao\excel\carros.xlsx");
        string valor = ex.Cells[1,2].Value.ToString();
        Console.WriteLine(valor);
        ex.Quit();
    }

    private static int getUltimaLinha(){
        int contador = 1;
        Application ex = new Application();
        ex.Workbooks.Add();
        do{
            contador++;
        } while (ex.Cells[contador,1] != null);
        ex.Quit();
        return contador;
    }
}