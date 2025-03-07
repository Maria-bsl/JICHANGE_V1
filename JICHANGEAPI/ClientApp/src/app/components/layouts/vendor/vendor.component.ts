import { CommonModule } from '@angular/common';
import {
  AfterViewInit,
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  ElementRef,
  Inject,
  OnInit,
  signal,
  ViewChild,
  WritableSignal,
} from '@angular/core';
import {
  NavigationCancel,
  NavigationEnd,
  NavigationError,
  NavigationStart,
  Router,
  RouterModule,
  RouterOutlet,
} from '@angular/router';
import { FooterComponent } from '../footer/footer.component';
import { VendorHeaderComponent } from '../vendor-header/vendor-header.component';
import {
  inOutAnimation,
  vendorAnimations,
} from '../main/router-transition-animations';
import { BreadcrumbModule, BreadcrumbService } from 'xng-breadcrumb';
import {
  TRANSLOCO_LOADER,
  TRANSLOCO_SCOPE,
  TranslocoModule,
  TranslocoService,
} from '@ngneat/transloco';
import { NgxLoadingModule } from 'ngx-loading';
import { NgxSonnerToaster } from 'ngx-sonner';
import { TranslocoHttpLoader } from '../../../transloco-loader';
import { AppUtilities } from '../../../utilities/app-utilities';
import {
  catchError,
  defaultIfEmpty,
  defer,
  filter,
  from,
  map,
  Observable,
  of,
  shareReplay,
  zip,
} from 'rxjs';
import { MatIconModule } from '@angular/material/icon';
import { FlatTreeControl, NestedTreeControl } from '@angular/cdk/tree';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { CdkAccordionItem, CdkAccordionModule } from '@angular/cdk/accordion';
import {
  trigger,
  state,
  style,
  transition,
  animate,
} from '@angular/animations';
import { AppConfigService } from 'src/app/core/services/app-config.service';
import { VendorLoginResponse } from 'src/app/core/models/login-response';
import { CompanyUserService } from 'src/app/core/services/vendor/company-user.service';
import { CompanyUser } from 'src/app/core/models/vendors/company-user';
import { MatButtonModule } from '@angular/material/button';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
} from '@angular/forms';
import { LanguagesPipe } from 'src/app/core/pipes/languages-pipe/languages.pipe';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

export interface ILanguage {
  code: string;
  label: string;
  icon: string;
}

interface FoodNode {
  name: string;
  children: FoodNode[];
  icon?: string;
  routerLink?: string;
}

const TREE_DATA: FoodNode[] = [
  {
    name: 'Dashboard',
    children: [],
    icon: 'dashboard',
    routerLink: '/vendor/dashboard',
  },
  {
    name: 'Customers',
    children: [],
    icon: 'storefront',
    routerLink: '/vendor/customers',
  },
  {
    name: 'Users',
    children: [],
    icon: 'group',
    routerLink: '/vendor/company',
  },
  {
    name: 'Invoices',
    routerLink: '/vendor/invoice',
    children: [
      { name: 'Waitlist', routerLink: '/vendor/invoice/list', children: [] },
      {
        name: 'Generated',
        routerLink: '/vendor/invoice/generated',
        children: [],
      },
    ],
    icon: 'summarize',
  },
  {
    name: 'Reports',
    icon: 'receipt_long',
    routerLink: '/vendor/reports',
    children: [
      {
        name: 'Payments',
        children: [],
        routerLink: '/vendor/reports/transactions',
      },
      {
        name: 'Completed Payments',
        children: [],
        routerLink: '/vendor/reports/payments',
      },
      {
        name: 'Invoice',
        children: [],
        routerLink: '/vendor/reports/invoice',
      },
      {
        name: 'Amendments',
        children: [],
        routerLink: '/vendor/reports/amendment',
      },
      {
        name: 'Cancelled',
        children: [],
        routerLink: '/vendor/reports/cancelled',
      },
      {
        name: 'Audit Trails',
        children: [],
        routerLink: '/vendor/reports/audit',
      },
    ],
  },
];

