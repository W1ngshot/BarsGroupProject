import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core'

@Component({
  selector: 'app-location-tag-list',
  templateUrl: './location-tag-list.component.html',
  styleUrls: ['./location-tag-list.component.scss'],
})
export class LocationTagListComponent implements OnInit {
  @Input() initialTags: string[] = []
  @Output() change = new EventEmitter<string[]>()

  tags: string[] = []
  focusTagOnMount = false

  ngOnInit(): void {
    setTimeout(() => (this.focusTagOnMount = true))
    this.tags = this.initialTags
  }

  onTagAdd() {
    this.tags.push('')
    this.change.emit([...this.tags])
  }

  onTagInput(index: number, value: string) {
    this.tags[index] = value
    this.change.emit([...this.tags])
  }

  onTagDelete(index: number) {
    this.tags.splice(index, 1)
    this.change.emit([...this.tags])
  }
}
