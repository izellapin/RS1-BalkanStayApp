import { Component, Input, OnInit, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ReviewService } from '../services/review/review.service';
import { CommonModule } from '@angular/common';
import { MyAuthService } from '../services/auth-services/my-auth.service';

@Component({
  selector: 'app-review',
  templateUrl: './review.component.html',
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  styleUrls: ['./review.component.css']
})
export class ReviewComponent implements OnInit {
  @Input() apartmentId!: number;  // Identifikacija apartmana za koji se ostavlja recenzija
  @Output() close = new EventEmitter<void>();
  @Output() reviewAdded = new EventEmitter<void>();  // Emituj event kad je recenzija dodana
  reviewForm: FormGroup;

  stars = [1, 2, 3, 4, 5];
  hoverRating = 0;

  setRating(value: number): void {
    this.reviewForm.get('rating')?.setValue(value);
  }

  constructor(
    private fb: FormBuilder,
    private authService: MyAuthService,
    private reviewService: ReviewService
  ) {
    this.reviewForm = this.fb.group({
      rating: [null, [Validators.required, Validators.min(1), Validators.max(5)]],
      comment: ['', Validators.required]
    });
  }

  ngOnInit(): void {}

  submitReview(): void {
    const loggedInUser = this.authService.getMyAuthInfo();
    if (!loggedInUser) {
      console.error('User is not logged in.');
      return;
    }
    console.log('Rating:', this.reviewForm.value.rating);
    console.log('Form Valid:', this.reviewForm.valid);

    const reviewData = {
      apartmentId: this.apartmentId,
      rating: String(this.reviewForm.value.rating),
      comment: this.reviewForm.value.comment,
      accountId: loggedInUser.userId
    };

    this.reviewService.addReview(reviewData).subscribe({
      next: () => {
        this.reviewForm.reset();
        this.reviewAdded.emit();
        this.close.emit();
      },
      error: (err) => {
        console.error('Error submitting review:', err);
        if (err.error && err.error.errors) {
          console.error('Validation Errors:', err.error.errors);
        }
      }

    });
  }
}
