import { Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

@Component({
  selector: 'app-app-top-bar',
  standalone: true,
  imports: [RouterModule],
  template: `
   <div class="top-bar">
   <div class="logo">
   <a routerLink="/">Logo</a>
 </div>

 <div class="links">
 <a routerLink="/books">Books</a>
 <a routerLink="/courses">Courses</a>
</div>
    
   </div>
  `,
  styleUrl: './app-top-bar.component.scss'
})
export class AppTopBarComponent {

}
