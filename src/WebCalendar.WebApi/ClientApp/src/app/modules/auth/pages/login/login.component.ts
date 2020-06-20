import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {ActivatedRoute, Router} from '@angular/router';
import {first} from 'rxjs/operators';
import {AuthenticationService} from '../../../../core/service/authentication.service';
import {User} from '../../../../data/schema/user';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  submitted = false;
  returnUrl: string;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private authenticationService: AuthenticationService) {
    if (this.authenticationService.currentUserValue) {
      this.router.navigate(['/calendar']);
    }
  }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6), Validators.pattern('.*[a-zA-Z].*')]]
    });

    this.returnUrl = this.route.snapshot.queryParams.returnUrl || '/calendar';
  }

  get f() {
    return this.loginForm.controls;
  }


  onSubmit() {
    this.submitted = true;

    if (this.loginForm.invalid) {
      return;
    }

    const user: User = {
      email: this.loginForm.value.email,
      password: this.loginForm.value.password
    };

    this.authenticationService.login(user.email, user.password)
      .pipe(first())
      .subscribe(
        data => {
          this.router.navigate([this.returnUrl]);
        },
        error => {
        });
  }
}
