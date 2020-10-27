import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { AuthService } from '../services/auth-service';

@Component({
  selector: 'app-user-register',
  templateUrl: './user-register.component.html',
  styleUrls: ['./user-register.component.scss']
})
export class UserRegisterComponent implements OnInit {
  registerForm: FormGroup;
  loading = false;
  submitted = false;
  error: string;
  @Output() messageToEmit = new EventEmitter<boolean>();

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private authenticationService: AuthService) {
    // redirect to home if already logged in
    if (this.authenticationService.loggedIn()) {
      this.router.navigate(['/']);
    }
  }

  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  // convenience getter for easy access to form fields
  get f() { return this.registerForm.controls; }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.registerForm.invalid) {
      return;
    }

    this.loading = true;
    this.authenticationService.register(this.registerForm.value)
      .pipe(first())
      .subscribe(
        data => {
          this.authenticationService.toastrMessage = 'User has successfully registered';
          this.router.navigate(['/login'], { queryParams: { registered: true } });
        },
        error => {
          this.error = error;
          this.loading = false;
        });
  }

  cancel() {
    this.messageToEmit.emit(false);
  }
}
