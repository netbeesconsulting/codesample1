import { CustomerModel } from './../model/customerModel';
import { CustomerService } from './../service/customer.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ExtensionService } from '../../core/services/extension.service';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { HttpOperation } from '../../core/enums/httpOperation';

@Component({
  selector: 'app-customer-update',
  templateUrl: './customer-update.component.html',
  styleUrls: ['./customer-update.component.scss']
})
export class CustomerUpdateComponent extends ExtensionService implements OnInit {

  customerForm: FormGroup;
  customer = new CustomerModel();

  id: number;
  isFormSubmitted = false;
  isEdit = false;

  constructor(private fb: FormBuilder, private router: Router, private activatedRoute: ActivatedRoute,
    private customerService: CustomerService, protected toasterService: ToastrService) {
    super(toasterService);
  }

  ngOnInit() {
    this.createForm();
    this.activatedRoute.params.subscribe((params: Params) => {
      this.id = params['id'];
      if (this.id != 0 && this.id != undefined) {
        this.isEdit = true;
        this.getCustomerById();
      }
    });
  }

  createForm() {
    this.customerForm = this.fb.group({
      name: ['', [Validators.required]],
      company: ['', [Validators.required]],
      contact: ['', [Validators.required]],
      address: [''],
      username: ['', [Validators.required]],
      password: ['', [Validators.required]],
    });
  }

  async getCustomerById() {
    try {
      (await this.customerService.generateRequest(this.setup(`customer/${this.id}`), HttpOperation.Get))
        .subscribe((res: any) => {
          this.customer = res;
          this.patchCustomer(this.customer);
        },
          error => {
            this.warning(error);
          });
    } catch (error) {
      this.warning(error);
    }
  }

  patchCustomer(customer: CustomerModel) {
    this.customerForm.patchValue({
      name: customer.name,
      company: customer.company,
      address: customer.address,
      contact: customer.contact
    });
  }

  async save() {
    try {
      this.isFormSubmitted = true;
      if (this.customerForm.invalid) return;
      this.customer = Object.assign({}, this.customer, this.customerForm.value);
      (await this.customerService.generateRequest(this.setup("customer"), HttpOperation.Post, this.customer))
        .subscribe((res: any) => {
          this.success("Customer has been created successfully!");
          this.router.navigate(['customer/view']);
        },
          error => {
            this.warning(error);
          });
    } catch (error) {
      this.warning(error);
    }
  }

  async delete() {
    try {
      (await this.customerService.generateRequest(this.setup(`customer/${this.id}`), HttpOperation.Delete))
        .subscribe((res: any) => {
          this.router.navigate(['customer/view']);
          this.success("Customer has been deleted successfully!");
        },
          error => {
            this.warning(error);
          });
    } catch (error) {
      this.warning(error);
    }
  }

  async update() {  
    try {
      this.isFormSubmitted = true;
      if (this.customerForm.invalid) return;
      this.customer = Object.assign({}, this.customer, this.customerForm.value);
      (await this.customerService.generateRequest(this.setup("customer"), HttpOperation.Put, this.customer))
        .subscribe((res: any) => {
          this.success("customer has been updated successfully!");
          this.router.navigate(['customer/view']);
        },
          error => {
            this.warning(error);
          });
    } catch (error) {
      this.warning(error);
    }
  }

}
