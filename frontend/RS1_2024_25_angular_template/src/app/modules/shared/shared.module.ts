import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {UnauthorizedComponent} from './unauthorized/unauthorized.component';
import {RouterLink, RouterLinkActive, RouterOutlet} from '@angular/router';
import {MyDialogSimpleComponent} from './dialogs/my-dialog-simple/my-dialog-simple.component';
import {
  MatDialogActions,
  MatDialogClose,
  MatDialogContent,
  MatDialogModule,
  MatDialogTitle
} from '@angular/material/dialog';
import {MatButton, MatButtonModule, MatIconButton} from '@angular/material/button';
import {MyDialogConfirmComponent} from './dialogs/my-dialog-confirm/my-dialog-confirm.component';
import {MatIcon} from '@angular/material/icon';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import {MyPageProgressbarComponent} from './progressbars/my-page-progressbar/my-page-progressbar.component';
import {MatProgressBar} from '@angular/material/progress-bar';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatSortModule} from '@angular/material/sort';
import {MatPaginatorModule} from '@angular/material/paginator';
import {FooterComponent} from './footer/footer.component';
import { HeaderComponent } from './header/header.component';


@NgModule({
  declarations: [
    UnauthorizedComponent,
    MyDialogSimpleComponent,
    MyDialogConfirmComponent,
    MyPageProgressbarComponent,
    FooterComponent,
    HeaderComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterLink,
    MatDialogTitle,
    MatDialogActions,
    MatButton,
    MatDialogClose,
    MatIcon,
    MatDialogContent,
    MatIconButton,
    MatSnackBarModule,
    MatProgressBar,
    MatToolbarModule,
    MatDialogModule,
    MatButtonModule,
    MatSortModule,
    MatPaginatorModule,
    RouterLinkActive,
    RouterOutlet
  ],
  exports: [
    UnauthorizedComponent, // Omogućavamo ponovno korištenje UnauthorizedComponent
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MyPageProgressbarComponent,
    FooterComponent,
    HeaderComponent

  ]
})
export class SharedModule {
}
