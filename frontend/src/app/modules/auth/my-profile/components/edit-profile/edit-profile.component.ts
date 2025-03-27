import { Component } from '@angular/core';

@Component({
  selector: 'app-edit-profile',
  template: `
    <div class="section-content">
      <h2>Edit Profile</h2>
      <p>This is the edit profile section</p>
    </div>
  `,
  styles: [`
    .section-content {
      padding: 20px;
    }
  `]
})
export class EditProfileComponent { } 