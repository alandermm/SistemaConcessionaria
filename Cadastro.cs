using System;
using System.Collections;
using System.Reflection;
using System.IO;
using NetOffice.ExcelApi;
public class Cadastro<T>{
    private int codigo {get; set;}
    private DateTime dataCadastro {get; set;}
    private void gerarCabecalho(String arquivo, T registro){
        Application ex = new Application();
        bool existeArquivo = File.Exists(arquivo);
        if(!existeArquivo){
            ex.Workbooks.Add();
        } else {
            ex.Workbooks.Open(arquivo);
        }
        int campo = 2;
        ex.Cells[1, 1].Value = "Código " + registro.GetType().Name; 
        if(!File.Exists(arquivo) || getUltimaLinha(arquivo) == 1){
            foreach(var item in registro.GetType().GetProperties()){
                if(item.PropertyType.IsClass &&  !item.PropertyType.Name.Equals("String")) {
                    var valor = item.GetValue(registro, null);
                    foreach(var val in valor.GetType().GetProperties()){
                        ex.Cells[1, campo].Value = val.Name.ToUpper();
                        campo++;
                    }
                } else {
                    ex.Cells[1, campo].Value = item.Name.ToUpper();
                    campo++;
                }
            }
            ex.Cells[1, campo].Value = "DATA CADASTRO";
        }
        if(existeArquivo){
            ex.ActiveWorkbook.Save();
        } else {
            ex.ActiveWorkbook.SaveAs(arquivo);
        }
        ex.Quit();
        ex.Dispose();
    }
    public void salvar(String arquivo, T registro){
        Application ex = new Application();
        if(!File.Exists(arquivo) || getUltimaLinha(arquivo) == 1){
            gerarCabecalho(arquivo, registro);
        }
        ex.Workbooks.Open(arquivo);
        int ultimaLinha = getUltimaLinha(arquivo);
        int campo = 2;
        this.codigo = ultimaLinha + 1;
        ex.Cells[ultimaLinha, 1].Value = this.codigo;
        foreach(var reg in registro.GetType().GetProperties()){
            var valor = reg.GetValue(registro, null);
            if(reg.PropertyType.IsClass &&  !reg.PropertyType.Name.Equals("String")) {
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
        ex.Dispose();
    }

    public Carro carregarObjeto(int codigo, String arquivo, Carro objeto){
        Application ex = new Application();
        ex.Workbooks.Open(arquivo);
        int linha = 2;
        int campo = 1;
        while(!ex.Cells[linha, 1].Value.ToString().Contains(codigo.ToString())){
            linha++;
        }
        foreach(var propriedade in objeto.GetType().GetProperties()){
            if(propriedade.PropertyType.IsClass &&  !propriedade.PropertyType.Name.Equals("String")) {
                foreach(var subPropriedade in propriedade.GetType().GetProperties()){
                    subPropriedade.SetValue(objeto, ex.Cells[linha, campo].Value);
                    campo++;
                }
            } else {
                propriedade.SetValue(objeto, ex.Cells[linha, campo].Value);
                campo++;
            }
        }
        return objeto;
    }

    public ArrayList buscar(String arquivo, String campo, String busca ){
        if(File.Exists(arquivo)){
            ArrayList codigos = new ArrayList();
            Application ex = new Application();
            ex.Workbooks.Open(arquivo);
            int numCampo = 1;
            String cabecalho = null, resultado = null;
            while(!ex.Cells[1,numCampo].Value.ToString().Equals(campo)){
                numCampo++;
            }
            int linha = 0;
            do{
                linha++;
                if(ex.Cells[linha, numCampo].Value.ToString().Equals(busca)){
                    numCampo = 1;
                    while(!ex.Cells[linha, numCampo].Value.Equals(null)){
                        if(numCampo == 1){
                            codigos.Add(ex.Cells[linha, numCampo].Value);
                        } 
                        resultado += ex.Cells[linha, numCampo].Value.ToString() + " | ";
                        numCampo++;
                    }
                    if(!resultado.Equals(null)){
                        resultado += "\n";
                    }
                }
            } while (ex.Cells[linha,1].Value != null);
            if(!resultado.Equals(null)){
                numCampo = 1;
                while(!ex.Cells[linha, numCampo].Value.Equals(null)){
                    cabecalho += ex.Cells[1, numCampo].Value.ToString() + " | ";
                    numCampo++;
                }
                Console.WriteLine("Resultado(s) encontrado(s): ");
                Console.WriteLine(cabecalho);
                Console.WriteLine(resultado);
                return codigos;
            } else {
                Console.WriteLine("O termo buscado não foi encontrado");
                return null;
            } 
            ex.Quit();
        } else {
            Console.WriteLine("O arquivo " + arquivo + " não foi encontrado!");
            return null;
        }
    }
    public void ler(String arquivo){
        if(File.Exists(arquivo)){
            Application ex = new Application();
            ex.Workbooks.Open(arquivo);
            int linha = 1;
            int campo = 1;
            string resultado = null, cabecalho = null;
            while(!ex.Cells[linha, campo].Value.Equals(null)){
                campo = 1;
                while(!ex.Cells[linha, campo].Value.Equals(null)){
                    resultado += ex.Cells[linha, campo].Value.ToString() + " | ";
                    campo++;
                }
                resultado += "\n";
                linha++;
            }
            if(!resultado.Equals(null)){
                campo = 1;
                while(!ex.Cells[linha, campo].Value.Equals(null)){
                    cabecalho += ex.Cells[1, campo].Value.ToString() + " | ";
                    campo++;
                }
                Console.WriteLine("Resultado(s) encontrado(s): ");
                Console.WriteLine(cabecalho);
                Console.WriteLine(resultado);
            } else {
                Console.WriteLine("O termo buscado não foi encontrado");
            } 
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
            ex.Dispose();
        } /*else {
            contador = 1;
        }*/
        return contador;
    }
}