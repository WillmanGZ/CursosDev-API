import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Lesson, CreateLessonRequest, UpdateLessonRequest } from '../../shared/models';

@Injectable({
  providedIn: 'root',
})
export class LessonService {
  private readonly apiUrl = `${environment.apiUrl}/lessons`;
  private http = inject(HttpClient);

  getAll(): Observable<Lesson[]> {
    return this.http.get<Lesson[]>(this.apiUrl);
  }

  getById(id: string): Observable<Lesson> {
    return this.http.get<Lesson>(`${this.apiUrl}/${id}`);
  }

  getByCourseId(courseId: string): Observable<Lesson[]> {
    return this.http.get<Lesson[]>(`${this.apiUrl}/by-course/${courseId}`);
  }

  create(request: CreateLessonRequest): Observable<Lesson> {
    return this.http.post<Lesson>(this.apiUrl, request);
  }

  update(id: string, request: UpdateLessonRequest): Observable<Lesson> {
    return this.http.put<Lesson>(`${this.apiUrl}/${id}`, request);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
