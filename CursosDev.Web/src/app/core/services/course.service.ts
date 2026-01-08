import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Course, CreateCourseRequest, UpdateCourseRequest } from '../../shared/models';

@Injectable({
  providedIn: 'root',
})
export class CourseService {
  private readonly apiUrl = `${environment.apiUrl}/courses`;
  private http = inject(HttpClient);

  getAll(): Observable<Course[]> {
    return this.http.get<Course[]>(this.apiUrl);
  }

  getById(id: string): Observable<Course> {
    return this.http.get<Course>(`${this.apiUrl}/${id}`);
  }

  create(request: CreateCourseRequest): Observable<Course> {
    return this.http.post<Course>(this.apiUrl, request);
  }

  update(id: string, request: UpdateCourseRequest): Observable<Course> {
    return this.http.put<Course>(`${this.apiUrl}/${id}`, request);
  }

  publish(id: string): Observable<void> {
    return this.http.patch<void>(`${this.apiUrl}/${id}/publish`, {});
  }

  unpublish(id: string): Observable<void> {
    return this.http.patch<void>(`${this.apiUrl}/${id}/unpublish`, {});
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
