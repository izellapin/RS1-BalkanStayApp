import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule} from '@angular/router';
import {FormsModule} from '@angular/forms';

import {PublicRoutingModule} from './public-routing.module';
import {AboutComponent} from './about/about.component';
import {BlogComponent} from './blog/blog.component';
import {ContactUsComponent} from './contact-us/contact-us.component';
import {HomeComponent} from './home/home.component';
import {PublicLayoutComponent} from './public-layout/public-layout.component';
import {TravelsComponent} from './travels/travels.component';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatInputModule} from '@angular/material/input';
import {MatNativeDateModule} from '@angular/material/core';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';
import {MatListModule} from '@angular/material/list';
import {MatDividerModule} from '@angular/material/divider';
import {MatCardModule} from '@angular/material/card';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import {MatMenu, MatMenuTrigger} from "@angular/material/menu";
import { ApartmentDetailsComponent } from './apartment-details/apartment-details.component';
import {SharedModule} from '../shared/shared.module';
import {ReservationBoxComponent} from '../reservation-box/reservation-box.component';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';

import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {ReviewComponent} from '../../review/review.component';

@NgModule({
  declarations: [
    AboutComponent,
    BlogComponent,
    ContactUsComponent,
    HomeComponent,
    PublicLayoutComponent,
    TravelsComponent,
    ApartmentDetailsComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    PublicRoutingModule,
    FormsModule,
    MatFormFieldModule,
    MatDatepickerModule,
    MatInputModule,
    MatNativeDateModule,
    MatIconModule,
    MatButtonModule,
    MatListModule,
    MatDividerModule,
    MatCardModule,
    MatAutocompleteModule,
    MatMenu,
    MatMenuTrigger,
    SharedModule,
    BsDatepickerModule.forRoot(),
    ReactiveFormsModule,
    ReservationBoxComponent,
    ReviewComponent
  ],
  exports: [
    AboutComponent,
    BlogComponent,
    ContactUsComponent,
    HomeComponent,
    PublicLayoutComponent,
    TravelsComponent,
    ApartmentDetailsComponent
  ]
})
export class PublicModule {
}
