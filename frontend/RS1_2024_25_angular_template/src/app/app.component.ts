import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  standalone: false
})
export class AppComponent {
  title = 'RS1 - 2024-25 - template 1 ';

  constructor(private translate: TranslateService) {
    // Postavljanje defaultnog jezika
    this.translate.setDefaultLang('en');
    this.translate.use('en');
  }

  // Funkcija za promjenu jezika
  changeLanguage(lang: string) {
    this.translate.use(lang);
  }
}
