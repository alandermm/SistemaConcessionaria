using System;
using NetOffice.ExcelApi;
/// <summary>
/// Classe Pessoa
/// </summary>
public class Pessoa{

    public string documento {get; set;}
    public string nome {get; set;}
    public string email {get; set;}
    public Endereco endereco {get; set;}
    //private String[] dados;

    /// <summary>
    /// Método para iniciar os dados do objeto da classe Pessoa
    /// </summary>
    /// <param name="tipoDoc">Define o tipo do cliente: "CPF" ou "CNPJ"</param>
    public void iniciarDados(String tipoDoc){
        Console.Write("Nome do Cliente: ");
        this.nome = Console.ReadLine();
        Console.Write("Email do cliente: ");
        this.email = Console.ReadLine();
        Console.Write(tipoDoc + " do cliente: ");
        Validacao documento = new Validacao();
        this.documento = tipoDoc.Equals("CPF") ? documento.pedirCPF() : documento.pedirCNPJ();
        this.endereco = new Endereco();
        Console.Write("Rua: ");
        this.endereco.rua = Console.ReadLine();
        Console.Write("Número: ");
        this.endereco.numero = Int16.Parse(Console.ReadLine());
        Console.Write("Bairro: ");
        this.endereco.bairro = Console.ReadLine();
    }

    /// <summary>
    /// Método para salvar os dados da pessoa no arquivo de cadastro
    /// </summary>
    /// <param name="arquivo">Path completo para o arquivo de cadastro</param>
    public void salvar(String arquivo){
        Application ex = new Application();
        int ultimaLinha = new Cadastro().getUltimaLinha(arquivo);
        ex.Workbooks.Open(arquivo);
        ex.Cells[ultimaLinha, 1].Value = this.documento;
        ex.Cells[ultimaLinha, 2].Value = this.nome;
        ex.Cells[ultimaLinha, 3].Value = this.email;
        ex.Cells[ultimaLinha, 4].Value = this.endereco.rua;
        ex.Cells[ultimaLinha, 5].Value = this.endereco.numero;
        ex.Cells[ultimaLinha, 6].Value = this.endereco.bairro;
        ex.Cells[ultimaLinha, 7].Value = DateTime.Now;
        ex.ActiveWorkbook.Save();
        ex.Quit();
        ex.Dispose();
    }

    /// <summary>
    /// Método para carregar o objeto Pessoa do arquivo de cadastro
    /// </summary>
    /// <param name="doc">Número do documento da Pessoa</param>
    /// <param name="arquivo">Path completo para o arquivo de cadastro</param>
    /// <returns></returns>
    public Pessoa carregarPessoa(Int64 doc, String arquivo){
        Application ex = new Application();
        ex.Workbooks.Open(arquivo);
        Pessoa pessoa = new Pessoa();
        int linha = 2;
        while(Int64.Parse(ex.Cells[linha, 1].Value.ToString()) != doc && ex.Cells[linha,1].Value != null ){
            linha++;
        }
        pessoa.documento = ex.Cells[linha, 1].Value.ToString();
        pessoa.nome = ex.Cells[linha, 2].Value.ToString();
        pessoa.email = ex.Cells[linha, 3].Value.ToString();
        pessoa.endereco = new Endereco();
        pessoa.endereco.rua = ex.Cells[linha, 4].Value.ToString();
        pessoa.endereco.numero = Int16.Parse(ex.Cells[linha, 5].Value.ToString());
        pessoa.endereco.bairro = ex.Cells[linha, 6].Value.ToString(); 
        ex.ActiveWorkbook.Close();
        ex.Quit();
        ex.Dispose();
        return pessoa;
    }
}