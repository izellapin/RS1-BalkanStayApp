import { Component, Input, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import { ReviewService } from '../services/review/review.service';  // Pretpostavimo da imate servis za recenzije

@Component({
  selector: 'app-review',
  templateUrl: './review.component.html',
  imports: [
    ReactiveFormsModule
  ],
  styleUrls: ['./review.component.css']
})
export class ReviewComponent implements OnInit {
  @Input() apartmentId!: number;  // Identifikacija apartmana za koji se ostavlja recenzija
  reviewForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private reviewService: ReviewService
  ) {
    this.reviewForm = this.fb.group({
      rating: [null, [Validators.required, Validators.min(1), Validators.max(5)]],
      comment: ['', Validators.required]
    });
  }

  ngOnInit(): void {}

  submitReview(): void {
    if (this.reviewForm.valid) {
      const review = this.reviewForm.value;
      review.apartmentId = this.apartmentId;

      this.reviewService.submitReview(review).subscribe(response => {
        // Možete dodati poruku o uspehu ili izvršiti neki drugi posao nakon što je recenzija poslata
        alert('Review submitted successfully!');
        this.reviewForm.reset();
      });
    }
  }
}
