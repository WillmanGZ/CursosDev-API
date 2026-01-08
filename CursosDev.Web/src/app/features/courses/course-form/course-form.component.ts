import { Component, inject, OnInit, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CourseService, LessonService } from '../../../core/services';
import { Course, Lesson } from '../../../shared/models';

@Component({
  selector: 'app-course-form',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './course-form.component.html',
})
export class CourseFormComponent implements OnInit {
  private fb = inject(FormBuilder);
  private courseService = inject(CourseService);
  private lessonService = inject(LessonService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);

  form = this.fb.nonNullable.group({
    title: ['', [Validators.required]],
  });

  course = signal<Course | null>(null);
  lessons = signal<Lesson[]>([]);
  loading = signal(false);
  isEdit = signal(false);

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if (id && id !== 'new') {
      this.isEdit.set(true);
      this.loadCourse(id);
    }
  }

  loadCourse(id: string) {
    this.courseService.getById(id).subscribe({
      next: (course) => {
        this.course.set(course);
        this.form.patchValue({ title: course.title });
        this.loadLessons(id);
      },
    });
  }

  loadLessons(courseId: string) {
    this.lessonService.getByCourseId(courseId).subscribe({
      next: (lessons) => this.lessons.set(lessons),
    });
  }

  onSubmit() {
    if (this.form.invalid) return;

    this.loading.set(true);
    const { title } = this.form.getRawValue();

    if (this.isEdit()) {
      const course = this.course();
      if (course) {
        this.courseService.update(course.id, { id: course.id, title }).subscribe({
          next: () => this.router.navigate(['/courses']),
          error: () => this.loading.set(false),
        });
      }
    } else {
      this.courseService.create({ title }).subscribe({
        next: () => this.router.navigate(['/courses']),
        error: () => this.loading.set(false),
      });
    }
  }

  deleteLesson(id: string) {
    if (confirm('¿Estás seguro de eliminar esta lección?')) {
      this.lessonService.delete(id).subscribe(() => {
        const course = this.course();
        if (course) this.loadLessons(course.id);
      });
    }
  }
}
