import { Pipe, PipeTransform } from '@angular/core';
import { ILanguage } from 'src/app/components/layouts/vendor/vendor.component';

@Pipe({
  name: 'findLanguage',
  standalone: true,
})
export class LanguagesPipe implements PipeTransform {
  transform(code: string, languages: ILanguage[]): ILanguage | null {
    try {
      const found = languages.find((language) => language.code === code);
      if (!found) throw new Error('Failed to find language');
      return found;
    } catch (err: any) {
      console.error(err.message);
      return null;
    }
  }
}
