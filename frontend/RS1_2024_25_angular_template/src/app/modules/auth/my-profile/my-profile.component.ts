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
  imagePath?: string;  // Optional field for profile image
}

interface Reservation {
  reservationID: number;
  accountID: number;
  apartmentId: number;
  startDate: string;
  endDate: string;
  status: boolean;
  username: string;
  email: string;
  firstName: string;
  lastName: string;
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
  profileImageUrl: string = this.apiUrl + '/images/default.jpg';
  profileForm: FormGroup;
  isLoading: boolean = false;
  errorMessage: string | null = null;
  successMessage: string | null = null;

  // Travels data
  travels = [
    {
      destination: 'Paris, France',
      date: 'June 2024',
      duration: '7 days',
      accommodation: 'Hotel Eiffel View',
      location: 'Champ de Mars'
    },
    {
      destination: 'Tokyo, Japan',
      date: 'August 2024',
      duration: '10 days',
      accommodation: 'Sakura Hotel',
      location: 'Shibuya'
    }
  ];

  // Bookings data
  bookings: Reservation[] = [];

  // Payments data
  totalSpent = 2500;
  upcomingPayments = 1500;
  payments = [
    {
      id: 'PAY001',
      status: 'Completed',
      date: '2024-03-15',
      amount: 1000,
      method: 'Credit Card'
    },
    {
      id: 'PAY002',
      status: 'Pending',
      date: '2024-04-01',
      amount: 1500,
      method: 'Bank Transfer'
    }
  ];

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
  }

  selectMenuItem(item: string) {
    this.selectedMenuItem = item;
  }

  onImageSelected(event: any) {
    const file = event.target.files[0];
    if (file) {
      const authInfo = localStorage.getItem('authinfo');
      if (authInfo) {
        const { userId } = JSON.parse(authInfo);
        
        const formData = new FormData();
        formData.append('file', file);
        formData.append('userId', userId.toString());

        this.isLoading = true;
        this.errorMessage = '';

        // Correct URL
        this.http.post(`${this.apiUrl}/User/UploadProfileImage`, formData)
          .subscribe({
            next: (response: any) => {
              console.log('Upload successful:', response);
              if (response.imagePath) {
                this.profileImageUrl = this.apiUrl + response.imagePath;
              }
              this.successMessage = 'Profile image updated successfully!';
              this.isLoading = false;
            },
            error: (error) => {
              console.error('Upload error:', error);
              this.errorMessage = 'Failed to upload profile image. Please try again.';
              this.isLoading = false;
              this.loadUserData();
            }
          });
      }
    }
  }

  onSubmit() {
    if (this.profileForm.valid) {
      this.isLoading = true;
      const authInfo = localStorage.getItem('authinfo');
      if (authInfo) {
        const { userId } = JSON.parse(authInfo);
        
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

        this.updateProfile(updateData).subscribe({
          next: (response) => {
            console.log('Full response:', response);
            this.successMessage = 'Profile updated successfully!';
            this.isLoading = false;
            this.loadUserData();
          },
          error: (error) => {
            console.error('Full error:', error);
            if (error.error?.errors) {
              // Log the full error object to see its structure
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
    const authInfo = localStorage.getItem('authinfo');
    if (authInfo) {
      const { userId } = JSON.parse(authInfo);
      
      this.http.get<UserProfile>(`${this.apiUrl}/User/GetById/${userId}`).subscribe({
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

          // Fix the URL construction
          if (userData.userImages && userData.userImages.length > 0) {
            // Remove the apiUrl if it's already in the imagePath
            const imagePath = userData.userImages[0].image.imagePath;
            this.profileImageUrl = imagePath.startsWith('http') 
              ? imagePath 
              : this.apiUrl + imagePath;
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

  viewBookingDetails(bookingId: number) {
    // Navigate to booking details page or show modal with details
    // Implement according to your requirements
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

  private updateProfile(updateData: UserUpdateRequest) {
    const headers = {
      'Content-Type': 'application/json'
    };

    return this.http.put(`${this.apiUrl}/User/Update`, updateData, { 
      headers: headers,
      observe: 'response'  // This will give us the full HTTP response
    });
  }

  public handleImageError(): void {
    this.profileImageUrl = this.apiUrl + '/images/default.jpg';
  }

  private loadUserBookings() {
    const authInfo = localStorage.getItem('authinfo');
    if (authInfo) {
      const { userId } = JSON.parse(authInfo);
      
      // Use the Get endpoint and filter for the current user
      this.http.get<Reservation[]>(`${this.apiUrl}/Reservation/Get`)
        .subscribe({
          next: (reservations) => {
            console.log('All reservations:', reservations);
            // Filter reservations for current user
            this.bookings = reservations.filter(res => res.accountID === userId);
            console.log('User bookings:', this.bookings);
          },
          error: (error) => {
            console.error('Error loading bookings:', error);
            this.errorMessage = 'Failed to load bookings';
          }
        });
    }
  }
}
