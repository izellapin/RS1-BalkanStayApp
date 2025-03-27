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
  profileForm: FormGroup;
  profileImageUrl: string = 'assets/default-avatar.png'; // Default image path

  constructor(private fb: FormBuilder) {
    this.profileForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: [''],
      address: [''],
      city: [''],
      country: ['']
    });
  }

  ngOnInit() {
    // Here you would typically load the user's data
    // this.loadUserData();
  }

  selectMenuItem(item: string) {
    this.selectedMenuItem = item;
  }

  onSubmit() {
    if (this.profileForm.valid) {
      console.log(this.profileForm.value);
      // Here you would typically send the data to your backend
    }
  }

  resetForm() {
    this.profileForm.reset();
    // Optionally reload the original user data
    // this.loadUserData();
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

  // Example method to load user data
  private loadUserData() {
    // Here you would typically make an API call to get user data
    const userData = {
      firstName: 'John',
      lastName: 'Doe',
      email: 'john@example.com',
      phone: '123-456-7890',
      address: '123 Main St',
      city: 'New York',
      country: 'USA'
    };
    this.profileForm.patchValue(userData);
  }
}
