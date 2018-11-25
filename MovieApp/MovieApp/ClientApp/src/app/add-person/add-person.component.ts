import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MovieActor, MovieProducer, Person } from '../MovieModel';
import { PersonService } from '../person.service';
import { Location } from '@angular/common';
import { Gender } from '../gender.enum'

@Component({
  selector: 'app-add-person',
  templateUrl: './add-person.component.html',
  styleUrls: ['./add-person.component.css']
})
export class AddPersonComponent implements OnInit {
  @Input() isMovieActor: boolean = false;
  @Output() sectionCollapsed: EventEmitter<boolean> = new EventEmitter();

  showPersonCreateSec: boolean = false;
  selectedGender: string;
  producer: MovieProducer = new MovieProducer();
  actor: MovieActor = new MovieActor();

  person: Person = new Person();
  keys: any[];
  genders = Gender;

  constructor(private personService: PersonService) {
    this.keys = Object.keys(this.genders);
  }

  ngOnInit() {
  }

  createPerson(): void {
    if (this.isMovieActor) {
      this.actor.name = this.person.name;
      this.actor.dob = this.person.dob;
      this.actor.gender = this.person.gender;
      this.actor.bio = this.person.bio;

      this.personService.createActor(this.actor)
        .subscribe(() => this.clearFields());
    } else {
      this.producer.name = this.person.name;
      this.producer.dob = this.person.dob;
      this.producer.gender = this.person.gender;
      this.producer.bio = this.person.bio;

      this.personService.createProducer(this.producer)
        .subscribe(() => this.clearFields());
    }
  }

  clearFields(): void {
    this.isMovieActor = false;
    this.showPersonCreateSec = false;
    this.actor = new MovieActor();
    this.producer = new MovieProducer();
    this.person = new Person();

    this.sectionCollapsed.emit(this.showPersonCreateSec);
  }

  cancel(): void {
    this.clearFields();
  }
}
