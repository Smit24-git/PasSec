import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'passwordMask',
  standalone: true
})
export class PasswordMaskPipe implements PipeTransform {

  transform(value: string, mask: boolean = true): unknown {
    return mask ? value.replace(/./g,'*') : value;
  }

}
