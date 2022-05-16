import { AuthService } from '@/app/shared/auth.service';
import { Component } from '@angular/core'

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent {
  constructor(private authService: AuthService) {}

  get isLoggedIn(): boolean {
    return !!this.authService.myAccount.value
  }

  get userNickname() {
    return this.authService.myAccount.value?.user.nickname
  }
}
