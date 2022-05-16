import { Component } from '@angular/core'
import { AuthService } from './shared/auth.service'
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
})
export class AppComponent {
  title = 'bars-frontend'

  constructor(private authService: AuthService) {
    this.authService.init()
  }

  get isInitialized() {
    return this.authService.isInitialized
  }
}
