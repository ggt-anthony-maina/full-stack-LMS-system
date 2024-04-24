import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AppTopBarComponent } from '../app-top-bar/app-top-bar.component';
import { UsersTableComponent } from '../users-table/users-table.component';
import { LoginComponent } from '../login/login.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterOutlet, AppTopBarComponent, UsersTableComponent, LoginComponent],
  template: `
  <div className=" main">
  <app-app-top-bar></app-app-top-bar>
  <router-outlet></router-outlet>
</div>
  `,
  styleUrl: './home.component.scss'
})
export class HomeComponent {

}
