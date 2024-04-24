import { Routes } from '@angular/router';
import { UsersTableComponent } from './users-table/users-table.component';
import { HomeComponent } from './home/home.component';
import { BooksComponent } from './books/books.component';
import { CoursesComponent } from './courses/courses.component';
import { LoginComponent } from './login/login.component';

export const routes: Routes = [
    { path: '', component: LoginComponent },
    {
        path: 'home', component: HomeComponent, children: [
            { path: 'users', component: UsersTableComponent },
            { path: 'books', component: BooksComponent },
            { path: 'courses', component: CoursesComponent },
            { path: '', redirectTo: 'users', pathMatch: 'full' } // Default child route
        ]
    },
    { path: '', redirectTo: 'login', pathMatch: 'full' } // Default route
];
