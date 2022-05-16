import { AfterViewInit, Component, ElementRef, EventEmitter, Input, Output, ViewChild } from '@angular/core'

@Component({
  selector: 'app-location-tag',
  templateUrl: './location-tag.component.html',
  styleUrls: ['./location-tag.component.scss'],
})
export class LocationTagComponent implements AfterViewInit {
  @Input() name?: string
  @Input() focusOnMount?: boolean
  @Input() isEditable = false

  @Output() input = new EventEmitter<string>()
  @Output() delete = new EventEmitter()

  @ViewChild('locationTagInput')
  locationTagInput!: ElementRef

  updateInputWidth() {
    const input = this.locationTagInput.nativeElement as HTMLInputElement | null
    if (!input) return

    input.style.width = '0px'
    const realWidth = input.scrollWidth
    input.style.width = realWidth + 'px'
  }

  focusInput() {
    const input = this.locationTagInput.nativeElement as HTMLInputElement | null
    if (!input) return

    input.focus()
  }

  onInput(event: Event) {
    this.updateInputWidth()
    event.stopPropagation()

    const target = event.target as HTMLInputElement
    const value = target.value

    if (value) this.input.emit(value)
  }

  onBlur(event: Event) {
    const target = event.target as HTMLInputElement
    const value = target.value

    if (!value) this.delete.emit()
  }

  ngAfterViewInit() {
    this.updateInputWidth()
    if (this.focusOnMount) {
      this.focusInput()
    }
  }
}
