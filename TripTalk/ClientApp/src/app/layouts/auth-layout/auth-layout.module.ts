import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'
import { RouterModule } from '@angular/router'

import { AuthLayoutComponent } from './auth-layout.component'
import { AuthLayoutRoutingModule } from './auth-layout-routing.module'

@NgModule({
  declarations: [AuthLayoutComponent],
  imports: [CommonModule, AuthLayoutRoutingModule, RouterModule],
})
export class AuthLayoutModule {}
