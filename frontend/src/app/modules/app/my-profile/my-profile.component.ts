import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

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

interface Reservation {
  reservationID: number;
  accountID: number;
  apartmentId: number;
  startDate: string;
  endDate: string;
  status: boolean;
}

interface Travel {
  reservationID: number;
  startDate: string;
  endDate: string;
  duration: number;
  apartment: any;
}

@Component({
  selector: 'app-my-profile',
  templateUrl: './my-profile.component.html',
  styleUrls: ['./my-profile.component.css']
})
export class MyProfileComponent implements OnInit {
  currentUser: UserProfile | null = null;
  activeSection: string = 'profile';
  public apiUrl = 'http://localhost:8000';
  profileForm: FormGroup;
  isLoading: boolean = false;
  errorMessage: string | null = null;
  successMessage: string | null = null;
  travels: Travel[] = [];
  bookings: Reservation[] = [];

  constructor(
    private authService: AuthService,
    private http: HttpClient,
    private fb: FormBuilder
  ) {
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

  ngOnInit(): void {
    this.loadUserProfile();
    this.loadUserBookings();
    this.loadUserTravels();
  }

  private loadUserProfile() {
    const authToken = localStorage.getItem('my-auth-token');
    if (!authToken) {
      this.errorMessage = 'No authentication token found';
      return;
    }

    try {
      const parsedToken = JSON.parse(authToken);
      const userId = parsedToken.myAuthInfo.userId;

      // Get the user profile data using the correct endpoint
      this.http.get<UserProfile>(`${this.apiUrl}/User/Get/${userId}`, {
        headers: {
          'my-auth-token': parsedToken.token
        }
      }).subscribe({
        next: (user) => {
          this.currentUser = user;
          // Update form with user data
          this.profileForm.patchValue({
            firstName: user.firstName,
            lastName: user.lastName,
            email: user.email,
            phone: user.phone,
            username: user.username,
            cityID: user.cityID,
            genderID: user.genderID
          });
          this.successMessage = 'Profile loaded successfully';
        },
        error: (error) => {
          console.error('Error loading user data:', error);
          this.errorMessage = 'Failed to load user data';
        }
      });
    } catch (error) {
      console.error('Error parsing auth token:', error);
      this.errorMessage = 'Invalid authentication token';
    }
  }

  private loadUserBookings() {
    const authToken = localStorage.getItem('my-auth-token');
    if (authToken) {
      const parsedToken = JSON.parse(authToken);
      const userId = parsedToken.myAuthInfo.userId;
      
      this.http.get<Reservation[]>(`${this.apiUrl}/Reservation/Get`, {
        headers: {
          'my-auth-token': parsedToken.token
        }
      }).subscribe({
        next: (reservations) => {
          this.bookings = reservations.filter(res => res.accountID === userId);
        },
        error: (error) => {
          console.error('Error loading bookings:', error);
          this.errorMessage = 'Failed to load bookings';
        }
      });
    }
  }

  private loadUserTravels() {
    const authToken = localStorage.getItem('my-auth-token');
    if (authToken) {
      const parsedToken = JSON.parse(authToken);
      const userId = parsedToken.myAuthInfo.userId;
      
      this.http.get<Reservation[]>(`${this.apiUrl}/Reservation/Get`, {
        headers: {
          'my-auth-token': parsedToken.token
        }
      }).subscribe({
        next: (reservations) => {
          const userReservations = reservations.filter(res => res.accountID === userId);
          
          userReservations.forEach(reservation => {
            this.http.get<any>(`${this.apiUrl}/Apartment/GetById/${reservation.apartmentId}`, {
              headers: {
                'my-auth-token': parsedToken.token
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
    }
  }

  setActiveSection(section: string) {
    this.activeSection = section;
  }

  onSubmit() {
    if (this.profileForm.valid) {
      this.isLoading = true;
      const authToken = localStorage.getItem('my-auth-token');
      if (authToken) {
        const parsedToken = JSON.parse(authToken);
        const userId = parsedToken.myAuthInfo.userId;
        
        const updateData = {
          ...this.profileForm.value,
          accountID: userId
        };

        this.http.put(`${this.apiUrl}/User/Update`, updateData, {
          headers: {
            'my-auth-token': parsedToken.token
          }
        }).subscribe({
          next: () => {
            this.successMessage = 'Profile updated successfully';
            this.loadUserProfile(); // Reload the profile after update
          },
          error: (error) => {
            console.error('Error updating profile:', error);
            this.errorMessage = 'Failed to update profile';
          },
          complete: () => {
            this.isLoading = false;
          }
        });
      }
    }
  }
} 