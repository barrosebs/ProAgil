import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-evento-lista',
  templateUrl: './evento-lista.component.html',
  styleUrls: ['./evento-lista.component.css']
})
export class EventoListaComponent implements OnInit {

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getEventos();
  }
  private _filtroLista: any;

  public eventosFiltrados: any = [];
  eventos: any = [];
  imagemLargura = 80;
  imagemMargem = 2;
  mostrarImagem = false;

  public get filtroLista(): string{
    return this._filtroLista;
  }

  set filtroLista(value: string){
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEvento(this.filtroLista) : this.eventos;
  }

  filtrarEvento(filtrarPor: string): any{
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      (evento: { tema: string; }) => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1);
  }

  alternarImagem()
  {
    this.mostrarImagem = !this.mostrarImagem;
  }
  getEventos() {
    this.eventos = this.http.get('http://localhost:5000/api/evento').subscribe(
      response => {
        this.eventos = response;
        this.eventosFiltrados = this.eventos;
        console.log(this.eventos);
      }, error => {
        console.log(error);
      });
  }

}
