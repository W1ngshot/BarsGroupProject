import { HttpErrorResponse } from '@angular/common/http'
import { ChangeDetectorRef, Component } from '@angular/core'
import { FormControl, FormGroup, Validators } from '@angular/forms'
import { Router } from '@angular/router'
import { Observable } from 'rxjs'

import { AuthService } from '../shared/auth.service'

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss'],
})
export class AuthComponent {
  form: FormGroup

  constructor(private router: Router, private authService: AuthService, private changeDetector: ChangeDetectorRef) {
    if (router.url === '/login') this.isLoginPage = true
    if (router.url === '/register') this.isRegisterPage = true

    const navigateToHome = () => router.navigateByUrl('/')

    authService.myAccount.subscribe(value => {
      if (value) navigateToHome()
    })
    if (authService.myAccount.value) navigateToHome()

    this.form = this._createForm()
  }

  isLoginPage = false
  isRegisterPage = false

  private _createForm(): FormGroup {
    const form = new FormGroup({
      nickname: new FormControl(null, [Validators.required, Validators.minLength(3)]),
      password: new FormControl(null, [Validators.required]),
    })

    if (this.isRegisterPage) {
      const validatePasswordRepeat = () => {
        const isInvalid = form.controls['password']?.value !== form.controls['confirmPassword']?.value
        if (isInvalid) return { passwordsNotMatching: true }
        return null
      }

      form.addControl('confirmPassword', new FormControl(null, [Validators.required, validatePasswordRepeat]))
      form.addControl('email', new FormControl(null, [Validators.required, Validators.email]))
    }

    return form
  }

  get pageTitle(): string {
    if (this.isLoginPage) return 'Вход'
    if (this.isRegisterPage) return 'Регистрация'
    return 'Ачё'
  }

  get submitButtonText(): string {
    if (this.isLoginPage) return 'Войти'
    if (this.isRegisterPage) return 'Зарегестрироваться'
    return 'Ачё'
  }

  get isFormNicknameErrorShown() {
    const control = this.form.controls['nickname']
    return control.invalid && control.touched
  }

  get isFormPasswordErrorShown() {
    const control = this.form.controls['password']
    return control.invalid && control.touched
  }

  get isFormEmailErrorShown() {
    if (!this.form.contains('email')) return false
    const control = this.form.controls['email']
    return control.invalid && control.touched
  }

  get isFormConfirmPasswordErrorShown() {
    if (!this.form.contains('confirmPassword')) return false
    const control = this.form.controls['confirmPassword']
    return control.invalid && control.touched
  }

  get alternativeLinkName() {
    if (this.isLoginPage) return 'Нет аккаунта?'
    if (this.isRegisterPage) return 'Уже есть аккаунт?'
    return 'Ачё'
  }

  get alternativeLinkPath() {
    if (this.isLoginPage) return '/register'
    if (this.isRegisterPage) return '/login'
    return '/'
  }

  handleRequestError(error: HttpErrorResponse) {
    if (!error.error) return
    if (typeof error.error === 'string') {
      try {
        const parsedError = JSON.parse(error.error)
        if (parsedError.message) return (this.errorMessage = parsedError.message)
      } catch {}
    }
    if (error.status === 0) return (this.errorMessage = 'Сайт недоступен')
    this.errorMessage = 'Неизвестная ошибка'
  }

  errorMessage = ''
  onFormSubmit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched()
      return
    }

    let request: Promise<string> | null = null

    if (this.isLoginPage) request = this.authService.login(this.form.value)
    if (this.isRegisterPage) request = this.authService.register(this.form.value)
    if (!request) return

    request
      .then(() => {
        this.router.navigateByUrl('/')
      })
      .catch((error: HttpErrorResponse) => {
        this.handleRequestError(error)
      })
  }
}
