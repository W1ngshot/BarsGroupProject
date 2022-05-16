import { HttpClient, HttpErrorResponse } from '@angular/common/http'
import { Injectable } from '@angular/core'
import { BehaviorSubject, firstValueFrom } from 'rxjs'

import { baseApiUrl } from '../app.config'
import { Account } from './interfaces/account.interface'

type JWToken = string

interface RegisterPayload {
  nickname: string
  email: string
  password: string
  confirmPassword: string
}

interface LoginPayload {
  nickname: string
  password: string
}

const localStorageJWTKey = 'trip-talk-jwt'

@Injectable({ providedIn: 'root' })
export class AuthService {
  private _isBusy = false
  private _isInitialized = false
  private _myAccount: BehaviorSubject<Account | null>

  constructor(private http: HttpClient) {
    this.init()
    this._myAccount = new BehaviorSubject<Account | null>(null)
  }

  get isBusy() {
    return this._isBusy
  }

  get isInitialized() {
    return this._isInitialized
  }

  get myAccount() {
    return this._myAccount
  }

  get isLoggedIn() {
    return !!this._myAccount.value
  }

  private get jwt() {
    return localStorage.getItem(localStorageJWTKey)
  }

  private set jwt(value: string | null) {
    if (!value) localStorage.removeItem(localStorageJWTKey)
    else localStorage.setItem(localStorageJWTKey, value)
  }

  public get headers(): Record<string, string> {
    if (!this.jwt) return {}
    return {
      Authorization: `Bearer ${this.jwt}`,
    }
  }

  init() {
    firstValueFrom(this.http.get<Account>(`${baseApiUrl}/Account/MyAccount`, { headers: this.headers }))
      .then(account => {
        this._myAccount.next(account)
        this._isInitialized = true
      })
      .catch((error: HttpErrorResponse) => {
        if (error.status === 401) this.jwt = null
        this._isInitialized = true
      })
  }

  register(payload: RegisterPayload) {
    this._isBusy = true
    const response = firstValueFrom(this.http.post(`${baseApiUrl}/Auth/Register`, payload, { responseType: 'text' }))
      .then(token => {
        this._isBusy = false
        this.jwt = token
        this.init()

        return token
      })
      .catch((error: HttpErrorResponse) => {
        this._isBusy = false
        return Promise.reject(error)
      })

    return response
  }

  login(payload: LoginPayload) {
    this._isBusy = true
    const response = firstValueFrom(this.http.post(`${baseApiUrl}/Auth/Login`, payload, { responseType: 'text' }))
      .then(token => {
        this._isBusy = false
        this.jwt = token
        this.init()

        return token
      })
      .catch((error: HttpErrorResponse) => {
        this._isBusy = false
        return Promise.reject(error)
      })

    return response
  }

  logout() {
    this.jwt = null
    this._myAccount.next(null)
  }
}
