import { Component, Input } from '@angular/core'

@Component({
  selector: 'app-user-avatar',
  templateUrl: './user-avatar.component.html',
  styleUrls: ['./user-avatar.component.scss'],
})
export class UserAvatarComponent {
  @Input() imageUrl!: string
  @Input() nickname?: string
  @Input() size = 'medium'

  get thumbnailLetter() {
    return this.nickname?.[0]
  }
}
