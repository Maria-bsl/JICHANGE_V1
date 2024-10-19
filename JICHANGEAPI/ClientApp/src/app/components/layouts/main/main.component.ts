import {
  AfterViewInit,
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  ElementRef,
  NO_ERRORS_SCHEMA,
  OnInit,
  ViewChild,
} from '@angular/core';
import { HeaderComponent } from '../header/header.component';
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
import { CommonModule } from '@angular/common';
import { BreadcrumbService, BreadcrumbModule } from 'xng-breadcrumb';
import { mainModuleAnimations } from './router-transition-animations';
import { TranslocoService } from '@ngneat/transloco';
import { NgxLoadingModule } from 'ngx-loading';
import { NgxSonnerToaster } from 'ngx-sonner';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
  standalone: true,
  changeDetection: ChangeDetectionStrategy.OnPush,
  animations: [mainModuleAnimations],
  schemas: [NO_ERRORS_SCHEMA],
  imports: [
    HeaderComponent,
    RouterModule,
    FooterComponent,
    CommonModule,
    BreadcrumbModule,
    NgxLoadingModule,
    NgxSonnerToaster,
    MatIconModule,
  ],
})
export class MainComponent implements OnInit, AfterViewInit {
  public themes: { label: string; color: string }[] = [
    { label: 'light', color: '#0B6587' },
    { label: 'coffee', color: '#20161F' },
  ]; //["light","coffee"]
  public selectedThemeIndex: number = 0;
  public routeLoading: boolean = false;
  @ViewChild('mainHeader') mainHeader!: HeaderComponent;
  @ViewChild('containerDoc', { static: true })
  containerDoc!: ElementRef<HTMLDivElement>;
  constructor(
    private breadcrumbService: BreadcrumbService,
    private router: Router,
    private tr: TranslocoService,
    private cdr: ChangeDetectorRef
  ) {}
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
          //this.mainHeader.routeLoading = false;
          break;
        }
      }
    });
  }
  private prepareReportsRoutes(route: any) {
    this.breadcrumbService.set(
      '@overview',
      this.tr.translate(`${route}.overview`)
    );
    this.breadcrumbService.set(
      '@invoice',
      this.tr.translate(`${route}.invoice`)
    );
    this.breadcrumbService.set(
      '@userLog',
      this.tr.translate(`${route}.userLog`)
    );
    this.breadcrumbService.set(
      '@customer',
      this.tr.translate(`${route}.vendor`)
    );
    this.breadcrumbService.set('@audit', this.tr.translate(`${route}.audit`));
    this.breadcrumbService.set(
      '@transactions-id',
      this.tr.translate(`${route}.details`)
    );
    this.breadcrumbService.set(
      '@payment',
      this.tr.translate(`${route}.payments`)
    );
    this.breadcrumbService.set(
      '@amendment',
      this.tr.translate(`${route}.amendment`)
    );
    this.breadcrumbService.set(
      '@invoice-consolidated',
      this.tr.translate(`${route}.invoiceConsolidated`)
    );
    this.breadcrumbService.set(
      '@payment-consolidated',
      this.tr.translate(`${route}.paymentConsolidated`)
    );
    this.breadcrumbService.set(
      '@vendors',
      this.tr.translate(`${route}.vendorReport`)
    );
    this.breadcrumbService.set(
      '@cancelled',
      this.tr.translate(`${route}.cancelled`)
    );
    this.breadcrumbService.set('@transactions', {
      label: this.tr.translate(`${route}.transaction`),
      routeInterceptor(routeLink: any, breadcrumb: any) {
        return routeLink.startsWith('/main') ? routeLink : `/main${routeLink}`;
      },
    });
  }
  private prepareSetupRoutes(routes: any) {
    this.breadcrumbService.set(
      '@country',
      this.tr.translate(`${routes}.country`)
    );
    this.breadcrumbService.set(
      '@region',
      this.tr.translate(`${routes}.region`)
    );
    this.breadcrumbService.set(
      '@district',
      this.tr.translate(`${routes}.district`)
    );
    this.breadcrumbService.set('@ward', this.tr.translate(`${routes}ward`));
    this.breadcrumbService.set(
      '@currency',
      this.tr.translate(`${routes}.currencies`)
    );
    this.breadcrumbService.set(
      '@designation',
      this.tr.translate(`${routes}.designation`)
    );
    this.breadcrumbService.set(
      '@branch',
      this.tr.translate(`${routes}.branch`)
    );
    this.breadcrumbService.set(
      '@question',
      this.tr.translate(`${routes}.question`)
    );
    this.breadcrumbService.set('@smtp', this.tr.translate(`${routes}.smtp`));
    this.breadcrumbService.set('@email', this.tr.translate(`${routes}.email`));
    this.breadcrumbService.set(
      '@user',
      this.tr.translate(`${routes}.bankUser`)
    );
    this.breadcrumbService.set(
      '@language',
      this.tr.translate(`${routes}.language`)
    );
    this.breadcrumbService.set(
      '@suspense',
      this.tr.translate(`${routes}.suspense`)
    );
    this.breadcrumbService.set(
      '@deposit',
      this.tr.translate(`${routes}.deposit`)
    );
    this.breadcrumbService.set(
      '@sms-settings',
      this.tr.translate(`${routes}.smsSettings`)
    );
    this.breadcrumbService.set(
      '@sms-text',
      this.tr.translate(`${routes}.smsText`)
    );
  }
  private prepareCompanyRoutes(routes: any) {
    this.breadcrumbService.set(
      '@summary',
      this.tr.translate(`${routes}.summary`)
    );
    this.breadcrumbService.set('@inbox', this.tr.translate(`${routes}.inbox`));
  }
  private prepareBankBreadcrumbs() {
    this.tr.selectTranslate('bankRoutes').subscribe({
      next: (res) => {
        this.breadcrumbService.set('@home', this.tr.translate(`${res}.home`));
        this.breadcrumbService.set(
          '@profile',
          this.tr.translate(`${res}.profile`)
        );
        this.prepareReportsRoutes(res.reports);
        this.prepareSetupRoutes(res.setup);
        this.prepareCompanyRoutes(res.company);
      },
    });
  }
  private hideNavBarOnScroll() {
    let containerDoc = this.containerDoc.nativeElement;
    let header = this.mainHeader.header.nativeElement;
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
    this.prepareBankBreadcrumbs();
    this.routeLoaderListener();
  }
  ngAfterViewInit(): void {
    this.hideNavBarOnScroll();
  }
  prepareRoute(outlet: RouterOutlet, animate: string): boolean {
    return (
      outlet && outlet.activatedRouteData && outlet.activatedRouteData[animate]
    );
  }
}
