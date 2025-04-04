import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

interface UserImage {
  userImageID: number;
  accountID: number;
  imageID: number;
  image: {
    imageID: number;
    imagePath: string;
  };
}

interface UserProfile {
  accountID: number;
  username: string;
  email: string;
  firstName: string;
  lastName: string;
  phone: string;
  genderID: number;
  cityID: number;
  userImages: {
    image: {
      imagePath: string;
    }
  }[];
  isUser: boolean;
  isAdministrator: boolean;
  isOwner: boolean;
}

interface UserUpdateRequest {
  accountID: number;
  username: string;
  email: string;
  firstName: string;
  lastName: string;
  phone: string;
  genderID: number;
  cityID: number;
  imagePath?: string;
}

interface Reservation {
  reservationID: number;
  accountID: number;
  apartmentId: number;
  startDate: string;
  endDate: string;
  status: boolean;
}

interface Apartment {
  apartmentId: number;
  name: string;
  cityName: string;
  adress: string;
  cityId?: number;
  pricePerNight: number;
  city?: {
    name: string;
    id: number;
  };
}

interface Travel {
  reservationID: number;
  startDate: string;
  endDate: string;
  duration: number;
  apartment: Apartment;
}

interface Payment {
  reservationID: number;
  amount: number;
  date: string;
  status: string;
  apartmentName: string;
  nights: number;
  pricePerNight: number;
}

interface ApartmentDetails extends Apartment {
  country: string;
  hostName: string;
  description?: string;
  pricePerNight: number;
}

interface Review {
  id: number;
  apartmentId: number;
  apartmentName: string;
  rating: number;
  comment: string;
  stayDate: string;
  userId: number;
}

@Component({
  selector: 'app-my-profile',
  standalone: false,
  templateUrl: './my-profile.component.html',
  styleUrl: './my-profile.component.css'
})
export class MyProfileComponent implements OnInit {
  selectedMenuItem: string = 'Edit Profile'; // Default selected item
  public apiUrl = 'http://localhost:8000';
  profileImageUrl: string = this.apiUrl + '/images/default.jpg'; // Default image path
  isImageLoading: boolean = false;
  selectedFile: File | null = null;
  profileForm: FormGroup;
  isLoading: boolean = false;
  errorMessage: string | null = null;
  successMessage: string | null = null;

  // Travels data
  travels: Travel[] = [];

  // Bookings data
  bookings: Reservation[] = [];

  // Payments data
  totalSpent: number = 0;
  upcomingPayments: number = 0;
  payments: Payment[] = [];

  // Activities data
  activities = [
    {
      title: 'Mountain Hiking',
      description: 'Experience the thrill of mountain hiking with our expert guides',
      date: '2024-06-20',
      location: 'Alps Mountains',
      duration: 'Full Day',
      image: 'assets/activities/hiking.jpg'
    },
    {
      title: 'City Tour',
      description: 'Explore the city\'s hidden gems and historical landmarks',
      date: '2024-07-05',
      location: 'City Center',
      duration: 'Half Day',
      image: 'assets/activities/city-tour.jpg'
    }
  ];

  // Add these properties
  selectedBooking: any = null;
  isModalOpen: boolean = false;
  apartmentDetails: ApartmentDetails | null = null;

  // Add this property
  reviews: Review[] = [];

