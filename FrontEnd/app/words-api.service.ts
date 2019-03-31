import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from 'rxjs';
import { Word } from './Word';

@Injectable({
    providedIn: "root"
})
export class WordsApiService {
  constructor(private http: HttpClient) {}

  addWord(value: string): Observable<void> {
    return this.http.post<void>("/api/words", { value });
  }

  removeWord(id: number): Observable<void> {
    return this.http.delete<void>(`/api/words/${id}`);    
  }

  loadWords(): Observable<Word[]> {
    return this.http.get<Word[]>("/api/words");
  }
}
