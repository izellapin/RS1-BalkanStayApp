import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {

  private apiUrl = 'http://localhost:8000/Review';  // URL za vaš API endpoint za recenzije

  constructor(private http: HttpClient) {}

  // POST metoda za slanje recenzije
  addReview(review: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/Insert`, review);
  }

  // GET metoda za preuzimanje recenzija za apartman
  getReviewsForApartment(apartmentId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/GetByApartment/${apartmentId}`);
  }

}
