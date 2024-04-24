import { Component , Input, Output} from '@angular/core';
import { LoginService } from '../login.service';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms'; // Import the FormsModule
import { NgModule } from '@angular/core';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  template: `
  <div class="main-container">
  <div class="formDiv">
    <h1>Login</h1>
    <form>
      <div class="form-group">
        <label for="username">Username:</label>
        <input type="text" id="username" name="username" class="form-control" [(ngModel)]="username" />
      </div>
      <div class="form-group">
       <label for="password">Password:</label>
        <input type="password" id="password" name="password" class="form-control" [(ngModel)]="password" />
      </div>
      <button type="submit" class="btn btn-primary" (click)="login()">Login</button>
    </form>
    <div>
      <a href="#">Forgot password?</a>
    </div>
    <div>
      <hr />
      <h4>Sign in with:</h4>
      <button class="btn btn-secondary">Google</button>
      <button class="btn btn-secondary">Facebook</button>
      <!-- Add other OAuth methods as needed -->
    </div>
  </div>
</div>

  `,
  styleUrl: './login.component.scss'
})
export class LoginComponent {

  username: string = '';
  password: string = '';

  constructor(private loginService: LoginService) { }

  login() {
    this.loginService.login(this.username, this.password)
      .subscribe(
        response => {
          // Handle successful login
        },
        error => {
          // Handle login error
        }
      );
  }
}
