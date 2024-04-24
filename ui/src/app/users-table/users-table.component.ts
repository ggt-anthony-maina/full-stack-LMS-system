import { Component } from '@angular/core';
import { FilterUsersComponent } from '../filter-users/filter-users.component';

@Component({
  selector: 'app-users-table',
  standalone: true,
  imports: [FilterUsersComponent],
  template: `
  <div class="total-add">
    <h2>Total Users</h2>
    <button class="addUser">Add User</button>
    </div>
  <div class="grid-container">
  <div class="filter-container">
  <app-filter-users (filterToggle)="toggleFilter($event)">   </app-filter-users>
  </div>
  
  <div class="table-container" [class.filtered]="isFilterActive">
  <table >
 
    <thead>
      <tr>
        <th>ID</th>
        <th>Name</th>
        <th>Email</th>
        <th>Email</th>
        <th>Email</th>
        <th>Email</th>
        <th>Email</th>
        <th>Email</th>
        <th>Email</th>
      </tr>
    </thead>
    <tbody>
      <tr >
        <td>1</td>
        <td>Anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
      </tr>

      <tr >
        <td>1</td>
        <td>Anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
      </tr>

      <tr >
        <td>1</td>
        <td>Anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        
      </tr>

      <tr >
        <td>1</td>
        <td>Anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
      </tr>

      <tr >
        <td>1</td>
        <td>Anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
      </tr>
      <tr >
        <td>1</td>
        <td>Anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
      </tr>

      <tr >
        <td>1</td>
        <td>Anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
      </tr>


      <tr >
        <td>1</td>
        <td>Anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        
      </tr>


      <tr >
        <td>1</td>
        <td>Anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        
      </tr>

      <tr >
        <td>1</td>
        <td>Anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
      </tr>

      <tr >
        <td>1</td>
        <td>Anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
        <td>maina.anthony</td>
      </tr>
    </tbody>
    <div class="pagination">
    <button >Previous</button>
    <span >1</span>
    <button >Next</button>
  </div>
  </table>
  </div>
  
</div>

  `,
  styleUrl: './users-table.component.scss'
})
export class UsersTableComponent {

  isFilterActive: boolean = false;

  toggleFilter(isActive: boolean) {
    this.isFilterActive = isActive;
  }

}
