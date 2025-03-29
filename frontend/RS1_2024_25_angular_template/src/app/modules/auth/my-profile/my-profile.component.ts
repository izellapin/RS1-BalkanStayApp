import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-my-profile',
  standalone: false,
  templateUrl: './my-profile.component.html',
  styleUrl: './my-profile.component.css'
})
export class MyProfileComponent implements OnInit {
  selectedMenuItem: string = 'Edit Profile'; // Default selected item
  profileImageUrl: string = 'assets/default-avatar.png'; // Default image path
  profileForm: FormGroup;

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
  bookings = [
    {
      id: 'BK001',
      status: 'Confirmed',
      property: 'Mountain View Villa',
      checkIn: '2024-07-15',
      checkOut: '2024-07-22',
      guests: 4
    },
    {
      id: 'BK002',
      status: 'Pending',
      property: 'Beachfront Resort',
      checkIn: '2024-08-01',
      checkOut: '2024-08-07',
      guests: 2
    }
  ];

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

  constructor(private fb: FormBuilder) {
    this.profileForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', Validators.required],
      address: ['', Validators.required],
      city: ['', Validators.required],
      country: ['', Validators.required]
    });
  }

  ngOnInit() {
    // Load user data here
    this.loadUserData();
  }

  selectMenuItem(item: string) {
    this.selectedMenuItem = item;
  }

  onImageSelected(event: any) {
    const file = event.target.files[0];
    if (file) {
      // Create a preview of the selected image
      const reader = new FileReader();
      reader.onload = (e: any) => {
        this.profileImageUrl = e.target.result;
      };
      reader.readAsDataURL(file);
    }
  }

  onSubmit() {
    if (this.profileForm.valid) {
      // TODO: Save user data to service
      console.log('Form submitted:', this.profileForm.value);
    }
  }

  resetForm() {
    this.profileForm.reset();
    this.loadUserData(); // Reload the original user data
  }

  private loadUserData() {
    // TODO: Load user data from service
    // For now, we'll just set some default values
    const userData = {
      firstName: '',
      lastName: '',
      email: '',
      phone: '',
      address: '',
      city: '',
      country: ''
    };
    this.profileForm.patchValue(userData);
  }

  // Booking actions
  cancelBooking(bookingId: string) {
    // TODO: Implement booking cancellation
    console.log('Cancelling booking:', bookingId);
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
}
