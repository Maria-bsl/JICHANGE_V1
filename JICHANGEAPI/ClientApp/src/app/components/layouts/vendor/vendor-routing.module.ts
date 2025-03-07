import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { VendorComponent } from './vendor.component';
import { DashboardComponent } from '../../../pages/vendor/dashboard/dashboard.component';
import { ProfileComponent } from '../../../pages/vendor/profile/profile.component';
import { CustomersListComponent } from '../../../pages/vendor/customers/customers-list/customers-list.component';
import { CustomerViewComponent } from '../../../pages/vendor/customers/customer-view/customer-view.component';
import { TransactionDetailsViewComponent } from '../../../pages/bank/reports/transaction-details/transaction-details-view/transaction-details-view.component';
import { AddInvoiceComponent } from '../../../pages/vendor/invoice/created-invoice-list/add-invoice/add-invoice.component';
import { CompanyUsersComponent } from '../../../pages/vendor/company-users/company-users.component';
import { CreatedInvoiceListComponent } from '../../../pages/vendor/invoice/created-invoice-list/list/created-invoice-list.component';
import { GeneratedInvoiceListComponent } from '../../../pages/vendor/invoice/generated-invoice-list/generated-invoice-list.component';
import { OverviewComponent } from '../../../pages/vendor/reports/overview/overview.component';
import { ListComponent } from '../../../pages/vendor/reports/transactions/list/list.component';
import { InvoiceDetailsComponent } from '../../../pages/vendor/reports/invoice-details/invoice-details.component';
import { PaymentDetailsComponent } from '../../../pages/vendor/reports/payment-details/payment-details.component';
import { AmendmentsComponent } from '../../../pages/vendor/reports/amendments/amendments.component';
import { InvoiceCancelledComponent } from '../../../pages/vendor/reports/invoice-cancelled/invoice-cancelled.component';
import { CustomerReportComponent } from '../../../pages/vendor/reports/customer-report/customer-report.component';
import { AuditTrailsComponent } from '../../../pages/vendor/reports/audit-trails/audit-trails.component';

