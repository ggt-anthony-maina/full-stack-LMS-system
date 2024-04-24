// import { Component, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-filter-users',
  standalone: true,
  imports: [FormsModule , CommonModule ],
  template: `
  <div class="filter" [class.active]="isActive" (click)="toggleFilter()">
  Filter
</div>
<div *ngIf="isActive" class="filter-options">
      <label><input type="checkbox" [(ngModel)]="criteria1"> Criteria 1</label>
      <label><input type="checkbox" [(ngModel)]="criteria2"> Criteria 2</label>
      <label><input type="checkbox" [(ngModel)]="criteria3"> Criteria 3</label>
    </div>
  `,
  styleUrl: './filter-users.component.scss'
})
export class FilterUsersComponent {

  isActive: boolean = false;
  criteria1: boolean = false;
  criteria2: boolean = false;
  criteria3: boolean = false;
  @Output() filterToggle = new EventEmitter<boolean>();

  toggleFilter() {
    this.isActive = !this.isActive;
    this.filterToggle.emit(this.isActive);
  }
}