@Component({
  selector: 'app-vendor',
  templateUrl: './vendor.component.html',
  styleUrls: ['./vendor.component.scss'],
  imports: [
    CommonModule,
    RouterModule,
    VendorHeaderComponent,
    FooterComponent,
    BreadcrumbModule,
    NgxLoadingModule,
    NgxSonnerToaster,
    TranslocoModule,
    MatIconModule,
    CdkAccordionModule,
    MatButtonModule,
    ReactiveFormsModule,
    LanguagesPipe,
  ],
  animations: [
    vendorAnimations,
    inOutAnimation,
    trigger('contentExpansion', [
      state(
        'expanded',
        style({ height: '*', opacity: 1, visibility: 'visible' })
      ),
      state(
        'collapsed',
        style({ height: '0px', opacity: 0, visibility: 'hidden' })
      ),
      transition(
        'expanded <=> collapsed',
        animate('300ms cubic-bezier(.37,1.04,.68,.98)')
      ),
    ]),
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class VendorComponent implements OnInit, AfterViewInit {
  public routeLoading: boolean = false;
  @ViewChild('vendorHeader') vendorHeader!: VendorHeaderComponent;
  @ViewChild('containerDoc', { static: true })
  containerDoc!: ElementRef<HTMLDivElement>;
  expanded: WritableSignal<boolean> = signal<boolean>(false);
  treeControl = new NestedTreeControl<FoodNode>((node) => node.children);
  dataSource = new MatTreeNestedDataSource<FoodNode>();
  hasChild = (_: number, node: FoodNode) =>
    !!node.children && node.children.length > 0;
  companyUser$!: Observable<CompanyUser>;
  languages$: Observable<ILanguage[]> = of([
    { code: 'en', label: 'English', icon: 'gb' },
    { code: 'sw', label: 'Swahili', icon: 'tz' },
  ]);
  formGroup: FormGroup = this._fb.group({
    language: this._fb.control(localStorage.getItem('activeLang') ?? 'en', []),
  });
  constructor(
    private breadcrumbService: BreadcrumbService,
    private router: Router,
    private tr: TranslocoService,
    private cdr: ChangeDetectorRef,
    private appConfig: AppConfigService,
    private companyUserService: CompanyUserService,
    private _fb: FormBuilder,
    @Inject(TRANSLOCO_SCOPE) private scope: any
  ) {
    this.registerIcons();
    this.dataSource.data = TREE_DATA;
    this.languageValueChanged();
  }
  private languageValueChanged() {
    // this.tr.setActiveLang(lang.code);
    // localStorage.setItem('activeLang', lang.code);
    this.language.valueChanges
      .pipe(filter((value) => value === 'en' || value === 'sw'))
      .subscribe({
        next: (value) =>
          this.tr.setActiveLang(value) &&
          localStorage.setItem('activeLang', value),
      });
  }
  private prepareVendorRoutes(routes: any) {
    this.breadcrumbService.set(
      '@profile',
      this.tr.translate(`${routes}.profile`)
    );
    this.breadcrumbService.set('@vendor', this.tr.translate(`${routes}.home`));
    this.breadcrumbService.set(
      '@customers',
      this.tr.translate(`${routes}.customer`)
    );
    this.breadcrumbService.set(
      '@view-customer',
      this.tr.translate(`${routes}.detail`)
    );
    this.breadcrumbService.set(
      '@view-customer-transactions',
      this.tr.translate(`${routes}.viewCustomerTransactions`)
    );
    this.breadcrumbService.set(
      '@company',
      this.tr.translate(`${routes}.users`)
    );
    this.breadcrumbService.set(
      '@invoice-created',
      this.tr.translate(`${routes}.created`)
    );
    this.breadcrumbService.set(
      '@invoice-amendments',
      this.tr.translate(`${routes}.amendment`)
    );
    this.breadcrumbService.set(
      '@invoice-cancelled',
      this.tr.translate(`${routes}.cancelled`)
    );
    this.breadcrumbService.set(
      '@invoice-generated',
      this.tr.translate(`${routes}.invoice`)
    );
    this.breadcrumbService.set(
      '@overview',
      this.tr.translate(`${routes}.overview`)
    );
    this.breadcrumbService.set(
      '@transactions',
      this.tr.translate(`${routes}.transactionsReport`)
    );
    this.breadcrumbService.set(
      '@transactions-id',
      this.tr.translate(`${routes}.detail`)
    );
    this.breadcrumbService.set(
      '@invoice',
      this.tr.translate(`${routes}.invoiceReport`)
    );
    this.breadcrumbService.set(
      '@payments',
      this.tr.translate(`${routes}.paymentReport`)
    );
    this.breadcrumbService.set(
      '@amendment',
      this.tr.translate(`${routes}.amendmentReport`)
    );
    this.breadcrumbService.set(
      '@customer',
      this.tr.translate(`${routes}.customerReport`)
    );
    this.breadcrumbService.set(
      '@addInvoice',
      this.tr.translate(`${routes}.addInvoice`)
    );
  }
  private routeLoaderListener() {
    this.router.events.subscribe((event) => {
      switch (true) {
        case event instanceof NavigationStart: {
          this.routeLoading = true;
          this.cdr.detectChanges();
          break;
        }
        case event instanceof NavigationEnd:
        case event instanceof NavigationCancel:
        case event instanceof NavigationError: {
          this.routeLoading = false;
          this.cdr.detectChanges();
          break;
        }
        default: {
          break;
        }
      }
    });
  }
  private requestCompanyUser() {
    this.companyUser$ = this.companyUserService
      .getCompanyUserByid({
        Sno: this.userProfile.Usno,
      })
      .pipe(
        catchError((err) => {
          throw err;
        }),
        filter((res) => !AppUtilities.hasErrorResult(res)),
        map((res) => res.response as CompanyUser),
        shareReplay()
      );
  }
  private registerIcons() {
    const icons = ['gb', 'tz'];
    this.appConfig.addIcons(icons, '/assets/img');
  }
  ngOnInit(): void {
    this.routeLoaderListener();
    this.tr.selectTranslate('vendorRoutes').subscribe({
      next: (res) => {
        this.prepareVendorRoutes(res);
      },
    });
    this.requestCompanyUser();
  }
  ngAfterViewInit(): void {
    //this.hideNavBarOnScroll();
    //this.prepareVendorRoutes();
    //this.createVendorRoutesTranslation();
  }
  prepareRoute(outlet: RouterOutlet, animate: string): boolean {
    return (
      outlet && outlet.activatedRouteData && outlet.activatedRouteData[animate]
    );
  }
  handleSidebarItemClicked(
    event: MouseEvent,
    accordionItem: CdkAccordionItem,
    data: FoodNode
  ) {
    event.stopPropagation(); // Stops event bubbling (optional, if needed)
    event.preventDefault(); // Prevents navigation

    if (data.children.length > 0) {
      this.expanded.set(true);
      setTimeout(() => {
        accordionItem.toggle();
      }, 200);
    } else {
      data.routerLink && this.router.navigate([data.routerLink]);
    }
  }
  handleExpandedSidebarItemClicked(
    event: MouseEvent,
    accordionItem: CdkAccordionItem,
    index: number,
    data: FoodNode
  ) {
    event.stopPropagation(); // Stops event bubbling (optional, if needed)
    event.preventDefault(); // Prevents navigation
    if (index > 2) {
      accordionItem.toggle();
    } else {
      data.routerLink && this.router.navigate([data.routerLink]);
    }
  }
  changeLanguage(event: MouseEvent, value: string) {
    this.language.setValue(value);
  }
  get userProfile() {
    return this.appConfig.getLoginResponse() as VendorLoginResponse;
  }
  get fullName$() {
    if (!this.companyUser$) return of();
    return this.companyUser$.pipe(
      map((user) => user.Fullname),
      defaultIfEmpty('N/A')
    );
  }
  get fullNameAcronym$() {
    if (!this.companyUser$) return of();
    return this.fullName$.pipe(
      map((fullName) =>
        fullName
          .split(' ')
          .filter((n) => n)
          .slice(0, 2)
          .map((n) => n[0])
          .join('')
          .toUpperCase()
      )
    );
  }
  get emailAddress$() {
    if (!this.companyUser$) return of();
    return this.companyUser$.pipe(map((user) => user?.Email));
  }
  get language() {
    return this.formGroup.get('language') as FormControl;
  }
}
