using System;
using System.Collections;
using System.Reflection;
using System.IO;
using NetOffice.ExcelApi;
public class Cadastro<T>{
    private DateTime dataCadastro {get; set;}

    private void gerarCabecalho(String arquivo, T registro){
        Application ex = new Application();
        bool existeArquivo = File.Exists(arquivo);
        if(!existeArquivo){
            ex.Workbooks.Add();
        } else {
            ex.Workbooks.Open(arquivo);
        }
        int campo = 1;
        if(!File.Exists(arquivo) || getUltimaLinha(arquivo) == 1){
            foreach(var item in registro.GetType().GetProperties()){
                ex.Cells[1, campo].Value = item.Name;
                campo++;
            }
            ex.Cells[1, campo].Value = "Data Cadastro";
        }
        if(existeArquivo){
            ex.ActiveWorkbook.Save();
        } else {
            ex.ActiveWorkbook.SaveAs(arquivo);
        }
        ex.Quit();
    }

    public void salvar(String arquivo, T registro){
        Application ex = new Application();
        if(!File.Exists(arquivo) || getUltimaLinha(arquivo) == 1){
            gerarCabecalho(arquivo, registro);
        }
        ex.Workbooks.Open(arquivo);
        int ultimaLinha = getUltimaLinha(arquivo);
        int campo = 1;
        foreach(var reg in registro.GetType().GetProperties()){
            var valor = reg.GetValue(registro, null);
            if(reg.PropertyType.IsClass &&  reg.PropertyType.Name != "String" && reg.PropertyType.Name != "string") {
                foreach(var val in valor.GetType().GetProperties()){
                    ex.Cells[ultimaLinha, campo].Value = val.GetValue(valor, null);
                    campo++;
                }
            } else {
                ex.Cells[ultimaLinha, campo].Value = valor;
                campo++;
            }
        }
        ex.Cells[ultimaLinha, campo].Value = DateTime.Now;
        ex.ActiveWorkbook.Save();
        ex.Quit();
    }

    public void ler(String arquivo, string busca){
        if(File.Exists(arquivo)){
            Application ex = new Application();
            ex.Workbooks.Open(arquivo);
            /*string valor = ex.Cells[1,2].Value.ToString();
            Console.WriteLine(valor);*/
            ex.Quit();
        } else {
            Console.WriteLine("O arquivo " + arquivo + " não foi encontrado!");
        }
    }

    private static int getUltimaLinha(String arquivo){
        int contador = 0;
        Application ex = new Application();
        if(File.Exists(arquivo)){
            ex.Workbooks.Open(arquivo);
            do{
                contador++;
            } while (ex.Cells[contador,1].Value != null);
            ex.Quit();
        } /*else {
            contador = 1;
        }*/
        return contador;
    }
}