import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { MyProfileComponent } from './my-profile/my-profile.component';
import { EditProfileComponent } from './my-profile/components/edit-profile/edit-profile.component';
import { MyBookingsComponent } from './my-profile/components/my-bookings/my-bookings.component';
import { FavoritesComponent } from './my-profile/components/favorites/favorites.component';
import { MyReviewsComponent } from './my-profile/components/my-reviews/my-reviews.component';
import { SettingsComponent } from './my-profile/components/settings/settings.component';

@NgModule({
  declarations: [
    MyProfileComponent,
    EditProfileComponent,
    MyBookingsComponent,
    FavoritesComponent,
    MyReviewsComponent,
    SettingsComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  exports: [
    MyProfileComponent
  ]
})
export class AuthModule { } 