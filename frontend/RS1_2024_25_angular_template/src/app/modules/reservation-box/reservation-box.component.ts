import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ReservationService } from '../../services/reservation/reservation.service';
import { Reservation } from '../../models/reservation.model';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MyAuthService } from '../../services/auth-services/my-auth.service';
import {NgClass} from '@angular/common';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-reservation-box',
  templateUrl: './reservation-box.component.html',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatFormFieldModule,
    MatInputModule,
    NgClass,
  ],
  styleUrls: ['./reservation-box.component.css']
})
export class ReservationBoxComponent implements OnInit {
  @Input() apartmentId!: number;
  reservationForm: FormGroup;
  occupiedDates: Date[] = [];
  minDate: Date = new Date();
  maxCheckOutDate: Date | null = null;
  minCheckOutDate: Date | null = null;
  message: string | null = null;
  messageType: 'error' | 'success' | null = null;
  confirmMode: boolean = false;
  pendingStartDate: Date | null = null;
  pendingEndDate: Date | null = null;


  constructor(
    private fb: FormBuilder,
    private reservationService: ReservationService,
    private authService: MyAuthService
  ) {
    this.reservationForm = this.fb.group({
      startDate: ['', Validators.required],
      endDate: ['', Validators.required]
    });
  }

  ngOnInit() {
    if (this.apartmentId) {
      this.loadOccupiedDates();
    }
  }

  private normalizeDate(date: Date): Date {
    return new Date(Date.UTC(date.getFullYear(), date.getMonth(), date.getDate()));
  }

  startDateFilter = (date: Date | null): boolean => {
    if (!date) return false;
    const normalizedDate = this.normalizeDate(date);


    const isLastDayOfReservation = this.occupiedDates.some((d, index, array) => {
      const nextDay = new Date(d);
      nextDay.setDate(nextDay.getDate() + 1); //sljedeci dan
      return (
        d.getTime() === normalizedDate.getTime() &&
        !array.some(occupiedDate => occupiedDate.getTime() === nextDay.getTime())
      );
    });

    // dozvoljava datum ako nije occupied ili ako je zadnji dan postojece rez.
    return (
      !this.occupiedDates.some(d => d.getTime() === normalizedDate.getTime()) ||
      isLastDayOfReservation
    );
  };

  endDateFilter = (date: Date | null): boolean => {
    if (!date || !this.reservationForm.controls['startDate'].value) return false;
    const normalizedDate = this.normalizeDate(date);

    console.log("Proverava End Date:", normalizedDate);

    const isFirstDayOfReservation = this.occupiedDates.some((d, index, array) => {
      const previousDay = new Date(d);
      previousDay.setDate(previousDay.getDate() - 1); // prethodni dan
      return (
        d.getTime() === normalizedDate.getTime() &&
        !array.some(occupiedDate => occupiedDate.getTime() === previousDay.getTime()) // prethodni slobodan
      );
    });

    // dozvoljava dan ako nije zauzer ili ako je prvi dan postojece
    return (
      !this.occupiedDates.some(d => d.getTime() === normalizedDate.getTime()) ||
      isFirstDayOfReservation
    );
  };

  loadOccupiedDates() {
    this.reservationService.getOccupiedDates(this.apartmentId).subscribe(dates => {
      let blockedDates: Date[] = [];
      dates.forEach(d => {
        let start = this.normalizeDate(new Date(d.startDate));
        let end = this.normalizeDate(new Date(d.endDate));
        let currentDate = new Date(start);
        while (currentDate <= end) {
          blockedDates.push(this.normalizeDate(new Date(currentDate)));
          currentDate.setDate(currentDate.getDate() + 1);
        }
      });
      this.occupiedDates = blockedDates;
    });
  }

  onStartDateChange(selectedDate: Date | null) {
    if (!selectedDate) return;

    const normalizedStart = this.normalizeDate(selectedDate);

    this.minCheckOutDate = normalizedStart;

    // reset endate dtp nakon sto je startdate izabran
    const firstOccupied = this.occupiedDates
      .filter(d => d > normalizedStart)
      .sort((a, b) => a.getTime() - b.getTime())[0];

    this.maxCheckOutDate = firstOccupied ?? null;

    // end date ne smije biti prije start date
    if (this.reservationForm.controls['endDate'].value &&
      new Date(this.reservationForm.controls['endDate'].value) < normalizedStart) {
      this.reservationForm.controls['endDate'].setValue(null); // resetuje endate ako je manji od start
    }
  }
  cancelConfirmation() {
    this.confirmMode = false;
    this.pendingStartDate = null;
    this.pendingEndDate = null;
    this.reservationForm.reset();
    this.minCheckOutDate = null;
    this.maxCheckOutDate = null;
  }


  confirmReservation() {
    const loggedInUser = this.authService.getMyAuthInfo();
    if (!loggedInUser || !this.pendingStartDate || !this.pendingEndDate) {
      this.message = 'Something went wrong.';
      this.messageType = 'error';
      return;
    }
    const reservation: Reservation = {
      reservationID: 0,
      accountID: loggedInUser.userId,
      apartmentId: this.apartmentId,
      startDate: this.pendingStartDate.toISOString(),
      endDate: this.pendingEndDate.toISOString(),
      status: true
    };

    this.reservationService.createReservation(reservation).subscribe(() => {
      this.message = 'Reservation successful! Find it on your profile.';
      this.messageType = 'success';
      this.reservationForm.reset();
      this.confirmMode = false;
      this.pendingStartDate = null;
      this.pendingEndDate = null;
      this.minCheckOutDate = null;
      this.maxCheckOutDate = null;


      setTimeout(() => {
        this.message = null;
        this.messageType = null;
      }, 5000);
    });
  }

  submitReservation() {
    this.message = null;
    this.messageType = null;

    if (this.reservationForm.valid) {
      const startDate = this.normalizeDate(this.reservationForm.value.startDate);
      const endDate = this.normalizeDate(this.reservationForm.value.endDate);

      const loggedInUser = this.authService.getMyAuthInfo();
      if (!loggedInUser) {
        this.message = 'You must be logged in to make a reservation.';
        this.messageType = 'error';
        return;
      }

      this.confirmMode = true;
      this.pendingStartDate = startDate;
      this.pendingEndDate = endDate;
    }
  }



}
