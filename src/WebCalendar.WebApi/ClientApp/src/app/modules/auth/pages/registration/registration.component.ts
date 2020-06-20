import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {Router} from '@angular/router';
import {User} from '../../../../data/schema/user';
import {AuthenticationService} from '../../../../core/service/authentication.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  registerForm: FormGroup;
  submitted = false;

  serverErrors = {
    duplicateEmail: false
  };

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private authenticationService: AuthenticationService
  ) {
  }

  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6), Validators.pattern('.*[a-zA-Z].*')]]
    });
  }

  get f() {
    return this.registerForm.controls;
  }

  hasServerErrors(): boolean {
    for (const key in this.serverErrors) {
      if (this.serverErrors[key] == true) {
        return true;
      }
    }
    return false;
  }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.registerForm.invalid) {
      console.log(this.registerForm);
      return;
    }

    const user: User = {
      firstName: this.registerForm.value.firstName,
      lastName: this.registerForm.value.lastName,
      email: this.registerForm.value.email,
      password: this.registerForm.value.password
    };

    this.authenticationService.register(user).subscribe(response => {
        this.router.navigate(['/login']);
      },
      errors => {
        if (errors?.some(m => m.code === 'DuplicateEmail')) {
          this.serverErrors.duplicateEmail = true;
        }
      });
  }

}
