using System;
using System.Collections;
using NetOffice.ExcelApi;
public class Cadastro<T>{
    private DateTime dataCadastro {get; set;}

    public void CriarExcel(String arquivo, T registro){
        Application ex = new Application();
        ex.Workbooks.Add();
        var propriedades = registro.GetType().GetProperties();
        int ultimaLinha = getUltimaLinha();
        int campo = 1;
        foreach(var propriedade in propriedades){
            ex.Cells[ultimaLinha, campo].Value = propriedade.GetValue(typeof(String));
            campo++;
        }
        ex.ActiveWorkbook.SaveAs(arquivo);
        ex.Quit();
    }

    public void LerExcel(){
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
        } while (ex.Cells[contador,1].Value != null);
        ex.Quit();
        return contador;
    }
}