import { Component, inject, OnInit, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CourseService } from '../../../core/services';
import { Course } from '../../../shared/models';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-course-list',
  standalone: true,
  imports: [RouterLink, CommonModule],
  templateUrl: './course-list.component.html',
})
export class CourseListComponent implements OnInit {
  private courseService = inject(CourseService);

  courses = signal<Course[]>([]);
  loading = signal(true);

  ngOnInit() {
    this.loadCourses();
  }

  loadCourses() {
    this.loading.set(true);
    this.courseService.getAll().subscribe({
      next: (courses) => {
        this.courses.set(courses);
        this.loading.set(false);
      },
      error: () => this.loading.set(false),
    });
  }

  getStatusClass(status: string): string {
    return status === 'Published'
      ? 'bg-green-500/20 text-green-400'
      : 'bg-yellow-500/20 text-yellow-400';
  }

  publish(id: string) {
    this.courseService.publish(id).subscribe(() => this.loadCourses());
  }

  unpublish(id: string) {
    this.courseService.unpublish(id).subscribe(() => this.loadCourses());
  }

  delete(id: string) {
    if (confirm('¿Estás seguro de eliminar este curso?')) {
      this.courseService.delete(id).subscribe(() => this.loadCourses());
    }
  }
}
