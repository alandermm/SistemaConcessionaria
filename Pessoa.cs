using System;
public class Pessoa{
    private String nome;
    private String email;
    private String documento;
    private Endereco endereco;
    
    public String getNome(){
        return nome;
    }

    public void setNome(String nome){
        this.nome = nome;
    }

    public String getEmail(){
        return email;
    }

    public void setEmail(String email){
        this.email = email;
    }

    public String getDocumento(){
        return documento;
    }

    public void setDocumento(String documento){
        this.documento = documento;
    }

    public Endereco getEndereco(){
        return endereco;
    }

    public void setEndereco(Endereco endereco){
        this.endereco = endereco;
    }
}