using System;
public class Carro{
    private string marca {get; set;}
    private string modelo {get; set;}
    private string cor {get; set;}
    private int kilometragem {get; set;}
    private string placa {get; set;}
    private bool novo {get; set;}
    private bool disponivel {get; set;}
    private OpcionaisCarro opcionais;

    public String getMarca(){
        return this.marca;
    }

    public void setMarca(String marca){
        this.marca = marca;
    }

    public String getModelo(){
        return this.modelo;
    }

    public void setModelo(String modelo){
        this.modelo = modelo;
    }

    public String getCor(){
        return this.cor;
    }

    public void setCor(String cor){
        this.cor = cor;
    }

    public int getKilometragem(){
        return this.kilometragem;
    }

    public void setKilometragem(int kilometragem){
        this.kilometragem = kilometragem;
    }

    public String getPlaca(){
        return this.placa;
    }

    public void setPlaca(String placa){
        this.placa = placa;
    }

    public bool getNovo(){
        return this.novo;
    }

    public void setNovo(bool novo){
        this.novo = novo;
    }
    
    public bool getDisponivel(){
        return this.disponivel;
    }

    public void setDisponivel(bool disponivel){
        this.disponivel = disponivel;
    }

    public OpcionaisCarro getOpcionais(){
        return this.opcionais;
    }

    public void setOpcionais(OpcionaisCarro opcionais){
        this.opcionais = opcionais;
    }
}