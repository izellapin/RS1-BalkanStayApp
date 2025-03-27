import { Component } from '@angular/core';

@Component({
  selector: 'app-favorites',
  template: `
    <div class="section-content">
      <h2>Favorites</h2>
      <p>This is the favorites section</p>
    </div>
  `,
  styles: [`
    .section-content {
      padding: 20px;
    }
  `]
})
export class FavoritesComponent { } 