import { Component } from '@angular/core';

@Component({
  selector: 'app-my-reviews',
  template: `
    <div class="section-content">
      <h2>My Reviews</h2>
      <p>This is the reviews section</p>
    </div>
  `,
  styles: [`
    .section-content {
      padding: 20px;
    }
  `]
})
export class MyReviewsComponent { } 