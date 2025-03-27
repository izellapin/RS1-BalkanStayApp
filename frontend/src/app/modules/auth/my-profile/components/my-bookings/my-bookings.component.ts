import { Component } from '@angular/core';

@Component({
  selector: 'app-my-bookings',
  template: `
    <div class="section-content">
      <h2>My Travels</h2>
      <p>This is the bookings section</p>
    </div>
  `,
  styles: [`
    .section-content {
      padding: 20px;
    }
  `]
})
export class MyBookingsComponent { } 