const routes: Routes = [
  {
    path: '',
    component: VendorComponent,
    children: [
      {
        path: '',
        data: { breadcrumb: { skip: true } },
        children: [
          {
            path: '',
            redirectTo: 'dashboard',
            pathMatch: 'full',
          },
          {
            path: 'dashboard',
            data: {
              breadcrumb: { alias: 'dashboard', skip: false },
              animationState: 'dashboard',
              reuseRoute: true,
            },
            component: DashboardComponent,
          },
          {
            path: 'profile',
            data: {
              breadcrumb: { alias: 'profile', skip: false },
              animationState: 'profile',
            },
            component: ProfileComponent,
          },
          {
            path: 'customers',
            data: { breadcrumb: { skip: true } },
            children: [
              {
                path: '',
                /*loadComponent: () =>
                  import(
                    '../../../pages/vendor/customers/customers-list/customers-list.component'
                  ).then((c) => c.CustomersListComponent),*/
                component: CustomersListComponent,
                data: {
                  breadcrumb: { alias: 'customers', skip: false },
                  animationState: 'isLeft-1',
                  reuseRoute: true,
                },
              },
              {
                path: ':id',
                children: [
                  {
                    path: '',
                    /*loadComponent: () =>
                      import(
                        '../../../pages/vendor/customers/customer-view/customer-view.component'
                      ).then((c) => c.CustomerViewComponent),*/
                    component: CustomerViewComponent,
                    data: {
                      breadcrumb: { alias: 'view-customer', skip: false },
                      animationState: 'isRight-1',
                      reuseRoute: true,
                    },
                  },

                  {
                    path: ':id',
                    /* loadComponent: () =>
                      import(
                        '../../../pages/bank/reports/transaction-details/transaction-details-view/transaction-details-view.component'
                      ).then((c) => c.TransactionDetailsViewComponent),*/
                    component: TransactionDetailsViewComponent,
                    data: {
                      breadcrumb: {
                        alias: 'view-customer-transactions',
                        skip: false,
                      },
                      animationState: 'isRight-3',
                    },
                  },
                  {
                    path: 'add/add',
                    /* loadComponent: () =>
                      import(
                        '../../../pages/vendor/invoice/created-invoice-list/add-invoice/add-invoice.component'
                      ).then((m) => m.AddInvoiceComponent),*/
                    component: AddInvoiceComponent,
                    data: {
                      breadcrumb: {
                        alias: 'addInvoice',
                        skip: false,
                      },
                      animationState: 'isRight-4',
                    },
                  },
                ],
              },
            ],
          },
          {
            path: 'company',
            data: { breadcrumb: { skip: true } },
            children: [
              {
                path: '',
                data: {
                  breadcrumb: { alias: 'company', skip: false },
                  animationState: 'company-module-1',
                },
                /* loadComponent: () =>
                  import(
                    '../../../pages/vendor/company-users/company-users.component'
                  ).then((c) => c.CompanyUsersComponent),*/
                component: CompanyUsersComponent,
              },
            ],
          },
          {
            path: 'invoice',
            data: { breadcrumb: { skip: true } },
            children: [
              {
                path: '',
                redirectTo: 'list',
                pathMatch: 'full',
              },
              {
                path: 'list',
                children: [
                  {
                    path: '',
                    /*loadComponent: () =>
                      import(
                        '../../../pages/vendor/invoice/created-invoice-list/list/created-invoice-list.component'
                      ).then((l) => l.CreatedInvoiceListComponent),*/
                    component: CreatedInvoiceListComponent,
                    data: {
                      breadcrumb: { alias: 'invoice-created', skip: false },
                      animationState: 'invoice-module-1',
                    },
                  },
                  {
                    path: 'add',
                    /*loadComponent: () =>
                      import(
                        '../../../pages/vendor/invoice/created-invoice-list/add-invoice/add-invoice.component'
                      ).then((a) => a.AddInvoiceComponent),*/
                    component: AddInvoiceComponent,
                    data: {
                      breadcrumb: { alias: 'add', skip: false },
                      animationState: 'invoice-module-2',
                    },
                  },
                ],
              },
              {
                path: 'generated',
                /*loadComponent: () =>
                  import(
                    '../../../pages/vendor/invoice/generated-invoice-list/generated-invoice-list.component'
                  ).then((c) => c.GeneratedInvoiceListComponent),*/
                component: GeneratedInvoiceListComponent,
                data: {
                  breadcrumb: { alias: 'invoice-generated', skip: false },
                  animationState: 'invoice-module-3',
                },
              },
            ],
          },
          {
            path: 'reports',
            data: { breadcrumb: { skip: true } },
            children: [
              {
                path: '',
                redirectTo: 'transactions',
                pathMatch: 'full',
              },
              {
                path: 'overview',
                redirectTo: 'transactions',
                pathMatch: 'full',
              },
              {
                path: 'transactions',
                data: {
                  breadcrumb: { alias: 'transactions', skip: true },
                  animationState: 'reports-module-2',
                },
                children: [
                  {
                    path: '',
                    /*loadComponent: () =>
                      import(
                        '../../../pages/vendor/reports/transactions/list/list.component'
                      ).then((c) => c.ListComponent),*/
                    component: ListComponent,
                    data: {
                      breadcrumb: { alias: 'transactions', skip: false },
                      animationState: 'isLeft-2',
                      reuseRoute: true,
                    },
                  },
                  {
                    path: ':id/:transactionId',
                    /*loadComponent: () =>
                      import(
                        '../../../pages/bank/reports/transaction-details/transaction-details-view/transaction-details-view.component'
                      ).then((c) => c.TransactionDetailsViewComponent),*/
                    component: TransactionDetailsViewComponent,
                    data: {
                      breadcrumb: {
                        alias: 'view-customer-transactions',
                        skip: false,
                        reuseRoute: true,
                      },
                      animationState: 'isRight-2',
                    },
                  },
                ],
              },
              {
                path: 'invoice',
                data: {
                  breadcrumb: { alias: 'invoice', skip: false },
                  animationState: 'reports-module-3',
                  reuseRoute: true,
                },
                /*loadComponent: () =>
                  import(
                    '../../../pages/vendor/reports/invoice-details/invoice-details.component'
                  ).then((c) => c.InvoiceDetailsComponent),*/
                component: InvoiceDetailsComponent,
              },
              {
                path: 'payments',
                data: {
                  breadcrumb: { alias: 'payments', skip: false },
                  animationState: 'reports-module-4',
                  reuseRoute: true,
                },
                /*loadComponent: () =>
                  import(
                    '../../../pages/vendor/reports/payment-details/payment-details.component'
                  ).then((p) => p.PaymentDetailsComponent),*/
                component: PaymentDetailsComponent,
              },
              {
                path: 'amendment',
                data: {
                  breadcrumb: { alias: 'amendment', skip: false },
                  animationState: 'reports-module-5',
                  reuseRoute: true,
                },
                /*loadComponent: () =>
                  import(
                    '../../../pages/vendor/reports/amendments/amendments.component'
                  ).then((a) => a.AmendmentsComponent),*/
                component: AmendmentsComponent,
              },
              {
                path: 'cancelled',
                /*loadComponent: () =>
                  import(
                    '../../../pages/vendor/reports/invoice-cancelled/invoice-cancelled.component'
                  ).then((c) => c.InvoiceCancelledComponent),*/
                component: InvoiceCancelledComponent,
                data: {
                  breadcrumb: { alias: 'invoice-cancelled', skip: false },
                  animationState: 'reports-module-6',
                  reuseRoute: true,
                },
              },
              {
                path: 'customer',
                data: {
                  breadcrumb: { alias: 'customer', skip: false },
                  animationState: 'reports-module-7',
                  reuseRoute: true,
                },
                /*loadComponent: () =>
                  import(
                    '../../../pages/vendor/reports/customer-report/customer-report.component'
                  ).then((c) => c.CustomerReportComponent),*/
                component: CustomerReportComponent,
              },
              {
                path: 'audit',
                data: {
                  breadcrumb: { alias: 'audit', skip: false },
                  animationState: 'reports-module-8',
                  reuseRoute: true,
                },
                component: AuditTrailsComponent,
                /*loadComponent: () =>
                  import(
                    '../../../pages/vendor/reports/audit-trails/audit-trails.component'
                  ).then((e) => e.AuditTrailsComponent),*/
              },
            ],
          },
        ],
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class VendorRoutingModule {}
