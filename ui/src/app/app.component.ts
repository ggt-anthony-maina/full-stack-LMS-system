import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AppTopBarComponent } from './app-top-bar/app-top-bar.component';
import { UsersTableComponent } from './users-table/users-table.component';
import { LoginComponent } from './login/login.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, AppTopBarComponent, UsersTableComponent, LoginComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'ui';
}
