import { Component, Input, OnInit } from '@angular/core'
import { FormControl } from '@angular/forms'
import { Router } from '@angular/router'

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss'],
})
export class SearchComponent implements OnInit {
  @Input() initialValue?: string
  @Input() initialTags: string[] = []

  inputControl = new FormControl(this.initialValue)
  tags: string[] = []

  constructor(private router: Router) {}

  ngOnInit() {
    this.inputControl.setValue(this.initialValue)
  }

  onFormSubmit(event: SubmitEvent) {
    event.preventDefault()
    const query = this.inputControl.value ?? ''
    const tags = this.tags.join(',')
    this.router.navigateByUrl(`/search?q=${query}&tags=${tags}`)
  }

  onTagsChange(tags: string[]) {
    this.tags = tags
  }
}
