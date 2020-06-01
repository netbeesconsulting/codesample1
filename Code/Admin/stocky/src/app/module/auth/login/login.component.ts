import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginModel } from '../model/loginModel';
import { HttpOperation } from '../../core/enums/httpOperation';
import { AuthService } from '../service/auth.service';
import { ExtensionService } from '../../core/services/extension.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent extends ExtensionService implements OnInit {

  userLoginForm: FormGroup;
  userLogin = new LoginModel();

  constructor(protected http: HttpClient, private fb: FormBuilder, private router: Router,
    private authService: AuthService, protected toasterService: ToastrService) {
    super(toasterService);
  }

  ngOnInit() {
    this.createForm();
  }

  createForm() {
    this.userLoginForm = this.fb.group({
      email: ['', [Validators.required]],
      password: ['', [Validators.required]],
    });
  }

  async authenticate() {
    try {
      this.userLogin = Object.assign({}, this.userLogin, this.userLoginForm.value);
      (await this.authService.generateRequest(this.setup("account/login"), HttpOperation.Post, this.userLogin))
        .subscribe((res: any) => {
          localStorage.setItem("Token", res.token);
          this.router.navigate(['dashboard']);
        },
          error => {
            this.warning(error);
          });
    } catch (error) {
      this.warning(error);
    }
  }
}
