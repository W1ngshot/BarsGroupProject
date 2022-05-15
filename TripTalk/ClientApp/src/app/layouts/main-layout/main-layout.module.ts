import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'

import { HeaderComponent } from './header/header.component'
import { MainLayoutComponent } from './main-layout.component'
import { MainLayoutRoutingModule } from './main-layout-routing.module'
import { SharedModule } from '@/app/shared/shared.module'
import { RouterModule } from '@angular/router'

@NgModule({
  declarations: [MainLayoutComponent, HeaderComponent],
  imports: [CommonModule, MainLayoutRoutingModule, SharedModule, RouterModule],
})
export class MainLayoutModule {}
