import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'
import { RouterModule } from '@angular/router'

import { SharedModule } from '../shared/shared.module'
import { ProfileComponent } from './profile.component'

@NgModule({
  declarations: [ProfileComponent],
  imports: [CommonModule, SharedModule, RouterModule],
})
export class ProfileModule {}
