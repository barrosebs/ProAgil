import { EventoListaComponent } from './components/eventos/evento-lista/evento-lista.component';
import { EventoDetalheComponent } from './components/eventos/evento-detalhe/evento-detalhe.component';
import { EventosComponent } from './components/eventos/eventos.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


const routes: Routes = [
  {
    path: 'eventos', component: EventosComponent,
    children: [
      {path: 'detalhes/:id', component: EventoDetalheComponent},
      {path: 'detalhes', component: EventoDetalheComponent},
      {path: 'lista', component: EventoListaComponent},
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
