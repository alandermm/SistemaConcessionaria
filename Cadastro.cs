using System;
using System.Collections;
using System.Reflection;
using System.IO;
using NetOffice.ExcelApi;
public class Cadastro<T>{
    private DateTime dataCadastro {get; set;}

    public void gerarCabecalho(String arquivo, T registro){
        Application ex = new Application();
        ex.Workbooks.Open(arquivo);
        int campo = 1;
        if(getUltimaLinha(arquivo) == 1){
            foreach(var item in registro.GetType().GetProperties()){
                ex.Cells[1, campo].Value = item.Name;
                campo++;
            }
        }
        ex.ActiveWorkbook.SaveAs(arquivo);
        ex.Quit();
    }

    public void salvar(String arquivo, T registro){
        Application ex = new Application();
        if(!File.Exists(arquivo)){
            ex.Workbooks.Add();
            gerarCabecalho(arquivo, registro);
        } else {
            ex.Workbooks.Open(arquivo);
        }
        //int ultimaLinha = getUltimaLinha();
        int campo = 1;
        foreach(var reg in registro.GetType().GetProperties()){
            var valor = reg.GetValue(registro, null);
            if(reg.PropertyType.IsClass &&  reg.PropertyType.Name != "String" && reg.PropertyType.Name != "string") {
                foreach(var val in valor.GetType().GetProperties()){
                    ex.Cells[getUltimaLinha(arquivo), campo].Value = val.GetValue(valor, null);
                    campo++;
                }
            } else {
                ex.Cells[getUltimaLinha(arquivo), campo].Value = valor;
                campo++;
            }
        }
        ex.ActiveWorkbook.Save();
        ex.Quit();
    }

    public void LerExcel(){
        Application ex = new Application();
        ex.Workbooks.Open(@"C:\Users\01317235614\Desktop\Orientacao\excel\carros.xlsx");
        string valor = ex.Cells[1,2].Value.ToString();
        Console.WriteLine(valor);
        ex.Quit();
    }

    private static int getUltimaLinha(String arquivo){
        int contador = 0;
        Application ex = new Application();
        ex.Workbooks.Open(arquivo);
        do{
            contador++;
        } while (ex.Cells[contador,1].Value != null);
        ex.Quit();
        return contador;
    }
}