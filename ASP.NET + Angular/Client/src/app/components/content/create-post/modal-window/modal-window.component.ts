import { Component, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { EventEmitter } from '@angular/core';
import { CreatePostComponent } from '../create-post.component';

@Component({
  selector: 'app-modal-window',
  templateUrl: './modal-window.component.html',
  styleUrls: ['./modal-window.component.css']
})
export class ModalWindowComponent implements OnInit {
  postForm: FormGroup;
  @Output() close = new EventEmitter<void>();

  constructor(private fb: FormBuilder,
    private createPostComponent: CreatePostComponent) { 
    this.postForm = this.fb.group({
      imageUrl: ['', [Validators.required]],
      description: ['']
    })
  }

  ngOnInit(): void {
  }

  create() {
    this.createPostComponent.postForm = this.postForm;
    this.close.emit()
  }

  get imageUrl() {
    return this.postForm.get('imageUrl');
  }

  get description() {
    return this.postForm.get('description');
  }

}
