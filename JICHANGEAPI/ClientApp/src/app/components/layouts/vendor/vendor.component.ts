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
import { vendorAnimations } from '../main/router-transition-animations';
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
import { zip } from 'rxjs';
import { MatIconModule } from '@angular/material/icon';
import { FlatTreeControl, NestedTreeControl } from '@angular/cdk/tree';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { CdkAccordionModule } from '@angular/cdk/accordion';
import {
  trigger,
  state,
  style,
  transition,
  animate,
} from '@angular/animations';

interface FoodNode {
  name: string;
  children?: FoodNode[];
  icon?: string;
  routerLink?: string;
}

const TREE_DATA: FoodNode[] = [
  {
    name: 'Dashboard',
    children: [],
    icon: 'dashboard',
    routerLink: '/vendor',
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
    children: [
      { name: 'Waitlist', routerLink: '/vendor/invoice/list' },
      { name: 'Generated', routerLink: '/vendor/invoice/generated' },
    ],
    icon: 'summarize',
  },
  {
    name: 'Reports',
    icon: 'receipt_long',
    children: [
      {
        name: 'Payments',
        children: [],
        routerLink: '/vendor/reports/transactions',
      },
      {
        name: 'Completed Payments',
        children: [],
        routerLink: '/vendor/reports/invoice',
      },
      {
        name: 'Invoice',
        children: [],
        routerLink: '/vendor/reports/payments',
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
    ],
    animations: [
        vendorAnimations,
        trigger('contentExpansion', [
            state('expanded', style({ height: '*', opacity: 1, visibility: 'visible' })),
            state('collapsed', style({ height: '0px', opacity: 0, visibility: 'hidden' })),
            transition('expanded <=> collapsed', animate('300ms cubic-bezier(.37,1.04,.68,.98)')),
        ]),
    ],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class VendorComponent implements OnInit, AfterViewInit {
  public routeLoading: boolean = false;
  @ViewChild('vendorHeader') vendorHeader!: VendorHeaderComponent;
  @ViewChild('containerDoc', { static: true })
  containerDoc!: ElementRef<HTMLDivElement>;
  expanded: WritableSignal<boolean> = signal<boolean>(true);
  treeControl = new NestedTreeControl<FoodNode>((node) => node.children);
  dataSource = new MatTreeNestedDataSource<FoodNode>();
  hasChild = (_: number, node: FoodNode) =>
    !!node.children && node.children.length > 0;
  constructor(
    private breadcrumbService: BreadcrumbService,
    private router: Router,
    private tr: TranslocoService,
    private cdr: ChangeDetectorRef,
    @Inject(TRANSLOCO_SCOPE) private scope: any
  ) {
    this.dataSource.data = TREE_DATA;
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
  private hideNavBarOnScroll() {
    let containerDoc = this.containerDoc.nativeElement;
    let header = this.vendorHeader.header.nativeElement;
    let prevScrollpos = header.offsetHeight;
    let navbar = document.getElementById('navbar');
    this.containerDoc.nativeElement.onscroll = function () {
      let currentScrollPos = containerDoc.scrollTop;
      if (navbar && prevScrollpos > currentScrollPos) {
        navbar.style.top = '0px';
      } else if (navbar && prevScrollpos < currentScrollPos) {
        navbar.style.top = `-${header.clientHeight}px`;
      }
      prevScrollpos = currentScrollPos;
    };
  }
  ngOnInit(): void {
    this.routeLoaderListener();
    this.tr.selectTranslate('vendorRoutes').subscribe({
      next: (res) => {
        this.prepareVendorRoutes(res);
      },
    });
    //this.prepareVendorRoutes();
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
}
