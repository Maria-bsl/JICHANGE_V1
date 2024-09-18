import { inject, Injectable } from '@angular/core';
import { Translation, TranslocoLoader } from '@ngneat/transloco';
import { HttpClient } from '@angular/common/http';
import { of } from 'rxjs';
import * as en from '../assets/i18n/en.json';
import * as ekk from '../../../ClientApp/src/assets/i18n/en.json'

@Injectable({ providedIn: 'root' })
export class TranslocoHttpLoader implements TranslocoLoader {
  private http = inject(HttpClient);

  getTranslation(lang: string) {
    //return fetch(`/ClientApp/src/assets/i18n/${lang}.json`).then<Translation>((res) => res.json());
    //return this.http.get<Translation>(`/assets/i18n/${lang}.json`);
    /*return this.http.get<Translation>(
      `/ClientApp/src/assets/i18n/${lang}.json`
    );*/
    /*return this.http.get<Translation>(
      `../../../ClientApp/src/assets/i18n/${lang}.json`
    );*/
    return this.http.get<Translation>(`/assets/i18n/${lang}.json`);
  }
}
