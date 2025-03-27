import { Component } from '@angular/core';

@Component({
  selector: 'app-settings',
  template: `
    <div class="section-content">
      <h2>Settings</h2>
      <p>This is the settings section</p>
    </div>
  `,
  styles: [`
    .section-content {
      padding: 20px;
    }
  `]
})
export class SettingsComponent { } 