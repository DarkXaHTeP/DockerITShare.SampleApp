import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

import { WordsApiService } from './words-api.service';
import { Word } from './Word';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-root',
  templateUrl: "./app.component.html"
})
export class AppComponent implements OnInit {
  public words$: Observable<Word[]>;
  public wordControl = new FormControl("");

  constructor(private wordsApi: WordsApiService) {}

  public ngOnInit() {
    this.loadWords();
  }

  public addWord(value: string) {
    this.wordsApi
      .addWord(value)
      .subscribe(
        () => {
          alert("Added");
          this.wordControl.setValue("");
          this.loadWords();
        },
        this.handleError
      )
  }

  public removeWord(id: number) {
    this.wordsApi
      .removeWord(id)
      .subscribe(
        () => {
          alert("Deleted");
          this.loadWords();
        },
        this.handleError
      )
  }

  private loadWords() {
    this.words$ = this.wordsApi.loadWords()
  }

  private handleError(error: any) {
    alert("Error: " + JSON.stringify(error));
  }
}