  constructor(private fb: FormBuilder, private http: HttpClient) {
    this.profileForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', Validators.required],
      username: ['', Validators.required],
      cityID: [''],
      genderID: ['']
    });
  }

  ngOnInit() {
    this.loadUserData();
    this.loadUserBookings();
    this.loadUserTravels();
    this.loadPayments();
    this.loadUserReviews();
  }

  selectMenuItem(item: string) {
    this.selectedMenuItem = item;
  }

  onImageSelected(event: any) {
    const file = event.target.files[0];
    if (file) {
      // Validate file type
      if (!file.type.startsWith('image/')) {
        this.errorMessage = 'Please select a valid image file.';
        return;
      }

      // Validate file size (max 5MB)
      if (file.size > 5 * 1024 * 1024) {
        this.errorMessage = 'Image size should be less than 5MB.';
        return;
      }

      this.selectedFile = file;
      this.isImageLoading = true;
      this.uploadImage();
    }
  }

  uploadImage() {
    if (!this.selectedFile) return;

    const authToken = localStorage.getItem('my-auth-token');
    if (!authToken) {
      this.errorMessage = 'Authentication token not found';
      this.isImageLoading = false;
      return;
    }

    try {
      const parsedToken = JSON.parse(authToken);
      const userId = parsedToken.myAuthInfo.userId;
      const token = parsedToken.token;

      // Create FormData
      const formData = new FormData();
      formData.append('file', this.selectedFile);
      formData.append('userId', userId.toString());

      // Make the request without withCredentials
      this.http.post(`${this.apiUrl}/User/UploadProfileImage`, formData, {
        headers: {
          'my-auth-token': token,
          'Accept': 'application/json'
        }
      }).subscribe({
        next: (response: any) => {
          console.log('Upload successful:', response);
          if (response.imagePath) {
            this.profileImageUrl = this.apiUrl + response.imagePath;
            this.successMessage = 'Profile image updated successfully!';
          } else {
            this.errorMessage = 'Invalid response from server';
          }
          this.isImageLoading = false;
          this.loadUserData(); // Reload user data to ensure everything is in sync
        },
        error: (error) => {
          console.error('Upload error:', error);
          if (error.error) {
            console.error('Error details:', error.error);
            this.errorMessage = error.error.message || 'Failed to upload profile image. Please try again.';
          } else {
            this.errorMessage = 'Failed to upload profile image. Please try again.';
          }
          this.isImageLoading = false;
          this.loadUserData(); // Revert to previous image if upload fails
        }
      });
    } catch (error) {
      console.error('Error parsing auth token:', error);
      this.errorMessage = 'Invalid authentication token';
      this.isImageLoading = false;
    }
  }

  onSubmit() {
    if (this.profileForm.valid) {
      this.isLoading = true;
      const authToken = localStorage.getItem('my-auth-token');
      if (authToken) {
        const parsedToken = JSON.parse(authToken);
        const userId = parsedToken.myAuthInfo.userId;
        
        const updateData: UserUpdateRequest = {
          accountID: Number(userId),
          username: this.profileForm.get('username')?.value || '',
          email: this.profileForm.get('email')?.value || '',
          firstName: this.profileForm.get('firstName')?.value || '',
          lastName: this.profileForm.get('lastName')?.value || '',
          phone: this.profileForm.get('phone')?.value || '',
          genderID: Number(this.profileForm.get('genderID')?.value) || 1,
          cityID: Number(this.profileForm.get('cityID')?.value) || 1,
          imagePath: this.profileImageUrl
        };

        console.log('Sending update data:', updateData);

        this.http.put(`${this.apiUrl}/User/Update`, updateData, {
          headers: {
            'my-auth-token': parsedToken.token
          }
        }).subscribe({
          next: (response) => {
            console.log('Full response:', response);
            this.successMessage = 'Profile updated successfully!';
            this.isLoading = false;
            this.loadUserData();
          },
          error: (error) => {
            console.error('Full error:', error);
            if (error.error?.errors) {
              console.log('Validation errors:', error.error.errors);
              const errorMessages = Object.values(error.error.errors).flat();
              this.errorMessage = errorMessages.join(', ');
            } else {
              this.errorMessage = 'Failed to update profile. Please try again.';
            }
            this.isLoading = false;
          }
        });
      }
    }
  }

  resetForm() {
    this.profileForm.reset();
    this.loadUserData(); // Reload the original user data
  }

  public loadUserData() {
    this.isLoading = true;
    const authToken = localStorage.getItem('my-auth-token');
    if (authToken) {
      try {
        const parsedToken = JSON.parse(authToken);
        const userId = parsedToken.myAuthInfo.userId;
        const token = parsedToken.token;
        
        if (!token) {
          this.errorMessage = 'Authentication token not found';
          this.isLoading = false;
          return;
        }

        console.log('Loading user data with userId:', userId);
        console.log('Token:', token);

        this.http.get<UserProfile>(`${this.apiUrl}/User/Get/${userId}`, {
          headers: {
            'my-auth-token': token
          }
        }).subscribe({
          next: (userData) => {
            console.log('Loaded user data:', userData);
            this.profileForm.patchValue({
              firstName: userData.firstName,
              lastName: userData.lastName,
              email: userData.email,
              phone: userData.phone,
              username: userData.username,
              cityID: userData.cityID,
              genderID: userData.genderID
            });

            // Handle profile image
            if (userData.userImages && userData.userImages.length > 0) {
              const imagePath = userData.userImages[0].image.imagePath;
              // Check if the path is already a full URL
              if (imagePath.startsWith('http')) {
                this.profileImageUrl = imagePath;
              } else if (imagePath.startsWith('data:')) {
                // If it's a base64 string, use the default image
                this.profileImageUrl = this.apiUrl + '/images/default.jpg';
              } else {
                this.profileImageUrl = this.apiUrl + imagePath;
              }
              console.log('Profile image URL:', this.profileImageUrl);
            } else {
              this.profileImageUrl = this.apiUrl + '/images/default.jpg';
            }
            
            this.isLoading = false;
          },
          error: (error) => {
            console.error('Error loading user data:', error);
            this.errorMessage = 'Failed to load user data. Please try again.';
            this.isLoading = false;
          }
        });
      } catch (error) {
        console.error('Error parsing auth token:', error);
        this.errorMessage = 'Invalid authentication token';
        this.isLoading = false;
      }
    } else {
      this.errorMessage = 'No authentication token found';
      this.isLoading = false;
    }
  }

  // Booking actions
  cancelBooking(bookingId: number) {
    if (confirm('Are you sure you want to cancel this booking?')) {
      this.http.put(`${this.apiUrl}/Reservation/Cancel/${bookingId}`, {})
        .subscribe({
          next: () => {
            this.loadUserBookings(); // Reload bookings after cancellation
            this.successMessage = 'Booking cancelled successfully';
          },
          error: (error) => {
            console.error('Error cancelling booking:', error);
            this.errorMessage = 'Failed to cancel booking. Please try again.';
          }
        });
    }
  }

  viewBookingDetails(reservationId: number) {
    const booking = this.bookings.find(b => b.reservationID === reservationId);
    if (booking) {
      this.selectedBooking = booking;
      
      const authToken = localStorage.getItem('my-auth-token');
      if (authToken) {
        const parsedToken = JSON.parse(authToken);
        const token = parsedToken.token;

        // First get apartment details
        this.http.get<ApartmentDetails>(`${this.apiUrl}/Apartment/GetById/${booking.apartmentId}`, {
          headers: { 'my-auth-token': token }
        }).subscribe({
          next: (details) => {
            // If country is not included in the response, we can set a default
            this.apartmentDetails = {
              ...details,
              country: 'Bosnia and Herzegovina' // Set default country if not provided by API
            };
            this.isModalOpen = true;
          },
          error: (error) => {
            console.error('Error loading apartment details:', error);
            this.errorMessage = 'Failed to load apartment details';
          }
        });
      }
    }
  }

  closeModal() {
    this.isModalOpen = false;
    this.selectedBooking = null;
    this.apartmentDetails = null;
  }

  // Calculate total price for the stay
  calculateTotalPrice(): number {
    if (this.selectedBooking && this.apartmentDetails) {
      const start = new Date(this.selectedBooking.startDate);
      const end = new Date(this.selectedBooking.endDate);
      const nights = Math.ceil((end.getTime() - start.getTime()) / (1000 * 3600 * 24));
      return nights * this.apartmentDetails.pricePerNight;
    }
    return 0;
  }

  // Activity actions
  bookActivity(activityTitle: string) {
    // TODO: Implement activity booking
    console.log('Booking activity:', activityTitle);
  }

  saveActivity(activityTitle: string) {
    // TODO: Implement activity saving
    console.log('Saving activity:', activityTitle);
  }

  public handleImageError(): void {
    this.profileImageUrl = this.apiUrl + '/images/default.jpg';
  }

  private loadUserBookings() {
    const authToken = localStorage.getItem('my-auth-token');
    if (authToken) {
      try {
        const parsedToken = JSON.parse(authToken);
        const userId = parsedToken.myAuthInfo.userId;
        const token = parsedToken.token;
        
        if (!token) {
          this.errorMessage = 'Authentication token not found';
          return;
        }

        this.http.get<Reservation[]>(`${this.apiUrl}/Reservation/Get`, {
          headers: {
            'my-auth-token': token
          }
        }).subscribe({
          next: (reservations) => {
            const today = new Date();
            // Filter only upcoming reservations for bookings
            this.bookings = reservations.filter(res => 
              res.accountID === userId && 
              new Date(res.startDate) >= today &&
              res.status // Only active reservations
            );

            // Sort bookings by date (soonest first)
            this.bookings.sort((a, b) => 
              new Date(a.startDate).getTime() - new Date(b.startDate).getTime()
            );
          },
          error: (error) => {
            console.error('Error loading bookings:', error);
            this.errorMessage = 'Failed to load bookings';
          }
        });
      } catch (error) {
        console.error('Error parsing auth token:', error);
        this.errorMessage = 'Invalid authentication token';
      }
    }
  }

  private loadUserTravels() {
    const authToken = localStorage.getItem('my-auth-token');
    if (authToken) {
      try {
        const parsedToken = JSON.parse(authToken);
        const userId = parsedToken.myAuthInfo.userId;
        const token = parsedToken.token;
        
        if (!token) {
          this.errorMessage = 'Authentication token not found';
          return;
        }

        this.http.get<Reservation[]>(`${this.apiUrl}/Reservation/Get`, {
          headers: {
            'my-auth-token': token
          }
        }).subscribe({
          next: (reservations) => {
            const today = new Date();
            // Filter only past reservations for travels
            const pastReservations = reservations.filter(res => 
              res.accountID === userId && 
              new Date(res.endDate) < today &&
              res.status // Only active/completed reservations
            );
            
            this.travels = []; // Clear existing travels

            pastReservations.forEach(reservation => {
              this.http.get<Apartment>(`${this.apiUrl}/Apartment/GetById/${reservation.apartmentId}`, {
                headers: {
                  'my-auth-token': token
                }
              }).subscribe({
                next: (apartment) => {
                  const start = new Date(reservation.startDate);
                  const end = new Date(reservation.endDate);
                  const duration = Math.ceil((end.getTime() - start.getTime()) / (1000 * 3600 * 24));

                  const travel: Travel = {
                    reservationID: reservation.reservationID,
                    startDate: reservation.startDate,
                    endDate: reservation.endDate,
                    duration: duration,
                    apartment: apartment
                  };

                  this.travels.push(travel);
                  // Sort travels by date (most recent first)
                  this.travels.sort((a, b) => 
                    new Date(b.startDate).getTime() - new Date(a.startDate).getTime()
                  );
                },
                error: (error) => {
                  console.error('Error loading apartment details:', error);
                }
              });
            });
          },
          error: (error) => {
            console.error('Error loading travels:', error);
            this.errorMessage = 'Failed to load travels';
          }
        });
      } catch (error) {
        console.error('Error parsing auth token:', error);
        this.errorMessage = 'Invalid authentication token';
      }
    }
  }

  viewTravelDetails(reservationId: number) {
    console.log('Viewing travel details for reservation:', reservationId);
  }

  loadPayments() {
    const authToken = localStorage.getItem('my-auth-token');
    if (authToken) {
      try {
        const parsedToken = JSON.parse(authToken);
        const userId = parsedToken.myAuthInfo.userId;
        const token = parsedToken.token;

        this.http.get<Reservation[]>(`${this.apiUrl}/Reservation/Get`, {
          headers: { 'my-auth-token': token }
        }).subscribe({
          next: (reservations) => {
            const userReservations = reservations.filter(res => res.accountID === userId);
            let totalSpent = 0;
            let upcomingPayments = 0;
            const payments: Payment[] = [];

            userReservations.forEach(reservation => {
              this.http.get<Apartment>(`${this.apiUrl}/Apartment/GetById/${reservation.apartmentId}`, {
                headers: { 'my-auth-token': token }
              }).subscribe({
                next: (apartment) => {
                  const startDate = new Date(reservation.startDate);
                  const endDate = new Date(reservation.endDate);
                  const today = new Date();

                  // Calculate the number of nights
                  const nights = Math.ceil((endDate.getTime() - startDate.getTime()) / (1000 * 60 * 60 * 24));
                  
                  // Calculate total amount for the stay (60 euros per night)
                  const pricePerNight = apartment.pricePerNight || 60; // Default to 60 euros if not set
                  const totalAmount = pricePerNight * nights;

                  const payment: Payment = {
                    reservationID: reservation.reservationID,
                    amount: totalAmount,
                    date: reservation.startDate,
                    status: startDate > today ? 'Upcoming' : 'Completed',
                    apartmentName: apartment.name,
                    nights: nights,
                    pricePerNight: pricePerNight
                  };

                  if (startDate <= today) {
                    totalSpent += totalAmount;
                  } else {
                    upcomingPayments += totalAmount;
                  }

                  payments.push(payment);
                  
                  // Update the component properties
                  this.totalSpent = totalSpent;
                  this.upcomingPayments = upcomingPayments;
                  this.payments = payments.sort((a, b) => 
                    new Date(b.date).getTime() - new Date(a.date).getTime()
                  );
                },
                error: (error) => {
                  console.error('Error loading apartment details:', error);
                  this.errorMessage = 'Failed to load payment details';
                }
              });
            });
          },
          error: (error) => {
            console.error('Error loading payments:', error);
            this.errorMessage = 'Failed to load payments';
          }
        });
      } catch (error) {
        console.error('Error parsing auth token:', error);
        this.errorMessage = 'Invalid authentication token';
      }
    }
  }

  // Add these methods
  loadUserReviews() {
    const authToken = localStorage.getItem('my-auth-token');
    if (authToken) {
      try {
        const parsedToken = JSON.parse(authToken);
        const userId = parsedToken.myAuthInfo.userId;
        const token = parsedToken.token;

        this.http.get<Review[]>(`${this.apiUrl}/Review/GetByUser/${userId}`, {
          headers: { 'my-auth-token': token }
        }).subscribe({
          next: (reviews) => {
            this.reviews = reviews;
          },
          error: (error) => {
            console.error('Error loading reviews:', error);
            this.errorMessage = 'Failed to load reviews';
          }
        });
      } catch (error) {
        console.error('Error parsing auth token:', error);
        this.errorMessage = 'Invalid authentication token';
      }
    }
  }

  editReview(reviewId: number) {
    // Implement edit functionality
    console.log('Editing review:', reviewId);
  }

  deleteReview(reviewId: number) {
    if (confirm('Are you sure you want to delete this review?')) {
      const authToken = localStorage.getItem('my-auth-token');
      if (authToken) {
        const parsedToken = JSON.parse(authToken);
        const token = parsedToken.token;

        this.http.delete(`${this.apiUrl}/Review/Delete/${reviewId}`, {
          headers: { 'my-auth-token': token }
        }).subscribe({
          next: () => {
            this.reviews = this.reviews.filter(r => r.id !== reviewId);
            this.successMessage = 'Review deleted successfully';
          },
          error: (error) => {
            console.error('Error deleting review:', error);
            this.errorMessage = 'Failed to delete review';
          }
        });
      }
    }
  }
}
